using System.Collections;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class TextSystem : MonoBehaviour
{
    public TextMeshProUGUI textOutput;
    public TextAsset speechFile;
    public string continueText;
    public string miniGame1;
    public string miniGame2;
    public string miniGame3;
    public string miniGame4;
    public string miniGame5;
    public string miniGameEx;

    private string[] speechText;
    private int speechIndex;
    private Coroutine textCoroutine;

    private void OnEnable()
    {
        MiniGameManager.BeginMinigame += BeginMinigame;
        MiniGameManager.EndMinigame += EndMinigame;
    }
    private void OnDisable()
    {
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
                StartCoroutine(MiniGameText(miniGame1));
                break;
            case 1:
                StartCoroutine(MiniGameText(miniGame2));
                break;
            case 2:
                StartCoroutine(MiniGameText(miniGame3));
                break;
            case 3:
                StartCoroutine(MiniGameText(miniGame4));
                break;
            case 4:
                StartCoroutine(MiniGameText(miniGame5));
                break;
            default:
                StartCoroutine(MiniGameText(miniGameEx));
                break;
        }
    }

    void EndMinigame()
    {
        StartCoroutine(ResumeSpeech());
    }

    IEnumerator WriteSpeechLine()
    {
        if (speechText.Length <= speechIndex)
        {
            Debug.Log("Has ganado");
        }
        else
        {
            string line = speechText[speechIndex];
            float duration = 1f;
            float delayPerChar = line.Length > 0 ? duration / line.Length : 0f;

            for (int i = 0; i < line.Length + 1; i++)
            {
                textOutput.text = line.Substring(0, i);
                yield return new WaitForSeconds(delayPerChar);
            }

            yield return new WaitForSeconds(2f);

            speechIndex++;
            textCoroutine = StartCoroutine(WriteSpeechLine());
        }
    }

    IEnumerator ResumeSpeech()
    {
        float duration = 1f;
        float delayPerChar = continueText.Length > 0 ? duration / continueText.Length : 0f;

        for (int i = 0; i < continueText.Length + 1; i++)
        {
            textOutput.text = continueText.Substring(0, i);
            yield return new WaitForSeconds(delayPerChar);
        }

        yield return new WaitForSeconds(2f);

        textCoroutine = StartCoroutine(WriteSpeechLine());
    }

    IEnumerator MiniGameText(string text)
    {
        float duration = 0.2f;
        float delayPerChar = text.Length > 0 ? duration / text.Length : 0f;

        for (int i = 0; i < text.Length + 1; i++)
        {
            textOutput.text = text.Substring(0, i);
            yield return new WaitForSeconds(delayPerChar);
        }
    }
}
