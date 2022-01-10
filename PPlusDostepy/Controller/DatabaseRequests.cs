using System.Data;
using System.Data.SqlClient;

namespace PPlusDostepy.Controller
{
    public static class DatabaseRequests
    {
        static readonly string ConnectionSQL = @"Server=jakisserver;user=jakislogin;password=jakieshaslo;database=jakasbaza";
        readonly static SqlConnection connect = new SqlConnection(ConnectionSQL);

        public static List<Model.Company> GetCompanies()
        {
            string query = @"SELECT Knt_Id, Knt_Kod, Knt_Nazwa1 FROM dbo.Kontrahenci";
            List<Model.Company> companies = new List<Model.Company>();
            try
            {
                connect.Open();

                SqlDataReader reader;
                SqlCommand mySqlCommand = new SqlCommand(query, connect);


                reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Model.Company company = new Model.Company()
                    {
                        Id = Convert.ToInt32(reader["Knt_Id"]),
                        CompanyName = reader["Knt_Kod"].ToString(),
                        CompanyDescription = reader["Knt_Nazwa1"].ToString()
                    };
                    companies.Add(company);
                }
                mySqlCommand.Dispose();
                reader.Dispose();

                return companies;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Błąd podczas pobierania danych pracownika!\n" + ex.Message);
                return new List<Model.Company>();
            }
            finally
            {
                connect.Close();
            }
        }
        public static List<Model.Access> GetAccesses(int companyId)
        {
            string query = $@"SELECT Dtp_Id, Dtp_Tytul, Dtp_Opis FROM dbo.Dostepy WHERE DTP_IdKontrahenta = {companyId}";
            List<Model.Access> accesses = new List<Model.Access>();
            try
            {
                connect.Open();

                SqlDataReader reader;
                SqlCommand mySqlCommand = new SqlCommand(query, connect);


                reader = mySqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Model.Access access = new Model.Access()
                    {
                        Id = Convert.ToInt32(reader["Dtp_Id"]),
                        Name = reader["Dtp_Tytul"].ToString(),
                        Description = reader["Dtp_Opis"].ToString()
                    };
                    accesses.Add(access);
                }
                mySqlCommand.Dispose();
                reader.Dispose();

                return accesses;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Błąd podczas pobierania danych pracownika!\n" + ex.Message);
                return new List<Model.Access>();
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
