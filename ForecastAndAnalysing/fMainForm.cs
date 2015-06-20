using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ForecastAndAnalysing
{
    public partial class fMainForm : Form
    {
        public fMainForm()
        {
            InitializeComponent();
        }

        // initiate db connectivity
        databaseConnectivity dbCon = new databaseConnectivity();


        private void Form1_Load(object sender, EventArgs e)
        {

            // setup userId value on global class
            setGlobalUserId();




            dbCon.getSqlData("common.getAppLabels @userId = '" + GlobalStaticClass.commonUserId.ToString() + "'", GlobalStaticClass.load_dtLabel);

            DataRow[] result = GlobalStaticClass.load_dtLabel.Select("tag='appName'");

            this.Text = result[0][1].ToString();

        }

        private void setGlobalUserId()
        {
            try {
                DataTable dt = new DataTable();
                dbCon.getSqlData("common.getUserDetails @userName = '" + System.Environment.UserName + "'", dt);

                GlobalStaticClass.commonUserId = (int)dt.Rows[0][0];
            } catch
            {
                Application.Exit();
            }
        }

        private void helpInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is fMainHelp)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            fMainHelp fmh = new fMainHelp();
            fmh.MdiParent = this;
            fmh.Show();
        }

        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is fDataForecasting)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Maximized;
                    frm.Focus();
                    return;
                }
            }
            fDataForecasting fmh = new fDataForecasting();
            fmh.MdiParent = this;
            fmh.WindowState = FormWindowState.Maximized;
            fmh.Show();

        }




    }
}
