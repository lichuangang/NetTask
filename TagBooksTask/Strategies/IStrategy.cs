using TagBooksTask.Models;

namespace TagBooksTask.Strategies
{
    public interface IStrategy
    {
        void HandleBegin(TagBook tagBook);

        void HandleEnd(HomeBookTime tagBook);
    }
}
