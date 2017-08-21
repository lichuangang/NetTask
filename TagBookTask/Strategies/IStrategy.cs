using TagBookTask.Models;

namespace TagBookTask.Strategies
{
    public interface IStrategy
    {
        void HandleBegin(TagBook tagBook);

        void HandleEnd(HomeBookTime tagBook);

        void HandleDeleted(TagBook tagBook);
    }
}
