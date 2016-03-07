using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


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

            //sqlCon.ConnectionString = "Data Source=93.105.202.70\\sql2014,50411; Initial Catalog=forecast;User ID=forecast;Password=Jeron1mo";
            sqlCon.ConnectionString = "Data Source=.\\sqlexpress; Initial Catalog=forecast;User ID=forecast;Password=Jeron1mo";

            try
            {
                sqlCon.Open();
            }
            catch (SqlException ex) {
                sqlException = ex.Message;
                sqlCon.Dispose();
                MessageBox.Show(sqlException);
                
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
                MessageBox.Show(sqlException);
                dt = null;
                return dt;
            }
        }

        public void runSql(string sqlText)
        {

            if (!isDbConnectionSet()) { sqlCon = setDbConnectivity(); }

            try
            {

                SqlCommand sqlCmd = new SqlCommand(sqlText, sqlCon);
                sqlCmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {

                sqlException = ex.Message;
                MessageBox.Show(sqlException);
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
