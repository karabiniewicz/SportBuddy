namespace SportBuddy.Core.Entities;

public class Group
{
    public Guid Id { get; private set; }
    public string Name { get; set;}
    public string Description { get; set;}
    public int Limit { get; set;}

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