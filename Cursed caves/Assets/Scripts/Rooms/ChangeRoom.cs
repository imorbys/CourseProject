using UnityEngine;
public class ChangeRoom : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;

    private Camera cam;
    [SerializeField] private bool triggered = false;
    private Vector3 originalCameraPos;

    private void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
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
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && triggered)
        {
            triggered = false;
        }
    }
}
