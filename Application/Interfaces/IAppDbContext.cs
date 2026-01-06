using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserType> UserTypes { get; set; }
        DbSet<Log> Logs { get; set; }
        DbSet<Issue> Issues { get; set; }
        DbSet<IssueStatus> Statuses { get; set; }
        DbSet<IssueType> IssueTypes { get; set; }
        DbSet<Pos> Pos { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<ConnectionType> ConnectionTypes { get; set; }
    }
}
