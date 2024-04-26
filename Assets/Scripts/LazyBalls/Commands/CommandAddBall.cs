using LazyBalls.Singletons;

namespace LazyBalls.Commands
{
    public class CommandAddBall : ICommand
    {
        public void Invoke()
        {
            PlayerInfo.Instance().DoCreateNewBall();
        }
    }
}