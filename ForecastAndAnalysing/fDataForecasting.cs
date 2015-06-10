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
            // setting up database connectivity
            dbConn = new databaseConnectivity();

            // reloading list of products available for user
            productListReload();

            // setting up currently selected product id
            selectedProductId = (int)comboBox_dataForecasting_productList.SelectedValue;

            
            // related to scenario management
            Forecast_ScenarioManagementTab scenarioManagementTab = new Forecast_ScenarioManagementTab();
            scenarioManagementTab.ScenarioListReload(ref dbConn, ref comboBox_scenarioList, ref selectedProductId);
                

            // setting up number of periods taken for trend calculation
            int trendEntityId = (int)numericUpDown1.Value;

            // refreshing chart
            chartRefresh(selectedProductId, trendEntityId);

            // refreshing forecasted data
            gridRefresh(selectedProductId);

            // not sure if it's required by current system, calculating A and B parameters for trend calculation
            //forecastTrendParameterCalculation();

            // setting up table with color for each tab
            foreach (TabPage tp in tabControl1.TabPages)
                TabColors.Add(tp, tp.BackColor);

            // setting up color no tab header, it's not available by default, can be set programically
            tabColorSet();
        }


        // loading combo box with product list data
        private void productListReload() {
            DataTable dtProductList = new DataTable();
            string sSql = "common.getProductList @userId='" + GlobalStaticClass.commonUserId.ToString() + "'";
            dbConn.getSqlData(sSql, dtProductList);

            comboBox_dataForecasting_productList.SelectedIndexChanged -= comboBox_dataForecasting_productList_SelectedIndexChanged;
            comboBox_dataForecasting_productList.DataSource = dtProductList;
            comboBox_dataForecasting_productList.DisplayMember = "productName";
            comboBox_dataForecasting_productList.ValueMember = "id";
            comboBox_dataForecasting_productList.SelectedIndexChanged += comboBox_dataForecasting_productList_SelectedIndexChanged;

        }

        // not sure if it's required by current system, calculating A and B parameters for trend calculation
        /*private void forecastTrendParameterCalculation() {

            // setting up class for lineralTrend parametes calculation
            forecast_LineralTrendCalculation fLineralTrendParameters = new forecast_LineralTrendCalculation();


            DataTable dtProductValues = new DataTable();
            //dbConn.getSqlData("forecast.productData 1", dtProductValues);
            string sSql = "forecast.getProductData 1";
            dbConn.getSqlData(sSql, dtProductValues);
            DateTime dtRegresionStart = DateTime.Parse("2015-01-01");

            double dA = 0;
            double dB = 0;

            fLineralTrendParameters.ForecastSetup_CalculateRegressionParameters(dtProductValues.Select(), dtRegresionStart, 1, ref dA, ref dB);

            label_AParam.Text = dA.ToString();
            label_BParam.Text = dB.ToString();
        }
        */


        // refreshing chart data with trend line based on historical period set with numeric value
        private void chartRefresh(int selectedProductId, int trendEntityId) {
            DataTable dtProductValues = new DataTable();
            //dbConn.getSqlData("forecast.productData 1", dtProductValues);
            //string sSql = "forecast.getProductDataByTrend " + selectedProductId.ToString() + " , " + trendEntityId.ToString()+"";
            string sSql = "forecast.getChartSalesAndTrend " + selectedProductId.ToString() + ", " + trendEntityId.ToString();
            dbConn.getSqlData(sSql, dtProductValues);

            //MessageBox.Show(dtProductValues.Rows.Count.ToString());
            chart1.Series.Clear();

            chart1.DataBindCrossTable(dtProductValues.Rows, "seriesT", "date", "value", "");

            foreach (Series sr in chart1.Series)
            {
                sr.ChartType = SeriesChartType.Line;
                sr.BorderWidth = 2;
                sr.SmartLabelStyle.Enabled = false;
            }

            
            /*
            chart1.Series.Add("TrendLine");
            chart1.Series["TrendLine"].ChartType = SeriesChartType.Spline;
            chart1.Series["TrendLine"].BorderWidth = 1;
            chart1.Series["TrendLine"].Color = Color.Red;
            // Line of best fit is linear
            string typeRegression = "Linear";//"Exponential";//
            //string typeRegression = "Power";//"Exponential";//
                                             // The number of days for Forecasting
            string forecasting = "0";
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
            */
            chart1.Update();
        }

        // refreshing forecaseted data based on calculation done on historical data and trend setup by user
        private void gridRefresh(int selectedProductId) {

            DataTable dtProductGridValues = new DataTable();
            //dbConn.getSqlData("forecast.productData 1", dtProductValues);
            string sSql = "forecast.[getProductDataForGridForecast] " + selectedProductId.ToString() + ", " + numericUpDown1.Value.ToString() + ", " + numericUpDown2.Value.ToString();
            dbConn.getSqlData(sSql, dtProductGridValues);

            dataGridView1.DataSource = dtProductGridValues;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.ReadOnly = true;
            


            
            
        }


        // event based on updated product data
        private void comboBox_dataForecasting_productList_SelectedIndexChanged(object sender, EventArgs e)
        {

            selectedProductId = (int)comboBox_dataForecasting_productList.SelectedValue;
            int trendEntityId = (int)numericUpDown1.Value;
            chartRefresh(selectedProductId, trendEntityId);

            chartRefresh(selectedProductId, trendEntityId);
            gridRefresh(selectedProductId);

        }

        // event - numeric drop down - used to setup trend calculation
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int trendEntityId = (int)numericUpDown1.Value;
            chartRefresh(selectedProductId, trendEntityId);
            gridRefresh(selectedProductId);
        }

        // event -  numeric drop down - used to calculate future forecast
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int trendEntityId = (int)numericUpDown1.Value;
            gridRefresh(selectedProductId);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Contains(tabPage_Seasonality))
                HideTabPage(tabPage_Seasonality);
            else
                ShowTabPage(tabPage_Seasonality);

            MessageBox.Show(System.Environment.UserName.ToString() + " " + System.Environment.UserDomainName.ToString());
            
        }

        private void HideTabPage(TabPage tp)
        {
            if (tabControl1.TabPages.Contains(tp))
                tabControl1.TabPages.Remove(tp);
        }

        private void ShowTabPage(TabPage tp)
        {
            ShowTabPage(tp, tabControl1.TabPages.Count);
        }


        private void ShowTabPage(TabPage tp, int index)
        {
            if (tabControl1.TabPages.Contains(tp)) return;
            InsertTabPage(tp, index);
        }


        private void InsertTabPage(TabPage tabpage, int index)
        {
            if (index < 0 || index > tabControl1.TabCount)
                throw new ArgumentException("Index out of Range.");
            tabControl1.TabPages.Add(tabpage);
            if (index < tabControl1.TabCount - 1)
                do
                {
                    SwapTabPages(tabpage, (tabControl1.TabPages[tabControl1.TabPages.IndexOf(tabpage) - 1]));
                }
                while (tabControl1.TabPages.IndexOf(tabpage) != index);
            tabControl1.SelectedTab = tabpage;
        }


        private void SwapTabPages(TabPage tp1, TabPage tp2)
        {
            if (tabControl1.TabPages.Contains(tp1) == false || tabControl1.TabPages.Contains(tp2) == false)
                throw new ArgumentException("TabPages must be in the TabControls TabPageCollection.");

            int Index1 = tabControl1.TabPages.IndexOf(tp1);
            int Index2 = tabControl1.TabPages.IndexOf(tp2);
            tabControl1.TabPages[Index1] = tp2;
            tabControl1.TabPages[Index2] = tp1;

            //Uncomment the following section to overcome bugs in the Compact Framework
            //tabControl1.SelectedIndex = tabControl1.SelectedIndex; 
            //string tp1Text, tp2Text;
            //tp1Text = tp1.Text;
            //tp2Text = tp2.Text;
            //tp1.Text=tp2Text;
            //tp2.Text=tp1Text;

        }



        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>();
        private void SetTabHeader(TabPage page, Color color)
        {
            TabColors[page] = color;
            tabControl1.Invalidate();
        }

        private void TabControlMain_DrawItem(object sender, DrawItemEventArgs e)
        {
            //e.DrawBackground();
            using (Brush br = new SolidBrush(TabColors[tabControl1.TabPages[e.Index]]))
            {
                e.Graphics.FillRectangle(br, e.Bounds);
                SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = e.Bounds;
                rect.Offset(0, 1);
                rect.Inflate(0, -1);
                e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                e.DrawFocusRectangle();
            }
        }


        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            tabColorSet();
        }
        private void tabColorSet() {

            this.tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabControlMain_DrawItem);
            SetTabHeader(tabControl1.SelectedTab, System.Drawing.Color.Green);
            foreach (TabPage tp in tabControl1.TabPages)
            {
                if (tp != tabControl1.SelectedTab)
                {
                    SetTabHeader(tp, System.Drawing.Color.DarkGray);
                }
            }

        }


    }
}
