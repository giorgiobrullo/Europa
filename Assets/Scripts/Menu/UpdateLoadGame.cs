using TMPro;
using UnityEngine;

namespace Menu
{
    public class UpdateLoadGame : MonoBehaviour
    {

        void Start()
        {
            // Load the last scene name from Easy Save 3
            string lastSceneName =  PlayerPrefs.GetString("LastSceneName", "N/A");
            Debug.Log("Last scene name: " + lastSceneName);
            
            // Update the text component
            GetComponent<TextMeshProUGUI>().text = "Load game (" + lastSceneName + ")";
        }
    }
}
