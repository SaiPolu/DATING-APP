using DATING.APP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.Extensions.Configuration;

using System.Data;
using System.Data.SqlClient;



namespace DATING.APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RootController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public RootController(IConfiguration configuration) {
            _configuration = configuration;
        }
        public string GetDetails(object? jsonConvert)
        {

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DatingAppConn").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Users", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<details> detlist = new List<details>();
            Response response = new Response();
            if(dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    details user = new details();
                    user.id = Convert.ToInt32(dt.Rows[i]["id"]);
                    user.name = Convert.ToString(dt.Rows[i]["name"]);
                    user.location = Convert.ToInt32(dt.Rows[i]["location"]);
                    user.gender = Convert.ToString(dt.Rows[i]["gender"]);
                    user.email = Convert.ToString(dt.Rows[i]["email"]);
                    detlist.Add(user);
                }
            }
            if (detlist.Count > 0)
            {
                object JsonConvert = null;
                return jsonConvert.SerializeObject(detlist);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "NoDataFound";
                return JsonConvert.SerializeObject(response);
            }
        }
    }
}
