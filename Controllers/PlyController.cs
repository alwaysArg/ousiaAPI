using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ousiaAPI.Model;

namespace ousiaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlyController : ControllerBase
    {
        [HttpGet]

        public async Task<ActionResult<List<Ply>>> GetAllPlies(){
            var plies = new List<Ply> 
            { 
                new Ply
                {
                    name = "E-Glass Unidirectional 400 gsm at 45 degrees",
                    volumeFraction = 0.5,
                    tensileModulus = [37864, 11224],
                    shearModulus = [3317, 3500, 3500],
                    angle = -45,
                    poissonRatio = [0.3, 0.3],
                    plyWeight = 400,
                },
                new Ply
                {
                    name = "CSM at 0",
                    volumeFraction = 0.50,
                    tensileModulus = [21214, 21214],
                    shearModulus = [7539, 3500, 3500],
                    angle = 0,
                    poissonRatio = [0.407, 0.407],
                    plyWeight = 225,
                }
            };

            return Ok(plies);
        }
    }
}
