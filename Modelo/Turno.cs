using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using AccesoDatos;

namespace Modelo
{
    public class Turno
    {
        private int idTurno;
        private DateTime fecha;
        private string hora;
        private int idPaciente;
        private int idHistorial;
        private Estados estado;

        public enum Estados { ausente = 1, concretado = 2, espera = 3 };

        public int IdTurno { get => idTurno; set => idTurno = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Hora { get => hora; set => hora = value; }
        public int IdPaciente { get => idPaciente; set => idPaciente = value; }
        public int IdHistorial { get => idHistorial; set => idHistorial = value; }
        public Estados Estado { get => estado; set => estado = value; }

        public Turno()
        {
            IdTurno = 0;
            Fecha = DateTime.Today;
            Hora = "";
            IdPaciente = 0;
            IdHistorial = 0;
        }

        public Turno(DateTime fecha, string hora, int idPaciente, int idHistorial)
        {
            Fecha = fecha;
            Hora = hora;
            IdPaciente = idPaciente;
            IdHistorial = idHistorial;
        }


        public bool RegistrarTurno(Turno oDato)
        {
            string stSql = "INSERT INTO turno (fecha, hora,idPaciente,estado,idHistorial) VALUES(@fecha,@hora,@idPaciente,@estado,@idHistorial)";
            BaseDatos oBD = new BaseDatos();
            MySqlCommand Insertar = new MySqlCommand(stSql, oBD.MyConex);
            commandSql(Insertar, oDato);
            return oBD.ExecuteCommando(Insertar);
        }

        public void ActualizarEstadoCita(int idTurno)
        {
            string stSQL = "UPDATE turno SET estado = 'concretado' WHERE idTurno = @idTurno";
            BaseDatos oBD = new BaseDatos();
            MySqlCommand update = new MySqlCommand(stSQL, oBD.MyConex);
            update.Parameters.AddWithValue("idTurno", idTurno);
            oBD.ExecuteCommando(update);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fecha"></param>
        public void ActualizarEstadoCita(DateTime fecha)
        {
            DateTime tresDias = fecha.AddDays(-3);
            string stSQL = "UPDATE turno SET estado = 'ausente' WHERE fecha <= @fecha AND fecha >= @tresDias AND estado = 'en espera'";
            BaseDatos oBD = new BaseDatos();
            MySqlCommand update = new MySqlCommand(stSQL, oBD.MyConex);
            update.Parameters.AddWithValue("fecha", fecha.ToString("yyyy-MM-dd"));
            update.Parameters.AddWithValue("tresDias", tresDias.ToString("yyyy-MM-dd"));
            oBD.ExecuteCommando(update);
        }


        public DataTable ObtenerTurnoDelDia(DateTime fecha)
        {
            string strMySql = $"SELECT (t.hora) AS 'Hora', concat_ws(' - ',concat_ws(', ',p.apellido, p.nombre),h.causa) AS 'Paciente', t.idPaciente, t.idTurno, t.idHistorial, t.estado " +
                $"FROM turno t, paciente p, historial h " +
                $"WHERE t.fecha = '{fecha:yyyy-MM-dd}' AND p.idPaciente = t.idPaciente AND h.idHistorial = t.idHistorial";
            BaseDatos oBase = new BaseDatos();
            try
            {
                DataTable oDataTable = oBase.ExecuteDataTable(strMySql);
                return oDataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private void commandSql(MySqlCommand cmdSQL, Turno oDatos)
        {
            cmdSQL.Parameters.AddWithValue("fecha", oDatos.Fecha);
            cmdSQL.Parameters.AddWithValue("hora", oDatos.Hora);
            cmdSQL.Parameters.AddWithValue("idPaciente", oDatos.IdPaciente);
            cmdSQL.Parameters.AddWithValue("estado", oDatos.Estado);
            cmdSQL.Parameters.AddWithValue("idHistorial", oDatos.IdHistorial);

        }
    }
}
