using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Common.Mapping
{
    /* ==============================================================================
     * 描述：HomeBookTime
     * 创建人：李传刚 2017/7/31 14:32:29
     * ==============================================================================
     */
    public class HomeBookTime
    {
        public long ID { get; set; }
        public string BookID { get; set; }
        public DateTime? EndTime { get; set; }
        public int? BookScore { get; set; }
        public DateTime? LastUpDate { get; set; }

        public int IntFlag2 { get; set; }
    }
}
