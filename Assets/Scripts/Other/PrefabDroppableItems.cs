using UnityEngine;

namespace Other
{
    [CreateAssetMenu(fileName = "PrefabAssets", menuName = "Assets/PrefabAssets")]
    public class PrefabDroppableItems : ScriptableObject
    {
        public GameObject coins;
        public GameObject hearts;
        public GameObject sword;
        public GameObject shield;
    }
}