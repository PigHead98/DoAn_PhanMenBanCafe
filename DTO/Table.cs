using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_LTWin.DTO
{
    class Table
    {


        public Table(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.status = row["status"].ToString(); 
        }

        public Table(int id, string name, string status) //đưa giá trị vào
        {
            this.ID = id;
            this.Name = name;
            this.Status = Status;
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
