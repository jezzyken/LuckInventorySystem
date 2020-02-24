using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LuckInventorySystem_v2.controller;
using Label = System.Reflection.Emit.Label;

namespace LuckInventorySystem_v2
{
    public partial class ctrInventory : UserControl
    {
        ItemController _itemController = ItemController.GetInstance;
        OrderController _orderController = OrderController.GetInstance;
        RepairController _repairController = RepairController.GetInstance;
        SupplierController _supplierController = SupplierController.GetInstance;
        DefectiveController _defectiveController = DefectiveController.GetInstance;
        SalesController _salesController = SalesController.GetInstance;

        


        StockinController _stockinController = StockinController.GetInstance;

        private int _userRowIndex;
        private ListViewItem selItem;

        private int _userRowIndex1;
        private ListViewItem selItem1;

        private int _userRowIndexOrder;
        private ListViewItem selItemOrder;

        private int _userRowIndexRepairs;
        private ListViewItem selItemRepairs;

        private string item_id;
        private string stocks_item_id;
        private int stockin_id;
        private string item_name;
        private int stocks;
        private int total_stocks = 0;
        private int item_stocks;

        private string ordered = "";
        private string cancelled = "";
        private string pending = "";

      

        public ctrInventory()
        {
            InitializeComponent();

            _itemController.LsvItem = LsvItem;
            LsvItem.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Item", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Supplier", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Stocks", 143, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Category", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Brand", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Model", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Selling Price", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Wholesale Price", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Description", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Note", 200, HorizontalAlignment.Left);
            LsvItem.Columns.Add("Date Added", 200, HorizontalAlignment.Left);
            _itemController.display();

            _itemController.LsvPurchases = LsvPurchases;
            LsvPurchases.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Item", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Supplier", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Stocks", 143, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Category", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Brand", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Model", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Selling Price", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Wholesale Price", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Description", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Note", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Date Added", 200, HorizontalAlignment.Left);
            LsvPurchases.Columns.Add("Item Id", 143, HorizontalAlignment.Left);
            _itemController.displayStocks();

            _salesController.LsvSales = LsvSales;
            LsvSales.Columns.Add("Item", 200, HorizontalAlignment.Left);
            LsvSales.Columns.Add("Supplier", 200, HorizontalAlignment.Left);
            LsvSales.Columns.Add("Sold Item", 200, HorizontalAlignment.Left);
            LsvSales.Columns.Add("Brand", 143, HorizontalAlignment.Left);
            LsvSales.Columns.Add("Model", 200, HorizontalAlignment.Left);
            LsvSales.Columns.Add("Date Purchased", 200, HorizontalAlignment.Left);
            _salesController.display();

            _orderController.LsvOrderList = LsvOrders;
            LsvOrders.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvOrders.Columns.Add("Order Item", 200, HorizontalAlignment.Left);
            LsvOrders.Columns.Add("Additional Info", 300, HorizontalAlignment.Left);
            LsvOrders.Columns.Add("Customer Name", 143, HorizontalAlignment.Left);
            LsvOrders.Columns.Add("Status", 200, HorizontalAlignment.Left);
            LsvOrders.Columns.Add("Contact No", 143, HorizontalAlignment.Left);
            LsvOrders.Columns.Add("Downpayment", 143, HorizontalAlignment.Left);
            LsvOrders.Columns.Add("Order Price", 143, HorizontalAlignment.Left);
            LsvOrders.Columns.Add("Representative", 143, HorizontalAlignment.Left);
            LsvOrders.Columns.Add("Date Ordered", 200, HorizontalAlignment.Left);

            _repairController.LsvRepairList = LsvRepairs;
            LsvRepairs.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvRepairs.Columns.Add("Customer Name", 200, HorizontalAlignment.Left);
            LsvRepairs.Columns.Add("contact No", 143, HorizontalAlignment.Left);
            LsvRepairs.Columns.Add("Item", 300, HorizontalAlignment.Left);
            LsvRepairs.Columns.Add("Issue", 200, HorizontalAlignment.Left);
            LsvRepairs.Columns.Add("Status", 143, HorizontalAlignment.Left);
            LsvRepairs.Columns.Add("Imei", 143, HorizontalAlignment.Left);
            LsvRepairs.Columns.Add("Downpayment", 143, HorizontalAlignment.Left);
            LsvRepairs.Columns.Add("Repair Price", 143, HorizontalAlignment.Left);
            LsvRepairs.Columns.Add("Representative", 143, HorizontalAlignment.Left);
            LsvRepairs.Columns.Add("Date Added", 200, HorizontalAlignment.Left);


