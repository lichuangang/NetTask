using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Common;
using Task.Common.Dao;
using Task.Common.Utilities;

namespace TagBookEndTask
{
    public class RunTask : BaseTask
    {
        ILog _log = Log4NetLoggerFactory.Instance.Create("TagBookEndTask.RunTask");

        public override void Process()
        {
            _log.InfoFormat("开始查询所有HomeBookTimes数据");
            var homeBookTimes = TagBookDao.GetExpireHomeBookTime();
            _log.InfoFormat("开始查询所有Book数据:{0}条", homeBookTimes.Count);
            var booksDict = BookDao.GetBookByIds(homeBookTimes.Select(m => m.BookID).ToList()).ToDictionary(m => m.BookID);
            homeBookTimes.ForEach(homeBookTime =>
            {
                try
                {
                    if (booksDict.ContainsKey(homeBookTime.BookID))
                    {
                        TagBookDao.RestoreBook(booksDict[homeBookTime.BookID], homeBookTime);
                        _log.InfoFormat("TagBookEndTask BookId：{0},恢复原价执行成功！", homeBookTime.BookID);
                    }
                    else
                    {
                        _log.InfoFormat("TagBookEndTask BookId:{0}未找到书籍", homeBookTime.BookID);
                    }
                }
                catch (Exception ex)
                {
                    _log.ErrorFormat("TagBookEndTask HomeBookTimeId:{0} 出现异常：{1}", homeBookTime.ID, ex);
                }
            });
        }
    }
}
