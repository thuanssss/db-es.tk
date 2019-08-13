using System;

namespace DatabaseEnsoulSharp.Models.Database
{
    public class ScriptRatingDetail
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdScript { get; set; }
        public int Point { get; set; }
        public DateTime CreateDate { get; set; }
    }
}