using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPause : MonoBehaviour
{
    public GameObject PauseMenu;
    public PlayerMovement player;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    public void ContinuePressed()
    {
        player.TogglePause();
    }
    public void ExitPressed()
    {
        PlayerPrefs.DeleteAll();
        LevelUp.level = 0;
        PlayerMovement.isPaused = false;
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
