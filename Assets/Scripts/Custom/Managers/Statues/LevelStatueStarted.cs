using Engine;
using Engine.Input;

namespace Main
{
    public class LevelStatueStarted : GameStatue<ILevelStarted>
    {
        public override void Start()
        {
            ControllerInputs.s_EnableInputs = true;

            Invoke(item => item.LevelStarted());
        }
    }
}