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
    public IEnumerable<User> Members => _members;

    private readonly HashSet<User> _members = new();

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
    
    public void AddMember(User user)
        => _members.Add(user);
    
    public void RemoveMember(UserId id)
        => _members.RemoveWhere(x => x.Id == id);
}