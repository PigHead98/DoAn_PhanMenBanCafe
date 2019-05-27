using DoAn_LTWin.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_LTWin.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MenuDAO();
                } return MenuDAO.instance;
            }
            private set { MenuDAO.instance = value; }
        }

        private MenuDAO() { }

        public List<Menu> getListMenuUncheckOutById( int id)
        {
            List<Menu> listMenu = new List<Menu>();

            DataTable data = DataProvider.Instance.ExecuteQuery("select fo.name,fo.price,bi.count,(fo.price*bi.count) [totalPrice] from dbo.Food as fo,dbo.BillInfo as bi,dbo.Bill as b where fo.id = bi.idFood and b.id = bi.idBill and b.status = 0 and b.idTable = " + id + "");

            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }

            return listMenu;
        }
    
    }
}