            _defectiveController.LsvDefective = LsvDefectiveItems;
            LsvDefectiveItems.Columns.Add("ID", 0, HorizontalAlignment.Left);
            LsvDefectiveItems.Columns.Add("Item", 200, HorizontalAlignment.Left);
            LsvDefectiveItems.Columns.Add("No of Defective Item", 200, HorizontalAlignment.Left);
            LsvDefectiveItems.Columns.Add("Item Issue", 300, HorizontalAlignment.Left);
            LsvDefectiveItems.Columns.Add("Supplier", 200, HorizontalAlignment.Left);
            LsvDefectiveItems.Columns.Add("Category", 200, HorizontalAlignment.Left);
            LsvDefectiveItems.Columns.Add("Brand", 200, HorizontalAlignment.Left);
            LsvDefectiveItems.Columns.Add("Model", 200, HorizontalAlignment.Left);
            LsvDefectiveItems.Columns.Add("Date Added", 200, HorizontalAlignment.Left);

            _defectiveController.display();

        }

        private void btnStocks_Click(object sender, EventArgs e)
        {
            new frmStocks(item_id, item_name, int.Parse(lblStocks.Text)).ShowDialog();
        }

        private void LsvItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem = LsvItem.Items[_userRowIndex];
            item_id = selItem.SubItems[0].Text;
            item_name = selItem.SubItems[1].Text;
            stocks = int.Parse(selItem.SubItems[3].Text);

