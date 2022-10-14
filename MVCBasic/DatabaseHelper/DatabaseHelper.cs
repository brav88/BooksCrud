using MVCBasic.Models;
using System.Data;
using System.Data.SqlClient;

namespace MVCBasic.DatabaseHelper
{
    public class DatabaseHelper
    {
        const string servidor = @"localhost";
        const string baseDatos = "MVCBasic";
        const string strConexion = "Data Source=" + servidor + ";Initial Catalog=" + baseDatos + ";Integrated Security=True";


        public static List<Book> GetBooks()
        {
            DataTable ds = ExecuteStoreProcedure("GetBooks", null);

            List<Book> books = new List<Book>();

            foreach (DataRow dr in ds.Rows)
            {
                books.Add(new Book()
                {
                    Isbn = Convert.ToInt64(dr["isbn"]),
                    Author = dr["author"].ToString(),
                    Date = Convert.ToDateTime(dr["date"]),
                    Photo = dr["photo"].ToString(),
                    Title = dr["title"].ToString()
                });
            }

            return books;
        }

        public static bool CreateBook(Book book)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>()
                {
                    new SqlParameter("@isbn", book.Isbn),
                    new SqlParameter("@title", book.Title),
                    new SqlParameter("@author", book.Author),
                    new SqlParameter("@date", book.Date),
                    new SqlParameter("@photo", book.Photo)
                };

                ExecStoreProcedure("CreateBook", param);

                return true;
            }
            catch
            {
                return false;
            }           
        }

        //Para select 
        public static DataTable ExecuteStoreProcedure(string procedure, List<SqlParameter> param)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConexion))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = procedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    if (param != null)
                    {
                        foreach (SqlParameter item in param)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }

                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ExecStoreProcedure(string procedure, List<SqlParameter> param)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConexion))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = procedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;

                    if (param != null)
                    {
                        foreach (SqlParameter item in param)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
