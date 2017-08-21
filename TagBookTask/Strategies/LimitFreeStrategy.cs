using TagBookTask.DAOS;
using TagBookTask.Models;

namespace TagBookTask.Strategies
{
    public class LimitFreeStrategy : BaseHomeBookStrategy
    {
        public override void DoUpdateHandle(TagBook tagBook)
        {
            BookDao.UpdateBookIntflag(tagBook.BookId, 3, null, null);
        }

        public override void DoHandleEnd(HomeBookTime tagBook)
        {
            BookDao.UpdateBookIntflag(tagBook.BookId, 0, null, null);
        }
    }
}
