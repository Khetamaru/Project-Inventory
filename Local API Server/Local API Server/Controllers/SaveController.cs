using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Local_API_Server.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Local_API_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveController : ControllerBase
    {
        MySqlCommand cmd;

        public string serverName = "localhost";
        public string userId = "root";
        public string password = "root";
        public bool persistsecurityinfo = true;
        public string databaseName = "project_inventory";
        public string dataStringConnection;

        public string databaseVersion = "V2";

        public SaveController(SaveContext saveContext) {

            dataStringConnection = "server=" + serverName + ";" +
                                    "Uid=" + userId + ";" +
                                    "password=" + password + ";" +
                                    "persistsecurityinfo=" + persistsecurityinfo + ";" +
                                    "database=" + databaseName;

            cmd = new MySqlCommand();
            cmd.Connection = new MySqlConnection(dataStringConnection);
        }

        [HttpPut("Update")]
        public IActionResult Update(RequestMySQL request)
        {
            cmd.CommandText = request.Request;
            cmd.Connection.Open();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch { return BadRequest(); }

            cmd.Connection.Close();

            try
            {
                using (StreamWriter fs = new StreamWriter(@"./Save/SaveUpdateRequests.txt", true))
                {
                    fs.WriteLine(request.Request);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Ok();
        }

        [HttpOptions("Cast")]
        public async Task<IActionResult> CastingSave()
        {
            cmd.CommandText = System.IO.File.ReadAllText(@"./Save/SaveUpdateRequests.txt");
            cmd.Connection.Open();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch { return BadRequest(); }

            cmd.Connection.Close();
            System.IO.File.Delete(@"./Save/SaveUpdateRequests.txt");

            return Ok();
        }
    }
}