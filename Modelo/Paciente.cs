using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using AccesoDatos;
using System.Data;
namespace Modelo
{
    public class Paciente
    {
        //Atributos
        private int idPaciente;
        private string nombre;
        private string apellido;
        private string dni;
        private string domicilio;
        private string telefono;
        private DateTime fechaNacimiento;
        
        //Encapsulamiento
        public int IdPaciente { get => idPaciente; set => idPaciente = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Domicilio { get => domicilio; set => domicilio = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }

        //Constructores
        /// <summary>
        /// Constructor 
        /// </summary>
        public Paciente()
        {
            IdPaciente = 0;
            Nombre = "";
            Apellido = "";
            Dni = "";
            Domicilio = "";
            Telefono = "";
            FechaNacimiento = new DateTime();
        }
        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="domicilio"></param>
        /// <param name="telefono"></param>
        /// <param name="fechaNacimiento"></param>
        public Paciente(string nombre, string apellido, string dni, string domicilio,string telefono, DateTime fechaNacimiento)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Dni = dni;
            this.Domicilio = domicilio;
            this.Telefono = telefono;
            this.FechaNacimiento = fechaNacimiento;
        }

        public bool InsertPaciente(Paciente oPaciente)
        {
            string stSql = "INSERT INTO paciente (nombre, apellido, dni, domicilio, telefono, fechaNacimiento)" +
                "VALUES(@nombre, @apellido, @dni, @domicilio, @telefono, @fechaNacimiento)";
            BaseDatos oBD = new BaseDatos();
            MySqlCommand Insertar = new MySqlCommand(stSql, oBD.MyConex);
            commandSql(Insertar, oPaciente);
            return oBD.ExecuteCommando(Insertar);
        }

        public bool ComprobarDni(string dni)
        {
            string stSql = $"SELECT * FROM paciente WHERE dni = '{dni}'";
            BaseDatos oBD = new BaseDatos();
            MySqlCommand Query = new MySqlCommand(stSql,oBD.MyConex);
            return oBD.ExecuteCommando(Query);

        }

        public Paciente GetOne(string dni)
        {
            string strCmd = $"SELECT idPaciente, nombre, apellido, dni, domicilio, telefono, fechaNacimiento FROM paciente WHERE dni = '{dni}'";
            BaseDatos oBD = new BaseDatos();
            DataTable oDaTa = oBD.ExecuteDataTable(strCmd);
            foreach(DataRow fila in oDaTa.Rows)
            {
                Paciente oPaciente = new Paciente();

                oPaciente.IdPaciente = Convert.ToInt32(fila["idPaciente"]);
                oPaciente.Nombre = fila["nombre"].ToString();
                oPaciente.apellido = fila["apellido"].ToString();
                oPaciente.Dni = fila["dni"].ToString();
                oPaciente.Domicilio = fila["domicilio"].ToString();
                oPaciente.Telefono = fila["telefono"].ToString();
                oPaciente.FechaNacimiento = Convert.ToDateTime(fila["fechaNacimiento"]);
                return oPaciente;
            }
            return null;

        }
        public int CalcularEdad()
        {
            int edad = -1;
            if (FechaNacimiento != null)
            {
                edad = DateTime.Today.Year - FechaNacimiento.Year;
                if(FechaNacimiento.Month > DateTime.Today.Month)
                {
                    --edad;
                }else if(FechaNacimiento.Month == DateTime.Today.Month)
                {
                    if(FechaNacimiento.Day > DateTime.Today.Day)
                    {
                        --edad;
                    }
                }
            }
            return edad;
        }

        private void commandSql(MySqlCommand cmdSQL, Paciente oDatos)
        {
            cmdSQL.Parameters.AddWithValue("nombre", oDatos.Nombre);
            cmdSQL.Parameters.AddWithValue("apellido", oDatos.Apellido);
            cmdSQL.Parameters.AddWithValue("dni", oDatos.Dni);
            cmdSQL.Parameters.AddWithValue("domicilio", oDatos.Domicilio);
            cmdSQL.Parameters.AddWithValue("telefono", oDatos.Telefono);
            cmdSQL.Parameters.AddWithValue("fechaNacimiento", oDatos.FechaNacimiento);

        }
    }
}
