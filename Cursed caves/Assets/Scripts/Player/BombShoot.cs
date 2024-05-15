using System.Collections.Generic;
using UnityEngine;

public class BombShoot : MonoBehaviour
{
    public Sprite bombSprite;
    public AnimationCurve bombArc;
    public float high = 50f;

    private List<BombData> _bombs;
    void Start()
    {
        bombArc = new AnimationCurve();
        bombArc.AddKey(new Keyframe(0, 0, 0.2f, 0.2f));
        bombArc.AddKey(new Keyframe(0.5f, 0.05f, 0, 0));
        bombArc.AddKey(new Keyframe(1, 0, -0.2f, -0.2f));


        _bombs = new List<BombData>();

        var camera = FindObjectOfType<Camera>();
        camera.orthographic = true;
        camera.transform.position = Vector3.forward * -10;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            createNewBomb();
        }

        for (int i = 0; i < _bombs.Count; i++)
        {
            BombData b = _bombs[i];
            if (b.time >= 1f)
            {
                Destroy(b.holder);
                _bombs.RemoveAt(i);
                i--;
                continue;
            }

            b.time += Time.deltaTime * b.speed;
            Vector2 bombPos = Vector2.Lerp(b.start, b.finish, b.time);
            b.bomb.position = bombPos + Vector2.up * bombArc.Evaluate(b.time) * high;
            b.shadow.position = bombPos;
        }
    }

    private void createNewBomb()
    {
        GameObject bombHolder = new GameObject("BombHolder");
        GameObject bomb = new GameObject("Bomb");
        bomb.transform.SetParent(bombHolder.transform);
        var spriteRend = bomb.AddComponent<SpriteRenderer>();
        spriteRend.sprite = bombSprite;
        spriteRend.sortingOrder = 0;
        GameObject shadow = new GameObject("Shadow");
        shadow.transform.localScale = new Vector3(1f, 0.3f, 1f);
        shadow.transform.SetParent(bombHolder.transform);
        spriteRend = shadow.AddComponent<SpriteRenderer>();
        spriteRend.sprite = bombSprite;
        spriteRend.color = new Color(0, 0, 0, 0.2f);
        spriteRend.sortingOrder = -1;
        BombData bombData = new BombData();
        bombData.holder = bombHolder;
        bombData.bomb = bomb.transform;
        bombData.shadow = shadow.transform;
        bombData.start = transform.position;
        bombData.finish = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _bombs.Add(bombData);
    }

    public class BombData
    {
        public float speed = 1f;
        public float time = 0;
        public float arcTime = 0;
        public Transform bomb;
        public Transform shadow;
        public GameObject holder;
        public Vector2 start;
        public Vector2 finish;
    }
}