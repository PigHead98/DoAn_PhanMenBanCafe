using DoAn_LTWin.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_LTWin.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return AccountDAO.instance; 
            }
            private set { AccountDAO.instance = value; }
        }

        private AccountDAO() { }

        public bool Login(string user,string pass)
        {
            string query = "exec USP_Login @useName , @passWord "; //lưu ý khải có khoảng trắng khi ","
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { user, pass });
            return result.Rows.Count == 1;//kiểm tra khi có lỗi ( > 0)
        }

        public bool UpdateAcc (string DisplayName,string UserName,string PassWord,string NewPass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_UpdateUser @UserName , @DisplayName , @PassWord , @NewPass", new object[] { UserName, DisplayName, PassWord, NewPass });
            return result > 0;
        }
        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from dbo.Account where UserName = '" + userName + "'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        public DataTable GetListAcc()
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select UserName, DisplayName, Type from dbo.Account");
            return data;
        }

        public bool insertAcc(string name,string display , int type)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("insert dbo.Account (UserName,DisplayName,PassWord,Type) values(N'" + name + "',N'" + display + "',N'" + 1 + "'," + type + ")");
            return result > 0;
        }

        public bool updateAccount(string name, string display, int type)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("update dbo.Account set DisplayName = N'" + display + "',Type = " + type + " where UserName = N'" + name + "'");
            return result > 0;
        }

        public bool delAcc(string UserName)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("delete dbo.Account where UserName = N'" + UserName + "'");
            return result > 0;
        }
    }
}
