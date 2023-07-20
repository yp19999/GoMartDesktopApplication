using System.Data.SqlClient;

namespace GoMartApplication
{
    class DBconnect
    {
       private SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=GoMart;Integrated Security=True");
    }
}
