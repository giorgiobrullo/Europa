using UnityEngine;

namespace Other
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private int coinValue = 1000;

        private int _score;
        private int _scoreCoins, _scoreGems, _scoreStars;

        public static ScoreManager Instance { get; private set; }

        public int Score
        {
            get => _score;
            private set => _score = value;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                ReadScore();
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void CollectScoreItem(ScoreItems type)
        {
            switch (type)
            {
                case ScoreItems.Coin:
                    _scoreCoins++;
                    UpdateScore(coinValue);
                    break;
                case ScoreItems.DroppedCoin:
                    _scoreCoins++;
                    UpdateScore(coinValue);
                    break;
                case ScoreItems.BigCoin:
                    _scoreCoins += 5;
                    UpdateScore(coinValue * 15);
                    break;
            }
        }

        private void UpdateScore(int itemValue)
        {
            Score += itemValue; // Incrementally updates the score
            WriteScore();
        }

        public void AddEnemyScore(int score)
        {
            Score += score;
            WriteScore();
        }
        
        private void WriteScore()
        {
            PlayerPrefs.SetInt("Score", Score);
            PlayerPrefs.SetInt("Coins", _scoreCoins);
        }
        
        private void ReadScore()
        {
            Score = PlayerPrefs.GetInt("Score", 0);
            _scoreCoins = PlayerPrefs.GetInt("Coins", 0);

        }

        public enum ScoreItems
        {
            DroppedCoin,
            Coin,
            BigCoin
        }
    }
}