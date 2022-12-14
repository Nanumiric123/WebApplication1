using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    public class getIbusinessData
    {
        public static string constr = @"Data Source=172.16.206.20;Initial Catalog=IBusiness;User ID=Data_writer;Password=Pasmy@2791381230";

        SqlConnection cnn = new SqlConnection(constr);

        SqlParameter p = new SqlParameter();
        public string get_part_description(string material)
        {
            string description = "";
            SqlDataAdapter adapter = new SqlDataAdapter("select * from [IBusiness].[dbo].[SAP_ITEM] where [MATERIAL] = @mat", cnn);
            p.ParameterName = "@mat";
            p.Value = material;

            adapter.SelectCommand.Parameters.Add(p);
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                cnn.Open();
                adapter.Fill(dt);
                cnn.Close();
                if (dt.Rows.Count > 0)
                {
                    description = dt.Rows[0][1].ToString();
                }
                adapter.SelectCommand.Parameters.Clear();

            }
            catch (SqlException ex)
            {
                description = ex.Message.ToString();
            }



            return description;
        }

        public string get_part_vendor_name(string material)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT DISTINCT [MATERIAL],[VENDOR_NAME] FROM [IBusiness].[dbo].[SAP_GR] WHERE [MATERIAL] = @mat", cnn);
            string vendorname = "";

            p.ParameterName = "@mat";
            p.Value = material;
            adapter.SelectCommand.Parameters.Add(p);
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                cnn.Open();
                adapter.Fill(dt);
                cnn.Close();
                if (dt.Rows.Count > 0)
                {
                    vendorname = dt.Rows[0][1].ToString();
                }

            }
            catch (SqlException ex)
            {
                vendorname = ex.Message.ToString();
            }
            return vendorname;
        }

    }
}