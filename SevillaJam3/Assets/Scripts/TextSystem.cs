using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextSystem : MonoBehaviour
{
    public TextMeshProUGUI textOutput;
    public TextAsset speechFile;
    public string continueText;
    public string duckText;
    public string hairText;
    public string chairText;

    private string[] speechText;
    private int speechIndex;
    private Coroutine textCoroutine;
    private int tutorialStop = 7;
    private int winGameIndex = 47;

    public static event Action TutorialStop;
    public static event Action GameOver;

    private void OnEnable()
    {
        MiniGameManager.TutorialDone += TutorialDone;
        MiniGameManager.BeginMinigame += BeginMinigame;
        MiniGameManager.EndMinigame += EndMinigame;
    }
    private void OnDisable()
    {
        MiniGameManager.TutorialDone -= TutorialDone;
        MiniGameManager.BeginMinigame -= BeginMinigame;
        MiniGameManager.EndMinigame -= EndMinigame;
    }

    void Start()
    {
        if (speechFile != null)
        {
            speechText = speechFile.text.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else
        {
            Debug.LogError("No se asignó el archivo de texto de diálogo.");
            return;
        }

        textOutput.text = "";
        textCoroutine = StartCoroutine(WriteSpeechLine());
    }

    void BeginMinigame(int index)
    {
        if(textCoroutine != null)
        {
            StopCoroutine(textCoroutine);
            textCoroutine = null;
        }

        switch (index)
        {
            case 0:
                StartCoroutine(MiniGameText(duckText));
                break;
            case 1:
                StartCoroutine(MiniGameText(hairText));
                break;
            case 2:
                StartCoroutine(MiniGameText(chairText));
                break;
        }
    }

    void EndMinigame()
    {
        StartCoroutine(ResumeSpeech());
    }

    void TutorialDone()
    {
        if(textCoroutine != null)
        {
            StopCoroutine(textCoroutine);
            textCoroutine = null;
        }
        textCoroutine = StartCoroutine(WriteSpeechLine());
    }

    IEnumerator WriteSpeechLine()
    {
        if (winGameIndex <= speechIndex)
        {
            GameOver.Invoke();
        }
        else
        {
            string line = speechText[speechIndex];
            float duration = 0.5f;
            float delayPerChar = line.Length > 0 ? duration / line.Length : 0f;

            string visibleText = "";
            int i = 0;

            while (i < line.Length)
            {
                if (line[i] == '<')
                {
                    int tagEnd = line.IndexOf('>', i);
                    if (tagEnd != -1)
                    {
                        string tag = line.Substring(i, tagEnd - i + 1);
                        visibleText += tag;
                        i = tagEnd + 1;
                        continue;
                    }
                }

                visibleText += line[i];
                textOutput.text = visibleText;

                i++;
                yield return new WaitForSeconds(delayPerChar);
            }

            if (tutorialStop - 1 == speechIndex)
            {
                TutorialStop.Invoke();
            }

            yield return new WaitForSeconds(3.5f);

            speechIndex++;

            if (tutorialStop != speechIndex)
            {
                textCoroutine = StartCoroutine(WriteSpeechLine());
            }
        }
    }

    IEnumerator ResumeSpeech()
    {
        float duration = 0.2f;
        float delayPerChar = continueText.Length > 0 ? duration / continueText.Length : 0f;

        for (int i = 0; i < continueText.Length + 1; i++)
        {
            textOutput.text = continueText.Substring(0, i);
            yield return new WaitForSeconds(delayPerChar);
        }

        yield return new WaitForSeconds(1f);

        if (textCoroutine != null)
        {
            StopCoroutine(textCoroutine);
            textCoroutine = null;
        }
        textCoroutine = StartCoroutine(WriteSpeechLine());
    }

    IEnumerator MiniGameText(string text)
    {
        float duration = 0.3f;
        float delayPerChar = text.Length > 0 ? duration / text.Length : 0f;

        for (int i = 0; i < text.Length + 1; i++)
        {
            textOutput.text = text.Substring(0, i);
            yield return new WaitForSeconds(delayPerChar);
        }
    }
}
