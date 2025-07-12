using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fuses : MonoBehaviour
{
    public Slider[] sliders = new Slider[0];
    public Image handle1;
    public Image handle2;
    public Image handle3;
    public Image handle4;

    private bool success1;
    private bool success2;
    private bool success3;
    private bool success4;
    public static event Action<bool> Success;

    private void OnEnable()
    {
        success1 = false;
        success2 = false;
        success3 = false; 
        success4 = false;

        handle1.color = Color.red;
        handle2.color = Color.red;
        handle3.color = Color.red;
        handle4.color = Color.red;
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].interactable = true;
            int randomValue = UnityEngine.Random.Range(1, 5);
            int chanceOfNegative = UnityEngine.Random.Range(0, 2);
            if(chanceOfNegative == 0)
            {
                randomValue *= -1;
            }
            sliders[i].value = randomValue;
        }
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void CheckSlider1Value()
    {
        if (sliders[0].value == 0)
        {
            sliders[0].interactable = false;
            handle1.color = Color.green;
            success1 = true;
            CheckCompletion();
        }
    }
    public void CheckSlider2Value()
    {
        if (sliders[1].value == 0)
        {
            sliders[1].interactable = false;
            handle2.color = Color.green;
            success2 = true;
            CheckCompletion();
        }
    }
    public void CheckSlider3Value()
    {
        if (sliders[2].value == 0)
        {
            sliders[2].interactable = false;
            handle3.color = Color.green;
            success3 = true;
            CheckCompletion();
        }
    }
    public void CheckSlider4Value()
    {
        if (sliders[3].value == 0)
        {
            sliders[3].interactable = false;
            handle4.color = Color.green;
            success4 = true;
            CheckCompletion();
        }
    }

    void CheckCompletion()
    {
        if (success1 && success2 && success3 && success4)
        {
            Invoke("Completed", 1f);
        }
    }

    void Completed()
    {
        Success.Invoke(true);
    }
}
