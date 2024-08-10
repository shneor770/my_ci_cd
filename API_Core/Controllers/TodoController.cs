using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace API_Core.Controllers
{
    [ApiController]
    public class TodoController : ControllerBase
    {
        private IConfiguration _config;

        public TodoController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("add_task")]
        public JsonResult add_task([FromForm] string task)
        {
            string query = "insert into todo values(@task)";
            DataTable dataTable = new DataTable();
            string dataSource = _config.GetConnectionString("TazizOti");
            SqlDataReader reader;
            using (SqlConnection con = new SqlConnection(dataSource))
            {
                con.Open();
                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    comm.Parameters.AddWithValue("@task", task);
                    reader = comm.ExecuteReader();
                    dataTable.Load(reader);
                }
            }
            return new JsonResult("נוסף בהצלחה");
        }

        [HttpGet("get_tasks")]
        public JsonResult get_tasks()
        {
            string query = "SELECT * FROM todo";
            DataTable dataTable = new DataTable();
            string dataSource = _config.GetConnectionString("TazizOti");
            SqlDataReader reader;
            using (SqlConnection con = new SqlConnection(dataSource))
            {
                con.Open();
                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    reader = comm.ExecuteReader();
                    dataTable.Load(reader);
                }
            }
            return new JsonResult(dataTable);
        }



        [HttpPut("edit_task")]
        public JsonResult edit_task([FromForm] int id, string task)
        {
            string query = "update todo set task = @task where id = @id";
            DataTable dataTable = new DataTable();
            string dataSource = _config.GetConnectionString("TazizOti");
            SqlDataReader reader;
            using (SqlConnection con = new SqlConnection(dataSource))
            {
                con.Open();
                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    comm.Parameters.AddWithValue("id", id);
                    comm.Parameters.AddWithValue("task", task);
                    reader = comm.ExecuteReader();
                    dataTable.Load(reader);
                }
            }
            return new JsonResult($"משימה מספר {id} עודכנה בהצלחה");
        }


        [HttpDelete("delete_task")]
        public JsonResult delete_task(string id)
        {
            string query = "delete from todo where id = @id";
            DataTable dataTable = new DataTable();
            string dataSource = _config.GetConnectionString("TazizOti");
            SqlDataReader reader;
            using (SqlConnection con = new SqlConnection(dataSource))
            {
                con.Open();
                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    comm.Parameters.AddWithValue("@id", id);
                    reader = comm.ExecuteReader();
                    dataTable.Load(reader);
                }
            }
            return new JsonResult($"משימה מספר {id} הוסרה בהצלחה");
        }
    }
}
