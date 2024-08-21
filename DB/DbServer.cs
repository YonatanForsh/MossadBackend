using MossadBackend.Models;
using Microsoft.EntityFrameworkCore;
namespace MossadBackend.DB
{
    public class DbServer: DbContext
    {
        public DbSet<Agent> AgentsList { get; set; }
        public DbSet<Target> TargetesList { get; set; }
        public DbSet<Mission> MissionsList { get; set; }

        public DbServer(DbContextOptions<DbServer> options) : base(options)
        {
            Database.EnsureCreated();
            //if (AgentsList.Count() == 0)
            //{
            //    SeadAgend();
            //}
        }

        //private void SeadAgend()
        //{
        //    Dictionary<string, int> Location = new Dictionary<string, int>();
        //    Location.Add("x", 10);
        //    Location.Add("y", 20);
        //    Agent agent = new Agent(
        //        "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.ynet.co.il%2Fnews%2Farticle%2Fhjs3ic7ly&psig=AOvVaw2ozxSdSYAf1oOzSowYkYE5&ust=1724317018147000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCKDx-szbhYgDFQAAAAAdAAAAABAE",
        //         "Agenst A",
        //         Location,
        //         AgentStatus.Active
        //         );
        //    AgentsList.Add(agent);
        //    SaveChanges();
        //}



        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}
