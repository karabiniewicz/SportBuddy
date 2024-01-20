using SportBuddy.Core.Consts;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Core.Entities;

// TODO: consider transform _members to List, and admin as a first element of this list
public class Group
{
    public GroupId Id { get; private set; }
    public UserId AdminId { get; private set; }
    public GroupName Name { get; private set; }
    public GroupDescription Description { get; private set; }
    public GroupType GroupType { get; private set; }
    public IEnumerable<User> Members => _members;
    public IEnumerable<Match> Matches => _matches;

    private readonly HashSet<User> _members = new();
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
    
    public void AddMember(User user)
        => _members.Add(user);
    
    public void RemoveMember(UserId id)
        => _members.RemoveWhere(x => x.Id == id);
    
    public void AddMatch(Match match)
        => _matches.Add(match);
    
    public void RemoveMatch(MatchId id)
        => _matches.RemoveWhere(x => x.Id == id);
}