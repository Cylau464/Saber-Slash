using Engine;
using Engine.Input;

namespace Main
{
    public class LevelStatueFailed : GameStatue<ILevelFailed>
    {
        public override void Start()
        {
            //ControllerInputs.s_EnableInputs = false;

            Invoke(item => item.LevelFailed());
        }
    }
}