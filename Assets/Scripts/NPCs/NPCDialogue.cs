using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace NPCs
{
    public class NpcDialogue : MonoBehaviour
    {
        private TextMeshProUGUI _dialogueText;
        private CanvasGroup _dialogueBubbleCanvasGroup;
        private SpriteRenderer _dialogueBubbleSprite;
        private string _fullText;
        private Coroutine _typingCoroutine;
        private Coroutine _fadeCoroutine;
        
        public float typingSpeed = 0.05f;  // Time between each letter

        private void Start()
        {
            _dialogueText = GetComponentInChildren<TextMeshProUGUI>();
            _dialogueBubbleCanvasGroup = GetComponentInChildren<CanvasGroup>();
            _dialogueBubbleSprite = GetComponentsInChildren<SpriteRenderer>().Skip(1).First(); 
            _fullText = _dialogueText.text;

            ResetUI();
        }

        private void ResetUI()
        {
            _dialogueBubbleCanvasGroup.alpha = 0;
            _dialogueText.text = "";

            var color = _dialogueBubbleSprite.color;
            color.a = 0;
            _dialogueBubbleSprite.color = color;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (_fadeCoroutine != null)
                    StopCoroutine(_fadeCoroutine);
                _fadeCoroutine = StartCoroutine(ShowDialogueBubble());

                if (_typingCoroutine != null)
                    StopCoroutine(_typingCoroutine);
                _typingCoroutine = StartCoroutine(StartDialogue());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (_fadeCoroutine != null)
                    StopCoroutine(_fadeCoroutine);
                _fadeCoroutine = StartCoroutine(HideDialogue());
            }
        }

        IEnumerator ShowDialogueBubble()
        {
            while (_dialogueBubbleCanvasGroup.alpha < 1)
            {
                _dialogueBubbleCanvasGroup.alpha = Mathf.Clamp(_dialogueBubbleCanvasGroup.alpha + Time.deltaTime / 0.5f, 0, 1);
                var color = _dialogueBubbleSprite.color;
                color.a = _dialogueBubbleCanvasGroup.alpha;
                _dialogueBubbleSprite.color = color;
                yield return null;
            }
        }

        IEnumerator StartDialogue()
        {
            _dialogueText.text = "";  // Ensure the text is clear before starting
            foreach (char letter in _fullText)
            {
                _dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        IEnumerator HideDialogue()
        {
            while (_dialogueBubbleCanvasGroup.alpha > 0)
            {
                _dialogueBubbleCanvasGroup.alpha = Mathf.Clamp(_dialogueBubbleCanvasGroup.alpha - Time.deltaTime / 0.5f, 0, 1);
                var color = _dialogueBubbleSprite.color;
                color.a = _dialogueBubbleCanvasGroup.alpha;
                _dialogueBubbleSprite.color = color;
                yield return null;
            }
            ResetUI();  // Fully reset after fading
        }
    }
}
