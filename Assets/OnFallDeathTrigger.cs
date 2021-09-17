using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFallDeathTrigger : MonoBehaviour
{

    public GameObject player;
    public GameObject zuliumExplosion;
    public GameObject zuliumImplosion;
    public PlayerStats playerStats;
    public BasePlayerMovement playerMovement;
    public Transform spawnPoint;
    public float postDeathTimer = 0;
    public int postDeathWaitTime = 3;
    public bool playerDeathReset = false;
    public bool resetReady = false;
    public Camera playerCamera;
    public Camera extraCamera;
    public bool regularDeath;
    public bool fallDeath;


    // Start is called before the first frame update
    void Start()
    {
        playerCamera.enabled = true;
        player = GameObject.Find("TestCharacter");
        playerStats = player.GetComponent<PlayerStats>();
        playerMovement = player.GetComponent<BasePlayerMovement>();
        extraCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDeathReset)
        {
            postDeathTimer += Time.deltaTime;

            if (postDeathTimer >= postDeathWaitTime)
            {
                postDeathTimer = 0;
                playerDeathReset = false;
                resetReady = true;
            }

            if (resetReady)
            {
               PlayerFallDeath();
               ResetStats();    
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(zuliumExplosion, other.transform.position, Quaternion.identity);
            extraCamera = GameObject.Find("ExtraCamera").GetComponent<Camera>();
            extraCamera.enabled = true;
            player.SetActive(false);
            playerDeathReset = true;
            fallDeath = true;
        }
    }

    public void PlayerDeath()
    {
        Debug.Log("Player Is Dead");
        GetComponent<Transform>().position = spawnPoint.position;
        this.gameObject.SetActive(true);
        playerCamera.enabled = true;
        extraCamera.enabled = false;
    }

    public void PlayerFallDeath()
    {
        //reseting player with an implosion and reseting the cameras
        Debug.Log("Player Is Dead");
        Instantiate(zuliumImplosion, spawnPoint.transform.position, Quaternion.identity);
        player.GetComponent<Transform>().position = spawnPoint.position;
        player.SetActive(true);
        playerCamera.enabled = true;
        extraCamera.enabled = false;
    }

    public void ResetStats()
    {
        //resetting player stats
        playerStats.currentPlayerHealth = playerStats.maxPlayerHealth;
        playerMovement.sprintTime = playerMovement.originalSprintTime;
        playerMovement.wallClimbTime = playerMovement.originalWallClimbTime;
    }

}
