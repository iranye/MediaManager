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

        [HttpGet("{m3uId}", Name = "GetM3uFile")]
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

        [HttpPost]
        public ActionResult<M3uFileDto> CreateM3uFile(string moniker, M3uFileDtoForUpsert m3uFile)
        {
            var volume = VolumesDataStore.Current.Volumes.FirstOrDefault(v => v.Moniker?.ToLower() == moniker.ToLower());
            if (volume is null)
            {
                return NotFound();
            }

            var maxId = VolumesDataStore.Current
                .Volumes.SelectMany(c => c.M3uFiles).Max(x => x.Id);

            var m3uFileResponse = new M3uFileDto()
            {
                Id = ++maxId,
                Title  = m3uFile.Title,
                Created = DateTime.Now,
                FilesInM3U = m3uFile.FilesInM3U
            };

            volume.M3uFiles.Add(m3uFileResponse);

            return CreatedAtRoute("GetM3uFile",
                 new
                 {
                     moniker = moniker,
                     m3uId = m3uFileResponse.Id
                 },
                 m3uFileResponse);
        }
    }
}
