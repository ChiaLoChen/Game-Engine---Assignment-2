using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;    // Speed of the player
    public float jumpHeight = 2f;   // Jump height
    public Camera playerCamera;

    public bool isGrounded;

    Rigidbody rb;

    GameObject _camera;
    CameraFollow _cameraFollow;

    bool _isPaused = false;
    
    InputHandler _input;

    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    GameObject bullet;

    public bool started = false;
    
    private bool _isRecording;

    private ObjectPool _pool;
    void Start()
    {
        _pool = gameObject.GetComponent<ObjectPool>();
        GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _cameraFollow = _camera.GetComponent<CameraFollow>();
        _input = FindObjectOfType<InputHandler>();//Leo
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q) && started)
        {
            if (_isPaused)
            {
                _input.resumeRecording();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                Time.timeScale = 1;
                _pauseMenu.SetActive(false);
                _cameraFollow._isPaused = false;

                _isPaused = false;

            }
            else if (!_isPaused)
            {
                _input.isNotRecording();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Time.timeScale = 0;
                _pauseMenu.SetActive(true);
                _cameraFollow._isPaused = true;

                _isPaused = true;
                
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void MoveForward()
    {
        //rb.velocity = transform.forward * moveSpeed;
        rb.AddForce(transform.forward.normalized * (moveSpeed * 10f * Time.deltaTime), ForceMode.Force);

        //transform.position += transform.forward * moveSpeed * Time.deltaTime;  
    }

    public void MoveLeft()
    {
        rb.AddForce(-transform.right.normalized * (moveSpeed * 10f * Time.deltaTime), ForceMode.Force);

        //transform.position -= transform.right * moveSpeed * Time.deltaTime;

    }

    public void MoveRight()
    {
        //transform.position += transform.right * moveSpeed * Time.deltaTime;

        rb.AddForce(transform.right.normalized * (moveSpeed * 10f * Time.deltaTime), ForceMode.Force);


    }

    public void MoveBackwards()
    {
        //transform.position -= transform.forward * moveSpeed * Time.deltaTime;

        rb.AddForce(-transform.forward.normalized * (moveSpeed * 10f * Time.deltaTime), ForceMode.Force);

    }

    public void Shoot()
    {
        _pool.SpawnAndShoot();
    }

    public void Jump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            rb.AddForce(transform.up.normalized * (jumpHeight * 200f), ForceMode.Force);
        }

    }
}

