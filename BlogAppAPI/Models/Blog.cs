namespace BlogAppAPI.Models
{
    public class Blog
    {
        public int BlogID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; } // Draft, Approved, Rejected, Published
        public int SubmittedByUserID { get; set; }
        public User SubmittedBy { get; set; }
        public DateTime SubmittedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedByUserID { get; set; }
        public User ApprovedBy { get; set; }
    }
}
