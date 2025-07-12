using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    public Button radioButton;
    public Slider radioSlider;
    public Image radioSliderHandle;
    public Image radioWheel;

    private bool canGrabWheel;
    private bool isDragging;
    private float totalAngle;
    private Vector2 lastDirection;
    private Vector2 centerScreenPoint;

    private List<int> randomList = new List<int>();

    public static event Action<bool> Success;

    void OnEnable()
    {
        radioButton.GetComponent<Image>().color = Color.gray;
        radioSliderHandle.color = Color.gray;
        radioWheel.color = Color.gray;

        radioSlider.value = radioSlider.maxValue;
        totalAngle = 0;

        radioButton.interactable = false;
        radioSlider.interactable = false;
        canGrabWheel = false;

        randomList = GenerateRandomList();
        ActiveItem();
    }

    private List<int> GenerateRandomList()
    {
        List<int> numbers = new List<int> { 0, 1, 2 };

        for (int i = numbers.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }

        return numbers;
    }

    private void ActiveItem()
    {
        if(randomList.Count == 0)
        {
            Invoke("Completed", 1f);
            return;
        }

        int item = randomList[0];
        randomList.RemoveAt(0);

        switch (item)
        {
            case 0:
                radioButton.interactable = true;
                radioButton.GetComponent<Image>().color = Color.red;
                break;
            case 1:
                radioSlider.interactable = true;
                radioSliderHandle.color = Color.red;
                break;            
            case 2:
                canGrabWheel = true;
                radioWheel.color = Color.red;
                break;
        }
    }

    public void RadioButtonPressed()
    {
        radioButton.GetComponent<Image>().color = Color.green;
        radioButton.interactable = false;

        ActiveItem();
    }

    public void RadioSlider()
    {
        if (radioSlider.value == 0)
        {
            radioSliderHandle.color = Color.green;
            radioSlider.interactable = false;

            ActiveItem();
        }
    }

    private void Update()
    {
        if (canGrabWheel)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(radioWheel.rectTransform, Input.mousePosition))
                {
                    isDragging = true;
                    lastDirection = GetMouseDirectionFromPivot();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            if (isDragging)
            {
                Vector2 currentDirection = GetMouseDirectionFromPivot();
                float angleDelta = Vector2.SignedAngle(lastDirection, currentDirection);

                radioWheel.rectTransform.Rotate(Vector3.forward, angleDelta);
                lastDirection = currentDirection;

                totalAngle += Mathf.Abs(angleDelta);

                if (totalAngle >= 360f)
                {
                    isDragging = false;
                    canGrabWheel = false;
                    radioWheel.color = Color.green;
                    ActiveItem();
                }
            }
        }
    }

    Vector2 GetMouseDirectionFromPivot()
    {
        Vector2 pivotScreenPos = RectTransformUtility.WorldToScreenPoint(null, radioWheel.rectTransform.position);
        Vector2 mouseScreenPos = Input.mousePosition;
        Vector2 dir = (mouseScreenPos - pivotScreenPos).normalized;

        return dir;
    }

    void Completed()
    {
        Success.Invoke(true);
    }
}
