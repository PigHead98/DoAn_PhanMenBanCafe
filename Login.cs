using DoAn_LTWin.DAO;
using DoAn_LTWin.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_LTWin
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string Username = txtUsername.Text;
            string Password = txtPassword.Text;
            if (login(Username,Password))
            {
                Account loginAcc = AccountDAO.Instance.GetAccountByUserName(Username);
                TableManager f = new TableManager(loginAcc);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Kiểm tra Username & Password");

        }

        bool login(string userName, string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn thật sự muốn thoát?","Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
