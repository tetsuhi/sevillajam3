using System;
using System.Collections;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    const float travelDistance = 3.5f;

    public GameObject bucket;
    public Transform drop;
    public Transform centerPoint;
    private int direction = 1;
    private int maxSpeed = 9;
    private int speed;

    private bool success;
    public static event Action<bool> Success;

    private void OnEnable()
    {
        speed = maxSpeed;
        success = false;
        drop.localPosition = Vector3.right * UnityEngine.Random.Range(-3.5f, 3.5f);
    }

    void Update()
    {
        if (!success)
        {
            Inputs();
            bucket.transform.position += Vector3.right * direction * speed * Time.deltaTime;
            HasReachedBorder();
        }
    }

    void Inputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine("PauseBucket");
            CheckCompletion();
        }
    }

    IEnumerator PauseBucket()
    {
        speed = 0;
        yield return new WaitForSeconds(.5f);
        speed = maxSpeed;
    }
    void HasReachedBorder()
    {
        if(Math.Abs(bucket.transform.localPosition.x) >= travelDistance)
        {
            direction *= -1;
        }
    }

    void CheckCompletion()
    {
        if (bucket.transform.localPosition.x <= drop.localPosition.x + 0.5f && bucket.transform.localPosition.x >= drop.localPosition.x - 0.5f)
        {
            success = true;
            Invoke("Completed", 1f);
        }
    }

    void Completed()
    {
        Success.Invoke(true);
    }
}
