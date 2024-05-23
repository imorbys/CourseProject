using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    Rigidbody2D rb;
    public Animator animTextLevel;
    public Text TextLevel;
    public Text TextScore;
    public Transform HPBar;
    public ButtonData databut;
    public int score = 0;
    public GameObject timerPrefab;
    public GameObject Pause;
    public Timer timer;
    public bool triggered = false;
    public static bool isPaused = false;

    [SerializeField] private static int health = 10;
    private static bool timerCreated = false;
    private GameObject timerPrefabObject;
    private Animator anim;
    private string formattedTime;
    private Transform targetObject;
    private GameObject foundObject;
    void Start()
    {
        if (!timerCreated)
        {
            timerPrefabObject = Instantiate(timerPrefab);
            DontDestroyOnLoad(timerPrefabObject);
            timerCreated = true;
        }
        score = PlayerPrefs.GetInt("SavedScore", 0);
        TextScore.text = "Очки: " + score;
        timer = FindObjectOfType<Timer>();
        TextLevel.text = "Уровень:" + LevelUp.level;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ChangeHPBar();
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
        health = health - healthDamage;
        ChangeHPBar();
        anim.SetTrigger("TakeDamage");
    }
    private void ChangeHPBar()
    {
        for (int i = health + 1; i <= 10; i++)
        {
            Transform targetObject = HPBar.Find(i.ToString());
            if (targetObject != null)
            {
                GameObject foundObject = targetObject.gameObject;
                foundObject.SetActive(false);
            }
        }
    }
}