using Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tasks;

public sealed class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly IAppDbContext _context;

    public DeleteTaskHandler(IAppDbContext context) => _context = context;

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (task is null)
        {
            return false;
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
