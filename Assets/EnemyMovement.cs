using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float moveSpeed = 1f;
    Rigidbody2D enemyBodyRigidBody;
    BoxCollider2D enemyFeetCollider;

    void Start()
    {
        enemyBodyRigidBody = GetComponent<Rigidbody2D>();
        enemyFeetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsFacingRight())
        {
            enemyBodyRigidBody.velocity = new Vector2(moveSpeed, 0f);
        } else
        {
            enemyBodyRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyBodyRigidBody.velocity.x)), 1f);
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
}
