using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyDamage : MonoBehaviour
{
    //enemy body
    public GameObject enemyObject;
    public Rigidbody2D enemyRigidBody;
    public int collideDamage;
    //hero body
    public GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        enemyObject = GameObject.FindGameObjectWithTag("Enemy");
        playerObject = GameObject.FindGameObjectWithTag("Player");
        enemyRigidBody = enemyObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "TestCharacter")
        {
            playerObject.GetComponent<PlayerStats>().currentPlayerHealth = -enemyObject.GetComponent<BaseEnemyDamage>().collideDamage;
            Debug.Log("PlayerDamaged");
        }
    }

    public void DoDamageToPlayer()
    {
        playerObject.GetComponent<PlayerStats>().currentPlayerHealth =- enemyObject.GetComponent<BaseEnemyDamage>().collideDamage;
        Debug.Log("PlayerDamaged");
    }*/
}
