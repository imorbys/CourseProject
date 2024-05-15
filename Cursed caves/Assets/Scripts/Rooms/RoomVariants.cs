using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariants : MonoBehaviour
{
    public GameObject[] TopRooms;
    public GameObject[] DownRooms;
    public GameObject[] RightRooms;
    public GameObject[] LeftRooms;

    [HideInInspector] public List<GameObject> rooms;

    public GameObject exit;

    private void Awake()
    {
        StartCoroutine(RandomSpawner());
    }
    IEnumerator RandomSpawner()
    {
        yield return new WaitForSeconds(4f);
        if (rooms.Count > 0)
        {
            GameObject lastRoom = rooms[rooms.Count - 1];
            Vector3 exitPosition = new Vector3(lastRoom.transform.position.x, lastRoom.transform.position.y, 2f);
            Instantiate(exit, exitPosition, Quaternion.identity);
            RoomsAndEnemies roomsAndEnemies = lastRoom.GetComponent<RoomsAndEnemies>();
            roomsAndEnemies.DestroyDoors();
            foreach (Transform spawner in roomsAndEnemies.enemySpawners)
            {
                Destroy(spawner.gameObject);
            }
        }
    }
}
