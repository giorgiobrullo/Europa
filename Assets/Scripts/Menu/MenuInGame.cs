using Cameras;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Volume;

namespace Menu
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject options; // Options canvas
        public GameObject menu; // Menu canvas - Main part
        public EventSystem eventsystem;
        public GameObject stats;

        public PlayerController player;

        // Use this to track the pause state of the game
        public static bool IsGamePaused;

        void OnApplicationFocus(bool hasFocus)
        {
        #if !UNITY_EDITOR
            if (!hasFocus) PauseGame();
        #endif
        }

        private void Update()
        {
            //Debug.Log(Input.GetKey(KeyCode.Escape) + ", " + !player.IsDead());
            if (Input.GetKeyDown(KeyCode.Escape) && !player.IsDead())
            {
                // log all possible states
                Debug.Log("ActiveSelf: " + options.activeSelf + ", IsGamePaused: " + IsGamePaused + ", stats.activeSelf: " + stats.activeSelf);
                if (options.activeSelf)
                {
                    eventsystem.SetSelectedGameObject(null);
                    options.SetActive(false);
                    menu.SetActive(true);
                }
                else if (IsGamePaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void ResumeGame()
        {
            GetComponent<Canvas>().enabled = false;
            stats.SetActive(true);
            Time.timeScale = 1f;
            IsGamePaused = false;
        }

        public void PauseGame()
        {
            if (PlayerController.Instance.IsDead()) return; //Already in game over
            
            GetComponent<Canvas>().enabled = true;
            stats.SetActive(false); // Adjust this based on your game's logic
            Time.timeScale = 0f;
            IsGamePaused = true;
        }

        public void BackToMenu()
        {
            if (PlayerController.Instance.IsDead()) return;
            Time.timeScale = 1f;
            IsGamePaused = false;
            VolumeController.Instance.StopAllCoroutines();
            SceneManager.LoadScene("Menu");
        }
    }
}