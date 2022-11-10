using UnityEngine;

namespace Examples
{
    public class StartGame : MonoBehaviour
    {
        public void MakeStarted()
        {
            Engine.DI.DIContainer.AsSingle<Engine.IMakeStarted>().MakeStarted();
        }
    }
}