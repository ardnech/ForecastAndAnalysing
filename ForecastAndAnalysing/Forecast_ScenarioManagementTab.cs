using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;


namespace ForecastAndAnalysing
{
    class Forecast_ScenarioManagementTab
    {

        public void ScenarioListReload(ref databaseConnectivity dbConn, ref ComboBox cb, ref int productId )
        {
            DataTable dtScenarioList = new DataTable();
            string sSql = "dtScenarioList @userId='" + GlobalStaticClass.commonUserId.ToString() + "', @productId = '"+productId.ToString()+"'";
            dbConn.getSqlData(sSql, dtScenarioList);


            cb.DataSource = dtScenarioList;
            cb.DisplayMember = "name";
            cb.ValueMember = "id";


        }

    }
}
