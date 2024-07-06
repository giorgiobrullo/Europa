using System;
using Cameras;
using Other;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Volume;

namespace Menu
{
    public class Menu : MonoBehaviour {
        [SerializeField] private Button loadGameButton;

        public void Start()
        {
            loadGameButton.interactable = ES3.FileExists("SaveFile.es3");
        }

        public void NewGame () {
            if(PlayerPrefs.GetInt("SetDifficulty", 0) == 0) // If no difficulty has been set, set it to 2 (Normal)
            {
                PlayerPrefs.SetInt("Difficulty", 2);
            }
            PlayerPrefs.DeleteKey("SetDifficulty");
            
            PlayerPrefs.DeleteKey("LastSceneName");
            PlayerPrefs.DeleteKey("Score");
            PlayerPrefs.DeleteKey("Coins");
            PlayerPrefs.DeleteKey("Power");
            PlayerPrefs.DeleteKey("Defense");

            LiveSplitIntegration.Instance.ResetCheckpointCounter();
            VolumeController.Instance.StopAllCoroutines();

            PlayerPrefs.Save();
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
        }

        public void LoadGame()
        {
            string sceneToLoad = PlayerPrefs.GetString("LastSceneName", "");

            PlayerPrefs.SetInt("LoadFromAutoSave", 1);
            SceneManager.LoadScene(sceneToLoad);        
        }
        public void ResumeGame() {
            Time.timeScale = 1;  
        }

        public void QuitGame () {
            Application.Quit();
        }

        public void PlayAgain() {
            SceneManager.LoadScene("Menu");
        }
        
        public void SetDifficulty(Single dif)
        {

                PlayerPrefs.SetInt("SetDifficulty", 1);
                PlayerPrefs.SetInt("Difficulty", (int)dif);
                PlayerPrefs.Save();
                Debug.Log("Setting Difficulty to " + dif);
            
        }
    }
}