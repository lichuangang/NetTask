using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRankTask.Models.Mappings
{
    public class BookRank
    {
        public string BookRankID { get; set; }
        public string BookID { get; set; }
        public string RankID { get; set; }
        public DateTime CreateDate { get; set; }
        public int RankType { get; set; }
        public int IsTop { get; set; }
        public int OrderNum { get; set; }
    }
}
