using Other;
using UnityEngine;

namespace Items
{
    public class DroppedCoin : GenericItem
    {
        protected override void OnPlayerTrigger(Collider2D player)
        {
            ScoreManager.Instance.CollectScoreItem(ScoreManager.ScoreItems.DroppedCoin);
            Destroy(gameObject);
        }
        
    }
}