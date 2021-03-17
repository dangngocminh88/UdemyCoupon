using Jobs.Configs;
using Jobs.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json;

namespace Jobs
{
    public class Data : BaseDataAccess
    {
        public Data(string connectionString) : base(connectionString)
        {

        }

        public void InsertCourse(List<Course> udemyLinkList)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            string json = JsonSerializer.Serialize(udemyLinkList);
            parameters.Add(base.GetParameter("@json", json));
            base.ExecuteScalar("job_InsertCourse", parameters);
        }

        public void DeleteOutdatedCourse()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            base.ExecuteScalar("job_DeleteOutdatedCourse", parameters);
        }
    }
}
