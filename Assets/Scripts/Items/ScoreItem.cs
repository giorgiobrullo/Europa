using Other;
using UnityEngine;

namespace Items
{
    public class ScoreItem : GenericItem
    {
        [SerializeField] private ScoreManager.ScoreItems type;

        protected override void OnPlayerTrigger(Collider2D player)
        {
            ScoreManager.Instance.CollectScoreItem(type);
            PlayPickupSound();
            
            gameObject.SetActive(false);
        }
    }
}