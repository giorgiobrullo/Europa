using Player;
using UnityEngine;

namespace Traps
{
    public class WaterDamage : MonoBehaviour
    {
        [SerializeField] private int damage = 25;

        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.gameObject.name != "Player") return;
            Stats.Instance.TakeDamage(damage, true);
            PlayerController.Instance.Respawn();
        }
    }
}
