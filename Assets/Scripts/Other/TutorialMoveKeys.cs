using UnityEngine;

namespace Other
{
    public class TutorialMoveKeys : MonoBehaviour
    {
        [SerializeField] private GameObject arrows;
        [SerializeField] private GameObject wasd;

        private void Update()
        {
            if (PlayerPrefs.GetInt("InputSystem", 1) == 0)
            {
                arrows.SetActive(true);
                wasd.SetActive(false);
            }
            else
            {
                arrows.SetActive(false);
                wasd.SetActive(true);
            }
        }
    }
}