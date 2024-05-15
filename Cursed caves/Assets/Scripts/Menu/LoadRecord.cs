using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
[System.Serializable]
public class Record
{
    public string nickname;
    public string time;
    public int score;
}
public class LoadRecord : MonoBehaviour
{
    public RectTransform NicknameHeight;
    public GridLayoutGroup Content;
    public GameObject Nickname;
    public GameObject Time;
    public GameObject Score;

    private List<Record> records = new List<Record>();
    private void Start()
    {
        string nick = PlayerPrefs.GetString("Nickname", "No Found");
        string time = PlayerPrefs.GetString("SavedTime", "No Found");
        int score = PlayerPrefs.GetInt("SavedScore", 0);
        if (nick != "No Found" || time != "No Found")
        {
            AddRecord(nick, time, score);
        }
        LoadRecordsFromJson();
    }
    private void CreateAndSetupTextObject(string text, GameObject parentObject)
    {
        GameObject textObject = new GameObject(text);
        RectTransform textRect = textObject.AddComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(300f, 100f);
        textObject.transform.SetParent(parentObject.transform, false);
        Text textComponent = textObject.AddComponent<Text>();
        textComponent.text = text;
        textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        textComponent.fontSize = 40;
        textComponent.color = Color.white;
    }
    public void AddRecord(string nickname, string time, int score)
    {
        string path = Application.persistentDataPath + "/records.json";
        if (File.Exists(path) && new FileInfo(path).Length > 0)
        {
            string jsonData = File.ReadAllText(path);
            records = JsonConvert.DeserializeObject<List<Record>>(jsonData) ?? new List<Record>();
        }
        else
        {
            records = new List<Record>();
        }
        Record existingRecord = records.Find(r => r.nickname == nickname);
        if (existingRecord != null)
        {
            if (string.Compare(existingRecord.time, time) > 0 ||
            (string.Compare(existingRecord.time, time) == 0 && existingRecord.score < score))
            {
                existingRecord.time = time;
                existingRecord.score = score;
            }
        }
        else
        {
            Record newRecord = new Record
            {
                nickname = nickname,
                time = time,
                score = score
            };
            records.Add(newRecord);
        }
        string updatedJsonData = JsonConvert.SerializeObject(records, Formatting.Indented);
        File.WriteAllText(path, updatedJsonData);
    }
    private void Update()
    {
        float height = NicknameHeight.rect.height;
        Vector2 newCellSize = new Vector2(Content.cellSize.x, height);
        Content.cellSize = newCellSize;
    }
    public void LoadRecordsFromJson()
    {
        string path = Application.persistentDataPath + "/records.json";
        if (File.Exists(path) && new FileInfo(path).Length > 0)
        {
            string jsonData = File.ReadAllText(path);
            records = JsonConvert.DeserializeObject<List<Record>>(jsonData) ?? new List<Record>();
            ClearTextObjects(Nickname);
            ClearTextObjects(Time);
            ClearTextObjects(Score);
            foreach (Record record in records)
            {
                CreateAndSetupTextObject(record.nickname, Nickname);
                CreateAndSetupTextObject(record.time, Time);
                CreateAndSetupTextObject(record.score.ToString(), Score);
            }
        }
        else
        {
            records = new List<Record>();
        }
    }
    private void ClearTextObjects(GameObject parentObject)
    {
        foreach (Transform child in parentObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void ExitInMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
