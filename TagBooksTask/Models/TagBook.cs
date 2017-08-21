using System;

namespace TagBooksTask.Models
{
    public class TagBook
    {
        public string Id { get; set; }
        public string BookId { get; set; }
        public int? BookScore { get; set; }
        public int Flag { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Status { get; set; }
    }
}
