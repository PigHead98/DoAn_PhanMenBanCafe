using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_LTWin.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int status, int discount = 0 ,float totalprice = 0)
        {
            this.ID = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
            this.Discount = discount;
            this.TotalPrice = totalprice;
        }
        public Bill(DataRow row) // lấy từng dòng
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["dateCheckIn"];

            var Check_dateCheckOut = row["dateCheckOut"];
            if (Check_dateCheckOut.ToString() != "")
                this.DateCheckOut = (DateTime?)Check_dateCheckOut;

            this.Status = (int)row["status"];

            if (row["discount"].ToString() != "")
            this.Discount = (int)row["discount"];

            this.TotalPrice = (float)Convert.ToDouble(row["totalPrice"].ToString());

        }

        private float totalPrice;

        public float TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }


        private int discount;

        public int Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private DateTime? dateCheckOut; // "?" để có thể null vì datetime không cho null

        public DateTime? DateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }

        private DateTime? dateCheckIn;

        public DateTime? DateCheckIn
        {
            get { return dateCheckIn; }
            set { dateCheckIn = value; }
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
