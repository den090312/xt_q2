
namespace _2._8.GAME
{
    interface IControllable : IMovable
    {
        void GoLeft(Player player, int countSteps);

        void GoRight(Player player, int countSteps);

        void GoUp(Player player, int countSteps);

        void GoDown(Player player, int countSteps);
    }
}
