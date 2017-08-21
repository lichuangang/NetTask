using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRankTask.Models.Enums
{
    public enum RankType
    {
        [Description("周榜")]
        Week = 0,
        [Description("月榜")]
        Month,
        [Description("总榜")]
        Total
    }
}
