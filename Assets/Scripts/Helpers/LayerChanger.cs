using UnityEngine;

namespace Helpers
{
    public static class LayerChanger
    {
        public static void Change(GameObject target, LayerMask layer)
        {
            target.layer = layer.FirstSetLayer();
        }

        public static void ChangeRecursively(Transform target, LayerMask layer)
        {
            target.gameObject.layer = layer.FirstSetLayer();

            foreach (Transform child in target)
            {
                ChangeRecursively(child, layer);
            }
        }

        public static int FirstSetLayer(this LayerMask mask)
        {
            int value = mask.value;

            if (value == 0) return 0;  // Early out

            for (int l = 1; l < 32; l++)
                if ((value & (1 << l)) != 0)
                    return l;  // Bitwise

            return -1;  // This line won't ever be reached but the compiler needs it
        }
    }
}