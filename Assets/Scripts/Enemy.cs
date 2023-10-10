using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // stats
    //[SerializeField] 
    //private int maxHealth = 100;

    //[SerializeField]
    //private int health = 100;

    //[SerializeField]
    //private int attack = 10;

    //[SerializeField]
    //private int defense = 10;

    //[SerializeField]
    //private float moveForce = 10f;

    //[SerializeField]
    //private float jumpForce = 5f;

    //private bool isStunned = false;

    //private string ATTACKED_AREA = "AttackCollider";

    private string HURT_ANIMATION = "Hurt";

    private Animator anim;
    private Rigidbody2D myBody;
    // private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        // sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(Vector2 attackPos)
    {

        anim.SetTrigger(HURT_ANIMATION);

        int direction;
        if (attackPos.x <= transform.position.x)
        {
            direction = 1;
        } 
        else
        {
            direction = -1;
        }

        float knockedBackForce = 0.9f;
        transform.position += new Vector3(direction, 0.5f, 0f) * knockedBackForce * Time.deltaTime;
        //myBody.AddForce(new Vector2(direction * knockedBackForce, 0f), ForceMode2D.Impulse);
    }

}
