using Domain.Tasks;
using Domain.Tasks.Events;
public class TaskItemTests
{
    [Fact]
    public void Create_ShouldAddTaskCreatedEvent()
    {
        var t = TaskItem.Create("X", null, 1);
        Assert.Contains(t.DomainEvents, e => e is TaskCreatedDomainEvent);
    }

    [Fact]
    public void MarkAsDone_ShouldChangeStatusAndAddEvent()
    {
        var t = TaskItem.Create("X", null, 1);

        t.MarkAsDone();

        Assert.Equal(Domain.Tasks.TaskStatus.Done, t.Status);
        Assert.Contains(t.DomainEvents, e => e is TaskCompletedDomainEvent);
    }
}
