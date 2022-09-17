using PayCore.Application.Constant.Roles;
using System.ComponentModel;

namespace PayCore.Application.Enums;

public enum Roles
{
    [Description(Role.Admin)]
    Admin = 1,

    [Description(Role.Member)]
    Member = 2
}
public enum RabbitMqQueue
{
    EmailSenderQueue = 0,
}
