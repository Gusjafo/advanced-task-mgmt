using Application.Common;
using Domain.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tasks;

public sealed class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, TaskItemDto?>
{
    private readonly IAppDbContext _context;

    public UpdateTaskHandler(IAppDbContext context) => _context = context;

    public async Task<TaskItemDto?> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (task is null)
        {
            return null;
        }

        task.UpdateDetails(request.Title, request.DueDate, request.Priority);
        task.ChangeStatus(request.Status);

        await _context.SaveChangesAsync(cancellationToken);

        return TaskItemDto.FromEntity(task);
    }
}
