using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUp : MonoBehaviour
{
    public static int level = 0;
    private bool hasEnteredTrigger = false;
    public Animator anim;
    private PlayerMovement player;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        anim = player.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasEnteredTrigger)
        {
            player.moveSpeed = 0;
            player.ChangeScore(500);
            level++;
            hasEnteredTrigger = true;
            anim.SetTrigger("Exit");
            Invoke("LoadNewLevel", 1f);
        }
    }
    void LoadNewLevel()
    {
        player.moveSpeed = 5;
        hasEnteredTrigger = false;
        PlayerPrefs.SetInt("SavedScore", player.score);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
