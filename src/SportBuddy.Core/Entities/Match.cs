using SportBuddy.Core.Consts;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Core.Entities;

public class Match(MatchId id, MatchName name, Discipline discipline, Location location,
    string start, string end, DateOnly date, Charge charge, MatchLimit limit, GroupId groupId)
{
    public MatchId Id { get; private set; } = id;
    public MatchName Name { get; private set; } = name;
    public Discipline Discipline { get; private set; } = discipline;
    public Location Location { get; private set; } = location;
    public DateOnly Date { get; private set; } = date;
    public string Start { get; private set; } = start;
    public string End { get; private set; } = end;

    // public RegistrationType RegistrationType { get; private set; }
    public Charge Charge { get; private set; } = charge;
    // public Currency Currency { get; private set; }
    public MatchLimit Limit { get; private set; } = limit;
    public GroupId GroupId { get; private set; } = groupId;
    public IEnumerable<User> Members => _members;
    private readonly HashSet<User> _members = new();
    // public IEnumerable<User> ReserveMembers => _reserveMembers;

    public void AddMember(User user)
        => _members.Add(user);

    public void RemoveMember(UserId id)
        => _members.RemoveWhere(x => x.Id == id);
}