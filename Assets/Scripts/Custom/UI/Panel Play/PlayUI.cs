using Engine.DI;
using System;
using TMPro;
using UnityEngine;

namespace Main.UI
{
    public class PlayUI : MonoBehaviour, IPlayUI, IDependency
    {
        public void Inject()
        {
            DIContainer.RegisterAsSingle<IPlayUI>(this);
        }
    }
}