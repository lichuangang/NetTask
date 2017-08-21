using System.ComponentModel;

namespace TagBookTask.Models
{
    public enum PreferentialType
    {
        [Description("默认")]
        Default = 0,
        [Description("限免")]
        LimitFree = 3,
        [Description("老版本全本")]
        All = 4,
        [Description("一分一章")]
        ChapterOne = 101,
        [Description("vip限免")]
        Vip = 200,
        [Description("千字优惠")]
        Words = 5,
        [Description("固定章节免费")]
        ChapterFree = 6,
        [Description("新版本全本")]
        AllNew = 7,
        [Description("折扣购买")]
        Discount = 8
    }
}
