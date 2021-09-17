using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int maxPlayerHealth;
    public int currentPlayerHealth;
    public int basePlayerStrength;
    public BasePlayerMovement playerMovement;
    public OnFallDeathTrigger deathTrigger;
    public GameObject zuliumExplosion;
    public GameObject zuliumImplosion;
    public Transform spawnPoint;
    public Camera playerCamera;
    public Camera extraCamera;




    // Use this for initialization
    void Start ()
    {
        playerMovement = this.gameObject.GetComponent<BasePlayerMovement>();
        deathTrigger = GameObject.Find("FallDeathTrigger").GetComponent<OnFallDeathTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayerHealth <= 0)
        {
            ZuliumExplosion();
            extraCamera = GameObject.Find("ExtraCamera").GetComponent<Camera>();
            extraCamera.enabled = true;
            this.gameObject.SetActive(false);
            deathTrigger.regularDeath = true;
            deathTrigger.playerDeathReset = true;
        }

        if(deathTrigger.resetReady)
        {
            RegularDeathReset();
        }
    }

    public void ZuliumExplosion()
    {
        Instantiate(zuliumExplosion, this.gameObject.transform.position, Quaternion.identity);
    }

    public void ZuliumImplosion()
    {
        Instantiate(zuliumImplosion, this.spawnPoint.transform.position, Quaternion.identity);
    }

    public void RegularDeathReset()
    {
        Debug.Log("Player Is Dead");
        ZuliumImplosion();
        this.gameObject.GetComponent<Transform>().position = spawnPoint.position;
        this.gameObject.SetActive(true);
        playerCamera.enabled = true;
        extraCamera.enabled = false;
        deathTrigger.resetReady = false;
    }

}
