using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

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



        public void ScenarioListReload()
        {
            int iProductId = GlobalStaticClass.commonProductId;
            DataTable dtScenarioList = new DataTable();
            string sSql = "forecast.scenarioGet @userId='" + GlobalStaticClass.commonUserId.ToString() + "', @productId = '"+ iProductId.ToString()+"'";
            dbConn.getSqlData(sSql, dtScenarioList);
            
            cb.DataSource = dtScenarioList;
            cb.DisplayMember = "name";
            cb.ValueMember = "id";
        }

        public void scenarioAdd() {
            int iProductId = GlobalStaticClass.commonProductId;
            string sScenarioName = scenarioTextBox.Text.ToString();
            string sSql = "forecast.scenarioAdd @userId='" + GlobalStaticClass.commonUserId.ToString() + "', @productId = '" + iProductId.ToString() + "', @name = '"+sScenarioName+"', @descr = ''";
            dbConn.runSql(sSql);

            ScenarioListReload();
        }

        public void scenarioDelete()
        {
            int iProductId = GlobalStaticClass.commonProductId;
            if (scenarioComboBox.SelectedIndex >= 0) {
                int iScenarioId = (int)scenarioComboBox.SelectedValue;
                string sSql = "forecast.scenarioDelete @userId='" + GlobalStaticClass.commonUserId.ToString() + "', @scenarioId = '" + iScenarioId.ToString() + "'";
                dbConn.runSql(sSql);

                ScenarioListReload();
            }
        }

        public void chartScenarioHistoricalRefresh(Chart chr, int trendEntityId)
        {
            int selectedProductId = GlobalStaticClass.commonProductId;

            DataTable dtProductValues = new DataTable();
            //dbConn.getSqlData("forecast.productData 1", dtProductValues);
            //string sSql = "forecast.getProductDataByTrend " + selectedProductId.ToString() + " , " + trendEntityId.ToString()+"";
            string sSql = "[forecast].[getScenarioHistoricalDataForChart] " + GlobalStaticClass.commonScenarioId.ToString() + ", " + trendEntityId.ToString();
            dbConn.getSqlData(sSql, dtProductValues);

            //MessageBox.Show(dtProductValues.Rows.Count.ToString());
            chr.Series.Clear();
            chr.DataBindCrossTable(dtProductValues.Rows, "seriesT", "month", "value", "");

            foreach (Series sr in chr.Series)
            {
                sr.ChartType = SeriesChartType.Line;
                sr.BorderWidth = 2;
                sr.SmartLabelStyle.Enabled = false;
            }


            chr.Update();
        }

        public void chartScenarioForecastRefresh(Chart chr, int trendEntityId)
        {
            int selectedProductId = GlobalStaticClass.commonProductId;

            DataTable dtProductValues = new DataTable();
            //dbConn.getSqlData("forecast.productData 1", dtProductValues);
            //string sSql = "forecast.getProductDataByTrend " + selectedProductId.ToString() + " , " + trendEntityId.ToString()+"";
            string sSql = "[forecast].[getScenarioForecastedDataForChart] " + GlobalStaticClass.commonScenarioId.ToString() + ", " + trendEntityId.ToString();
            dbConn.getSqlData(sSql, dtProductValues);

            //MessageBox.Show(dtProductValues.Rows.Count.ToString());
            chr.Series.Clear();
            chr.DataBindCrossTable(dtProductValues.Rows, "seriesT", "date", "value", "");

            foreach (Series sr in chr.Series)
            {
                sr.ChartType = SeriesChartType.Line;
                sr.BorderWidth = 2;
                sr.SmartLabelStyle.Enabled = false;
            }


            chr.Update();
        }

    }
}
