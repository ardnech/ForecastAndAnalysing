using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ForecastAndAnalysing
{
    class databaseConnectivity
    {

        SqlConnection sqlCon = new SqlConnection();
        string sqlException = "";
        SqlDataAdapter sda = new SqlDataAdapter();

        public databaseConnectivity() {
            if (!isDbConnectionSet()) {
                sqlCon = setDbConnectivity();
            }
        }


        private SqlConnection setDbConnectivity() {

            sqlCon.ConnectionString = "Data Source=SOFTQL_ARTUR\\SQLEXPRESS; Initial Catalog=forecast;User ID=forecast;Password=Jeron1mo";

            try
            {
                sqlCon.Open();
            }
            catch (SqlException ex) {
                sqlException = ex.Message;
                sqlCon.Dispose();
                return null;
            }
            return sqlCon;            
        }

        public DataTable getSqlData(string sqlText, DataTable dt) {

            if (!isDbConnectionSet()) { sqlCon = setDbConnectivity(); }

            try
            {

                SqlCommand sqlCmd = new SqlCommand(sqlText, sqlCon);

                sda.SelectCommand = sqlCmd;

                sda.Fill(dt);

                return dt;

            }
            catch (SqlException ex)
            {

                sqlException = ex.Message;
                dt = null;
                return dt;
            }
        }



        public bool isDbConnectionSet() {
            if (sqlCon.State == ConnectionState.Open) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
