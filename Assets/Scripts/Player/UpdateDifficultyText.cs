using TMPro;
using UnityEngine;

namespace Player
{
    public class UpdateDifficultyText : MonoBehaviour
    {

        void Start()
        {
            string text = null;
            Color color = default;
            switch(PlayerPrefs.GetInt("Difficulty", 4))
            {
                case 1:
                    text = "Playing in Cute";
                    color = Color.cyan;
                    break;
                case 2:
                    text = "Playing in Normal";
                    color = Color.yellow;
                    break;
                case 3:
                    text = "Playing in Hardcore";
                    color = Color.red;
                    break;
                case 4:
                    text = "Something went wrong";
                    color = Color.red;
                    break;
                    
            }

            GetComponent<TextMeshProUGUI>().text = text;
            GetComponent<TextMeshProUGUI>().color = color;
        }
    }
}