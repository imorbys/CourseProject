using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum GunType
    {
        Default,
        Enemy
    }
    public GunType gunType;
    public GameObject bulletPrefab;
    public Transform shotPoint;
    public ButtonData databut;
    public float starttimeBtwShoots;

    private Vector3 shootDirection = Vector3.right;
    private float timeBtwShoots;
    private PlayerMovement player;
    private PlayerAnimator playerAnimator;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        playerAnimator = FindObjectOfType<PlayerAnimator>();
    }
    
    void Update()
    {
        if (timeBtwShoots <= 0)
        {
            if (gunType == GunType.Enemy)
            {
                ShootEnemy();
            }
            else
            {
                KeyCode keyCodeLeft = databut.ShootKeyCodeLeft;
                KeyCode keyCodeRight = databut.ShootKeyCodeRight;
                KeyCode keyCodeUp = databut.ShootKeyCodeUp;
                KeyCode keyCodeDown = databut.ShootKeyCodeDown;
                if (Input.GetKey(keyCodeRight))
                {
                    playerAnimator.SpriteDirectionChecker();
                    Shoot(Vector3.up);
                }
                else if (Input.GetKey(keyCodeLeft))
                {
                    playerAnimator.SpriteDirectionChecker();
                    Shoot(Vector3.down);
                }
                else if (Input.GetKey(keyCodeUp))
                {
                    Shoot(Vector3.left);
                }
                else if (Input.GetKey(keyCodeDown))
                {
                    Shoot(Vector3.right);
                }
            }
        }
        else
        {
            timeBtwShoots -= Time.deltaTime;
        }
    }
    void Shoot(Vector3 direction)
    {
        Instantiate(bulletPrefab, shotPoint.position, Quaternion.LookRotation(Vector3.forward, direction));
        timeBtwShoots = starttimeBtwShoots;
    }
    void ShootEnemy()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        Quaternion finalRotation = rotation * Quaternion.Euler(0, 0, 90f);
        Instantiate(bulletPrefab, shotPoint.position, finalRotation);
        timeBtwShoots = starttimeBtwShoots;
    }
}
