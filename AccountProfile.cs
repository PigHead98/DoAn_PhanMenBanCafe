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
    public partial class AccountProfile : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAcc(loginAccount); }
        }
        public AccountProfile(Account Acc)
        {
            InitializeComponent();
            this.LoginAccount = Acc;
        }

        void ChangeAcc(Account Acc)
        {
            txtDisplayName.Text = Acc.DisplayName;
            txtUsername.Text = Acc.UserName;
        }

        void UpdateAcc()
        {
            string DisplayName = txtDisplayName.Text;
            string UserName = txtUsername.Text;
            string PassWord = txtPassword.Text;
            string NewPass = txtNewPass.Text;
            string ReNewPass = txtReNewPass.Text;

            if (!NewPass.Equals(ReNewPass)) MessageBox.Show("Mật khẩu không trùng khớp");
            else
            {
                if(AccountDAO.Instance.UpdateAcc(DisplayName,UserName,PassWord,NewPass))
                {
                    MessageBox.Show("Cập nhật thành công");
                }
                else
                {
                    MessageBox.Show("kiểm tra lại mật khẩu");
                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAcc();
        }
    }
}
