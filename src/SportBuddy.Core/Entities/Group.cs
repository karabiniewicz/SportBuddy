using SportBuddy.Core.Consts;

namespace SportBuddy.Core.Entities;

public class Group
{
    // TODO: add value objects
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public GroupType GroupType { get; private set; }
    public IEnumerable<Match> Matches => _matches;

    private readonly HashSet<Match> _matches = new();

    private Group()
    {
    }

    public Group(string name, string description, GroupType groupType)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        GroupType = groupType;
    }
}