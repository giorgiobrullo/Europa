using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class ResolutionChanger : MonoBehaviour
    {
        public TMP_Dropdown resolutionDropdown;
        public Toggle fullscreenToggle;

        private List<Resolution> _resolutions;

        void Start()
        {
            resolutionDropdown = GetComponent<TMP_Dropdown>();
            
            if (resolutionDropdown == null)
            {
                Debug.LogError("TMP_Dropdown component not found on the GameObject.");
                return;
            }
            
            InitializeResolutions();
            InitializeFullScreenToggle();
        }

        void InitializeResolutions()
        {
            resolutionDropdown.ClearOptions(); // Clear existing options
            _resolutions = new List<Resolution>();
            
            foreach (var res in Screen.resolutions)
            {
                // Filtering for 16:9 aspect ratio
                float aspectRatio = (float)res.width / res.height;
                if (Math.Abs(aspectRatio - (16f / 9f)) < 0.01)
                {
                    _resolutions.Add(res);
                }
            }

            // Sorting resolutions from highest to lowest
            _resolutions.Sort((a, b) => b.width.CompareTo(a.width) != 0 ? b.width.CompareTo(a.width) : b.refreshRateRatio.CompareTo(a.refreshRateRatio));

            List<string> options = new List<string>();
            int currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0); // Default to the first resolution if not set

            for (int i = 0; i < _resolutions.Count; i++)
            {
                string option = _resolutions[i].width + " x " + _resolutions[i].height + " @" + _resolutions[i].refreshRateRatio.value + "Hz";
                options.Add(option);
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });

            // Apply saved resolution
            if (_resolutions.Count > currentResolutionIndex)
            {
                SetResolution(currentResolutionIndex);
            }
        }

        void InitializeFullScreenToggle()
        {
            bool isFullScreen = PlayerPrefs.GetInt("IsFullScreen", 1) == 1; // Default to fullscreen if not set
            fullscreenToggle.isOn = isFullScreen;
            SetFullScreen(isFullScreen);
            fullscreenToggle.onValueChanged.AddListener(SetFullScreen);
        }

        public void SetResolution(int resolutionIndex)
        {
            if (resolutionIndex >= 0 && resolutionIndex < _resolutions.Count)
            {
                Resolution resolution = _resolutions[resolutionIndex];
                Screen.SetResolution(resolution.width, resolution.height, fullscreenToggle.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, resolution.refreshRateRatio);
                PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
            }
        }

        public void SetFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
            PlayerPrefs.SetInt("IsFullScreen", isFullScreen ? 1 : 0);
        }
    }
}
