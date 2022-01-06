using MySql.Data.MySqlClient;
using MySql.Data;
using System;
using System.Data;

namespace AccesoDatos
{
    public class BaseDatos
    {
        private string stConexion = "server = localhost; Port = 3306; database = turneroodontologo; uid = root; pwd = password";

        private MySqlConnection myConex;

        public MySqlConnection MyConex { get => myConex; set => myConex = value; }
        public BaseDatos()
        {
            Open();
        }

        private void Open()
        {
            MyConex = new MySqlConnection(stConexion);
            MyConex.Open();
        }
        public void Close()
        {
            MyConex.Close();
        }

        public bool ExecuteCommando(MySqlCommand cmd)
        {
            try
            {
                MySqlCommand oComando = cmd;
                int Id = Convert.ToInt32(oComando.ExecuteScalar());

                myConex.Close();
                if (Id == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                myConex.Close();
                return false;
            }
        }
        public int ExecuteCommandoInt(MySqlCommand cmd)
        {
            try
            {
                MySqlCommand oCommando = cmd;
                int Id = Convert.ToInt32(oCommando.ExecuteScalar());
                int ultimoId = Convert.ToInt32(oCommando.LastInsertedId);
                return ultimoId;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public DataTable ExecuteDataTable(string strSql)
        {
            DataTable functionReturnValue = null;

            DataSet oData = new DataSet();

            try
            {
                MySqlDataAdapter oAdap = new MySqlDataAdapter(strSql, myConex);
                oAdap.Fill(oData, "Registros");
                functionReturnValue = oData.Tables[0];
            }
            catch (Exception ex)
            {
                myConex.Close();
                throw ex;
            }
            return functionReturnValue;
        }
    }
}
