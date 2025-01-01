namespace BlogAppAPI.Models
{
    public class ActivityLog
    {
        public int ActivityLogID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public string ActivityCode { get; set; }
        public string ActivityString { get; set; }
        public string IP { get; set; }
    }
}
