using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using MySql.Data.MySqlClient;

namespace Modelo
{
    public class Historial
    {
        private int idHistorial;
        private string causa;
        private string solucion;
        private int idPaciente;

        public int IdHistorial { get => idHistorial; set => idHistorial = value; }
        public string Causa { get => causa; set => causa = value; }
        public string Solucion { get => solucion; set => solucion = value; }
        public int IdPaciente { get => idPaciente; set => idPaciente = value; }

        public Historial()
        {
            IdHistorial = 0;
            Causa = "";
            Solucion = "";
            IdPaciente = 0;
        }

        public int RegistrarNuevoHistorial(Historial oDato)
        {
            string stsql = "INSERT INTO historial (causa, idPaciente) VALUES(@causa, @idPaciente)";
            BaseDatos oBD = new BaseDatos();
            MySqlCommand Insertar = new MySqlCommand(stsql, oBD.MyConex);
            commandSql(Insertar, oDato);
            int id = oBD.ExecuteCommandoInt(Insertar);
            return id;   
        }
        public void ActualizarHistorial(Historial oDato)
        {
            string stsql = "UPDATE historial SET solucion = @solucion WHERE idHistorial = @idHistorial";
            BaseDatos oBD = new BaseDatos();
            MySqlCommand Update = new MySqlCommand(stsql, oBD.MyConex);
            Update.Parameters.AddWithValue("solucion", oDato.Solucion);
            Update.Parameters.AddWithValue("idHistorial", oDato.IdHistorial);
            oBD.ExecuteCommando(Update);
        }

        public DataTable GetHistorial(int idPaciente)
        {
            string stsql = $"SELECT turno.fecha, historial.causa,historial.solucion,turno.estado FROM turno, historial WHERE historial.idhistorial = turno.idhistorial AND historial.idPaciente = {idPaciente} ORDER BY turno.fecha ASC";
            BaseDatos oBD = new BaseDatos();
            try
            {
                DataTable oDataTable = oBD.ExecuteDataTable(stsql);
                return oDataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Historial GetHistorialTurno(int idHistorial)
        {
            string stSql = $"SELECT causa, solucion FROM historial WHERE idHistorial = {idHistorial}";
            BaseDatos oBD = new BaseDatos();
            try
            {
                DataTable oDataTable = oBD.ExecuteDataTable(stSql);
                foreach(DataRow fila in oDataTable.Rows)
                {
                    Historial oHistorial = new Historial();
                    oHistorial.Causa = fila["causa"].ToString();
                    if(fila["solucion"].ToString() != "")
                    {
                        oHistorial.Solucion = fila["solucion"].ToString();
                    }
                    return oHistorial;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void commandSql(MySqlCommand cmdSQL, Historial oDatos)
        {
            cmdSQL.Parameters.AddWithValue("causa", oDatos.Causa);
            cmdSQL.Parameters.AddWithValue("idPaciente", oDatos.IdPaciente);
        }
    }
}
