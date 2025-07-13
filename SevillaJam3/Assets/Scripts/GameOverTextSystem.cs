using NUnit.Framework.Interfaces;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverTextSystem : MonoBehaviour
{
    public TextAsset[] endings = new TextAsset[0];
    public string[] endingTextLine;
    private int speechIndex = 0;
    public TextMeshProUGUI textOutput;
    public GameObject gameOverText;
    bool nextText;

    public GameObject badEndingImage;
    public GameObject goodEndingImage;
    public GameObject gameOverImage;

    void Start()
    {
        badEndingImage.SetActive(false);
        goodEndingImage.SetActive(false);
        gameOverImage.SetActive(false);
        gameOverText.SetActive(false);

        if(GameOverScene.GameOverIndex != -1)
        {
            endingTextLine = endings[GameOverScene.GameOverIndex].text.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            switch (GameOverScene.GameOverIndex)
            {
                case 0:
                    badEndingImage.SetActive(true);
                    break;
                case 1:
                    goodEndingImage.SetActive(true);
                    break;
                case 2:
                    gameOverImage.SetActive(true);
                    break;
            }
        }
        StartCoroutine(WriteGameOverLine());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && nextText)
        {
            nextText = false;
            StartCoroutine(WriteGameOverLine());
        }
    }

    IEnumerator WriteGameOverLine()
    {
        if (speechIndex >= endingTextLine.Length)
        {
            nextText = false;
            textOutput.text = "";
            gameOverText.SetActive(true);
            yield break;
        }

        string line = endingTextLine[speechIndex];
        float duration = 1f;
        float delayPerChar = line.Length > 0 ? duration / line.Length : 0f;

        for (int i = 0; i < line.Length + 1; i++)
        {
            textOutput.text = line.Substring(0, i);
            yield return new WaitForSeconds(delayPerChar);
        }

        nextText = true;
        
        speechIndex++;
    }
}
