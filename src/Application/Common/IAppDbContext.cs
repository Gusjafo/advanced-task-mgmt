using Domain.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Common;

public interface IAppDbContext
{
    DbSet<TaskItem> Tasks { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
