using SportBuddy.Core.Consts;

namespace SportBuddy.Core.Entities;

public class Match
{
    // TODO: add value objects
    public Guid Id { get; }
    public string Name { get; }
    public Discipline Discipline { get; }
    public DateTimeOffset Date { get; }
    public string GroupName { get; }
    // TODO: public Localization Localization { get; }
    
    public Match()
    {
    }
    
    public Match(string name, Discipline discipline, DateTimeOffset date, string groupName)
    {
        Id = Guid.NewGuid();
        Name = name;
        Date = date;
        Discipline = discipline;
        GroupName = groupName;
    }
}