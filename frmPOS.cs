using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Diagram.Core.Native.Ribbon;
using DevExpress.XtraEditors;
using LuckInventorySystem_v2.controller;

namespace LuckInventorySystem_v2
{
    public partial class frmPOS : Form
    {
        ItemController _itemController = ItemController.GetInstance;
        SalesController _salesController = SalesController.GetInstance;

        private int _userRowIndex;
        private int _userRowIndex1;
        private ListViewItem selItem;
        private ListViewItem selItem1;  

        private int item_id;
        private string tranx_no;
        private int total_stocks;


        //for grand total
        string format = "#,##0.00;-$#,##0.00;Zero";
        double gtotal = 0;
        double discount = 0;


        private double amount;
        private double change;

        Boolean checkDiscount = false;

        public frmPOS()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            _itemController.LsvItem = LsvItem;
            LsvItem.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Item", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Supplier", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Stocks", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Category", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Brand", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Model", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Selling Price", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Wholesale Price", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Description", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Note", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Date Added", 200, HorizontalAlignment.Left);
            _itemController.display();


            LsvPurchased.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvPurchased.Columns.Add("Item", 200, HorizontalAlignment.Left);
            LsvPurchased.Columns.Add("Selling Price", 200, HorizontalAlignment.Left);
            LsvPurchased.Columns.Add("Quantity", 200, HorizontalAlignment.Left);
            LsvPurchased.Columns.Add("Total", 200, HorizontalAlignment.Left);

        }

