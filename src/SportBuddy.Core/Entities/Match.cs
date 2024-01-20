using SportBuddy.Core.Consts;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Core.Entities;

public class Match(MatchId id, MatchName name, Discipline discipline, Location location, DateTimeOffset start,
    DateTimeOffset end, Charge charge, MatchLimit limit, GroupId groupId)
{
    public MatchId Id { get; private set; } = id;
    public MatchName Name { get; private set; } = name;
    public Discipline Discipline { get; private set; } = discipline;
    public Location Location { get; private set; } = location;
    public DateTimeOffset Start { get; private set; } = start;

    public DateTimeOffset End { get; private set; } = end;

    // public RegistrationType RegistrationType { get; private set; }
    public Charge Charge { get; private set; } = charge;
    // public Currency Currency { get; private set; }
    public MatchLimit Limit { get; private set; } = limit;
    public GroupId GroupId { get; private set; } = groupId;
    public IEnumerable<User> Members => _members;
    private readonly HashSet<User> _members = new();
    // public IEnumerable<User> ReserveMembers => _reserveMembers;
}