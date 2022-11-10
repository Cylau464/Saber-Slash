using Main;
using Engine;
using UnityEngine;
using Engine.DI;
using System.Linq;
using Engine.Senser;

namespace Examples
{
    public class AudioExample : MonoBehaviour, ILevelCompleted
    {
        private void OnEnable()
        {
            LevelStatueCompleted.Subscribe(this);
            for (int i = 0; i < DIContainer.Collect<ISenser>().ToArray().Length; i++)
            {
                print("Value: " + DIContainer.Collect<ISenser>().ToArray()[i]);
            }

            print("DIContainer.Collect<ISenser>().NonNull().ToArray(): " + DIContainer.Collect<ISenser>().NonNull().ToArray().Length);
            print("DIContainer.Collect<ISenser>().OfType<SenserInfo>().ToArray(): " + DIContainer.Collect<ISenser>().OfType<Senser>().ToArray().Length);
        }

        private void OnDisable()
        {
            LevelStatueCompleted.Unsubscribe(this);
        }

        public void LevelCompleted()
        {
            Senser senser = DIContainer
                .Collect<ISenser>()
                .NonNull()
                .OfType<Senser>()
                .Where((info) => info.type == SenserType.Audio)
                .Last();

            if (senser != null) Handheld.Vibrate();
        }
    }
}
