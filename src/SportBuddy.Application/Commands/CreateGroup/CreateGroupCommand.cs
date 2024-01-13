﻿using SportBuddy.Application.Abstractions;
using SportBuddy.Core.Consts;

namespace SportBuddy.Application.Commands.CreateGroup;

public sealed record CreateGroupCommand(Guid GroupId, Guid AdminId, string Name, string Description, GroupType GroupType) : ICommand;
