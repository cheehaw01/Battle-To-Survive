using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{

    private bool playerIsAttacking = false;
    private Vector3 tempScale;

    private string ENEMY_TAG = "Enemy";

    private BoxCollider2D boxCol;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnEnable()
    {
        Player.PlayerIsAttacking += PlayerAttack;
    }

    private void OnDisable()
    {
        Player.PlayerIsAttacking -= PlayerAttack;
    }

    public void PlayerAttack(bool isAttacking)
    {
        playerIsAttacking = isAttacking;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(ENEMY_TAG) && playerIsAttacking)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(transform.position);
        }
        
    }

}
