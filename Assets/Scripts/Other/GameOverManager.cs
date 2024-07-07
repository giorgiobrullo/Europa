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
        
        [SerializeField] private Button loadAutoSave;
        [SerializeField] private TextMeshProUGUI loadAutoSaveText; // Assuming you have a Text component for the button text
        
        private bool _isBackEnabled = true;
        private bool _isLoadEnabled = true;
        
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
            _isBackEnabled = false;
            _isLoadEnabled = false;
            StartCoroutine(EnableButtonWithDelay(3, goBackToMenu, goBackToMenuText, true)); // Start the coroutine to enable the button after 3 seconds
            StartCoroutine(EnableButtonWithDelay(0.5f, loadAutoSave, loadAutoSaveText, false)); // Start the coroutine to enable the button after 3 seconds
        }

        public void LoadAutoSave()
        {
            if (!_isLoadEnabled) return;
            goBackToMenuText.text = "Back to Main Menu";

            ES3AutoSaveMgr.Current.Load();
            DestroyAllGenericItems();
            goBackToMenu.interactable = false; 
            loadAutoSave.interactable = false;
            stats.TriggerCheckpointSequence();
        }

        public void BackToMenu()
        {
            if(_isBackEnabled)
                SceneManager.LoadScene("Menu");    
        }

        private IEnumerator EnableButtonWithDelay(float delay, Button button, TextMeshProUGUI text, bool isBackButton = false)
        {
            string originalText = text.text; // Store the original button text
            for (float i = delay; i > 0.0f; i-=0.5f)
            {
                text.text = $"{originalText} ({i}s)"; // Update the button text with the countdown
                yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
            }

            text.text = originalText; // Reset the button text
            button.interactable = true; // Enable the button
            
            if(isBackButton) _isBackEnabled = true;
            else _isLoadEnabled = true;
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