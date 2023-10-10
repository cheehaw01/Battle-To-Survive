using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    Animator anim;
    Player player;
    private float cooldownTimer = Mathf.Infinity;

    private string CAST_ANIMATION = "Cast";

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && cooldownTimer > attackCoolDown && player.canCast())
            Cast();
        cooldownTimer += Time.deltaTime;
    }

    private void Cast()
    {
        anim.SetTrigger(CAST_ANIMATION);
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
