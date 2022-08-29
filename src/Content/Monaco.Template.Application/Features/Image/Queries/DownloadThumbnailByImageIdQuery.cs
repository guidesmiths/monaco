﻿using Monaco.Template.Common.Application.Queries;
using Monaco.Template.Application.DTOs;

namespace Monaco.Template.Application.Features.Image.Queries;

public record DownloadThumbnailByImageIdQuery : QueryByIdBase<FileDownloadDto>
{
    public DownloadThumbnailByImageIdQuery(Guid id) : base(id)
    {
    }
}