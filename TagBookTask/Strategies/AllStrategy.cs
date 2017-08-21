using TagBookTask.DAOS;
using TagBookTask.Models;

namespace TagBookTask.Strategies
{
    public class AllStrategy : BaseStrategy
    {
        public override void DoHandleBegin(TagBook tagBook)
        {
            var orginBookScore = BookDao.GetBookScore(tagBook.BookId);
            if (TagBookDao.IsExsitHomeTime(tagBook.BookId))
            {
                TagBookDao.UpdateHomeTime(tagBook.BookId, tagBook.EndTime, orginBookScore, tagBook.Flag);
            }
            else
            {
                TagBookDao.InsertHomeTime(tagBook.BookId, tagBook.EndTime, orginBookScore, tagBook.Flag);
            }
            BookDao.UpdateBookIntflag(tagBook.BookId, 4, tagBook.Score, 0);
        }

        public override void DoHandleEnd(HomeBookTime homeBookTime)
        {
            var homeBookTimes = TagBookDao.GetBookTime(homeBookTime.BookId);
            if (homeBookTimes != null)
            {
                var orginBookScore = homeBookTimes.BookScore;
                BookDao.UpdateBookIntflag(homeBookTime.BookId, 0, orginBookScore, null);
            }
        }
    }
}
