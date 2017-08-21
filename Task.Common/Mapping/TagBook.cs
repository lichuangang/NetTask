using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Common.Mapping
{
    /* ==============================================================================
     * 描述：TagBook
     * 创建人：李传刚 2017/7/31 14:30:45
     * ==============================================================================
     */
    public class TagBook
    {
        public string Id { get; set; }

        public string BookId { get; set; }

        public string BookScore { get; set; }

        public int Flag { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Status { get; set; }
    }
}
