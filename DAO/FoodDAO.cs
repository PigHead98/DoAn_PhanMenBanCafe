using DoAn_LTWin.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_LTWin.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new FoodDAO();
                }
                return FoodDAO.instance; 
            }
            private set { FoodDAO.instance = value; }
        }

        private FoodDAO() { }

        public List<Food> loadListFoodByIdCat(int id)
        {
            List<Food> listFood = new List<Food>();

            DataTable data = DataProvider.Instance.ExecuteQuery("select * from dbo.Food where idCategory = "+id+"");

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }

            return listFood;
        }

        public List<Food> loadListFood()
        {
            List<Food> listFood = new List<Food>();

            DataTable data = DataProvider.Instance.ExecuteQuery("select * from dbo.Food");

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }

            return listFood;
        }

        public bool insertFood(string name, int idCat,float price)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("insert dbo.Food (name,idCategory,price) values(N'"+name+"',"+idCat+","+price+")");
            return result > 0;
        }

        public bool updateFood(int id,string name, int idCat, float price)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("update dbo.Food set name = N'" + name + "',idCategory = " + idCat + " ,price = " + price + " where id = "+id);
            return result > 0;
        }

        public bool delFood(int id)
        {
            Bill_InfoDAO.Instance.DelBill_InfoByIdFood(id);
            int result = DataProvider.Instance.ExecuteNonQuery("delete dbo.Food where id = "+id);
            return result > 0;
        }
        public void DelFoodIdCat(int id)
        {
            Bill_InfoDAO.Instance.DelBill_InfoByIdCat(id);
            DataProvider.Instance.ExecuteQuery("delete  Food where idCategory = " + id);
        }
        public List<Food> searchListFood(string Name)
        {
            List<Food> listFood = new List<Food>();

            DataTable data = DataProvider.Instance.ExecuteQuery("select * from dbo.Food where name like N'%"+Name+"%' or price like N'%"+Name+"%'");

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }

            return listFood;
        }
    }
}
