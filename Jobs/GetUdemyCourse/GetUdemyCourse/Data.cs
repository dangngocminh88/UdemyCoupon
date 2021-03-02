using GetUdemyCourse.Configs;
using GetUdemyCourse.Models;
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

        public void Execute(List<Course> udemyLinkList)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            string json = JsonSerializer.Serialize(udemyLinkList);
            parameters.Add(base.GetParameter("@json", json));
            base.ExecuteScalar("job_InsertCourse", parameters);
        }
    }
}
