using System;
using System.Collections;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    const float travelDistance = 3.5f;

    public GameObject bucket;
    public Transform drop;
    public Transform centerPoint;
    public int direction = 1;
    public int speed = 4;

    private Coroutine bucketCoroutine;

    private bool success;
    public static event Action<bool> Success;

    private void OnEnable()
    {
        success = false;
        drop.localPosition = Vector3.right * UnityEngine.Random.Range(-3.5f, 3.6f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
            speed = 0;
            bucketCoroutine = StartCoroutine("PauseBucket");
            CheckCompletion();
        }
    }

    IEnumerator PauseBucket()
    {
        yield return new WaitForSeconds(.5f);
        speed = 4;
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
            Debug.Log("has recogido gotas :)");
            success = true;
            Invoke("Completed", 1f);
        }
    }

    void Completed()
    {
        Success.Invoke(true);
    }
}
