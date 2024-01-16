using SportBuddy.Core.Consts;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Core.Entities;

public class Group
{
    public GroupId Id { get; private set; }
    public UserId AdminId { get; private set; }
    public GroupName Name { get; private set; }
    public GroupDescription Description { get; private set; }
    public GroupType GroupType { get; private set; }
    public IEnumerable<Match> Matches => _matches;

    private readonly HashSet<Match> _matches = new();

    private Group()
    {
    }

    public Group(GroupId id, UserId adminId, GroupName name, GroupDescription description, GroupType groupType)
    {
        Id = id;
        AdminId = adminId;
        Name = name;
        Description = description;
        GroupType = groupType;
    }
}