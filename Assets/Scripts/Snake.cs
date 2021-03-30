using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private PlayerControls _playerControls;
    
    [SerializeField] private Rigidbody head;
    [SerializeField] private float maxSpeed = 10;

    private Vector3 _forwardDirection = Vector3.zero;
    private Vector3 _accumulatedMovement = Vector3.zero;

    [SerializeField] private float amplitude = 0.05f;
    [SerializeField] private float frequency = 0.5f;
    [SerializeField] private float phase = 0;

    private float _x;
    private float _speed = 0;
    
    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _x = 0;
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _playerControls.Land.Move.performed += ctx =>
        {
            _speed = maxSpeed;
            var v2 = ctx.ReadValue<Vector2>();
            _forwardDirection.x = v2.x;
            _forwardDirection.z = v2.y;
            _forwardDirection.Normalize();
        };

        _playerControls.Land.Move.canceled += _ =>
        {
            _forwardDirection = Vector3.zero;
            _x = 0;
        };
    }
    
    // Update is called once per frame
    private void Update()
    {
        // regular movement
        var forwardMovement = _speed * Time.deltaTime;
        _x += forwardMovement;
        _accumulatedMovement += _forwardDirection * forwardMovement;
        
        // perpendicular offset
        var offset = amplitude * Mathf.Sin(frequency * _x + phase);
        var v2 = Vector2.Perpendicular(new Vector2(_forwardDirection.x, _forwardDirection.z));
        var offsetDirection = new Vector3(v2.x, 0, v2.y).normalized;
        _accumulatedMovement += offsetDirection * offset;
    }

    private void FixedUpdate()
    {
        var nextPosition = head.position + _accumulatedMovement;
        _accumulatedMovement = Vector3.zero;
        
        head.MovePosition(nextPosition);
    }
}
