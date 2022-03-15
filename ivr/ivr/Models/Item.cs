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
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Item)) return false;
            var other = obj as Item;
            return (Id.Equals(other.Id)) &&
                (ParentId.Equals(other.ParentId)) &&
                (Type.Equals(other.Type)) &&
                (Title.Equals(other.Title)) &&
                (IsFinished.Equals(other.IsFinished)) &&
                (Dt.Equals(other.Dt)) &&
                (string.IsNullOrEmpty(Data) && string.IsNullOrEmpty(other.Data) || Data.Equals(other.Data)) &&
                (EClass.Equals(other.EClass));
        }
        public override string ToString()
        {
            return Id.ToString() + ":" + ParentId.ToString() + " " + Type.ToString() + " " + Title + " " + Data + " "
                + " " + IsFinished.ToString() + " " + EClass.ToString();
        }
        public string JournalView
        {
            get
            {
                if (Type == 2) return $"Task: {Title}";
                return Title;
            }
        }
    }
}
