using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [Header("Controller settings")]
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _jumpSpeed = 4.5f;
    [SerializeField]
    private float _groundDetect =1.08f;
    private bool _isGrounded;
    private float _gravity = -9.8f;
    private Vector3 _velocity;

    [Header("Camera Settings")]
    [SerializeField]
    private float _mouseSpeed = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Character controller on player is null!");
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraLook();
        Movement();
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Debug.Log(Cursor.lockState);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Debug.Log(Cursor.lockState);
            }
        }
    }

    private void CameraLook()
    {
        if (Input.GetMouseButton(1))
        {
            //pan left and right
            var currentRotation = transform.localEulerAngles;
            currentRotation.y += _mouseSpeed * Input.GetAxis("Mouse X");
            transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

            //tilt up and down
            Vector3 currentCameraRotation = Camera.main.transform.localEulerAngles;
            currentCameraRotation.x -= _mouseSpeed * Input.GetAxis("Mouse Y");
            currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 0.0f, 8.0f);
            Camera.main.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
        }
    }

    private void Movement()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDetect);
        if (_isGrounded == true)
        {
            _velocity = Vector3.zero;
            var horzontalIn = Input.GetAxis("Horizontal");
            var verticalIn = Input.GetAxis("Vertical");
            _velocity += _speed * transform.forward * verticalIn;
            _velocity += _speed * transform.right * horzontalIn;
        }
        //jump if grounded
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
        {
            _velocity.y = _jumpSpeed;
        }
        else
        {
            _velocity.y += _gravity * Time.deltaTime;
        }
        _controller.Move(_velocity * Time.deltaTime);
    }
}
