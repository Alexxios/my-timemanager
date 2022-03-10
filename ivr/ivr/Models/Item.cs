using System;
using SQLite;
namespace ivr.Models
{
    [Table("Items")]
    public class Item
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public bool IsFinished { get; set; }
        public DateTime Dt { get; set; }
        public string Data { get; set; }
        public int EClass { get; set; }
    }
}