            lblStocks.Text = stocks.ToString();

        }

        private void LsvItem_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex = e.ItemIndex;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new frmUpdateStocks(stockin_id, int.Parse(lblStocksAdded.Text), stocks_item_id).ShowDialog();
        }

        private void LsvPurchases_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndex1 = e.ItemIndex;
        }

        private void LsvPurchases_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItem1 = LsvPurchases.Items[_userRowIndex1];
            stockin_id = int.Parse(selItem1.SubItems[0].Text);
            item_stocks = int.Parse(selItem1.SubItems[3].Text);
            stocks_item_id = selItem1.SubItems[12].Text;
            lblStocksAdded.Text = item_stocks.ToString();
        }

        private void ctrInventory_Load(object sender, EventArgs e)
        {
            _itemController.countItem();
            _orderController.countItem();
            _repairController.countItem();
            _defectiveController.countItem();
            _supplierController.countItem();

            txtItem.Text = _itemController.Count.ToString();
            txtOrder.Text = _orderController.Count.ToString();
            txtDefective.Text = _defectiveController.Count.ToString();
            txtRepair.Text = _repairController.Count.ToString();
            txtSupplier.Text = _supplierController.Count.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;

            if (LsvOrders.SelectedItems.Count == 0)
            {
                XtraMessageBox.Show("Please Select Order",
                    "Nothing Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                dialogResult = XtraMessageBox.Show("Do you want to remove this order?",
                  "Updating...",
                  MessageBoxButtons.OKCancel,
                  MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)
                {
                    _orderController.OrderId = _orderController.OrderId;
                    _orderController.removedOrder();

                    XtraMessageBox.Show("Cancelled Ordered has been removed",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    cboFiltered.ResetText();_orderController.displayOrder();
                }
            }

          
        }

        private void LsvOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItemOrder = LsvOrders.Items[_userRowIndexOrder];
            _orderController.OrderId = int.Parse(selItemOrder.SubItems[0].Text);
            _orderController.Status = selItemOrder.SubItems[4].Text;
     
        }

        private void LsvOrders_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndexOrder = e.ItemIndex;

         
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFiltered.Text == "Ordered")
            {
                //LsvOrders.Clear();
                _orderController.search(cboFiltered.Text);
            }
            else if (cboFiltered.Text == "Pending")
            {
                //LsvOrders.Clear();
                _orderController.search(cboFiltered.Text);
            }

            if (cboFiltered.Text == "Cancelled")
            {


                LsvOrders.Clear();

                _orderController.LsvCancelled = LsvOrders;
                LsvOrders.Columns.Add("ID", 0, HorizontalAlignment.Left);
                LsvOrders.Columns.Add("Order Item", 200, HorizontalAlignment.Left);
                LsvOrders.Columns.Add("Additional Info", 300, HorizontalAlignment.Left);
                LsvOrders.Columns.Add("Customer Name", 143, HorizontalAlignment.Left);
                LsvOrders.Columns.Add("Status", 200, HorizontalAlignment.Left);
                LsvOrders.Columns.Add("Contact No", 143, HorizontalAlignment.Left);
                LsvOrders.Columns.Add("Downpayment", 143, HorizontalAlignment.Left);
                LsvOrders.Columns.Add("Order Price", 143, HorizontalAlignment.Left);
                LsvOrders.Columns.Add("Representative", 143, HorizontalAlignment.Left);
                LsvOrders.Columns.Add("Date Ordered", 200, HorizontalAlignment.Left);
                LsvOrders.Columns.Add("Cancelled By", 200, HorizontalAlignment.Left);

                _orderController.searchCancelled();
            }



            //cancelled = chkCancelled.Text;

            //if (chkCancelled.Checked)
            //{
            //    _orderController.search(ordered, pending, cancelled);
            //}
            //else if (!chkCancelled.Checked)
            //{
            //    cancelled = "";
            //    _orderController.search(ordered, pending, cancelled);
            //}
            //else
            //{
            //    cancelled = "";
            //    _orderController.displayOrder();
            //}
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cboFilteredRepair.Text == "Repaired")
            {
                _repairController.search(cboFilteredRepair.Text);
            }
            if (cboFilteredRepair.Text == "Pending")
            {
                _repairController.search(cboFilteredRepair.Text);
            }

            if (cboFilteredRepair.Text == "Cancelled")
            {
                LsvRepairs.Clear();

                _repairController.LsvCancelledRepair = LsvRepairs;
                LsvRepairs.Columns.Add("ID", 0, HorizontalAlignment.Left);
                LsvRepairs.Columns.Add("Customer Name", 200, HorizontalAlignment.Left);
                LsvRepairs.Columns.Add("contact No", 143, HorizontalAlignment.Left);
                LsvRepairs.Columns.Add("Item", 300, HorizontalAlignment.Left);
                LsvRepairs.Columns.Add("Issue", 200, HorizontalAlignment.Left);
                LsvRepairs.Columns.Add("Status", 143, HorizontalAlignment.Left);
                LsvRepairs.Columns.Add("Imei", 143, HorizontalAlignment.Left);
                LsvRepairs.Columns.Add("Downpayment", 143, HorizontalAlignment.Left);
                LsvRepairs.Columns.Add("Repair Price", 143, HorizontalAlignment.Left);
                LsvRepairs.Columns.Add("Representative", 143, HorizontalAlignment.Left);
                LsvRepairs.Columns.Add("Date Added", 200, HorizontalAlignment.Left);
                LsvRepairs.Columns.Add("Cancelled by", 200, HorizontalAlignment.Left);


                _repairController.searchCancelled();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;

            if (LsvRepairs.SelectedItems.Count == 0)
            {
                XtraMessageBox.Show("Please Select repair",
                    "Nothing Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                dialogResult = XtraMessageBox.Show("Do you want to remove this repair?",
                  "Updating...",
                  MessageBoxButtons.OKCancel,
                  MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)
                {
                    _repairController.RepairId = _repairController.RepairId;
                    _repairController.removed();

                    XtraMessageBox.Show("Repaire has been removed",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void LsvRepairs_SelectedIndexChanged(object sender, EventArgs e)
        {
            selItemRepairs = LsvRepairs.Items[_userRowIndexRepairs];
            _repairController.RepairId = int.Parse(selItemRepairs.SubItems[0].Text);
        }

        private void LsvRepairs_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _userRowIndexRepairs = e.ItemIndex;
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _itemController.search(bunifuTextBox1.Text);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
