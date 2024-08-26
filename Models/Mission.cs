namespace MossadBackend.Models
{
    public class Mission
    {    
        public Guid? Id { get; set; }
        public Agent? Agent { get; set; }
        public Target? Target { get; set; }
        public Double Time { get; set; }
        public DateTime killingTime { get; set; }
        public string? Status { get; set; }

        public Mission() { }
        public Mission(Guid id, int time, DateTime killingTime)
        { 
            this.Id = id;
            this.Time = time;
            this.killingTime = killingTime;
        }
        public Mission (Guid id,  Agent? agent, Target? target, Double time, DateTime killingTime, string Status)
        {
            this.Id = id;
            this.Agent = agent;
            this.Target = target;
            this.Time = time;
            this.killingTime = killingTime;
            this.Status = Status;
        }
    }
}
