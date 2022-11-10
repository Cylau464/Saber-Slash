using Engine;
using Engine.Input;

namespace Main
{
    public class LevelStatueCompleted : GameStatue<ILevelCompleted>
    {
        public override void Start()
        {
            //ControllerInputs.s_EnableInputs = false;

            Invoke(item => item.LevelCompleted());
        }
    }
}