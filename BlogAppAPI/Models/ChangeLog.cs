namespace BlogAppAPI.Models
{
    public class ChangeLog
    {
        public int ChangeLogID { get; set; }
        public int BlogID { get; set; }
        public string ChangedItem { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public int UserID { get; set; }
        public DateTime UpdateDate { get; set; }
        public string IP { get; set; }
    }
}
