using TagBookTask.DAOS;
using TagBookTask.Models;

namespace TagBookTask.Strategies
{
    public class AllNewStrategy : BaseHomeBookStrategy
    {
        public override void DoUpdateHandle(TagBook tagBook)
        {
            BookDao.UpdateBookIntflag(tagBook.BookId, 7, null, 0);
        }

        public override void DoHandleEnd(HomeBookTime tagBook)
        {
            BookDao.UpdateBookIntflag(tagBook.BookId, 0, null, null);
        }
    }
}
