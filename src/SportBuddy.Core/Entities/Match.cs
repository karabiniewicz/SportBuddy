using SportBuddy.Core.Consts;

namespace SportBuddy.Core.Entities;

public class Match
{
    // TODO: add value objects
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Discipline Discipline { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public string GroupName { get; private set; }
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