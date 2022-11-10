using UnityEngine;
using UnityEngine.UI;

namespace Main.UI
{
    public class PanelPlay : Panel
    {
        public void ReloadScene()
        {
            GameScenes.ReloadScene();
        }
    }
}
