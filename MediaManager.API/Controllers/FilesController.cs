namespace MediaManager.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

[Route("api/files")]
[ApiController]
public class FilesController : ControllerBase
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="FilesController.cs" company="IRANYE">
    //   Copyright (c) IRANYE. All rights reserved.
    // </copyright>
    // --------------------------------------------------------------------------------------------------------------------
    private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
    private readonly ILogger<FilesController> logger;

    public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider, ILogger<FilesController> logger)
    {
        _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
            ?? throw new System.ArgumentNullException(
                nameof(fileExtensionContentTypeProvider));
        this.logger = logger;
    }

    [HttpGet("{fileId}")]
    public ActionResult GetFile(string fileId)
    {
        try
        {
            var pathToFile = "volumes.json";

            // check whether the file exists
            if (!System.IO.File.Exists(pathToFile))
            {
                logger.LogInformation("[FilesController] File with fileId {fileId} not found.", fileId);
                return NotFound();
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(pathToFile, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, contentType, Path.GetFileName(pathToFile));
        }
        catch (Exception ex)
        {
            logger.LogCritical("[FilesController] Exception in GET method File with fileId {fileId} '{Message}'.", fileId, ex.Message);
            return this.StatusCode(StatusCodes.Status500InternalServerError, "Failure handling your request");
        }
    }
}
