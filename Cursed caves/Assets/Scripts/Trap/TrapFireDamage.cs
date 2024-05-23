using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFireDamage : MonoBehaviour
{
    public PlayerMovement player;

    private static bool hasTriggered = false;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            player.ChangeHeath(1);
            hasTriggered = true;
            StartCoroutine(ResetTrigger());
        }
    }

    IEnumerator ResetTrigger()
    {
        yield return new WaitForSeconds(1f);
        hasTriggered = false;
    }
}
