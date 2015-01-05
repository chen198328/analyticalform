using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnalyticalPlatform
{
    public partial class SaveFree : Form
    {
        public SaveFree()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void SaveFree_Load(object sender, EventArgs e)
        {
            txtAddintional.Text = string.Format("{0}{1}{2}", DateTime.Now.Year, DateTime.Now.Month>9?DateTime.Now.Month.ToString():"0"+DateTime.Now.Month.ToString(), DateTime.Now.Day>9?DateTime.Now.Day.ToString():"0"+DateTime.Now.Day);
        }

        private void SaveFree_Paint(object sender, PaintEventArgs e)
        {
         
        }

        private void SaveFree_Resize(object sender, EventArgs e)
        {
           
        }
    }
}
