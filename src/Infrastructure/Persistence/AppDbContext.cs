using Application.Common;
using Application.Common.Notifications;
using Domain.Common;
using Domain.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    private readonly IMediator _mediator;

    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator)
        : base(options) => _mediator = mediator;

    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var result = await base.SaveChangesAsync(ct);

        // Publicar domain events como notificaciones MediatR
        var entitiesWithEvents = ChangeTracker.Entries<IHasDomainEvents>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count > 0)
            .ToList();

        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.DomainEvents.ToList();
            entity.ClearDomainEvents();

            foreach (var domainEvent in events)
                await _mediator.Publish(DomainEventNotification.Wrap(domainEvent), ct);
        }

        return result;
    }
}
