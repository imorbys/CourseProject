using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    public GameObject bulletEffect;
    public bool enemyBullet;
    private void Start()
    {
        Invoke("DestroyBullet", lifetime);
    }
    private void Update()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);
        if (hitinfo.collider != null)
        {
            if (hitinfo.collider.CompareTag("Enemy") && !enemyBullet)
            {
                hitinfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                DestroyBullet();
            }
            if (hitinfo.collider.CompareTag("DemonBoss") && !enemyBullet)
            {
                hitinfo.collider.GetComponent<Boss>().TakeDamage(damage);
                DestroyBullet();
            }
            if (hitinfo.collider.CompareTag("Player") && enemyBullet)
            {
                hitinfo.collider.GetComponent<PlayerMovement>().ChangeHeath(damage);
                DestroyBullet();
            }
            if (hitinfo.collider.CompareTag("Block") || hitinfo.collider.CompareTag("Door"))
            {
                DestroyBullet();
            }
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    public void DestroyBullet()
    {
        GameObject effect = Instantiate(bulletEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(effect,lifetime);
    }
}
