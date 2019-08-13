namespace DatabaseEnsoulSharp.Models.Database
{
    public class ChampionScript
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public float Rating { get; set; }
        public int TotalRate { get; set; }
        public string Status { get; set; }
        public int IdChampion { get; set; }
        public int IdScriptInfo { get; set; }

        public Champion Champion { get; set; }
        public ScriptInfo ScriptInfo { get; set; }
    }
}