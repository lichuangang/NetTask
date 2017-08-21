using TagBooksTask.DAOS;
using TagBooksTask.Models;

namespace TagBooksTask.Strategies
{
    public class DiscountStrategy : BaseHomeBookStrategy
    {
        public override void DoUpdateHandle(TagBook tagBook)
        {
            BookDao.UpdateBookIntflag(tagBook.BookId, 8, null, 0);
        }

        public override void DoHandleEnd(HomeBookTime tagBook)
        {
            BookDao.UpdateBookIntflag(tagBook.BookId, 0, null, 1);
        }
    }
}
