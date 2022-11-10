using Engine;
using Engine.Input;

namespace Main
{
    public class LevelStatueContinued : GameStatue<ILevelContinued>
    {
        public override void Start()
        {
            ControllerInputs.s_EnableInputs = true;
            
            Invoke(item => item.LevelContinued());
        }
    }
}