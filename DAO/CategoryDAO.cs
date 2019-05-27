using DoAn_LTWin.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_LTWin.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                } return CategoryDAO.instance;
            }
            private set { CategoryDAO.instance = value; }
        }

        private CategoryDAO() { }

        public List<Category> LoadCategory()
        {
            List<Category> listCat = new List<Category>();

            DataTable data = DataProvider.Instance.ExecuteQuery("exec USP_GetCat");

            foreach (DataRow item in data.Rows)
            {
                Category cat = new Category(item);
                listCat.Add(cat);
            }

            return listCat;
        }

        public Category GetCatById(int id)
        {
            Category Cat = null;

            DataTable data = DataProvider.Instance.ExecuteQuery("select * from FoodCategory where id = " + id );

            foreach (DataRow item in data.Rows)
            {
                Cat = new Category(item);
                return Cat;
            }

            return Cat;

        }

        public bool insertCat(string name)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("insert dbo.FoodCategory (name) values(N'" + name + "')");
            return result > 0;
        }

        public bool updateCat(int id, string name)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("update dbo.FoodCategory set name = N'" + name + "' where id = " + id);
            return result > 0;
        }

        public bool delCat(int id)
        {
            FoodDAO.Instance.DelFoodIdCat(id);
            int result = DataProvider.Instance.ExecuteNonQuery("delete dbo.FoodCategory where id = " + id);
            return result > 0;
        }

        public List<Category> searchListCat(string Name)
        {
            List<Category> listCat = new List<Category>();

            DataTable data = DataProvider.Instance.ExecuteQuery("select * from dbo.FoodCategory where name like N'%" + Name + "%'");

            foreach (DataRow item in data.Rows)
            {
                Category cat = new Category(item);
                listCat.Add(cat);
            }

            return listCat;
        }
    }
}
