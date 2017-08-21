using TagBooksTask.DAOS;
using TagBooksTask.Models;

namespace TagBooksTask.Strategies
{
    public class VipStrategy : BaseStrategy
    {
        public override void DoHandleBegin(TagBook tagBook)
        {
            BookDao.UpdateBookIntflag(tagBook.BookId, 200, null, null);
        }

        public override void DoHandleEnd(HomeBookTime tagBook)
        {
            BookDao.UpdateBookIntflag(tagBook.BookId, 0, null, null);
        }
    }
}
