using Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tasks;

public sealed class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TaskItemDto?>
{
    private readonly IAppDbContext _context;

    public GetTaskByIdHandler(IAppDbContext context) => _context = context;

    public async Task<TaskItemDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .AsNoTracking()
            .Where(t => t.Id == request.Id)
            .Select(t => new TaskItemDto(t.Id, t.Title, t.CreatedAt, t.DueDate, t.Priority, t.Status))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
