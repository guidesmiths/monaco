﻿#if filesSupport
using MediatR;
using Monaco.Template.Backend.Application.Services.Contracts;
using Monaco.Template.Backend.Common.Application.Commands;
using Monaco.Template.Backend.Common.Application.Commands.Contracts;

namespace Monaco.Template.Backend.Application.Features.File.Commands;

public sealed class FileCommandsHandlers : IRequestHandler<FileCreateCommand, ICommandResult<Guid>>,
										   IRequestHandler<FileDeleteCommand, ICommandResult>
{
	private readonly IFileService _fileService;

	public FileCommandsHandlers(IFileService fileService)
	{
		_fileService = fileService;
	}

	public async Task<ICommandResult<Guid>> Handle(FileCreateCommand request, CancellationToken cancellationToken)
	{
		var file = await _fileService.Upload(request.Stream, request.FileName, request.ContentType, cancellationToken);

		return new CommandResult<Guid>(file.Id);
	}

	public async Task<ICommandResult> Handle(FileDeleteCommand request, CancellationToken cancellationToken)
	{
		await _fileService.Delete(request.Id, cancellationToken);

		return new CommandResult();
	}
}
#endif