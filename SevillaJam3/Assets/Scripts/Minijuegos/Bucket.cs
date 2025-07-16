using System;
using System.Collections;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    const float travelDistance = 3.5f;

    public GameObject bucket;
    public GameObject heldBucket;
    public Transform drop;
    public Transform centerPoint;
    private int direction = 1;
    private int maxSpeed = 9;
    private int speed;

    private bool success;
    public static event Action Success;

    private void OnEnable()
    {
        heldBucket.transform.localPosition = Vector3.zero;
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

        yield return StartCoroutine(MoveBucketY(-1.7f, 0.25f));
        yield return StartCoroutine(MoveBucketY(0f, 0.25f));

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
        if (bucket.transform.localPosition.x <= drop.localPosition.x + 0.9f && bucket.transform.localPosition.x >= drop.localPosition.x - 0.9f)
        {
            success = true;
            Success.Invoke();
        }
        else
        {
            AudioManager.instance.PlayColocarCosaMal();
        }
    }

    IEnumerator MoveBucketY(float targetY, float duration)
    {
        Vector3 startPos = heldBucket.transform.localPosition;
        Vector3 endPos = new Vector3(startPos.x, targetY, startPos.z);
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            heldBucket.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        heldBucket.transform.localPosition = endPos;
    }
}
