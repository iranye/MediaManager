using MediaManager.API.Data;
using MediaManager.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace MediaManager.API.Controllers
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="M3usController.cs" company="IRANYE">
    //   Copyright (c) IRANYE. All rights reserved.
    // </copyright>
    // --------------------------------------------------------------------------------------------------------------------
    [ApiController]
    [Route("api/volumes/{moniker}/m3us")]
    public class M3usController:ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<FileEntryDto>> GetFileEntries(string moniker)
        {
            try
            {
                var result = VolumesDataStore.Current.Volumes.FirstOrDefault(v => v.Moniker?.ToLower() == moniker.ToLower());
                if (result is null)
                {
                    return NotFound();
                }
                return Ok(result.M3uFiles);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{m3uId}")]
        public ActionResult<M3uFileDto> GetM3uFile(string moniker, int m3uId)
        {
            try
            {
                var volume = VolumesDataStore.Current.Volumes.FirstOrDefault(v => v.Moniker?.ToLower() == moniker.ToLower());
                if (volume is null)
                {
                    return NotFound();
                }
                var m3uFile = volume.M3uFiles.FirstOrDefault(m => m.Id == m3uId);
                if (m3uFile is null)
                {
                    return NotFound();
                }
                return Ok(m3uFile);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
