namespace MossadBackend.ViewModel
{
    public class ViewActiveMissions
    {
        public string AgentName  { get; set; }
        public int AgentXLocation { get; set; }
        public int AgentYLocation { get; set; }
        public string TargetName { get; set; }
        public int TargetXLocation { get; set; }
        public int TargetYLocation { get; set; }
        public Double Distance { get; set; }
        public DateTime killingTime { get; set; }
    }
}
