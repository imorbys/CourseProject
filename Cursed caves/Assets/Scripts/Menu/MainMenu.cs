using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public InputField inputField;
    public void PlayPressed()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("Nickname", inputField.text);
        PlayerPrefs.Save();
        LevelUp.level = 0;
        SceneManager.LoadScene("Game");
    }
    public void ShowRecord()
    {
        SceneManager.LoadScene("Records");
    }
    public void ExitPressed()
    {
        Application.Quit();
    }
}
