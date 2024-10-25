using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset = new Vector3 (0, 1, 0);   // Offset position of the camera from the player
    public float mouseSensitivity = 20f; // Sensitivity for mouse movement
    //public float pitchLimit = 85f; // Limit for camera pitch rotation

    private float X; // Y-axis rotation
    private float Y; // X-axis rotation

    float xRotation;
    float yRotation;

    public bool _isPaused = false;

    public bool started = false;

    void Start()
    {
        // Initialize camera rotation
    }

    void Update()
    {
        if (!_isPaused && started && timer.Instance.canCount)
        {
            // Get mouse input for rotation
            X = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
            Y = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;


            xRotation -= Y;
            yRotation += X;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);


            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            player.rotation = Quaternion.Euler(0, yRotation, 0);

            transform.position = player.position + offset;
        }
        
    }
}