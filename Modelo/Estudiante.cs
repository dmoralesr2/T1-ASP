using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace web_umg_bd {
  public class Estudiante {
    ConexionBD conectar;

    private DataTable drop_sangre() {
      DataTable tabla = new DataTable();
      conectar = new ConexionBD();
      conectar.AbrirConexion();

      string strConsulta = string.Format("SELECT id_tipo_sangre AS id, sangre FROM tipos_sangre;");
      MySqlDataAdapter consulta = new MySqlDataAdapter(strConsulta, conectar.conectar);
      consulta.Fill(tabla);
      conectar.CerarConexion();
      return tabla;
    }

    public void drop_sangre(DropDownList drop) {
      drop.ClearSelection();
      drop.Items.Clear();
      drop.AppendDataBoundItems = true;
      drop.Items.Add("<< Elige un Tipo de Sangre >>");
      drop.Items[0].Value = "0";
      drop.DataSource = drop_sangre();
      drop.DataTextField = "sangre";
      drop.DataValueField = "id";
      drop.DataBind();
    }

    private DataTable grid_estudiantes() {
      DataTable tabla = new DataTable();
      conectar = new ConexionBD();
      conectar.AbrirConexion();

      String consulta = string.Format("SELECT e.*, ts.sangre FROM estudiantes e INNER JOIN tipos_sangre ts ON ts.id_tipo_sangre=e.id_tipo_sangre;");
      MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
      query.Fill(tabla);
      conectar.CerarConexion();
      return tabla;
    }

    public void grid_estudiantes(GridView grid) {
      grid.DataSource = grid_estudiantes();
      grid.DataBind();
    }

    public int agregar(string carne, string nombres, string apellidos, string direccion, string telefono, string correo_electronico, int id_tipo_sangre, string fecha_nacimiento) {
      int no_ingreso = 0;
      conectar = new ConexionBD();
      conectar.AbrirConexion();

      string strConsulta = string.Format("INSERT INTO estudiantes(carne,nombres,apellidos,direccion,telefono,correo_electronico,id_tipo_sangre,fecha_nacimiento) VALUES('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}');", carne, nombres, apellidos, direccion, telefono, correo_electronico, id_tipo_sangre, fecha_nacimiento);
      MySqlCommand insertar = new MySqlCommand(strConsulta, conectar.conectar);
      insertar.Connection = conectar.conectar;
      no_ingreso = insertar.ExecuteNonQuery();
      conectar.CerarConexion();
      return no_ingreso;
    }

    public int modificar(int id, string carne, string nombres, string apellidos, string direccion, string telefono, string correo_electronico, int id_tipo_sangre, string fecha_nacimiento) {
      int no_ingreso = 0;
      conectar = new ConexionBD();
      conectar.AbrirConexion();

      string strConsulta = string.Format("UPDATE estudiantes SET carne='{0}', nombres='{1}', apellidos='{2}', direccion='{3}', telefono='{4}', correo_electronico='{5}', id_tipo_sangre={6}, fecha_nacimiento='{7}' WHERE id_estudiante={8};", carne, nombres, apellidos, direccion, telefono, correo_electronico, id_tipo_sangre, fecha_nacimiento, id);
      MySqlCommand modificar = new MySqlCommand(strConsulta, conectar.conectar);
      modificar.Connection = conectar.conectar;
      no_ingreso = modificar.ExecuteNonQuery();
      conectar.CerarConexion();
      return no_ingreso;
    }

    public int eliminar(int id) {
      int no_ingreso = 0;
      conectar = new ConexionBD();
      conectar.AbrirConexion();

      string strConsulta = string.Format("DELETE FROM estudiantes WHERE id_estudiante={0};", id);
      MySqlCommand eliminar = new MySqlCommand(strConsulta, conectar.conectar);
      eliminar.Connection = conectar.conectar;
      no_ingreso = eliminar.ExecuteNonQuery();
      conectar.CerarConexion();
      return no_ingreso;
    }
  }
}
