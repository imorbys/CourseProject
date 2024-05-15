using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationRoom : MonoBehaviour
{
    public Direction direction;
    public enum Direction
    {
        Top,
        Down,
        Left,
        Right,
        None
    }
    private RoomVariants variants;
    private int rand;
    private bool spawned = false;
    private float waitTime = 3f;
    //public static int spawnedCount = 0;
    //public static int MaxspawnedCount = 10;

    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Destroy(gameObject, waitTime);
        //if (spawnedCount < MaxspawnedCount)
        //{
            Invoke("Spawn", 0.2f);
        //}
    }
    public void Spawn()
    {
        if (!spawned)
        {
            if (direction == Direction.Top)
            {
                rand = Random.Range(0, variants.TopRooms.Length);
                Instantiate(variants.TopRooms[rand], transform.position, variants.TopRooms[rand].transform.rotation);
                //spawnedCount++;
            }
            else if (direction == Direction.Down)
            {
                rand = Random.Range(0, variants.DownRooms.Length);
                Instantiate(variants.DownRooms[rand], transform.position, variants.DownRooms[rand].transform.rotation);
                //spawnedCount++;
            }
            else if (direction == Direction.Right)
            {
                rand = Random.Range(0, variants.RightRooms.Length);
                Instantiate(variants.RightRooms[rand], transform.position, variants.RightRooms[rand].transform.rotation);
                //spawnedCount++;
            }
            else if (direction == Direction.Left)
            {
                rand = Random.Range(0, variants.LeftRooms.Length);
                Instantiate(variants.LeftRooms[rand], transform.position, variants.LeftRooms[rand].transform.rotation);
                //spawnedCount++;
            }
            //Debug.Log(spawnedCount);
            spawned = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("RoomPoint") && other.GetComponent<GenerationRoom>().spawned)
        {
            Destroy(gameObject);
        }
    }
}
