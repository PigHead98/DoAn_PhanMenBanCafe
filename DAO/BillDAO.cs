using DoAn_LTWin.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_LTWin.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }

        private BillDAO() { }

        /// <summary>
        /// thành công => bill Id
        /// thất bại => -1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int getUnCheckOutBill(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from dbo.Bill where idTable = "+ id +" and status = 0");//uncheck thì status = 0
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]); //lấy id bill
                return bill.ID;
            }
            return -1;
        }


        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBill @idtable", new object[] { id });
        }

        public void CheckOut(int id,int discout, float totalPrice)
        {
            DataProvider.Instance.ExecuteNonQuery("update dbo.Bill set status = 1 , DateCheckOut = GETDATE() , discount = " + discout + ", totalPrice = " + totalPrice + " where id = " + id);
        }

        public DataTable GetlistBillByDate(DateTime? DateIn, DateTime? DateOut)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetBillByDate @DateIn , @DateOut", new object[] { DateIn, DateOut });

            return data;
        }

        public int getMaxIdBill()
        {
            try{
            return (int)DataProvider.Instance.ExecuteScalar("select max(id) from dbo.Bill");
            }
            catch{
                return 1;
            }

        }
        public void DellBillByIdTable(int id)
        {
            Bill_InfoDAO.Instance.DelBill_InfoByIdTable(id);
            DataProvider.Instance.ExecuteQuery("delete from dbo.Bill where idTable = " + id);
        }
    }
}
