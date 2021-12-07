﻿using DcslGs.Template.Api.Auth;
using DcslGs.Template.Application.DTOs;
using DcslGs.Template.Application.Queries.File;
using DcslGs.Template.Application.Queries.Image;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DcslGs.Template.Api.Controllers
{
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [Authorize(Scopes.FilesRead)]
        public async Task<ActionResult<ImageDto>> Get(Guid id)
        {
            var result = await _mediator.Send(new GetImageByIdQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}/Thumbnail")]
        [Authorize(Scopes.FilesRead)]
        public async Task<ActionResult<ImageDto>> GetThumbnail(Guid id)
        {
            var result = await _mediator.Send(new GetThumbnailByImageIdQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}/Download")]
        [Authorize(Scopes.FilesRead)]
        [ProducesResponseType(typeof(FileContentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Download(Guid id)
        {
            var result = await _mediator.Send(new DownloadFileByIdQuery(id));

            if (result == null)
                return NotFound();

            return File(result.FileContent, result.ContentType, result.FileName);
        }

        [HttpGet("{id}/Thumbnail/Download")]
        [Authorize(Scopes.FilesRead)]
        [ProducesResponseType(typeof(FileContentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DownloadThumbnail(Guid id)
        {
            var result = await _mediator.Send(new DownloadThumbnailByImageIdQuery(id));

            if (result == null)
                return NotFound();

            return File(result.FileContent, result.ContentType, result.FileName);
        }
    }
}
