using System;
using QCommon.Extentions;
using ServiceStack.Common;

namespace TagBookTask.Models
{
    public class TagBook
    {
        public string Id { get; set; }
        public string BookId { get; set; }
        public string BookScore { get; set; }

        public int Score
        {
            get
            {
                if (BookScore.IsNullOrEmpty())
                {
                    return 0;
                }
                return BookScore.ConvertType<int>();
            }
        }

        public int Flag { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Status { get; set; }
    }
}
