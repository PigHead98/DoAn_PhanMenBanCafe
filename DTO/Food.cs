using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_LTWin.DTO
{
    public class Food
    {
        public Food(string foodName, int id,int idCat, float price = 0)
        {
            this.Name = foodName;
            this.ID = id;
            this.Price = price;
            this.IDCat = idCat;
        }
        public Food(DataRow row)
        {
            this.Name = row["Name"].ToString();
            this.ID = (int)row["id"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
            this.IDCat = (int)row["idCategory"];
        }

        private float price;

        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int iDCat;

        public int IDCat
        {
            get { return iDCat; }
            set { iDCat = value; }
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
