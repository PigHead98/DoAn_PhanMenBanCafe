using DoAn_LTWin.DAO;
using DoAn_LTWin.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_LTWin
{
    public partial class TableManager : Form
    {
        public float TongTien = 0;

        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAcc(loginAccount.Type); }
        }
        public TableManager(Account Acc)
        {
            InitializeComponent();
            //if (Type == 1) truyền type
            //    adminToolStripMenuItem.Enabled = true;
            this.LoginAccount = Acc;
            LoadTable();
            LoadCat();

        }


        #region hàm show/add/edit/del

        void ChangeAcc(int Type)
        {
            if (Type != 0)
            {
            adminToolStripMenuItem.Enabled = false;                
            }
 // type = 1 => k phải admin => k cho truy cập
            
        }
        void LoadCat()
        {
            List<Category> listCat = CategoryDAO.Instance.LoadCategory();
            cbCategory.DataSource = listCat;
            cbCategory.DisplayMember = "name";
            cbCategory.ValueMember = "id";
            //LoadFoodByCatId((int)cbCategory.SelectedValue);
        }

        void LoadFoodByCatId(int id)
        {
            List<Food> listFood = FoodDAO.Instance.loadListFoodByIdCat(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "name";
            cbFood.ValueMember = "id";
        }

        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> listTable = TableDAO.Instance.LoadTableList();

            cbSwitchTable.DataSource = listTable;
            cbSwitchTable.DisplayMember = "name";
            cbSwitchTable.ValueMember = "id";

            foreach (Table item in listTable)
            {
                Button Ban = new Button()
                {
                    Width = TableDAO.widthTable,
                    Height = TableDAO.heightTable
                };
                
                Ban.Text = item.Name + "\n" + item.Status; //tên button bàn
                Ban.Click += Ban_Click; //tạo nút nhấn
                Ban.Tag = item; //lấy được dữ liệu trong item
                switch (item.Status) // status = 0 => bàn trống => màu của bàn
                {
                    case "Trống": Ban.BackColor = Color.DarkGray; break;
                    default:  Ban.BackColor = Color.White; break;
                    
                }
                flpTable.Controls.Add(Ban); //hiển thị các btn

            }

        }

        void showBill(int id)
        {
            float TotalPrice = 0;
            lsvBill.Items.Clear();
            List<DoAn_LTWin.DTO.Menu> ListBillInfo = MenuDAO.Instance.getListMenuUncheckOutById(id);

            foreach (DoAn_LTWin.DTO.Menu item in ListBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());


                TotalPrice += item.TotalPrice;

                lsvBill.Items.Add(lsvItem);
            }

            CultureInfo culture = new CultureInfo("vi-VN");
            //System.Threading.Thread.CurrentThread.CurrentCulture = culture; //chỉ ảnh hưởng trong chương trình
            
            txtTotalPrice.Text = TotalPrice.ToString("c", culture); //đơn vị tiền 
            TongTien = TotalPrice;

        }

        #endregion

        #region Nút nhấn/CBbox
        void Ban_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID; //cùng với ban.tag ở trên lấy dữ liệu bàn
            lsvBill.Tag = (sender as Button).Tag; // tag dữ liệu của bàn để lấy
            showBill(tableID);

            
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountProfile f = new AccountProfile(LoginAccount);
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin f = new Admin();
            f.ShowDialog();
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            
        }


        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb == null) return;

            Category selected = cb.SelectedItem as Category;
            id = selected.ID;

            LoadFoodByCatId(id);

        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            try
            {
                int idBill = BillDAO.Instance.getUnCheckOutBill(table.ID); //hàm trả về idbill
                int idFood = (cbFood.SelectedItem as Food).ID;
                int countFood = (int)nmFoodCount.Value;

                if (idBill == -1)
                {
                    BillDAO.Instance.InsertBill(table.ID);
                    Bill_InfoDAO.Instance.InsertBill_Info(BillDAO.Instance.getMaxIdBill(), idFood, countFood);

                }
                else
                {
                    Bill_InfoDAO.Instance.InsertBill_Info(idBill, idFood, countFood); //khi idbill tồn tại
                }


                LoadTable();
            }
            catch
            {
                MessageBox.Show("Bạn chưa chọn bàn");
            }
            showBill(table.ID); //reload lại
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table; // lay  table

            int idBill = BillDAO.Instance.getUnCheckOutBill(table.ID);
            int discount = (int)nmDiscount.Value;

            double totalPri = Convert.ToDouble(TongTien);
            double finalPri = totalPri - (totalPri / 100) * discount;


            if (idBill != -1)
            {
                if(MessageBox.Show(string.Format("Bạn muốn thanh toán bàn {0}\n Tổng tiền sau giảm giá = {1} - ({1}/100) * {2} = {3} ",table.Name,totalPri,discount,finalPri),"thanh toán",MessageBoxButtons.OKCancel)== System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, (float)finalPri); 
                    showBill(table.ID); //reload lại
                    LoadTable();
                }
            }


            
        }
        private void btnSwitchTable_Click(object sender, EventArgs e)
        {

            Table Ftable = lsvBill.Tag as Table;
            Table NextTable = cbSwitchTable.SelectedItem as Table;

            if (MessageBox.Show(string.Format("Bạn muốn chuyển bàn {0} sang bàn {1}?", Ftable.Name, NextTable.Name), "Chuyển bàn", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.switchTable(Ftable.ID, NextTable.ID);
                LoadTable();
            }

        }

        #endregion

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        




    }
}
