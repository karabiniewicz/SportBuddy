namespace SportBuddy.Core.Entities;

public class Group
{
    // TODO: add value objects
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Limit { get; private set; }

    private Group()
    {
    }

    public Group(string name, string description, int limit)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Limit = limit;
    }
}