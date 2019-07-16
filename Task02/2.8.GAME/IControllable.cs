
namespace _2._8.GAME
{
    interface IControllable : IMovable
    {
        void GoLeft(int countSteps);

        void GoRight(int countSteps);

        void GoUp(int countSteps);

        void GoDown(int countSteps);

        void Stop();
    }
}
