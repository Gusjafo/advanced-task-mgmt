using Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tasks;

public sealed class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery, IReadOnlyCollection<TaskItemDto>>
{
    private readonly IAppDbContext _context;

    public GetAllTasksHandler(IAppDbContext context) => _context = context;

    public async Task<IReadOnlyCollection<TaskItemDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _context.Tasks
            .AsNoTracking()
            .OrderByDescending(t => t.Priority)
            .ThenBy(t => t.DueDate)
            .Select(t => new TaskItemDto(t.Id, t.Title, t.CreatedAt, t.DueDate, t.Priority, t.Status))
            .ToListAsync(cancellationToken);

        return tasks;
    }
}
