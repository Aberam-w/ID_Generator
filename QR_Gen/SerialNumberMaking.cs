using iTextSharp.text;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QR_Gen
{
    public partial class SerialNumberMaking : MetroFramework.Forms.MetroForm
    {
        public SerialNumberMaking()
        {
            InitializeComponent();
        }
        public void DataLoad()
        {
            //Funtion of Product Names Binding with Combo Box and Data load--------------->
            try
            {
                cmbitemname.Sorted = true;
                string connstring = ConfigurationManager.ConnectionStrings["condb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connstring))
                {
                    con.Open();
                    string query = "Select Product_name from Arshia_Inventory";
                    SqlDataAdapter adpt = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmbitemname.Items.Add(dt.Rows[i]["Product_name"]);
                    }
                    con.Close();
                }
                cmbitemname.Items.Add("--Select Product--");
                cmbitemname.SelectedItem = "--Select Product--";
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SerialNumberMaking_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            cmbyear.SelectedItem = dt.Year.ToString();
            cmbmonth.SelectedItem = dt.Month.ToString();
            DataLoad();
            cmbitemname.DropDownStyle= ComboBoxStyle.DropDown;
        }

        private void cmbitemname_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbitemname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbitemname.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
    }
}
