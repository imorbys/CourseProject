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

    public GameObject Boss;

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
            GameObject BossObj = Instantiate(Boss, Vector3.zero, Quaternion.identity);
            BossObj.transform.SetParent(lastRoom.transform, false);
            BossObj.SetActive(false);
            RoomsAndEnemies roomsAndEnemies = lastRoom.GetComponent<RoomsAndEnemies>();
            roomsAndEnemies.Boss = BossObj;
            foreach (Transform spawner in roomsAndEnemies.enemySpawners)
            {
                Destroy(spawner.gameObject);
            }
            roomsAndEnemies.enemySpawners = null;
            foreach (GameObject traps in roomsAndEnemies.trap)
            {
                Destroy(traps);
            }
            roomsAndEnemies.trap = null;
        }
    }
}
