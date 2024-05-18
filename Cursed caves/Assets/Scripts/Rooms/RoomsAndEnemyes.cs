using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsAndEnemies : MonoBehaviour
{
    [Header("Doors")]
    public GameObject doorUpClose;
    public GameObject doorUpOpen;

    public GameObject doorDownClose;
    public GameObject doorDownOpen;

    public GameObject doorLeftClose;
    public GameObject doorLeftOpen;

    public GameObject doorRightClose;
    public GameObject doorRightOpen;

    public GameObject parentObjectDoor;

    [Header("Enemies")]
    public GameObject[] enemyTypes;
    public Transform[] enemySpawners;

    [Header("Traps")]
    public GameObject[] trap;

    [Header("Boss")]
    public GameObject Boss;

    [HideInInspector] public List<GameObject> enemies;
    private RoomVariants variants;
    private bool spawned;
    private PlayerMovement player;
    private void Awake()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        variants.rooms.Add(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (trap != null)
            {
                ActivateTraps();
            }
        }
        if (enemySpawners != null)
        {
            if (collision.CompareTag("Player") && !spawned)
            {
                player.ChangeScore(50);
                spawned = true;
                foreach (Transform spawner in enemySpawners)
                {
                    if (spawner != null)
                    {
                        GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                        GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
                        enemy.transform.parent = transform;
                        enemies.Add(enemy);
                    }
                }
                StartCoroutine(CheckEnemies());
            }
        }
        else
        {
            if (Boss != null)
            {
                Boss.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (trap != null)
            {
                DeactivateTraps();
            }
        }
    }
    private void DeactivateTraps()
    {
        foreach (var trapObject in trap)
        {
            Animator animator = trapObject.GetComponent<Animator>();
            animator.enabled = false;
        }
    }
    private void ActivateTraps()
    {
        foreach (var trapObject in trap)
        {
            Animator animator = trapObject.GetComponent<Animator>();
            animator.enabled = true;
        }
    }
    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyDoors();
    }
    public void DestroyDoors()
    {
        if (doorUpClose != null)
        {
            Destroy(doorUpClose);
            Instantiate(doorUpOpen, parentObjectDoor.transform);
        }
        if (doorDownClose != null)
        {
            Destroy(doorDownClose);
            Instantiate(doorDownOpen, parentObjectDoor.transform);
        }
        if (doorLeftClose != null)
        {
            Destroy(doorLeftClose);
            Instantiate(doorLeftOpen, parentObjectDoor.transform);
        }
        if (doorRightClose != null)
        {
            Destroy(doorRightClose);
            Instantiate(doorRightOpen, parentObjectDoor.transform);
        }
    }
}
