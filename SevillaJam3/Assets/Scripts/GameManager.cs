using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const float maxAudienceMeter = 20;

    public GameObject settingsMenu;
    public float audienceMeter = maxAudienceMeter;

    public void QuitGame()
    {
        AudioManager.instance.PlayClick();
        SceneManager.LoadScene(0);
    }

    public void OpenSettings()
    {
        AudioManager.instance.PlayClick();
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        AudioManager.instance.PlayClick();
        settingsMenu.SetActive(false);
    }

    public float GetAudienceMeter()
    {
        return audienceMeter;
    }

    public void SetAudienceMeter(float quantity)
    {
        if (audienceMeter + quantity <= maxAudienceMeter)
        {
            audienceMeter += quantity;
        }
        IsAudienceGone();
    }

    bool IsAudienceGone()
    {
        Debug.Log("se acabó el jogo");
        return GetAudienceMeter() <= 0.0f;
    }
}
