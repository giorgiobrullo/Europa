using UnityEngine;

namespace Enemies.Generic
{
    public class EnemyItemDrop : MonoBehaviour
    {
        public GameObject coins;
        public GameObject hearts;
        public GameObject sword;
        public GameObject shield;

        [SerializeField] private int maxCoins = 5;
        [SerializeField] private int maxHearts = 3;
        [SerializeField] private int maxSwords = 2;
        [SerializeField] private int maxShields = 2;

        [SerializeField] private float dropForce = 10f; // Adjust force as needed
        [SerializeField] private float upwardForce = 5f; // Adjust upward force as needed
        
        public void DropItems()
        {
            if (coins) DropItem(coins, Random.Range(1, maxCoins + 1), 0);
            if (hearts) DropItem(hearts, Random.Range(maxHearts == 0 ? 0 : 1, maxHearts + 1), 0.5f);
            if (sword) DropItem(sword, Random.Range(maxSwords == 0 ? 0 : 1, maxSwords + 1), 1f);
            if (shield) DropItem(shield, Random.Range(maxShields == 0 ? 0 : 1, maxShields + 1), 1.5f);
        }

        private void DropItem(GameObject item, int quantity, float xOffset)
        {
            int dif = PlayerPrefs.GetInt("Difficulty");
            switch (dif)
            {
                case 1:
                    quantity = (quantity * 2);
                    break;
                case 2:
                    break;
                case 3:
                    quantity = (int)(quantity * 1.2); // Make it 20% more for hardcore, otherwise it's not worth killing enemies
                    break;
            }
            
            for (int i = 0; i < quantity; i++)
            {
                var position = new Vector3(transform.position.x + xOffset * (i - (float)quantity / 2),
                    transform.position.y + 1.0f, transform.position.z);

                var droppedItem = Instantiate(item, position, Quaternion.identity);

                // Add a Rigidbody2D component if it doesn't already have one
                var rb = droppedItem.GetComponent<Rigidbody2D>();
                if (rb == null)
                {
                    rb = droppedItem.AddComponent<Rigidbody2D>();
                }

                // Apply force to the item to "spit" it out like a fountain
                float randomXForce = Random.Range(-dropForce, dropForce);
                rb.AddForce(new Vector2(randomXForce, upwardForce), ForceMode2D.Impulse);

                // Enable gravity and bouncing
                rb.gravityScale = 1f; // Ensure gravity is enabled
                var dropCollider = droppedItem.GetComponent<Collider2D>();
                if (dropCollider != null)
                {
                    dropCollider.sharedMaterial = new PhysicsMaterial2D { bounciness = 0.4f, friction = 0.4f };
                }
            }
        }
    }
}
