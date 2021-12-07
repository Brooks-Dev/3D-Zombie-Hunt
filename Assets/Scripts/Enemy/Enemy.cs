using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterController _controller;
    private GameObject _player;

    [Header("Controller settings")]
    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private float _groundDetect = 1.08f;
    [SerializeField]
    private float _speed = 2.0f;
    private float _gravity = -9.8f;
    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Character controller is null in enemy!");
        }
        _player = GameObject.FindWithTag("Player");
        if (_player == null)
        {
            Debug.LogError("Player is null in enemy!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDetect);
        if (_isGrounded == true)
        {
            var direction = _player.transform.position - transform.position;
            _velocity = _speed * direction.normalized;
        }
        _velocity.y += _gravity * Time.deltaTime;
        transform.LookAt(_player.transform);
        _controller.Move(_velocity * Time.deltaTime);
    }
}
