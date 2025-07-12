using UnityEngine;
using UnityEngine.UI;

public class PointAndClickMinigame : MonoBehaviour
{
    public Button[] eventButtons = new Button[0];

    void Start()
    {
        for (int i = 0; i < eventButtons.Length; i++)
        {
            eventButtons[i].interactable = false;
        }
        InvokeRepeating("ActivateEvent", 40f, UnityEngine.Random.Range(30f, 41f));
    }

    void ActivateEvent()
    {
        int randIndex = UnityEngine.Random.Range(0, eventButtons.Length);
        eventButtons[randIndex].interactable = true;
    }

    public void DeactivateEvent(int index)
    {
        eventButtons[index].interactable = false;
    }
}
