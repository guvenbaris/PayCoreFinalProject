using PayCore.Application.Models;
using System.ComponentModel;

namespace PayCore.Application.Enums
{
    public class Enums
    {
        public enum RoleEnum
        {
            [Description(Role.Admin)]
            Admin = 1,

            [Description(Role.User)]
            User = 2
        }
    }
}