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

        ComboBox cb = null;
        databaseConnectivity dbConn = null;
        TextBox scenarioTextBox = null;



        public ComboBox scenarioComboBox
        {
            get { return cb; }
            set { cb = value; }
        }
        public databaseConnectivity dbConnectivity
        {
            get { return dbConn; }
            set { dbConn = value; }
        }
        public TextBox scenarioTextBoxSet
        {
            get { return scenarioTextBox; }
            set { scenarioTextBox = value; }
        }
        public void ScenarioTextBoxActivate()
        {
            scenarioTextBox.Enabled = true;
        }



        public void ScenarioListReload(ref int iProductId)
        {

            DataTable dtScenarioList = new DataTable();
            string sSql = "forecast.scenarioGet @userId='" + GlobalStaticClass.commonUserId.ToString() + "', @productId = '"+ iProductId.ToString()+"'";
            dbConn.getSqlData(sSql, dtScenarioList);


            cb.DataSource = dtScenarioList;
            cb.DisplayMember = "name";
            cb.ValueMember = "id";
        }

        public void scenarioAdd(ref int iProductId) {
            string sScenarioName = scenarioTextBox.Text.ToString();
            string sSql = "forecast.scenarioAdd @userId='" + GlobalStaticClass.commonUserId.ToString() + "', @productId = '" + iProductId.ToString() + "', @name = '"+sScenarioName+"', @descr = ''";
            dbConn.runSql(sSql);

            ScenarioListReload(ref iProductId);
        }

        public void scenarioDelete(ref int iProductId)
        {
            if (scenarioComboBox.SelectedIndex >= 0) {
                int iScenarioId = (int)scenarioComboBox.SelectedValue;
                string sSql = "forecast.scenarioDelete @userId='" + GlobalStaticClass.commonUserId.ToString() + "', @scenarioId = '" + iScenarioId.ToString() + "'";
                dbConn.runSql(sSql);

                ScenarioListReload(ref iProductId);
            }
        }

    }
}
