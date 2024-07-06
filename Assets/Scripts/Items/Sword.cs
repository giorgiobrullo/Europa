using Player;
using UnityEngine;

namespace Items
{
    public class Sword : GenericItem
    {
        protected override void OnPlayerTrigger(Collider2D player)
        {
            Stats.Instance.GainPower(1);
            Destroy(gameObject);
        }
    }
}