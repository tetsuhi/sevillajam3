using System;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform[] hangPositions;
    public Collider2D[] colliders;

    private bool[] slotUsed;
    private float snapThreshold = 0.4f;

    Collider2D currentItem;
    Vector3 offset;

    public static event Action Success;

    private void OnEnable()
    {
        hangPositions[0].localPosition = new Vector3(UnityEngine.Random.Range(-2.3f, -1.8f), UnityEngine.Random.Range(-1.5f, 2.5f), 0f);
        hangPositions[1].localPosition = new Vector3(UnityEngine.Random.Range(-.75f, .76f), UnityEngine.Random.Range(-1.5f, 2.5f), 0f);
        hangPositions[2].localPosition = new Vector3(UnityEngine.Random.Range(1.9f, 2.4f), UnityEngine.Random.Range(-1.5f, 2.5f), 0f);

        foreach (Collider2D c in colliders)
        {
            c.enabled = true;
            c.GetComponent<SpriteRenderer>().color = Color.red;
            Vector3 pos = c.transform.localPosition;
            pos.y = -3.5f;
            c.transform.localPosition = pos;
        }

        slotUsed = new bool[hangPositions.Length];
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hit = Physics2D.OverlapPoint(mousePosition);
            if (hit != null)
            {
                foreach (Collider2D c in colliders)
                {
                    currentItem = hit;
                    offset = currentItem.transform.position - mousePosition;
                    break;
                }
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            currentItem = null;
        }

        if (currentItem != null && Input.GetMouseButton(0))
        {
            currentItem.transform.position = mousePosition + offset;

            for (int i = 0; i < hangPositions.Length; i++)
            {
                if (slotUsed[i]) continue;

                float dist = Vector2.Distance(currentItem.transform.position, hangPositions[i].position);
                if (dist < snapThreshold)
                {
                    currentItem.transform.position = hangPositions[i].position;
                    currentItem.enabled = false;
                    currentItem.GetComponent<SpriteRenderer>().color = Color.green;
                    slotUsed[i] = true;
                    currentItem = null;
                    AllSlotsUsed();
                    break;
                }
            }
        }
    }

    private void AllSlotsUsed()
    {
        foreach (bool occupied in slotUsed)
        {
            if (!occupied)
                return;
        }
        Success.Invoke();
    }
}
