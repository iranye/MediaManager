using MediaManager.API.Data;
using MediaManager.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace MediaManager.API.Controllers
{
    [ApiController]
    [Route("api/volumes")]
    public class VolumesController : ControllerBase
    {
        public ActionResult<IEnumerable<VolumeDto>> GetVolumes()
        {
            return Ok(VolumesDataStore.Current.Volumes);
        }

        [HttpGet("{id}")]
        public ActionResult<VolumeDto> GetVolume(int id)
        {
            var result = VolumesDataStore.Current.Volumes.FirstOrDefault(v => v.Id == id);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
