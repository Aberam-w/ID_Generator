using com.itextpdf.text.pdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MetroFramework;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MetroFramework.Drawing.MetroPaint.BorderColor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using DataTable = System.Data.DataTable;
using Microsoft.VisualBasic;
using System.Data.OleDb;


namespace QR_Gen
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();

        }

        //Funtion of Database Connection with Form and Data load--------------->
        public void DataLoadDataGridView()
        {
            //try
            //{
            //    string connstring = ConfigurationManager.ConnectionStrings["condb"].ConnectionString;
            //    using (SqlConnection con = new SqlConnection(connstring))
            //    {
            //        con.Open();
            //        SqlCommand cmd = new SqlCommand("Select * from Arshia_Inventory", con);
            //        SqlDataReader sdr = cmd.ExecuteReader();
            //        DataTable dt = new DataTable();
            //        dt.Load(sdr);
            //        qrgrid.DataSource = dt;
            //        con.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MetroMessageBox.Show(this, ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        public void DataLoadComboBox()
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
        
        private void btnqrgen_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtqrcount.Text == "0" || txtqrcount.Text=="")
                {
                    MetroMessageBox.Show(this, "Please Enter Correct Count of QR you need to Generate...", "QR Generation Process Stopped!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else
                {

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void DataRefresh()
        {
            try
            {
                cmbitemname.Sorted = true;
                string connstring = ConfigurationManager.ConnectionStrings["condb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connstring))
                {
                    con.Open();
                    string query = "Select * from QR_Data";
                    SqlDataAdapter adpt = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    databasegrid.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ItemsLoad()
        {
            string xelPath = Environment.CurrentDirectory + @"\Resources\ItemLoad\item.txt";
            DataTable dtt = new DataTable();
            dtt.Columns.Add("Item_Name", typeof(string));//column 1 
            dtt.Columns.Add("item_ID", typeof(string));//column 2 
            try
            {
                string[] lineOfContents = File.ReadAllLines(xelPath);
                foreach (var line in lineOfContents)
                {
                    string[] tokens = line.Split('\t');
                    //cmbitemname.Items.Add(tokens[0]);
                    dtt.Rows.Add(new Object[] { tokens[0], tokens[1] });
                }
                metroGrid1.DataSource = dtt;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        private void Form1_Load(object sender, EventArgs e)
        {
            //Database Load--------------->
            DataLoadDataGridView();
            //DataLoadComboBox();
            DateTime dt = DateTime.Now;
            cmbyear.SelectedItem = dt.Year.ToString();
            cmbmonth.SelectedItem = dt.Month.ToString();
            cmbitemname.DropDownStyle = ComboBoxStyle.DropDown;
            //tabcontrol.TabPages.Remove(tabprint);
            this.tabcontrol.SelectedIndex = 0;
            cmbitemname.Items.Add("--Select Product--");
            cmbitemname.SelectedItem = "--Select Product--";
            cmbyear.SelectedItem = "23";
            cmbmonth.SelectedItem = "01";
            //DataRefresh();
            String version = Environment.Version.ToString();
            String[] versionArray = version.Split('.');
            var newVersion = string.Join(".", versionArray.Take(2));
            lblversion.Text = "Ver. " + newVersion;
            lblowner.Text = "© 2023 Arshia - A Flügel Company";
            txtqrcount.Focus();
            ItemsLoad();
        }

        private void qrgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            //Next Label ID will see the ListBox or LabelID--------------->
            if (lstlabelid.SelectedIndex < lstlabelid.Items.Count - 1 && lstserialno.SelectedIndex < lstserialno.Items.Count - 1)
            {
                lstlabelid.SelectedIndex = lstlabelid.SelectedIndex + 1;
                lstserialno.SelectedIndex = lstserialno.SelectedIndex + 1;
            }
        }

        private void btnprevious_Click(object sender, EventArgs e)
        {
            //Previous Label ID will see the ListBox or LabelID--------------->
            if (lstlabelid.SelectedIndex > 0 && lstserialno.SelectedIndex > 0)
            {
                lstlabelid.SelectedIndex = lstlabelid.SelectedIndex - 1;
                lstserialno.SelectedIndex = lstserialno.SelectedIndex - 1;
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            //Listbox Clear Coding--------------->
            lstlabelid.Items.Clear();
            lbllabelid.Text = "";
        }

        private void cmbitemname_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (e.KeyChar < 32 || e.KeyChar > 126)
            //{
            //    return;
            //}
            //string t = cmbitemname.Text;
            //string typedT = t.Substring(0, cmbitemname.SelectionStart);
            //string newT = typedT + e.KeyChar;

            //int i = cmbitemname.FindString(newT);
            //if (i == -1)
            //{
            //    e.Handled = true;
            //}

            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            cb.DroppedDown = true;
            string strFindStr = "";
            if (e.KeyChar == (char)8)
            {
                if (cb.SelectionStart <= 1)
                {
                    cb.Text = "";
                    return;
                }

                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text.Substring(0, cb.Text.Length - 1);
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart - 1);
            }
            else
            {
                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text + e.KeyChar;
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            }
            int intIdx = -1;
            // Search the string in the ComboBox list.
            intIdx = cb.FindString(strFindStr);
            if (intIdx != -1)
            {
                cb.SelectedText = "";
                cb.SelectedIndex = intIdx;
                cb.SelectionStart = strFindStr.Length;
                cb.SelectionLength = cb.Text.Length;
                e.Handled = true;
            }
            else
                e.Handled = true;
        }
        public int TotalDigit;
        private void btnserialnogen_Click(object sender, EventArgs e)
        {
            //QR Printing to pdf Code--------------->
            try
            {
                if (cmbitemname.SelectedItem.ToString() == "--Select Product--")
                {
                    MetroMessageBox.Show(this, "Please Select Which Item You Need to Make a ID", "Serial Number Process Stopped!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cmbyear.SelectedIndex.ToString() == "")
                {
                    MetroMessageBox.Show(this, "Please Select Product Manufacturer Year", "Serial Number Process Stopped!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cmbmonth.SelectedIndex.ToString() == "")
                {
                    MetroMessageBox.Show(this, "Please Select Product Manufacturer Month", "Serial Number Process Stopped!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtitemsku.Text == "")
                {
                    MetroMessageBox.Show(this, "Please Enter Product Correct Item ID", "Serial Number Process Stopped!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtqrcount.Text == "" || txtqrcount.Text=="0")
                {
                    MetroMessageBox.Show(this, "Please Enter QR Count", "Serial Number Process Stopped!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //QR_Gen Making--------------->
                    lstlabelid.Items.Clear();
                    Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                    picboxqr.Image = qrcode.Draw("https://chk.arshiaonline.com/", 10);

                    //Random Label ID Making and Printing to Listbox--------------->
                    Random RNG = new Random();
                    const string range = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTVUWXYZ";
                    var chars = Enumerable.Range(0, 8).Select(x => range[RNG.Next(0, range.Length)]);

                    for (int i = 0; i < Convert.ToInt32(txtqrcount.Text); i++)
                    {
                        lstlabelid.Items.Add(new string(chars.ToArray()));
                        //int idx = gridview.Rows.Add();
                        //gridview.Rows[idx].Cells["Label_ID"].Value = (new string(chars.ToArray()));
                    }
                    lstlabelid.SelectedIndex = 0;
                    lbllabelid.Text = lstlabelid.SelectedItem.ToString();


                    //Serial Number Making Code--------------------->
                    lstserialno.Items.Clear();
                    int idcount = Convert.ToInt32(txtqrcount.Text);

                    string xelPath = Environment.CurrentDirectory + @"\Resources\ItemLoad\LastDigit.txt";
                    string LastDigit = File.ReadAllText(xelPath);
                    int serailNoStarter = Convert.ToInt32(LastDigit);
                    for (int i = 1; i <= idcount; i++)
                    {
                        TotalDigit = serailNoStarter + i;
                        string serialNumber = Convert.ToInt32(TotalDigit).ToString();
                        lstserialno.Items.Add(cmbmonth.SelectedItem.ToString() + cmbyear.SelectedItem.ToString() + txtitemsku.Text + serialNumber);
                    }
                    lstserialno.SelectedIndex = 0;
                    lblserialno.Text = lstserialno.SelectedItem.ToString();

                    
                    Data_ID_Load();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtitemsku_TextChanged(object sender, EventArgs e)
        {
            txtitemsku.SelectionStart = txtitemsku.Text.Length;
            txtitemsku.Text = txtitemsku.Text.ToUpper();
            try
            {
                (metroGrid1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Item_ID LIKE '{0}%'", txtitemsku.Text);
                //cmbitemname.Items.Add(metroGrid1.SelectedCells[0].Value.ToString());
                cmbitemname.SelectedItem = metroGrid1.SelectedCells[0].Value.ToString();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtqrcount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }

            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //public void Data_ADDtoDatabase()
        //{
        //    string StrQuery;
        //    string cmbItemName = cmbitemname.SelectedItem.ToString();
        //    string cmbItemID = txtitemsku.Text;
        //    try
        //    {
        //        using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-V1KUSSB;Initial Catalog=Sample;Integrated Security=True"))
        //        {
        //            using (SqlCommand comm = new SqlCommand())
        //            {
        //                comm.Connection = sqlCon;
        //                sqlCon.Open();

        //                for (int i = 0; i < gridview.Rows.Count; i++)
        //                {
        //                    StrQuery = @"INSERT INTO QR_Data (Label_ID,Serial_Number,Item_Name,Item_ID) VALUES ('" 
        //                        + gridview.Rows[i].Cells["Label ID"].Value + "', '"
        //                        + gridview.Rows[i].Cells["Serial Number"].Value + "', '" 
        //                        + gridview.Rows[i].Cells["Item Name"].Value + "', '"
        //                        + gridview.Rows[i].Cells["Item ID"].Value + "')";
        //                    comm.CommandText = StrQuery;
        //                    comm.ExecuteNonQuery();
        //                }
        //                //sqlCon.Close();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MetroMessageBox.Show(this, ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}
        DataTable dt = new DataTable();
        public void InstantDatabase()
        {
            string cmbItemName = cmbitemname.SelectedItem.ToString();
            string cmbItemID = txtitemsku.Text;
            string imgURL = txtimgurl.Text;
            int rowIndex = dt.Rows.Count;
            if (rowIndex > 0)
            {
                for (int i = 0; i < lstlabelid.Items.Count; i++)
                {
                    dt.Rows.Add(new Object[] { lstlabelid.Items[i].ToString(), lstserialno.Items[i].ToString(), cmbItemName, cmbItemID, imgURL });
                }
                databasegrid.DataSource = dt;
            }
            else
            {
                dt.Columns.Add("Label ID", typeof(string));//column 0
                dt.Columns.Add("Serial Number", typeof(string));//column 1  
                dt.Columns.Add("Product Name", typeof(string));//column 2  
                dt.Columns.Add("Product ID", typeof(string));//column 3  
                dt.Columns.Add("Product Image", typeof(string));//column 4 
                dt.Columns.Add("Date", typeof(string));//column 5 
                for (int i = 0; i < lstlabelid.Items.Count; i++)
                {
                    dt.Rows.Add(new Object[] { lstlabelid.Items[i].ToString(), lstserialno.Items[i].ToString(), cmbItemName, cmbItemID, imgURL,"" });
                    //dt.Rows.Add(lstlabelid.Items[i].ToString(), lstserialno.Items[i].ToString(), cmbItemName, cmbItemID);
                }
                databasegrid.DataSource = dt;

            }
        }


        public void Data_ID_Load()
        {
            string cmbItemName = cmbitemname.SelectedItem.ToString();
            string cmbItemID = txtitemsku.Text;
            string imgURL = txtimgurl.Text;
            DataTable dts = new DataTable();
            dts.Columns.Add("Label ID", typeof(string));//column 0
            dts.Columns.Add("Serial Number", typeof(string));//column 1  
            dts.Columns.Add("Product Name", typeof(string));//column 2  
            dts.Columns.Add("product ID", typeof(string));//column 3  
            dts.Columns.Add("Product Image", typeof(string));//column 4 
            dts.Columns.Add("Date", typeof(string));//column 5 
            for (int i = 0; i < lstlabelid.Items.Count; i++)
            {
                dts.Rows.Add(new Object[] { lstlabelid.Items[i].ToString(), lstserialno.Items[i].ToString(), cmbItemName, cmbItemID, imgURL,"" });
            }
            gridview.DataSource = dts;
        }
        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbllabelid.Text == "" || lblserialno.Text == "")
                {
                    MetroMessageBox.Show(this, "Please Make First Label ID and Serial No", "ID Print Submission Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    InstantDatabase();
                    //Data_ID_Load();
                    lbltotal.Text = "Total - " + databasegrid.Rows.Count.ToString();
                    this.tabcontrol.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.Message, "Error");
            }
            finally
            {
                GC.Collect();
            }
        }
        public void Excel_Export()
        {
            try
            {
                if (databasegrid.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Excel (.xlsx)|  *.xlsx";
                    sfd.FileName = "Arshia_" + DateTime.Now.ToString("MM-dd-yyyy hh-mm") + ".xlsx";
                    bool fileError = false;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(sfd.FileName))
                        {
                            try
                            {
                                File.Delete(sfd.FileName);
                            }
                            catch (IOException ex)
                            {
                                fileError = true;
                                MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                                MetroMessageBox.Show(this, ex.Message + Environment.NewLine + "It wasn't possible to write the data to the disk.", "Excel Export Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        if (!fileError)
                        {
                            try
                            {
                                Microsoft.Office.Interop.Excel.Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                                Microsoft.Office.Interop.Excel._Workbook workbook = XcelApp.Workbooks.Add(Type.Missing);
                                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                                worksheet = workbook.Sheets["Sheet1"];
                                worksheet = workbook.ActiveSheet;
                                worksheet.Name = "Arshia ID Making List";
                                worksheet.Application.ActiveWindow.SplitRow = 1;
                                worksheet.Application.ActiveWindow.FreezePanes = true;

                                for (int i = 1; i < databasegrid.Columns.Count + 1; i++)
                                {
                                    worksheet.Cells[1, i] = databasegrid.Columns[i - 1].HeaderText;
                                    worksheet.Cells[1, i].Font.NAME = "Calibri";
                                    worksheet.Cells[1, i].Font.Bold = true;
                                    worksheet.Cells[1, i].Interior.Color = Color.Wheat;
                                    worksheet.Cells[1, i].Font.Size = 12;
                                }

                                for (int i = 0; i < databasegrid.Rows.Count; i++)
                                {
                                    for (int j = 0; j < databasegrid.Columns.Count; j++)
                                    {
                                        worksheet.Cells[i + 2, j + 1] = databasegrid.Rows[i].Cells[j].Value.ToString();
                                    }
                                }

                                worksheet.Columns.AutoFit();
                                workbook.SaveAs(sfd.FileName);
                                XcelApp.Quit();
                                ReleaseObject(worksheet);
                                ReleaseObject(workbook);
                                ReleaseObject(XcelApp);

                                MetroMessageBox.Show(this, "Data Exported Successfully !!!", "Excel Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MetroMessageBox.Show(this, ex.Message, "Excel Export Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MetroMessageBox.Show(this, "Exported Failed!!!", "Excel Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            Excel_Export();
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private void lstserialno_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            //if the item state is selected them change the back color 
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          Color.Red);//Choose the color

            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Draw the current item text
            e.Graphics.DrawString(lstserialno.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        private void lstlabelid_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            //if the item state is selected them change the back color 
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          Color.Red);//Choose the color

            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Draw the current item text
            e.Graphics.DrawString(lstlabelid.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        private void btnclearsn_Click(object sender, EventArgs e)
        {
            cmbitemname.SelectedItem = "--Select Product--";
            cmbyear.SelectedItem = "23";
            cmbmonth.SelectedItem = "01";
            txtitemsku.Text = "";
            txtqrcount.Text = "0";
            lstlabelid.Items.Clear();
            lstserialno.Items.Clear();
            gridview.DataSource = "";
            txtimgurl.Text = "";
        }

        private void lstlabelid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //When select the item from ListBox selecetion will change--------------->
            lbllabelid.Text = lstlabelid.SelectedItem.ToString();
        }

        private void lstserialno_SelectedIndexChanged(object sender, EventArgs e)
        {
            //When select the item from ListBox selecetion will change--------------->
            lblserialno.Text = lstserialno.SelectedItem.ToString();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            gridview.DataSource = "";
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            //DataRefresh();
            DialogResult dialogResult = MessageBox.Show("If you clear this datasheet, Data will lost", Environment.NewLine + "Are you sure want to clear this datasheet?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                databasegrid.DataSource = "";
                dt.Clear();
                dt.Columns.Clear();
                lbltotal.Text = "Total - " + databasegrid.Rows.Count.ToString();
            }
            else if (dialogResult == DialogResult.No)
            {
                //Close();
                lbltotal.Text = "Total - " + databasegrid.Rows.Count.ToString();
            }
        }
        public void SQLExport()
        {
            try
            {
                if (databasegrid.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "SQL Query (.sq)|  *.sq";
                    sfd.FileName = "Arshia_" + DateTime.Now.ToString("MM-dd-yyyy hh-mm") + ".sq";
                    bool fileError = false;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(sfd.FileName))
                        {
                            try
                            {
                                File.Delete(sfd.FileName);
                            }
                            catch (IOException ex)
                            {
                                fileError = true;
                                MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                                MetroMessageBox.Show(this, ex.Message + Environment.NewLine + "It wasn't possible to write the data to the disk.", "Excel Export Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        if (!fileError)
                        {
                            try
                            {
                                TextWriter writer = new StreamWriter(sfd.FileName);
                                for (int i = 0; i < databasegrid.Rows.Count; i++)
                                {
                                    writer.WriteLine("INSERT INTO tbl_QR_Data (user_name, password, item_name, item_id, item_image, date) VALUES (");
                                    for (int j = 0; j < databasegrid.Columns.Count; j++)
                                    {
                                        var queryText = "'" + databasegrid.Rows[i].Cells[j].Value.ToString() + "',";
                                        writer.Write("'" + databasegrid.Rows[i].Cells[j].Value.ToString() +"',");
                                    }
                                    writer.WriteLine(");");
                                }
                                writer.Close();
                                StreamReader sr = new StreamReader(sfd.FileName, Encoding.Default);
                                StreamWriter sw = new StreamWriter(sfd.FileName + "l", false, Encoding.Default);
                                string al;

                                while (!sr.EndOfStream)
                                {
                                    al = sr.ReadLine();
                                    al = al.Replace(",)", ")");
                                    sw.WriteLine(al);
                                }
                                sw.Close();
                                sr.Close();
                                File.Delete(sfd.FileName);
                                MetroMessageBox.Show(this, "SQL Data Exported Successfully !!!", "SQL Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            catch (Exception ex)
                            {
                                MetroMessageBox.Show(this, ex.Message + Environment.NewLine + "Error", "SQL Export Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message + Environment.NewLine + "Error", "SQL Export Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://tableconvert.com/excel-to-sql");
            SQLExport();
            string xelPath = Environment.CurrentDirectory + @"\Resources\ItemLoad\LastDigit.txt";
            StreamWriter writeLastDigit = new StreamWriter(xelPath);
            writeLastDigit.Write(TotalDigit);
            writeLastDigit.Close();
        }

        private void cmbitemname_Leave(object sender, EventArgs e)
        {
            //string t = cmbitemname.Text;

            //if (cmbitemname.SelectedItem == null)
            //{
            //    cmbitemname.Text = "";
            //}
        }

        private void cmbmonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            cb.DroppedDown = true;
            string strFindStr = "";
            if (e.KeyChar == (char)8)
            {
                if (cb.SelectionStart <= 1)
                {
                    cb.Text = "";
                    return;
                }

                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text.Substring(0, cb.Text.Length - 1);
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart - 1);
            }
            else
            {
                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text + e.KeyChar;
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            }
            int intIdx = -1;
            // Search the string in the ComboBox list.
            intIdx = cb.FindString(strFindStr);
            if (intIdx != -1)
            {
                cb.SelectedText = "";
                cb.SelectedIndex = intIdx;
                cb.SelectionStart = strFindStr.Length;
                cb.SelectionLength = cb.Text.Length;
                e.Handled = true;
            }
            else
                e.Handled = true;
        }

        private void cmbyear_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            cb.DroppedDown = true;
            string strFindStr = "";
            if (e.KeyChar == (char)8)
            {
                if (cb.SelectionStart <= 1)
                {
                    cb.Text = "";
                    return;
                }

                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text.Substring(0, cb.Text.Length - 1);
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart - 1);
            }
            else
            {
                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text + e.KeyChar;
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            }
            int intIdx = -1;
            // Search the string in the ComboBox list.
            intIdx = cb.FindString(strFindStr);
            if (intIdx != -1)
            {
                cb.SelectedText = "";
                cb.SelectedIndex = intIdx;
                cb.SelectionStart = strFindStr.Length;
                cb.SelectionLength = cb.Text.Length;
                e.Handled = true;
            }
            else
                e.Handled = true;
        }
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            System.Windows.Forms.Label label = new System.Windows.Forms.Label();
            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
            System.Windows.Forms.Button buttonOk = new System.Windows.Forms.Button();
            System.Windows.Forms.Button buttonCancel = new System.Windows.Forms.Button();
            form.Text = title;
            label.Text = promptText;
            label.Font = new System.Drawing.Font("Eurostile", 14);
            textBox.Font = new System.Drawing.Font("Eurostile", 12);
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;
            label.SetBounds(36, 36, 372, 13);
            textBox.SetBounds(36, 86, 400, 50);
            buttonOk.SetBounds(180, 160, 120, 30);
            buttonCancel.SetBounds(320, 160, 120, 30);
            label.AutoSize = true;
            form.ClientSize = new Size(500, 200);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        private void metroButton2_Click_1(object sender, EventArgs e)
        {
            string value = "";
            if (InputBox("Add New Product", "Please Enter New Prduct Name...", ref value) == DialogResult.OK)
            {
                cmbitemname.Items.Add(value);
                cmbitemname.SelectedItem = value;
            }
        }
        
        private void databasegrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lbltotal.Text = "Total - " + databasegrid.Rows.Count.ToString();
        }

        private void btnimgurl_Click(object sender, EventArgs e)
        {
            txtimgurl.Text = "";
            string imgURL = "https://arshiastore.ae/search?q=" + txtitemsku.Text;
            System.Diagnostics.Process.Start(imgURL);
        }
        private void txtitemsku_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void metroGrid1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnimport_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://chk.arshiaonline.com/admin-login.php");
        }
    }
}
