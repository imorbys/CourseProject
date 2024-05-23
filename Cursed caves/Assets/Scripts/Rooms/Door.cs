using System.Collections;
using UnityEngine;
public class Door : MonoBehaviour
{
    public GameObject block;

    private bool triggered = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Block"))
        {
            Instantiate(block, transform.GetChild(1).position, Quaternion.identity);
            Instantiate(block, transform.GetChild(2).position, Quaternion.identity);
            Destroy(gameObject);
            triggered = true;
        }
    }
}
