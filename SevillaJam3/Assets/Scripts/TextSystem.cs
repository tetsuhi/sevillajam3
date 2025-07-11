using System.Collections;
using System.Transactions;
using TMPro;
using UnityEngine;

public class TextSystem : MonoBehaviour
{
    public string[] textLines = new string[0];
    public TextMeshPro textOutput;
    public int textIndex = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textOutput.text = "";
        //StartCoroutine(WriteTextLine());
        ResumeDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    textOutput.text = "";
        //    textIndex += 1;
        //    WriteTextLine();
        //}
    }

    IEnumerator WriteTextLine(int lineIndex)
    {
        for (int i = 0; i < textLines[lineIndex].Length + 1; i++)
        {
            textOutput.text = textLines[lineIndex].Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
        Debug.Log("he terminado de escribir una frase");
    }

    void ResumeDialogue()
    {
        StartCoroutine(WriteTextLine(0));
    }
}
