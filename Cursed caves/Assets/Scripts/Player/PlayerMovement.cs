using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    private Animator anim;

    public float moveSpeed = 5;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    Rigidbody2D rb;
    private int health = 5;
    public Animator animTextLevel;
    public Text TextLevel;
    public Text TextScore;
    public Transform HPBar;
    public ButtonData databut;
    public int score = 0;
    public GameObject timerPrefab;
    public GameObject Pause;
    public Timer timer;
    private static bool timerCreated = false;
    private GameObject timerPrefabObject;
    public bool triggered = false;
    private string formattedTime;
    void Start()
    {
        if (!timerCreated)
        {
            timerPrefabObject = Instantiate(timerPrefab);
            DontDestroyOnLoad(timerPrefabObject);
            timerCreated = true;
        }
        score = PlayerPrefs.GetInt("SavedScore", 0);
        timer = FindObjectOfType<Timer>();
        TextLevel.text = "Уровень:" + LevelUp.level;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        InputManagement();
        formattedTime = timer.GetFormattedTime();
    }
    public void ChangeScore(int takescore)
    {
        TextScore.text = "Очки: " + score;
        score = score + takescore;
    }
    void FixedUpdate()
    {
        Move();
        if (health <= 0)
        {
            timer.Stop();
            PlayerPrefs.SetString("SavedTime", formattedTime);
            Destroy(timerPrefabObject);
            timerCreated = false;
            PlayerPrefs.SetInt("SavedScore", score);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Menu");
        }
    }
    public static bool isPaused = false;
    public void TogglePause()
    {
        isPaused = !isPaused;
        Pause.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
    void InputManagement()
    {
        float moveX = 0f;
        float moveY = 0f;
        KeyCode keyCodeLeft = databut.MoveKeyCodeLeft;
        KeyCode keyCodeRight = databut.MoveKeyCodeRight;
        KeyCode keyCodeUp = databut.MoveKeyCodeUp;
        KeyCode keyCodeDown = databut.MoveKeyCodeDown;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        if (Input.GetKey(keyCodeRight))
        {
            moveX = 1f;
        }
        else if (Input.GetKey(keyCodeLeft))
        {
            moveX = -1f;
        }

        if (Input.GetKey(keyCodeUp))
        {
            moveY = 1f;
        }
        else if (Input.GetKey(keyCodeDown))
        {
            moveY = -1f;
        }

        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
        }

        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
        }
    }
    void Move()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
    public void ChangeHeath(int healthDamage)
    {
        ChangeHPBar();
        health = health - healthDamage;
        anim.SetTrigger("TakeDamage");
    }
    private Transform targetObject;
    private GameObject foundObject;
    private void ChangeHPBar()
    {
        targetObject = HPBar.Find(health.ToString());
        foundObject = targetObject.gameObject;
        foundObject.SetActive(false);
    }
}