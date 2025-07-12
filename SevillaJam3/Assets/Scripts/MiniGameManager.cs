using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MiniGameManager : MonoBehaviour
{
    public GameObject buttons;
    public Button MGB1;
    public Button MGB2;
    public Button MGB3;
    public Button MGB4;
    public Button MGB5;
    public Button MGBEx;
    public GameObject scene;
    public GameObject MGUI1;
    public GameObject MG1;
    public GameObject MGUI2;
    public GameObject MG2;
    public GameObject MGUI3;
    public GameObject MG3;
    public GameObject MGUI4;
    public GameObject MG4;
    public GameObject MGUI5;
    public GameObject MG5;   
    public GameObject MGUIEx;
    public GameObject MGEx;

    int lastGame = -1;

    public static event Action<int> BeginMinigame;
    public static event Action EndMinigame;

    private void OnEnable()
    {
        Dynamo.Success += Success;
        Fuses.Success += Success;
        Radio.Success += Success;
    }
    private void OnDisable()
    {
        Dynamo.Success -= Success;
        Fuses.Success -= Success;
        Radio.Success -= Success;
    }

    private void Start()
    {
        StartCoroutine(ActivateMiniGame());
    }

    IEnumerator ActivateMiniGame()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 4f));

        int selectMiniGame = UnityEngine.Random.Range(0, 5);

        if(lastGame == -1)
        {
            lastGame = selectMiniGame;
        }
        else if(lastGame == selectMiniGame)
        {
            selectMiniGame++;
            lastGame = selectMiniGame = selectMiniGame % 5;
        }

        EnableMiniGameSelection(selectMiniGame);
    }

    private void EnableMiniGameSelection(int selectMiniGame)
    {
        switch(selectMiniGame)
        {
            case 0:
                MGB1.interactable = true;
                break;
            case 1:
                MGB2.interactable = true;
                break;
            case 2:
                MGB3.interactable = true;
                break;
            case 3:
                MGB4.interactable = true;
                break;
            case 4:
                MGB5.interactable = true;
                break;
            default:
                MGBEx.interactable = true;
                break;
        }
    }

    public void PlayMiniGame(int selectMiniGame)
    {
        BeginMinigame.Invoke(selectMiniGame);

        scene.SetActive(false);
        buttons.SetActive(false);

        switch (selectMiniGame)
        {
            case 0:
                MGUI1.SetActive(true);
                MG1.SetActive(true);
                break;
            case 1:
                MGUI2.SetActive(true);
                MG2.SetActive(true);
                break;
            case 2:
                MGUI3.SetActive(true);
                MG3.SetActive(true);
                break;
            case 3:
                MGUI4.SetActive(true);
                MG4.SetActive(true);
                break;
            case 4:
                MGUI5.SetActive(true);
                MG5.SetActive(true);
                break;
            default:
                MGUIEx.SetActive(true);
                MGEx.SetActive(true);
                break;
        }
    }

    void Success(bool value)
    {
        StartCoroutine(ActivateMiniGame());
        EndMinigame.Invoke();

        if (value)
        {
        }
        else
        {
        }

        scene.SetActive(true);
        buttons.SetActive(true);

        MGB1.interactable = false;
        MGB2.interactable = false;
        MGB3.interactable = false;
        MGB4.interactable = false;
        MGB5.interactable = false;
        MGBEx.interactable = false;
        MGUI1.SetActive(false);
        MG1.SetActive(false);
        MGUI2.SetActive(false);
        MG2.SetActive(false);
        MGUI3.SetActive(false);
        MG3.SetActive(false);
        MGUI4.SetActive(false);
        MG4.SetActive(false);
        MGUI5.SetActive(false);
        MG5.SetActive(false);
        MGUIEx.SetActive(false);
        MGEx.SetActive(false);
    }
}
