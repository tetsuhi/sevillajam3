using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int seconds = 0;
    public int minutes = 0;
    public TextMeshProUGUI timerText;

    void Start()
    {
        InvokeRepeating("IncreaseSeconds", 1f, 1f);
    }

    void IncreaseSeconds()
    {
        seconds += 1;
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        string secondsText = "00";
        string minutesText = "00";

        if (seconds >= 60)
        {
            minutes += 1;
            seconds = 0;
        }

        if (seconds < 10)
        {
            secondsText = "0" + seconds.ToString();
        }
        else
        {
            secondsText = seconds.ToString();
        }

        if (minutes < 10)
        {
            minutesText = "0" + minutes.ToString();
        }
        else
        {
            minutesText = minutes.ToString();
        }

        timerText.text = "Timer: " + minutesText + ":" + secondsText;

    }
}
