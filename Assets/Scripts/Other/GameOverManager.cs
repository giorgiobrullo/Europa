using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Items;
using TMPro;

namespace Other
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField] private Stats stats;
        [SerializeField] private Button goBackToMenu;
        [SerializeField] private TextMeshProUGUI goBackToMenuText; // Assuming you have a Text component for the button text
        private bool _isButtonEnabled = true;

        public bool IsButtonEnabled => _isButtonEnabled;

        public static GameOverManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        
        private void OnEnable()
        {
            _isButtonEnabled = false;
            StartCoroutine(EnableButtonWithDelay(3)); // Start the coroutine to enable the button after 3 seconds
        }

        public void LoadAutoSave()
        {
            ES3AutoSaveMgr.Current.Load();
            DestroyAllGenericItems();
            goBackToMenu.interactable = false; 
            stats.TriggerCheckpointSequence();
        }

        public void BackToMenu()
        {
            if(_isButtonEnabled)
                SceneManager.LoadScene("Menu");    
        }

        private IEnumerator EnableButtonWithDelay(int delay)
        {
            for (int i = delay; i > 0; i--)
            {
                goBackToMenuText.text = $"Back to Menu Main ({i}s)"; // Update the button text with the countdown
                yield return new WaitForSeconds(1); // Wait for 1 second
            }

            goBackToMenuText.text = "Back to Main Menu"; // Reset the button text
            goBackToMenu.interactable = true; // Enable the button
            _isButtonEnabled = true;
        }
        
        public void DestroyAllGenericItems()
        {
            // Find all game objects in the scene
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            
            foreach (GameObject obj in allObjects)
            {
                // Check if the game object has a component derived from GenericItem
                if (obj.GetComponent<GenericItem>() != null && obj.GetComponent<SaveObject>() == null && obj.GetComponent<ScoreItem>() == null)
                {
                    Destroy(obj); // Destroy the game object
                }
            }
        }
    }
}