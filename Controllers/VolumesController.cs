using MediaManager.API.Data;
using MediaManager.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace MediaManager.API.Controllers
{
    [ApiController]
    [Route("api/volumes")]
    public class VolumesController : ControllerBase
    {
        public ActionResult<IEnumerable<VolumeDto>> GetVolumes(bool includeM3us = false)
        {
            try
            {
                var results = VolumesDataStore.Current.Volumes;
                return Ok(results);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{moniker}")]
        public ActionResult<VolumeDto> GetVolume(string moniker)
        {
            try
            {
                var result = VolumesDataStore.Current.Volumes.FirstOrDefault(v => v.Moniker?.ToLower() == moniker.ToLower());
                if (result is null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
