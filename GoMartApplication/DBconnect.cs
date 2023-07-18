using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace GoMartApplication
{
    class DBconnect
    {
        private SqlConnection con=new SqlConnection(@"Data Source=.;Initial Catalog=GoMart;Integrated Security=True");
    }
}
