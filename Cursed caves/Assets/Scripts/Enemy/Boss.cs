using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    public GameObject healthBarCanv;
    public Image healthBar;
    public float health;
    public float maxhealth = 50;
    public static float speed = 1.4f;
    public int damage;
    public GameObject fireballPrefab;
    public Transform orbitTarget;
    public float orbitRadius = 3f;
    public GameObject exit;
    public static bool InTrigger = false;

    private Animator anim;
    private PlayerMovement player;
    private List<GameObject> fireballs = new List<GameObject>();
    [SerializeField]private RoomsAndEnemies room;
    private void Start()
    {
        anim = GetComponent<Animator>();
        healthBarCanv = GameObject.Find("HPBoss");
        healthBar = healthBarCanv.GetComponentInChildren<Image>();
        healthBar.enabled = true;
        player = FindObjectOfType<PlayerMovement>();
        room = GetComponentInParent<RoomsAndEnemies>();
        CreateOrbitingFireballs();
    }
    private void Update()
    {
        healthBar.fillAmount = health / maxhealth;
        if (health <= 0)
        {
            healthBar.enabled = false;
            anim.SetTrigger("Dead");
        }
        else
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
            UpdateOrbitingFireballs();
        }
    }
    private void DestroyBossAndNewLevel()
    {
        player.ChangeScore(1000);
        Destroy(gameObject);
        Vector3 exitPosition = new Vector3(transform.position.x, transform.position.y, 2f);
        Instantiate(exit, exitPosition, Quaternion.identity);
        room.DestroyDoors();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    public void OnEnemyAttack()
    {
        if (InTrigger)
        {
            player.ChangeHeath(damage);
        }
    }
    private void CreateOrbitingFireballs()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            fireball.transform.SetParent(transform);
            fireballs.Add(fireball);
        }
    }
    private void UpdateOrbitingFireballs()
    {
        float angleStep = 360f / fireballs.Count;
        for (int i = 0; i < fireballs.Count; i++)
        {
            float angle = angleStep * i + Time.time * 50;
            float rad = Mathf.Deg2Rad * angle;
            Vector3 offset = new Vector3(Mathf.Sin(rad), Mathf.Cos(rad), 0) * orbitRadius;
            fireballs[i].transform.position = orbitTarget.position + offset;
        }
    }
}
