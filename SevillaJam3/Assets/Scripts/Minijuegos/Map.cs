using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform[] hangPositions = new Transform[0];

    private void OnEnable()
    {
        hangPositions[0].localPosition = new Vector3(UnityEngine.Random.Range(-2.3f, -1.8f), UnityEngine.Random.Range(-2f, 2.5f), 0f);
        hangPositions[1].localPosition = new Vector3(UnityEngine.Random.Range(-.75f, .76f), UnityEngine.Random.Range(-2f, 2.5f), 0f);
        hangPositions[2].localPosition = new Vector3(UnityEngine.Random.Range(1.9f, 2.4f), UnityEngine.Random.Range(-2f, 2.5f), 0f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
