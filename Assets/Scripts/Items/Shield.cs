using Player;
using UnityEngine;

namespace Items
{
    public class Shield : GenericItem
    {
        protected override void OnPlayerTrigger(Collider2D player)
        {
            Stats.Instance.GainDefense(1);
            Destroy(gameObject);
        }
    }
}