        private void LsvItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem = LsvItem.Items[_userRowIndex];
            lblItemId.Text = selItem.SubItems[0].Text;
            lblItem.Text = selItem.SubItems[1].Text;
            lblSupplier.Text = selItem.SubItems[2].Text;
            lblStocks.Text = selItem.SubItems[3].Text;
            lblCategory.Text = selItem.SubItems[4].Text;
            lblPrice.Text = selItem.SubItems[7].Text;
         
        }

        private void LsvItem_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex = e.ItemIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int quantity = int.Parse(txtQty.Text);
            double total_price = quantity * double.Parse(lblPrice.Text);

            ListViewItem item = new ListViewItem();
            item.Text = lblItemId.Text;
            item.SubItems.Add(lblItem.Text);
            item.SubItems.Add(lblPrice.Text); // price 2
            item.SubItems.Add(quantity.ToString()); //quantity 3
            item.SubItems.Add(total_price.ToString(format)); //total 4

            if (int.Parse(txtQty.Text) > int.Parse(lblStocks.Text))
            {
                MessageBox.Show("Not Enough Stocks");
            }
            else
            {
                if (LsvPurchased.FindItemWithText(lblItemId.Text) == null)
                {
                    LsvPurchased.Items.Add(item);
                }
                else
                {
                    foreach (ListViewItem i in LsvPurchased.Items)
                    {
                        if (i.SubItems[0].Text == lblItemId.Text)
                        {
                            //Add Quantity
                            i.SubItems[3].Text =
                                Convert.ToString(Convert.ToInt32(quantity) +
                                                 Convert.ToInt32(i.SubItems[3].Text));

                            if (int.Parse(i.SubItems[3].Text) > int.Parse(lblStocks.Text))
                            {

                                i.SubItems[3].Text = lblStocks.Text;

                                MessageBox.Show("Not Enought Stocks");

                                //multiply quantity to item price
                                i.SubItems[4].Text = Convert.ToString(Convert.ToInt32(i.SubItems[2].Text) *
                                                                      Convert.ToInt32(i.SubItems[3].Text));
                            }
                            else
                            {
                                //multiply quantity to item price
                                i.SubItems[4].Text = Convert.ToString(Convert.ToInt32(i.SubItems[2].Text) *
                                                                      Convert.ToInt32(i.SubItems[3].Text));
                            }
                        }
                    }
                }

                gtotal = 0;

                foreach (ListViewItem lstItem in LsvPurchased.Items)
                {
                    gtotal += double.Parse(lstItem.SubItems[4].Text);
                }
            }

            txtTotal.Text = gtotal.ToString(format);

            chkDiscount.Checked = false;
            clear();

        }

        private void chkDiscount_CheckedChanged(object sender, EventArgs e)
        {

            checkDiscount = chkDiscount.Checked;

            if (!checkDiscount)
            {
                clear();
                txtDiscount.Text = "";
                txtDiscount.Enabled = false;
                txtTotal.Text = gtotal.ToString(format);
            }
            else
            {
                clear();
                txtDiscount.Text = "";
                txtDiscount.Enabled = true;
            }

        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (checkDiscount)
            {
                clear();
                txtDiscount.Enabled = true;
                discount = gtotal - double.Parse(txtDiscount.Text);
                txtTotal.Text = discount.ToString(format);
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                amount = double.Parse(txtAmount.Text);

                change = amount - double.Parse(txtTotal.Text);

                if (amount < double.Parse(txtTotal.Text))
                {
                    txtChange.Text = "invalid";
                    btnProcess.Enabled = false;
                }
                else
                {
                    if (change.ToString(format) == "Zero")
                    {
                        txtChange.Text = "0.00";
                    }
                    else
                    {
                        txtChange.Text = change.ToString(format);
                    }

                    btnProcess.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                
            }
 
        }

        public void clear()
        {
            txtAmount.Text = "";
            txtChange.Text = "";
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;
            int number_of_item;

            dialogResult = XtraMessageBox.Show("Do you want to Process this transaction?",
                    "Saving...",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);

            if (dialogResult == DialogResult.OK)
            {
                generateTranxNo();

                try
                {
                    foreach (ListViewItem item in LsvPurchased.Items)
                    {

                        number_of_item = LsvPurchased.Items.Count;

                        if (txtDiscount.Text == "")
                        {
                            discount = discount / number_of_item;
                        }
                        else
                        {
                            discount = double.Parse(txtDiscount.Text) / number_of_item;
                        }
                         
                        _salesController.ItemId = item.Text;
                        _salesController.SoldQuantity = item.SubItems[3].Text;
                        _salesController.Discount = discount;
                        _salesController.TranxNo = tranx_no;
                        _salesController.UserId = 1;
                        _salesController.add();

                        _itemController.ItemId = item.Text;
                        _itemController.getAvailableStocks(item.Text);
                        _itemController.Stocks = _itemController.Stocks - int.Parse(item.SubItems[3].Text);
                        _itemController.updateStocks();
                        _itemController.display();
                    }

                    XtraMessageBox.Show("Transaction Complete " ,
                        "Sucess",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    clearTransaction();

                    DialogResult receiptDialog = XtraMessageBox.Show("Do you want to Print Receipt?",
                  "Printing...",
                  MessageBoxButtons.OKCancel,
                  MessageBoxIcon.Question);

                    if (receiptDialog == DialogResult.OK)
                    {
                        new frmReceipt(tranx_no).ShowDialog();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public string generateTranxNo()
        {
            string strTranx = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+{}:<>?+-";
            string strTranxNo = "";

            Random rnd = new Random();
            for (int i = 0; i <= 30; i++)
            {
                int iRandom = rnd.Next(0, strTranx.Length - 1);
                strTranxNo += strTranx.Substring(iRandom, 1);
            }

            tranx_no = "tranx" + strTranxNo;

            return tranx_no;
        }

        public void clearTransaction()
        {

            chkDiscount.Checked = false;
            LsvPurchased.Items.Clear();
            txtTotal.Text = "";
            txtDiscount.Text = "";
            txtAmount.Text = "";
            txtChange.Text = "";lblStocks.Text = "";
            lblItemId.Text = "";
            lblSupplier.Text = "";
            lblCategory.Text = "";
            lblItem.Text = "";
            lblItemId.Text = "";
            lblPrice.Text = "";

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtSystemDate.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new frmOrders().ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            new frmMain().Show();
        }
    }
}
