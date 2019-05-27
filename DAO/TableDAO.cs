using DoAn_LTWin.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_LTWin.DAO
{
    class TableDAO
    {
        public static int widthTable = 90;
        public static int heightTable = 90;

        private static TableDAO instance;

        public static TableDAO Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new TableDAO();
                }
                return TableDAO.instance;
            }
            private set { TableDAO.instance = value; }
        }

        private TableDAO() {}

        public List<Table> LoadTableList()
        {
            List<Table> TableList = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuteQuery("exec USP_GetTableList");

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                TableList.Add(table);
            }

            return TableList;
        }

        public void switchTable(int idFirstTable, int idNextTable)
        {
            DataProvider.Instance.ExecuteQuery("exec USP_SwitchTable @idFirstTable , @idNextTable", new object[] { idFirstTable, idNextTable });
        }

        public bool insertTable(string name,string status)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("insert dbo.TableFood (name,status) values(N'" + name + "',N'" + status + "')");
            return result > 0;
        }

        public bool updateTable(int id, string name,string status)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("update dbo.TableFood set name = N'" + name + "',status = N'" + status + "' where id = " + id);
            return result > 0;
        }

        public bool delTable(int id)
        {
            //BillDAO.Instance.DellBillByIdTable(id);
            int result = DataProvider.Instance.ExecuteNonQuery("delete dbo.TableFood where id = " + id);
            return result > 0;
        }

    }
}
