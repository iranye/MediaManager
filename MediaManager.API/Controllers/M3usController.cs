﻿using MediaManager.API.Data;
using MediaManager.API.Model;
using Microsoft.AspNetCore.JsonPatch;
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
            try
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
                    Title = m3uFile.Title,
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
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{m3uId}")]
        public ActionResult UpdatePointOfInterest(string moniker, int m3uId, M3uFileDtoForUpsert m3uFile)
        {
            try
            {
                var volume = VolumesDataStore.Current.Volumes.FirstOrDefault(v => v.Moniker?.ToLower() == moniker.ToLower());
                if (volume is null)
                {
                    return NotFound();
                }

                // find point of interest
                var m3uFileFromStore = volume.M3uFiles.FirstOrDefault(c => c.Id == m3uId);
                if (m3uFileFromStore == null)
                {
                    return NotFound();
                }

                m3uFileFromStore.Title = m3uFile.Title;
                m3uFileFromStore.FilesInM3U = m3uFile.FilesInM3U;
                m3uFileFromStore.LastModified = DateTime.Now;

                return NoContent();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPatch("{m3uId}")]
        public ActionResult PartiallyUpdateM3uFile(string moniker, int m3uId, JsonPatchDocument<M3uFileDtoForUpsert> patchDocument)
        {
            try
            {
                var volume = VolumesDataStore.Current.Volumes.FirstOrDefault(v => v.Moniker?.ToLower() == moniker.ToLower());
                if (volume is null)
                {
                    return NotFound();
                }

                var m3uFileFromStore = volume.M3uFiles.FirstOrDefault(c => c.Id == m3uId);
                if (m3uFileFromStore == null)
                {
                    return NotFound();
                }

                var m3uFileToPatch = new M3uFileDtoForUpsert()
                {
                    Title = m3uFileFromStore.Title,
                    FilesInM3U = m3uFileFromStore.FilesInM3U
                };

                patchDocument.ApplyTo(m3uFileToPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!TryValidateModel(m3uFileToPatch))
                {
                    return BadRequest(ModelState);
                }

                m3uFileFromStore.Title = m3uFileToPatch.Title;
                m3uFileFromStore.FilesInM3U = m3uFileToPatch.FilesInM3U;
                m3uFileFromStore.LastModified = DateTime.Now;

                return NoContent();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete("{m3uId}")]
        public ActionResult DeleteM3uFile(string moniker, int m3uId)
        {
            try
            {
                var volume = VolumesDataStore.Current.Volumes.FirstOrDefault(v => v.Moniker?.ToLower() == moniker.ToLower());
                if (volume is null)
                {
                    return NotFound();
                }

                var m3uFileFromStore = volume.M3uFiles.FirstOrDefault(c => c.Id == m3uId);
                if (m3uFileFromStore == null)
                {
                    return NotFound();
                }

                volume.M3uFiles.Remove(m3uFileFromStore);
                return NoContent();
            }                
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
