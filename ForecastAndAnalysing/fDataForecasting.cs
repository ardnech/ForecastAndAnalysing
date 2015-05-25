using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ForecastAndAnalysing
{
    public partial class fDataForecasting : Form
    {
        public fDataForecasting()
        {
            InitializeComponent();
        }

        private void fDataForecasting_Load(object sender, EventArgs e)
        {
            databaseConnectivity dbConn = new databaseConnectivity();


            DataTable dtProductList = new DataTable();
            dbConn.getSqlData("common.getProductList", dtProductList);

            comboBox_dataForecasting_productList.DataSource = dtProductList;
            comboBox_dataForecasting_productList.DisplayMember = "productName";
            comboBox_dataForecasting_productList.ValueMember = "id";
            
            DataTable dtProductValues = new DataTable();
            //dbConn.getSqlData("forecast.productData 1", dtProductValues);
            dbConn.getSqlData("forecast.productData 1", dtProductValues);
            chart1.DataSource = dtProductValues;
            
            chart1.Series.First().XValueMember = "month";
            chart1.Series.First().YValueMembers = "value";
            chart1.Series[0].BorderWidth = 4;

            chart1.DataBind();
/*
            DataTable dtProductValuesForTrend = new DataTable();
            dbConn.getSqlData("forecast.productDataForTrend 1, 3", dtProductValuesForTrend);
            chart1.DataSource = dtProductValuesForTrend;

            chart1.Series.Add("HistoricalData");
            chart1.Series["HistoricalData"].XValueMember = "month";
            chart1.Series["HistoricalData"].YValueMembers = "value";
            chart1.Series["HistoricalData"].BorderWidth = 4;
            chart1.Series["HistoricalData"].BorderColor = Color.Green;

            chart1.DataBind();




            chart1.Series.Add("TrendLine");
            chart1.Series["TrendLine"].ChartType = SeriesChartType.Line;
            chart1.Series["TrendLine"].BorderWidth = 1;
            chart1.Series["TrendLine"].Color = Color.Red;
            // Line of best fit is linear
            string typeRegression = "Linear";//"Exponential";//
                                             // The number of days for Forecasting
            string forecasting = "1";
            // Show Error as a range chart.
            string error = "false";
            // Show Forecasting Error as a range chart.
            string forecastingError = "false";
            // Formula parameters
            string parameters = typeRegression + ',' + forecasting + ',' + error + ',' + forecastingError;
            //chart1.Series[0].Sort(PointSortOrder.Ascending, "X");

            // Create Forecasting Series.
            chart1.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, parameters, chart1.Series["HistoricalData"], chart1.Series["TrendLine"]);
            //chart1.DataManipulator.FinancialFormula()
*/
            chart1.Update();

            textBox1.Text = comboBox_dataForecasting_productList.SelectedValue.ToString();
        }
    }
}
