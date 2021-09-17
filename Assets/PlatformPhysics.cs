using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPhysics : MonoBehaviour {

    public GameObject player;
    public BasePlayerMovement playerMovement;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("TestCharacter");
        playerMovement = GameObject.Find("TestCharacter").GetComponent<BasePlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {



	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerMovement.isGrounded = true;
            Debug.Log("Player is grounded.");
        }
    }
}
