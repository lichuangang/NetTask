using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Common.Mapping
{
    /* ==============================================================================
     * 描述：Book
     * 创建人：李传刚 2017/7/31 15:01:42
     * ==============================================================================
     */
    public class Book
    {
        public string BookID { get; set; }
        public string BookTitle { get; set; }
        public string BookDesc { get; set; }
        public string BookAuthor { get; set; }
        public string BookLanguageID { get; set; }
        public DateTime? BookPublishedDate { get; set; }
        public int? BookScore { get; set; }
        public int? BookDisplayTimes { get; set; }
        public int? BookDownloadsTimes { get; set; }
        public string BookCoverURL { get; set; }
        public string BookURL { get; set; }
        public string BookFromURL { get; set; }
        public string BookOriginalFileName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? BookLastUpdateDate { get; set; }
        public string UserIP { get; set; }
        public string BookUploadedUserID { get; set; }
        public string BookFormatID { get; set; }
        public string BookStatus { get; set; }
        public long BookSize { get; set; }
        public float? BookAverageRating { get; set; }
        public string BookRemark { get; set; }
        public string MD5HasCode { get; set; }
        public bool? IsPublic { get; set; }
        public string EBookCoverURL { get; set; }
        public string EBookURL { get; set; }
        public int? Version { get; set; }
        public string PDFtxtContentURL { get; set; }
        public int? ContentRating { get; set; }
        public int? intFlag1 { get; set; }
        public int? intFlag2 { get; set; }
        public int? intFlag3 { get; set; }
        public string strFlag1 { get; set; }
        public string strFlag2 { get; set; }
        public string strFlag3 { get; set; }
        public double? chapterscore { get; set; }
        public int? startchapter { get; set; }
        public int? BookWords { get; set; }
        public int? WriteStatus { get; set; }
        public string NewBookID { get; set; }
        public DateTime? ChapterLastUpdate { get; set; }
        public int? BookCommentsNum { get; set; }
    }
}
