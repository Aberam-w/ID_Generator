using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QR_Gen
{
    public partial class Loader : MetroFramework.Forms.MetroForm
    {
        public Loader()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            metroProgressBar1.Value = metroProgressBar1.Value + 2;
            label2.Text = metroProgressBar1.Value.ToString() + "%";
            if (metroProgressBar1.Value>=99)
            {
                timer1.Enabled = false;
                Form1 frm = new Form1();
                frm.Show();
                this.Hide();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            metroProgressSpinner1.Value = metroProgressSpinner1.Value + 3;
            if (metroProgressSpinner1.Value>=99)
            {
                metroProgressSpinner1.Value = 0;
            }
        }
    }
}
