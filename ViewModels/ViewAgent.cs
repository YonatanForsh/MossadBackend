namespace MossadBackend.ViewModels
{
    public class ViewAgent
    {
        public int AgentId { get; set; }
        public string Name { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public string? Status { get; set; }
        public int? MissionId { get; set; }
        public DateTime? killingTime { get; set; }
        public int? Kills { get; set; }


    }
}
