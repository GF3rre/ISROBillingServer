using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBillingServer.Model
{
    internal class BillingDB
    {
        private static BillingDB m_instance = null;
        private SqlConnection cnn;

        public static BillingDB Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new BillingDB();
                }

                return m_instance;
            }
        }

        public BillingData GetBillingData(string Channel,string UserID, string Pwd)
        {



            SqlCommand cmd = new SqlCommand("EXEC Billing_Login @chn,@user,@pw", cnn);
            cmd.Parameters.AddWithValue("@chn", Channel);
            cmd.Parameters.AddWithValue("@user", UserID);
            cmd.Parameters.AddWithValue("@pw", Pwd);



                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    reader.Read();


                    return new BillingData(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[1]), reader[2].ToString(), Convert.ToInt32(reader[3]), DateTime.Parse(reader[4].ToString()));
                }
        }
        public string UpdateLockPw(string JID, string Email,string LockPW)
        {

            SqlCommand cmd = new SqlCommand("EXEC Update_ItemLock @jid,@mail,@pw", cnn);
            cmd.Parameters.AddWithValue("@jid", JID);
            cmd.Parameters.AddWithValue("@mail", Email);
            cmd.Parameters.AddWithValue("@pw", LockPW);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                reader.Read();


                return reader[0].ToString();
            }
        }
        private BillingDB()
        {
            DatabaseConfiguration dbcfg = new DatabaseConfiguration("Settings/config.ini");

            string connectionString = "Data Source=" + dbcfg.Host + ";Initial Catalog=" + dbcfg.Database + ";User ID=" + dbcfg.Username + ";Password=" + dbcfg.Password;
            cnn = new SqlConnection(connectionString);

            try
            {
                cnn.Open();
                Console.WriteLine("Connection to DB successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Could not connect to database");
                throw ex;
            }
        }
        public void Init()
        {
            // We do nothing here, the singleton will do all the work
            // We just need a func to call
        }
    }
}
