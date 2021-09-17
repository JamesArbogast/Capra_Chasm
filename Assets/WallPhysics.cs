using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPhysics : MonoBehaviour {

    public GameObject player;
    public BasePlayerMovement playerMovement;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("TestCharacter");
        playerMovement = GameObject.Find("TestCharacter").GetComponent<BasePlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {



    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "TestCharacter")
        {
            playerMovement.wallSliding = true;
            Debug.Log("Player is wall sliding");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "TestCharacter")
        {
            playerMovement.wallSliding = false;
            playerMovement.wallClimbing = false;
        }
    }
}
