using MossadBackend.Enums;
namespace MossadBackend.Models
{
    public class Agent
    {

        public Guid Id { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public string? Status { get; set; }

        //public Agent(string Picture, string Name, Locations location, AgentStatus Status)
        
        public Agent() { }

        public Agent(string Picture, string Name)
        {
            this.Picture = Picture;
            this.Name = Name;
            //this.Location = location;
            //this.Status = Status;
        }
    }
}
