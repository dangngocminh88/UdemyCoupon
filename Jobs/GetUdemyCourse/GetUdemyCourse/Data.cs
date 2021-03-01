using GetUdemyCourse.Configs;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json;

namespace GetUdemyCourse
{
    public class Data : BaseDataAccess
    {
        public Data(string connectionString) : base(connectionString)
        {

        }

        public void Execute(List<string> udemyLinkList)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            string json = JsonSerializer.Serialize(udemyLinkList);
            parameters.Add(base.GetParameter("udemy", json));
            base.ExecuteScalar("abc", parameters);
        }
    }
}
