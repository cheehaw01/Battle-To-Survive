using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private CapsuleCollider2D capsuleCollider;
    private bool dead = false;

    private string HURT_ANIMATION = "Hurt";
    private string DIE_ANIMATION = "Die";
    private string ENEMY_TAG = "Enemy";

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger(HURT_ANIMATION);
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger(DIE_ANIMATION);
                GetComponent<Player>().enabled = false;
                dead = true;
            }
        }
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E)) 
        //    TakeDamage(1);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            TakeDamage(0.5f);
        }
    }
}
