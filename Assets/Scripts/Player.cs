using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public delegate void PlayerAttacking(bool isAttacking);
    public static event PlayerAttacking PlayerIsAttacking;

    // stats
    //[SerializeField] 
    //private int maxHealth = 100;

    //[SerializeField]
    //private int health = 100;

    //[SerializeField]
    //private int attack = 10;

    //[SerializeField]
    //private int defense = 10;

    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 5f;

    [SerializeField]
    private Boolean doubleJump = true;

    [SerializeField]
    private LayerMask groundLayer;

    private bool isAttacking = false;
    private bool isStunned = false;
    //private bool isGrounded = true;

    private float movementX;
    private Animator anim;
    private SpriteRenderer sr;
    private Rigidbody2D myBody;
    private CapsuleCollider2D capsuleCollider;
    private string WALK_ANIMATION = "Walk";
    private string ATTACK_ANIMATION = "Attack";
    private string HURT_ANIMATION = "Hurt";

    //private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();

    }

    private void FixedUpdate() 
    {
        PlayerJump();
        PlayerAttack();
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer()
    {
        if (isStunned)
        {
            isStunned = false;
            StartCoroutine(StunWait());
        }

        if (movementX != 0) // moving either left or right
        {
            anim.SetBool(WALK_ANIMATION, true);
            transform.localScale = new Vector3(movementX, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }
    }

    void PlayerJump()
    {
        if (isGrounded() && doubleJump == false)
        {
            doubleJump = true;
        } 
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            //isGrounded = false;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
            //myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        if (Input.GetButtonDown("Jump") && doubleJump)
        {
            doubleJump = false;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
        }
    }

    void PlayerAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isAttacking = true;
            anim.SetTrigger(ATTACK_ANIMATION);
        }
        else 
        { 
            isAttacking = false;
        }
        PlayerIsAttacking(isAttacking);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag(GROUND_TAG))
        //{
        //    isGrounded = true;
        //}
        
        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            float knockBackForce = 4f;
            anim.SetTrigger(HURT_ANIMATION);
            isStunned = true;
            myBody.AddForce(new Vector2(-knockBackForce * transform.localScale.x, 8f), ForceMode2D.Impulse);
        }

    }

    private bool isGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, capsuleCollider.direction, 0, Vector2.down, 0.1f, groundLayer);
        return rayCastHit.collider != null;
    }

    public bool canCast()
    {
        return movementX == 0 && isGrounded();
    }

    IEnumerator StunWait()
    {
        float normalMoveForce = moveForce;
        moveForce -= normalMoveForce;
        yield return new WaitForSeconds(0.6f);
        moveForce += normalMoveForce;
    }

}
