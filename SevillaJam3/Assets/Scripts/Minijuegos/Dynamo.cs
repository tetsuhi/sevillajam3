using System;
using UnityEngine;
using UnityEngine.UI;

public class Dynamo : MonoBehaviour
{
    public Transform pivot;
    public Transform handle;
    public Transform lever;

    public Slider energyBar;
    public Image fillImage;

    public GameObject audioDynamo;

    private float completion = 0f;
    private float dischargeRate = 0.3f;
    private float chargeRate = 0.25f;

    private bool isDragging = false;
    private Vector2 lastDirection;

    private bool succes;
    public static event Action Success;

    private void OnEnable()
    {
        completion = 0;
        energyBar.value = completion;
        succes = false;
    }

    void Update()
    {
        if (!succes)
        {
            HandleInput();
            EnergyBar();
            UpdateBarColor();
        }
        audioDynamo.SetActive(isDragging);
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == handle)
                {
                    isDragging = true;
                    lastDirection = GetMouseDirectionFromPivot();
                }
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

            pivot.Rotate(Vector3.forward, angleDelta);

            lastDirection = currentDirection;

            float frameCharge = (Mathf.Abs(angleDelta) / 360f) * chargeRate;

            completion += frameCharge;
            completion = Mathf.Clamp01(completion);
            energyBar.value = completion;

            if (completion >= 1f)
            {
                succes = true;
                isDragging = false;
                Success.Invoke();
            }
        }
    }

    Vector2 GetMouseDirectionFromPivot()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 ejeScreenPos = Camera.main.WorldToScreenPoint(pivot.position);

        Vector2 dir = (mousePos - ejeScreenPos).normalized;
        return dir;
    }

    void EnergyBar()
    {
        if (completion > 0)
        {
            completion -= dischargeRate * Time.deltaTime;
            completion = Mathf.Clamp01(completion);
            energyBar.value = completion;
        }
    }

    void UpdateBarColor()
    {
        if (completion <= 0.5f)
        {
            fillImage.color = Color.Lerp(Color.red, Color.yellow, completion / 0.5f);
        }
        else
        {
            fillImage.color = Color.Lerp(Color.yellow, Color.green, (completion - 0.5f) / 0.5f);
        }
    }
}
