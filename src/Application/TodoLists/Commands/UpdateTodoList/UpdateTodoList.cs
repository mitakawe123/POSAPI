﻿using POSAPI.Application.Common.Interfaces;

namespace POSAPI.Application.TodoLists.Commands.UpdateTodoList;

public record UpdateTodoListCommand : IRequest
{
    public Guid Id { get; init; }

    public string? Title { get; init; }
}

public class UpdateTodoListCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateTodoListCommand>
{
    public async Task Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.TodoLists
            .FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;

        await context.SaveChangesAsync(cancellationToken);

    }
}
