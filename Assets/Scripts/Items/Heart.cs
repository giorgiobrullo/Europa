using Player;
using UnityEngine;

namespace Items
{
    public class Heart : GenericItem
    {
        [SerializeField] private int healAmount = 1;
        protected override void OnPlayerTrigger(Collider2D player)
        {
            Stats.Instance.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}