using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Other
{
    public class CreditsScroller : MonoBehaviour
    {
        public float scrollSpeed = 20f;  // The speed at which the camera moves downwards
        public float endY = -1000f;      // The Y position at which the camera stops scrolling
        public string mainMenuSceneName = "Menu";  // The name of the main menu scene
        
        [SerializeField] private TextMeshProUGUI difficultyText;
        [SerializeField] private TextMeshProUGUI scoreText;
        
        void UpdateStrings()
        {
            string text = null;
            Color color = default;
            switch(PlayerPrefs.GetInt("Difficulty", 4))
            {
                case 1:
                    text = "Played in Cute";
                    color = Color.cyan;
                    break;
                case 2:
                    text = "Played in Normal";
                    color = Color.yellow;
                    break;
                case 3:
                    text = "Played in Hardcore";
                    color = Color.red;
                    break;
                case 4:
                    text = "Something went wrong";
                    color = Color.red;
                    break;
                    
            }

            difficultyText.text = text;
            difficultyText.color = color;
            
            float score = PlayerPrefs.GetInt("Score", 0);
            switch(PlayerPrefs.GetInt("Difficulty", 4))
            {
                case 1:
                    score *= 0.22f;
                    break;
                case 2:
                    score *= 1.23f;
                    break;
                case 3:
                    score *= 5.32f;
                    break;
                case 4:
                    score = 0;
                    break;
            }
            scoreText.text = "Score: " + (int)score;
        }

        private void Awake()
        {
            UpdateStrings();
        }

        void Update()
        {
            // Calculate the new position of the camera
            float newY = Mathf.MoveTowards(transform.position.y, endY, scrollSpeed * Time.deltaTime);

            // Move the camera to the new position
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // Check if the camera has reached the end position
            if (transform.position.y <= endY)
            {
                // Optionally, do something when the camera reaches the end
                // For example, stop the credits or trigger an event
                Debug.Log("Credits finished scrolling.");
            }

            // Check for user input to switch to the main menu
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            {
                LoadMainMenu();
            }
        }

        void LoadMainMenu()
        {
            // Delete save
            ES3.DeleteFile("SaveFile.es3");
            PlayerPrefs.DeleteKey("LastSceneName");
            PlayerPrefs.DeleteKey("Score");
            PlayerPrefs.DeleteKey("Coins");
            PlayerPrefs.DeleteKey("Power");
            PlayerPrefs.DeleteKey("Defense");

            PlayerPrefs.Save();
            SceneManager.LoadScene(mainMenuSceneName);
        }

        void OnDrawGizmos()
        {
            // Draw a gizmo at the end position
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(transform.position.x - 5, endY, transform.position.z), new Vector3(transform.position.x + 5, endY, transform.position.z));
            Gizmos.DrawLine(new Vector3(transform.position.x, endY - 5, transform.position.z), new Vector3(transform.position.x, endY + 5, transform.position.z));
        }
    }
}