using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private Animator anim;
    private float timeBtwAttack;
    public float starttimeBtwAttack;
    private float timeBtwAttack1;
    public float starttimeBtwAttack1;
    public GameObject Fire;
    private Boss BossScript;
    private bool hasEnteredTrigger = false;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        BossScript = GetComponentInParent<Boss>();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.name == "TriggerAttackNear" && !hasEnteredTrigger)
            {
                if (timeBtwAttack <= 0)
                {
                    hasEnteredTrigger = true;
                    Boss.speed = 0f;
                    Boss.InTrigger = true;
                    anim.SetTrigger("Attack");
                    Invoke("timer", 1.5f);
                }
                else
                {
                    timeBtwAttack -= Time.deltaTime;
                }
            }
            else if (gameObject.name == "TriggerAttackDown")
            {
                if (timeBtwAttack1 <= 0)
                {
                    Boss.InTrigger = true;
                    Fire.SetActive(true);
                    BossScript.OnEnemyAttack();
                    Invoke("Deact", 1f);
                    timeBtwAttack1 = starttimeBtwAttack1;
                }
                else
                {
                    timeBtwAttack1 -= Time.deltaTime;
                }
            }
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.name == "TriggerAttackNear")
            {
                Boss.InTrigger = false;
            }
        }
    }
    void timer()
    {
        Boss.speed = 1.4f;
        hasEnteredTrigger = false;
        timeBtwAttack = starttimeBtwAttack;
    }
    private void Deact()
    {
        Fire.SetActive(false);
    }
}
