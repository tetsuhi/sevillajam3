using NUnit.Framework.Interfaces;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverTextSystem : MonoBehaviour
{
    public TextAsset[] endings = new TextAsset[0];
    public string[] endingTextLine;
    public int selectedTextIndex = -1;
    private int speechIndex = 0;
    public TextMeshProUGUI textOutput;
    public GameObject gameOverText;
    bool nextText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverText.SetActive(false);
        if(selectedTextIndex != -1)
        {
            endingTextLine = endings[selectedTextIndex].text.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
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
