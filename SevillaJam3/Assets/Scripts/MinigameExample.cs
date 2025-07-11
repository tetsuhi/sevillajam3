using UnityEngine;
using UnityEngine.InputSystem;

public class MinigameExample : MonoBehaviour
{

    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        if (Input.GetButtonDown("space"))
        {
            gameManager.SetAudienceMeter(-20.0f);
        }
    }
}
