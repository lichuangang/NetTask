using TagBooksTask.DAOS;
using TagBooksTask.Models;

namespace TagBooksTask.Strategies
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
            BookDao.UpdateBookIntflag(tagBook.BookId, 4, tagBook.BookScore, 0);
        }

        public override void DoHandleEnd(HomeBookTime homeBookTime)
        {
            BookDao.UpdateBookIntflag(homeBookTime.BookId, 0, homeBookTime.BookScore, 1);
        }
    }
}
