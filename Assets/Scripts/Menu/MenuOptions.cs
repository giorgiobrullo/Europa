using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MenuOptions : MonoBehaviour
    {
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private TMP_Dropdown  inputSystemToggle; // dropdown


        private void OnEnable()
        {
            // Enable all children
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        
        private void OnDisable()
        {
            // Disable all children
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        void Start()
        {
            float volume = PlayerPrefs.GetFloat("AudioVolume", 1f); // Default to 1 (max volume) if not set
            volumeSlider.value = volume;
            
            float sfxVolume = PlayerPrefs.GetFloat("SFXAudioVolume", 1f); // Default to 1 (max volume) if not set
            sfxVolumeSlider.value = sfxVolume;
            
            int inputSystem = PlayerPrefs.GetInt("InputSystem", 0); // Default to 0 (keyboard) if not set
            inputSystemToggle.value = inputSystem;
            
            Debug.Log("Volume: " + volume + ", SFX Volume: " + sfxVolume);
        }
        
        public void SetInputSystem(int inputSystem)
        {
            PlayerPrefs.SetInt("InputSystem", inputSystem);
            PlayerPrefs.Save();
            Debug.Log("Setting Input System to " + inputSystem);
        }
    }
}