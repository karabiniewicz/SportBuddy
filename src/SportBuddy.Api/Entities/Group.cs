namespace SportBuddy.Api.Entities;

public class Group
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public int Limit { get; }

    private Group()
    {
    }
    
    public Group(Guid id, string name, string description, int limit)
    {
        Id = id;
        Name = name;
        Description = description;
        Limit = limit;
    }
}