using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Medi
{
    /// <summary>
    /// Animates text appearing, like a typewriter.
    /// </summary>
    public class Typewriter : MonoBehaviour
    {
        private static VerboseLogger Verbose = VerboseLogger.Get(nameof(Typewriter));

        public event Action PlayStarted;
        public event Action PlayPaused;
        public event Action PlayResumed;
        public event Action PlayFinished;
        public Coroutine PlayCoroutine => playCoroutine;
        public TextMeshProUGUI TextUi => textUi;

        [SerializeField]
        private TextMeshProUGUI textUi;

        [SerializeField]
        private float characterDelay = .1f;

        [SerializeField]
        private float shortPauseDuration = 0.4f;

        [SerializeField]
        private float longPauseDuration = 1f;

        [SerializeField]
        private string shortPauseToken = "<short>";

        [SerializeField]
        private string longPauseToken = "<long>";

        [TextArea]
        public string testText = "test text here";

        private Coroutine playCoroutine = null;
        private string currentText = null;
        private Dictionary<int, float> pauseTokens = new Dictionary<int, float>();

        public void Play() 
        { 
            if (playCoroutine != null)
            {
                StopCoroutine(playCoroutine);
                PlayFinished?.Invoke();
            }
            playCoroutine = StartCoroutine(RunTypewriter());
        }

        public void Clear() 
        { 
            currentText = "";
            textUi.text = "";
        }

        public void SetText(string text) 
        { 
            currentText = text;
            pauseTokens.Clear();

            for (int i = 0; i < currentText.Length; ++i)
            {
                if (currentText.Substring(i).StartsWith(shortPauseToken))
                {
                    currentText = currentText.Remove(i, shortPauseToken.Length);
                    pauseTokens.Add(i-1, shortPauseDuration);
                }
                else if (currentText.Substring(i).StartsWith(longPauseToken))
                {
                    currentText = currentText.Remove(i, longPauseToken.Length);
                    pauseTokens.Add(i-1, longPauseDuration);
                }
            }
        }
        public void SetTextColor(Color color) { }
        public void SetSound(AudioClip clip) { }

        private IEnumerator RunTypewriter()
        {
            if (currentText == null || currentText.Length < 1)
            {
                yield break;
            }

            textUi.gameObject.SetActive(true);
            textUi.SetText("");
            PlayStarted?.Invoke();
            for (int i = 0; i < currentText.Length; ++i)
            {
                string nextCharacter = currentText.Substring(i, 1);
                textUi.SetText($"{textUi.text}{nextCharacter}");
                if (pauseTokens.ContainsKey(i))
                {
                    PlayPaused?.Invoke();
                    yield return YieldInstructionCache.WaitForSeconds(pauseTokens[i]);
                }
                else
                {
                    PlayResumed?.Invoke();
                    yield return YieldInstructionCache.WaitForSeconds(characterDelay);
                }
            }
            PlayFinished?.Invoke();
            playCoroutine = null;
        }

        private void Update()
        {
            // hacky hacky for resonance
            return;
        }

        [ContextMenu("Test Play")]
        private void DebugTestPlay()
        {
            SetText(testText);
            Play();
        }
    }
}