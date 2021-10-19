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
        SaveContext _saveContext;
        MySqlCommand cmd;

        public SaveController(SaveContext saveContext) {

            cmd = new MySqlCommand();
            cmd.Connection = new MySqlConnection();
        }

        [HttpOptions("/Update/{request}")]
        public async Task<IActionResult> Update(string request)
        {
            cmd.CommandText = request;
            cmd.Connection.Open();

            try 
            { 
                cmd.ExecuteNonQuery(); 
            }
            catch { return BadRequest(); }

            cmd.Connection.Close();

            try
            {
                using (FileStream fs = System.IO.File.Create(@"./Save/SaveUpdateRequests.txt"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(request);
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Ok();
        }

        [HttpOptions("/Cast")]
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