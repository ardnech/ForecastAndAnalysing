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

        databaseConnectivity dbConn = null;
        int selectedProductId;

        private void fDataForecasting_Load(object sender, EventArgs e)
        {
            dbConn = new databaseConnectivity();
            

            DataTable dtProductList = new DataTable();
            dbConn.getSqlData("common.getProductList", dtProductList);

            comboBox_dataForecasting_productList.SelectedIndexChanged -= comboBox_dataForecasting_productList_SelectedIndexChanged;
            comboBox_dataForecasting_productList.DataSource = dtProductList;
            comboBox_dataForecasting_productList.DisplayMember = "productName";
            comboBox_dataForecasting_productList.ValueMember = "id";
            comboBox_dataForecasting_productList.SelectedIndexChanged += comboBox_dataForecasting_productList_SelectedIndexChanged;


            selectedProductId = (int)comboBox_dataForecasting_productList.SelectedValue;
            int trendEntityId = (int)numericUpDown1.Value;

            chartRefresh(selectedProductId, trendEntityId);
            gridRefresh(selectedProductId);


        }

        private void chartRefresh(int selectedProductId, int trendEntityId) {
            DataTable dtProductValues = new DataTable();
            //dbConn.getSqlData("forecast.productData 1", dtProductValues);
            string sSql = "forecast.getProductDataByTrend " + selectedProductId.ToString() + " , " + trendEntityId.ToString()+"";
            dbConn.getSqlData(sSql, dtProductValues);

            //MessageBox.Show(dtProductValues.Rows.Count.ToString());
            chart1.Series.Clear();

            chart1.DataBindCrossTable(dtProductValues.Rows, "seriesT", "month", "value", "");

            foreach (Series sr in chart1.Series)
            {
                sr.ChartType = SeriesChartType.Line;
                sr.BorderWidth = 2;
                sr.SmartLabelStyle.Enabled = false;
            }



            chart1.Series.Add("TrendLine");
            chart1.Series["TrendLine"].ChartType = SeriesChartType.Spline;
            chart1.Series["TrendLine"].BorderWidth = 1;
            chart1.Series["TrendLine"].Color = Color.Red;
            // Line of best fit is linear
            string typeRegression = "Linear";//"Exponential";//
                                             // The number of days for Forecasting
            string forecasting = "3";
            // Show Error as a range chart.
            string error = "false";
            // Show Forecasting Error as a range chart.
            string forecastingError = "false";
            // Formula parameters
            string parameters = typeRegression + ',' + forecasting + ',' + error + ',' + forecastingError;
            //chart1.Series[0].Sort(PointSortOrder.Ascending, "X");

            // Create Forecasting Series.
            chart1.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, parameters, chart1.Series["TrendCalculated"], chart1.Series["TrendLine"]);
            //chart1.DataManipulator.FinancialFormula()
            chart1.Update();
        }


        private void gridRefresh(int selectedProductId) {

            DataTable dtProductGridValues = new DataTable();
            //dbConn.getSqlData("forecast.productData 1", dtProductValues);
            string sSql = "forecast.getProductDataForGrid " + selectedProductId.ToString() + "";
            dbConn.getSqlData(sSql, dtProductGridValues);

            dataGridView1.DataSource = dtProductGridValues;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.ReadOnly = true;
            

            foreach (DataGridViewRow drv in dataGridView1.Rows) {
                switch( (int)drv.Cells[0].Value)
                
                {
                    case 1:
                        drv.DefaultCellStyle.Format = "D4";
                        break;
                    case 2:
                        drv.DefaultCellStyle.Format = "N2";
                        break;
                    case 3:
                        drv.DefaultCellStyle.Format = "N2";
                        break;
                }
            }

            
            
        }

        private void comboBox_dataForecasting_productList_SelectedIndexChanged(object sender, EventArgs e)
        {

            selectedProductId = (int)comboBox_dataForecasting_productList.SelectedValue;
            int trendEntityId = (int)numericUpDown1.Value;
            chartRefresh(selectedProductId, trendEntityId);

            chartRefresh(selectedProductId, trendEntityId);
            gridRefresh(selectedProductId);

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int trendEntityId = (int)numericUpDown1.Value;
            chartRefresh(selectedProductId, trendEntityId);
        }
    }
}
