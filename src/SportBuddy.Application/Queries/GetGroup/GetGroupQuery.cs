using SportBuddy.Application.Abstractions;
using SportBuddy.Application.DTO;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Application.Queries.GetGroup;

public sealed record GetGroupQuery(GroupId GroupId) : IQuery<GroupDto>;