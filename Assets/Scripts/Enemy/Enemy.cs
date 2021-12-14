using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    public EnemyState currentState = EnemyState.Chase;
    private float _coolDown = 1.5f;
    private float _timer;

    private GameObject _player;
    private Health _playerHealth;

    private CharacterController _controller;

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
        _playerHealth = _player.transform.GetComponent<Health>();
        if (_player == null || _playerHealth == null)
        {
            Debug.LogError("Player component is null in enemy!");
        }
        _timer = Time.time + _coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Chase:
                EnemyMove();
                break;
            case EnemyState.Attack:
                EnemyAttack();
                break;
            case EnemyState.Idle:
                break;
            default:
                break;
        }
    }

    private void EnemyMove()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDetect);
        if (_isGrounded == true)
        {
            if (_player != null)
            {
                var direction = _player.transform.position - transform.position;
                _velocity = _speed * direction.normalized;
            }
        }
        _velocity.y += _gravity * Time.deltaTime;
        if (_player != null)
        {
            Vector3 lookPos = _player.transform.position;
            lookPos.y = 1.59f;
            transform.LookAt(lookPos);
        }
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void EnemyAttack()
    {
        if (_playerHealth != null && _timer < Time.time)
        {
            _playerHealth.Damage(20);
            _timer = Time.time + _coolDown;
        }
    }
}
