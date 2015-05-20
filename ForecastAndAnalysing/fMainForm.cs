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


        private void Form1_Load(object sender, EventArgs e)
        {
            databaseConnectivity dbCon = new databaseConnectivity();

            //label_test1.Text = dbCon.isDbConnectionSet().ToString();



            
            DataTable dt = new DataTable();
            dbCon.getSqlData("select * from dbo.test", dt);

            /*
            dataGridView1.Columns["ordId"].DataPropertyName = "ordId";
            dataGridView1.Columns["ordData"].DataPropertyName = "ordData";
            dataGridView1.DataSource = dt;
            */


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
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            fDataForecasting fmh = new fDataForecasting();
            fmh.MdiParent = this;
            fmh.Show();

        }




    }
}
