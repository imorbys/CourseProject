using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private Camera cam;
    private bool triggered = false;

    private Vector3 originalPlayerPos;
    private Vector3 originalCameraPos;

    private void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
        originalPlayerPos = transform.position;
        originalCameraPos = cam.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            other.transform.position += playerChangePos;
            cam.transform.position += cameraChangePos;
        }
    }

    private void Update()
    {
        if (triggered &&
            Vector3.Distance(transform.position, originalPlayerPos) < 0.01f &&
            Vector3.Distance(cam.transform.position, originalCameraPos) < 0.01f)
        {
            triggered = false;
        }
    }
}
