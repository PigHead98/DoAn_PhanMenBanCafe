using DoAn_LTWin.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_LTWin.DAO
{
    public class Bill_InfoDAO
    {
        private static Bill_InfoDAO instance;

        public static Bill_InfoDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Bill_InfoDAO();
                } return Bill_InfoDAO.instance;
            }
            private set { Bill_InfoDAO.instance = value; }
        }

        private Bill_InfoDAO() { }

        public List<Bill_Info> getBillInfoBy(int id)
        { 
            List<Bill_Info> listBill_Info = new List<Bill_Info>(); //tạo list lưu ds

            DataTable data = DataProvider.Instance.ExecuteQuery("select * from dbo.BillInfo where idBill = "+ id +""); //truy vấn lấy data

            foreach (DataRow item in data.Rows) 
            {
                Bill_Info info = new Bill_Info(item);
                listBill_Info.Add(info); //thêm vào list
            }

            return listBill_Info;
        }

        public void InsertBill_Info(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBill_Info @idBill , @idFood , @count ", new object[] { idBill, idFood, count });
        }

        public void DelBill_InfoByIdFood(int id){
            DataProvider.Instance.ExecuteQuery("delete from dbo.BillInfo where idFood = " + id);
        }

        public void DelBill_InfoByIdCat(int id){
            DataProvider.Instance.ExecuteQuery("delete from dbo.BillInfo where idFood in (select id from Food where idCategory = "+id+")");
        }
        public void DelBill_InfoByIdTable(int id)
        {
            DataProvider.Instance.ExecuteQuery("delete from dbo.BillInfo where idBill in (select id from dbo.Bill where idTable = "+id+")");
        }
    }
}
