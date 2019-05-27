using DoAn_LTWin.DAO;
using DoAn_LTWin.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_LTWin
{
    public partial class Admin : Form
    {
        BindingSource foodClickBinding = new BindingSource();
        BindingSource AccClickBinding = new BindingSource();

        public Admin()
        {
            InitializeComponent();
            Load();
        }
        void Load()
        {
            dgvFood.DataSource = foodClickBinding;
            dgvUser.DataSource = AccClickBinding;

            loadFood();
            loadUser();
            loadTable();
            loadCategory();
            loadBill(dtpFromDate.Value,dtpToDate.Value);
            foodBinding();
            tableBinding();
            CatBinding();
            AccBinding();
        }

        void load_DtpBill()
        {   //mặc định đầu và cuối tháng
            DateTime toDay = DateTime.Now;
            dtpFromDate.Value = new DateTime(toDay.Year, toDay.Month, 1);
            dtpToDate.Value = dtpFromDate.Value.AddMonths(1).AddDays(-1); //ngày đầu tháng + 1 tháng - 1 ngày = ngày cuối tháng

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        void loadUser()
        {

            AccClickBinding.DataSource = AccountDAO.Instance.GetListAcc();


        }
        void AccBinding()
        {
            txtUsername.DataBindings.Add(new Binding("text", dgvUser.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txtDisplayName.DataBindings.Add(new Binding("text", dgvUser.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nmType.DataBindings.Add(new Binding("Value", dgvUser.DataSource, "Type", true, DataSourceUpdateMode.Never));

        }
        void loadFood()
        {
            cbFoodCategory.DataSource = CategoryDAO.Instance.LoadCategory();
            cbFoodCategory.ValueMember = "name";
            foodClickBinding.DataSource = FoodDAO.Instance.loadListFood();
        }
       
        void foodBinding()
        {
            txtFoodname.DataBindings.Add(new Binding("text", dgvFood.DataSource, "name",true, DataSourceUpdateMode.Never));
            txtIDFood.DataBindings.Add(new Binding("text", dgvFood.DataSource, "id",true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("value", dgvFood.DataSource, "price",true, DataSourceUpdateMode.Never));
        }
        void loadTable()
        {
            cbTableStatus.DataSource = TableDAO.Instance.LoadTableList();
            cbTableStatus.DisplayMember = "status";
            dgvTable.DataSource = TableDAO.Instance.LoadTableList();
        }
        void tableBinding()
        {
            txtTableID.DataBindings.Add(new Binding("text", dgvTable.DataSource, "id", true, DataSourceUpdateMode.Never));
            txtTableName.DataBindings.Add(new Binding("text", dgvTable.DataSource, "name", true, DataSourceUpdateMode.Never));
            cbTableStatus.DataBindings.Add(new Binding("text", dgvTable.DataSource, "status", true, DataSourceUpdateMode.Never));
        }
        void loadCategory()
        {
            
            dgvCategory.DataSource = CategoryDAO.Instance.LoadCategory();
        }
        void CatBinding()
        {
            txtCatName.DataBindings.Add(new Binding("text", dgvCategory.DataSource, "name", true, DataSourceUpdateMode.Never));
            txtCatID.DataBindings.Add(new Binding("text", dgvCategory.DataSource, "id", true, DataSourceUpdateMode.Never));

        }
        void loadBill(DateTime DateIn, DateTime DateOut)
        {
            dgvBill.DataSource = BillDAO.Instance.GetlistBillByDate(DateIn, DateOut);
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            DateTime DateIn = dtpFromDate.Value;
            DateTime DateOut = dtpToDate.Value;
            loadBill(DateIn,DateOut);
        }

        private void dgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            loadFood();

        }

        private void txtIDFood_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dgvFood.SelectedCells[0].OwningRow.Cells["idCat"].Value; //idCat tìm trong giao diện admin/thức ăn

                    Category Cat = CategoryDAO.Instance.GetCatById(id);
                    cbFoodCategory.SelectedItem = Cat;

                    int Index = -1;
                    int i = 0;
                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.ID == Cat.ID)
                        {
                            Index = i;
                            break;
                        }
                        i++;
                    }
                    cbFoodCategory.SelectedIndex = Index;
                }
            }
            catch 
            {
            }
            
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txtFoodname.Text;
            int idCat = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.insertFood(name, idCat, price))
            {
                MessageBox.Show("thành công");
                loadFood();
            }
            else
                MessageBox.Show("thất bại");
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDFood.Text);
            string name = txtFoodname.Text;
            int idCat = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.updateFood(id,name, idCat, price))
            {
                MessageBox.Show("thành công");
                loadFood();
            }
            else
                MessageBox.Show("thất bại");
        }

        private void btnDelFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDFood.Text);

            if (FoodDAO.Instance.delFood(id))
            {
                MessageBox.Show("thành công");
                loadFood();
            }
            else
                MessageBox.Show("thất bại");
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            string Name = txtSearchFood.Text;
            foodClickBinding.DataSource = SearchFoodByName(Name);
        }
        
        List<Food> SearchFoodByName(string Name)
        {
            List<Food> listFood = FoodDAO.Instance.searchListFood(Name);
            return listFood;
        }

        private void txtCatID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            string name = txtCatName.Text;
           

            if (CategoryDAO.Instance.insertCat(name))
            {
                MessageBox.Show("thành công");
                loadCategory();
            }
            else
                MessageBox.Show("thất bại");
        }

        private void btnDelCat_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCatID.Text);


            if (CategoryDAO.Instance.delCat(id))
            {
                MessageBox.Show("thành công");
                loadCategory();
            }
            else
                MessageBox.Show("thất bại");
        }

        private void btnEditCat_Click(object sender, EventArgs e)
        {
            string name = txtCatName.Text;
            int id = Convert.ToInt32(txtCatID.Text);

            if (CategoryDAO.Instance.updateCat(id,name))
            {
                MessageBox.Show("thành công");
                loadCategory();
            }
            else
                MessageBox.Show("thất bại");
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txtTableName.Text;
            string status = (cbTableStatus.SelectedItem as Table).Status;

            if (TableDAO.Instance.insertTable(name,status))
            {
                MessageBox.Show("thành công");
                loadTable();
            }
            else
                MessageBox.Show("thất bại");
        }

        private void btnDelTable_Click(object sender, EventArgs e)
        {
            try
            {
            int id = Convert.ToInt32(txtTableID.Text);
            if (TableDAO.Instance.delTable(id))
            {
                MessageBox.Show("thành công");
                loadTable();
            }
            else
                MessageBox.Show("thất bại");
            }
            catch
            {
                MessageBox.Show("Bàn còn chứa bill không thể xóa");
            }

        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtTableID.Text);
            string name = txtTableName.Text;
            string status = (cbTableStatus.SelectedItem as Table).Status;

            if (TableDAO.Instance.updateTable(id,name,status))
            {
                MessageBox.Show("thành công");
                loadTable();
            }
            else
                MessageBox.Show("thất bại");
        }

        private void btnShowUser_Click(object sender, EventArgs e)
        {
            loadUser();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            string UserName = txtUsername.Text;
            string Display = txtDisplayName.Text;
            int Type = (int)nmType.Value;
            if (AccountDAO.Instance.insertAcc(UserName,Display,Type))
            {
                MessageBox.Show("thành công");
                loadUser();
            }
            else
                MessageBox.Show("thất bại");
        }

        private void btnDelUser_Click(object sender, EventArgs e)
        {
            string UserName = txtUsername.Text;


            if (AccountDAO.Instance.delAcc(UserName))
            {
                MessageBox.Show("thành công");
                loadUser();
            }
            else
                MessageBox.Show("thất bại");
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            string UserName = txtUsername.Text;
            string Display = txtDisplayName.Text;
            int Type = (int)nmType.Value;
             if (AccountDAO.Instance.updateAccount(UserName, Display, Type))
            {
                MessageBox.Show("thành công");
                loadUser();
            }
            else
                MessageBox.Show("thất bại");
        }




    }
}
