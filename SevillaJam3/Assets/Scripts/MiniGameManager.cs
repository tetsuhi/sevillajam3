using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MiniGameManager : MonoBehaviour
{
    public Image audienceBar;
    public Image audienceIcon;
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

    public GameObject defaultForecaster;
    public GameObject baldForecaster;
    public GameObject duckForecaster;
    public GameObject fallForecaster;

    public Sprite[] audienceIcons;

    bool begin;
    bool tutorialMinigameBeat;
    bool minigameActive;
    bool clickgameActive;
    int lastGame = -1;
    int randIndex;

    float audience = 0.5f;
    float audienceGain = 0.03f;
    float audienceLoss = 0.05f;
    float audienceMegaLoss = 0.08f;

    private float minTime = 10f;
    private float maxTime = 14f;

    private Coroutine duckSound;

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
        TextSystem.TutorialBegin += ShowFirstMiniGame;
        TextSystem.TutorialStop += BeginTutorial;
    }

    private void OnDisable()
    {
        Dynamo.Success -= Success;
        Fuses.Success -= Success;
        Radio.Success -= Success;
        Bucket.Success -= Success;
        Map.Success -= Success;
        TextSystem.TutorialBegin -= ShowFirstMiniGame;
        TextSystem.TutorialStop -= BeginTutorial;
    }

    private void Start()
    {
        Image img = MGB1.GetComponent<Image>();
        Color c = img.color;
        c.a = 50f / 255f;
        img.color = c;

        MGB1.GetComponent<Image>().color = c;
        MGB2.GetComponent<Image>().color = c;
        MGB3.GetComponent<Image>().color = c;
        MGB4.GetComponent<Image>().color = c;
        MGB5.GetComponent<Image>().color = c;
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

        audience = Mathf.Clamp01(audience);
        audienceBar.fillAmount = audience;

        audienceBar.color = Color.Lerp(Color.red, Color.green, audience);

        int index = Mathf.FloorToInt(audience * audienceIcons.Length);
        if (index >= audienceIcons.Length) index = audienceIcons.Length - 1;

        audienceIcon.sprite = audienceIcons[index];
    }

    IEnumerator ActivateMiniGame()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minTime, maxTime));

        minTime = Math.Max(minTime - 1, 2f);
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
        AudioManager.instance.PlayActivarMinijuego();

        switch (selectMiniGame)
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
        if (!begin) return;

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

        MGB1.interactable = false;
        MGB2.interactable = false;
        MGB3.interactable = false;
        MGB4.interactable = false;
        MGB5.interactable = false;
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

    void ShowFirstMiniGame()
    {
        EnableMiniGameSelection(0);
    }

    void BeginTutorial()
    {
        begin = true;

        Image img = MGB1.GetComponent<Image>();
        Color c = img.color;
        c.a = 1;
        img.color = c;

        MGB1.GetComponent<Image>().color = c;
        MGB2.GetComponent<Image>().color = c;
        MGB3.GetComponent<Image>().color = c;
        MGB4.GetComponent<Image>().color = c;
        MGB5.GetComponent<Image>().color = c;
    }

    void TutorialComplete()
    {
        InvokeRepeating("ActivateEvent", 20f, UnityEngine.Random.Range(30f, 40f));
    }

    void ActivateEvent()
    {
        clickgameActive = true;
        randIndex = UnityEngine.Random.Range(0, 2);
        BeginMinigame.Invoke(randIndex);

        defaultForecaster.SetActive(false);
        switch (randIndex)
        {
            case 0:
                duckForecaster.SetActive(true);
                duckSound = StartCoroutine(DuckSound());
                break;
            case 1:
                baldForecaster.SetActive(true);
                AudioManager.instance.PlayRafagaViento();
                break;
            case 2:
                fallForecaster.SetActive(true); break;
        }
    }

    public void DeactivateEvent()
    {
        clickgameActive = false;
        EndMinigame.Invoke();

        defaultForecaster.SetActive(true);
        duckForecaster.SetActive(false);
        baldForecaster.SetActive(false);
        fallForecaster.SetActive(false);

        if(duckSound != null)
        {
            StopCoroutine(duckSound);
            duckSound = null;
        }
    }

    IEnumerator DuckSound()
    {
        AudioManager.instance.PlayPato();
        yield return new WaitForSeconds(2f);
        duckSound = StartCoroutine(DuckSound());
    }
}
