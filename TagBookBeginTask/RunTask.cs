using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Common;
using Task.Common.Dao;
using Task.Common.Utilities;

namespace TagBookBeginTask
{
    public class RunTask : BaseTask
    {
        ILog _log = Log4NetLoggerFactory.Instance.Create("TagBookBeginTask.RunTask");

        public override void Process()
        {
            _log.InfoFormat("开始查询所有TagBook数据");
            var tagBooks = TagBookDao.GetNeedRecordsTagBooks();
            _log.InfoFormat("开始查询所有Book数据:{0}条", tagBooks.Count);
            var booksDict = BookDao.GetBookByIds(tagBooks.Select(m => m.BookId).ToList()).ToDictionary(m => m.BookID);

            tagBooks.ForEach(tagBook =>
            {
                try
                {
                    if (booksDict.ContainsKey(tagBook.BookId))
                    {
                        TagBookDao.UpdateBook(booksDict[tagBook.BookId], tagBook);
                        _log.InfoFormat("TagBookBeginTask BookId：{0},TagBookId:{1}执行成功！", tagBook.BookId, tagBook.Id);
                    }
                    else
                    {
                        _log.InfoFormat("TagBookBeginTask BookId:{0}未找到书籍", tagBook.BookId);
                    }
                }
                catch (Exception ex)
                {
                    _log.ErrorFormat("TagBookBeginTask TagBookId:{0} 出现异常：{1}", tagBook.Id, ex);
                }
            });


        }
    }
}
