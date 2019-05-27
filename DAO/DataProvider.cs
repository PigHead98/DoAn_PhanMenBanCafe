using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_LTWin.DAO
{
    class DataProvider
    {
        private static DataProvider instance;//ctrl + R + E

        internal static DataProvider Instance
        {
            get {
                if(instance == null) instance = new DataProvider(); 
                return  DataProvider.instance; 
            }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { 
        
        }

        private string conStr = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

        public DataTable ExecuteQuery(string query, object[] parameter = null)//lay duoc nhieu tham so truyen vao
        {
            DataTable data = new DataTable();

            using(SqlConnection con = new SqlConnection(conStr))
            { 

                con.Open();

                SqlCommand command = new SqlCommand(query, con);
                
                if(parameter != null){
                    string[] listPara = query.Split(' ');//tach theo khoang trang
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))//@ nam trong cau lenh sql
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                con.Close();

            }
            return data;
        }


        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection con = new SqlConnection(conStr))
            {

                con.Open();

                SqlCommand command = new SqlCommand(query, con);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                con.Close();

            }
            return data;
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;

            using (SqlConnection con = new SqlConnection(conStr))
            {

                con.Open();

                SqlCommand command = new SqlCommand(query, con);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                con.Close();

            }
            return data;
        }



    }//dong class DataProvide
}//dong namespace DoAn_LTWin.DAO
