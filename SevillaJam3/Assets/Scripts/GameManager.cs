using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject settingsMenu;

    public void QuitGame()
    {
#if !UNITY_WEBGL
        AudioManager.instance.PlayClick();
        SceneManager.LoadScene(0);
#endif
    }

    public void OpenSettings()
    {
        AudioManager.instance.PlayClick();
        settingsMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseSettings()
    {
        AudioManager.instance.PlayClick();
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
