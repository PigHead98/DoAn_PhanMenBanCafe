using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_LTWin.DTO
{
    public class Account
    {
        public Account(string UserName, string DisplayName, string PassWord, int Type)
        {
            this.DisplayName = DisplayName;
            this.PassWord = PassWord;
            this.UserName = UserName;
            this.Type = Type;
        }

        public Account(DataRow row)
        {
            this.DisplayName = row["DisplayName"].ToString();
            this.PassWord = row["PassWord"].ToString();
            this.UserName = row["UserName"].ToString();
            this.Type = (int)row["Type"];
        }

        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }

        private string displayName;

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }


    }
}
