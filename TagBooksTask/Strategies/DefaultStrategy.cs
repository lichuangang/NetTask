using TagBooksTask.Models;

namespace TagBooksTask.Strategies
{
    public class DefaultStrategy : BaseStrategy
    {
        public override void DoHandleBegin(TagBook tagBook)
        {
            //默认书籍什么都不做
        }

        public override void DoHandleEnd(HomeBookTime tagBook)
        {
            //默认书籍什么都不做
        }
    }
}
