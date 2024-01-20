using SportBuddy.Application.Abstractions;
using SportBuddy.Application.Exceptions;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Application.Commands.CreateMatch;

internal sealed class CreateMatchCommandHandler(IGroupRepository groupRepository, IMatchRepository matchRepository) : ICommandHandler<CreateMatchCommand>
{
    public async Task HandleAsync(CreateMatchCommand command)
    {
        var (id, name, discipline, location, date, start, end, charge, limit, groupId, userId) = command;

        var group = await groupRepository.GetAsync(groupId);
        if (group is null)
        {
            throw new GroupNotFoundException(groupId);
        }

        if (group.AdminId != userId)
        {
            throw new UserIsNotAdminException(userId, groupId);
        }
        
        var match = new Match(id, name, discipline, location, start, end, date, charge, limit, groupId);
        group.AddMatch(match);
        await matchRepository.AddAsync(match);
    }
}