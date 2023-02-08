namespace Workflow;

public class WorkflowModel
{
    public string Id { get; private set; }
    public Status Status { get; private set; }
    public string Description { get; private set; }
    public string? CancellationReason { get; private set; }
    public string? parentId { get; private set; }

    private DateTime StartDate { get; set; }
    private DateTime? EndDate { get; set; }
}