using TMPro;
using UnityEngine;

namespace Player
{
    public class Stats : MonoBehaviour
    {
        private static readonly int Die = Animator.StringToHash("die");

        // Singleton instance
        public static Stats Instance { get; private set; }

        // Serialized fields
        [Header("Character Attributes")]
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int attackDamage = 100;
        [SerializeField] private int defense;

        [Header("UI Elements")]
        [SerializeField] private ProgressBarPro healthBar;
        [SerializeField] private ProgressBarPro powerBar;
        [SerializeField] private TextMeshProUGUI damageText;
        [SerializeField] private TextMeshProUGUI defenseText;
        [SerializeField] private GameObject gameOverCamera;
        [SerializeField] private GameObject statsPanel;

        [Header("Sound Effects")]
        [SerializeField] private GameObject deathSound;
        [SerializeField] private GameObject damageSound;

        private int _health;
        public int power;

        // Properties
        public int Power => power;
        public int AttackDamage => attackDamage;
        
        private Animator _animation;
        private static readonly int Hurt = Animator.StringToHash("hurt");

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _health = maxHealth;
            _animation = GetComponent<Animator>();
            LoadStats();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateUI();
        }

        private void LoadStats()
        {
            attackDamage = Mathf.Max(100, PlayerPrefs.GetInt("AttackDamage", attackDamage));
            defense = PlayerPrefs.GetInt("Defense", defense);
            power = PlayerPrefs.GetInt("Power", power);
            UpdateStatsText();
        }

        private void UpdateUI()
        {
            UpdateHealthBar();
            UpdateStatsText();
        }

        private void UpdateStatsText()
        {
            damageText.text = $"+{power}";
            defenseText.text = $"+{defense}";
        }

        private void UpdateHealthBar()
        {
            float percentage = (float)_health / maxHealth;
            //Debug.Log("Health: " + _health + " Max Health: " + maxHealth + " Percentage: " + percentage);
            healthBar.SetValue(percentage);
        }
        
        private void CheckDeath()
        {
            if (_health <= 0 && !PlayerController.Instance.IsDead())
            {
                TriggerDeathSequence();
            }
        }

        private void TriggerDeathSequence()
        {
            GetComponentInParent<Animator>().SetBool(Die, true);
            // Uncomment the following if the destroy method should be called.
            // PlayerController.Instance.DestroySelf();
            PlayerController.Instance.SetDead(true);
            PlayerController.Instance.ResetExternalForce();
            gameOverCamera.SetActive(true);
            statsPanel.SetActive(false);
            Instantiate(deathSound, transform);
            
        }

        public void TriggerCheckpointSequence()
        {
            GetComponentInParent<Animator>().SetBool(Die, false);
            
            PlayerController.Instance.SetDead(false);
            statsPanel.SetActive(true);
        }

        public void TakeDamage(int value, bool defenceIgnored = false)
        {
            // Edit the following block to account for 'Difficulty' playerprefs, where 1 is 'cute' and very easy, '2' should be current and 3 should be 'hardcore' and actually very hard
            int dif = PlayerPrefs.GetInt("Difficulty");
            switch (dif)
            {
                case 1:
                    value = (int)(value * 0.5);
                    break;
                case 2:
                    break;
                case 3:
                    value = (int)(value * 2.1);
                    break;
            }
            Debug.Log("Hit for " + value + " damage before defense");
            if(value <= 0) return;
            
            if (defenceIgnored)
            {
                _health -= value;
            }
            else
            {
                int effectiveDamage = Mathf.Max(value - defense, 0);
                _health -= effectiveDamage;
            }
            
            
            _animation.SetTrigger(Hurt);
            if(damageSound) Instantiate(damageSound, transform);
            
            CheckDeath();
        }

        public void GainPower(int value)
        {
            power += value;
            UpdateStatsText();
            PlayerPrefs.SetInt("Power", power);
        }
        
        public void GainDefense(int value)
        {
            defense += value;
            PlayerPrefs.SetInt("Defense", defense);
            UpdateStatsText();
        }

        public void FullHeal()
        {
            Debug.Log("Full heal");
            _health = maxHealth;
        }
        
        public void Heal(int value)
        {
            Debug.Log("Healing for " + value + " health");
            _health = Mathf.Min(_health + value, maxHealth);
        }

    }
}
