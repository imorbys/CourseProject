using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    private float timeBtwAttack;
    public float starttimeBtwAttack;
    public int health;
    public float speed;
    private Animator anim;
    private PlayerMovement player;
    private RoomsAndEnemies roomsAndEnemies;
    public int damage;
    public enum EnemyType
    {
        Shooter,
        Near
    }
    private void Start()
    {
        roomsAndEnemies = GetComponentInParent<RoomsAndEnemies>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        if (health <= 0)
        {
            anim.SetTrigger("Dead");
        }
        else if (enemyType == EnemyType.Near)
        {
            if (player.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else if (enemyType == EnemyType.Shooter)
        {
            if (player.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            Vector2 directionToPlayer = (Vector2)transform.position - (Vector2)player.transform.position;
            transform.position = (Vector2)transform.position + directionToPlayer.normalized * speed * Time.deltaTime;
        }
    }
    private void DestroyEnemy()
    {
        player.ChangeScore(200);
        Destroy(gameObject);
        roomsAndEnemies.enemies.Remove(gameObject);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (enemyType == EnemyType.Near)
        {
            if (other.CompareTag("Player"))
            {
                if (timeBtwAttack <= 0)
                {
                    anim.SetTrigger("Attack");
                    timeBtwAttack = starttimeBtwAttack;
                }
                else
                {
                    timeBtwAttack -= Time.deltaTime;
                }
            }
        }
    }
    public void OnEnemyAttack()
    {
        player.ChangeHeath(damage);
    }
}
