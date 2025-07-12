using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MiniGameManager : MonoBehaviour
{
    public Image AudienceBar;
    public GameObject buttons;
    public Button MGB1;
    public Button MGB2;
    public Button MGB3;
    public Button MGB4;
    public Button MGB5;
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

    public Button[] eventButtons = new Button[0];

    bool tutorialMinigameBeat;
    bool minigameActive;
    bool clickgameActive;
    int lastGame = -1;

    float audience = 0.5f;
    float audienceGain = 0.03f;
    float audienceLoss = 0.04f;
    float audienceMegaLoss = 0.08f;

    public float minTime = 12f;
    public float maxTime = 17f;

    public static event Action TutorialDone;
    public static event Action<int> BeginMinigame;
    public static event Action EndMinigame;

    private void OnEnable()
    {
        Dynamo.Success += Success;
        Fuses.Success += Success;
        Radio.Success += Success;
        Bucket.Success += Success;
        Map.Success += Success;
    }

    private void OnDisable()
    {
        Dynamo.Success -= Success;
        Fuses.Success -= Success;
        Radio.Success -= Success;
        Bucket.Success -= Success;
        Map.Success -= Success;
    }

    private void Start()
    {
        StartCoroutine(ActivateMiniGame());
        for (int i = 0; i < eventButtons.Length; i++)
        {
            eventButtons[i].interactable = false;
        }
    }

    private void Update()
    {
        if (minigameActive)
        {
            if(tutorialMinigameBeat) audience -= audienceLoss * Time.deltaTime;
        }
        else
        {
            audience += audienceGain * Time.deltaTime;
        }
        if(clickgameActive) audience -= audienceMegaLoss * Time.deltaTime;

        audience = Math.Clamp(audience, 0, 1);
        AudienceBar.fillAmount = audience;

        AudienceBar.color = Color.Lerp(Color.red, Color.green, audience);
    }

    IEnumerator ActivateMiniGame()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minTime, maxTime));

        minTime = Math.Max(minTime - 1, 3f);
        maxTime = Math.Max(maxTime - 1, 4f);

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
        }

        minigameActive = true;
    }

    public void PlayMiniGame(int selectMiniGame)
    {
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
        }
    }

    void Success()
    {
        minigameActive = false;
        Invoke("RealSuccess", 1f);
    }

    void RealSuccess()
    {
        StartCoroutine(ActivateMiniGame());

        scene.SetActive(true);
        buttons.SetActive(true);

        MGB1.interactable = false;
        MGB2.interactable = false;
        MGB3.interactable = false;
        MGB4.interactable = false;
        MGB5.interactable = false;
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

        if (!tutorialMinigameBeat)
        {
            tutorialMinigameBeat = true;
            TutorialComplete();
            TutorialDone.Invoke();
        }
    }

    void TutorialComplete()
    {
        InvokeRepeating("ActivateEvent", 20f, UnityEngine.Random.Range(30f, 40f));
    }

    void ActivateEvent()
    {
        clickgameActive = true;
        int randIndex = UnityEngine.Random.Range(0, eventButtons.Length);
        eventButtons[randIndex].interactable = true;
        BeginMinigame.Invoke(randIndex);
    }

    public void DeactivateEvent()
    {
        for (int i = 0; i < eventButtons.Length; i++)
        {
            eventButtons[i].interactable = false;
        }
        clickgameActive = false;
        EndMinigame.Invoke();
    }
}
