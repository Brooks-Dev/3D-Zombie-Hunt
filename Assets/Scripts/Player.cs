using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _jumpSpeed = 4.5f;
    private float _gravity = -9.8f;
    [SerializeField]
    private float _groundDetect =1.08f;

    private Vector3 _velocity;

    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Character controller on player is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //pan left and right
        if (Input.GetMouseButton(1))
        {
            var currentRotation = transform.localEulerAngles;
            currentRotation.y += Input.GetAxis("Mouse X");
            transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);
            //tilt up and down
            Vector3 currentCameraRotation = Camera.main.transform.localEulerAngles;
            currentCameraRotation.x -= Input.GetAxis("Mouse Y");
            Camera.main.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
        }

        Movement();
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

    /*private void Movement()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDetect);
        if (_isGrounded == true)
        {
            _velocity = Vector3.zero;
            var horzontalIn = Input.GetAxis("Horizontal");
            var verticalIn = Input.GetAxis("Vertical");
            _velocity.z += _speed * verticalIn;
            _velocity.x += _speed * horzontalIn;
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
    }*/
}
