using SportBuddy.Api.Consts;

namespace SportBuddy.Api.Entities;

public class Match
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Discipline Discipline { get; set; }
    public DateTimeOffset Date { get; set; }
    
// public Localization Localization { get; set; }
    public Match()
    {
    }
    
    public Match(string name, DateTimeOffset date, Discipline discipline)
    {
        Id = new Guid();
        Name = name;
        Date = date;
        Discipline = discipline;
    }
}