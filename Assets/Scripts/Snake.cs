using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private PlayerControls _playerControls;
    
    [SerializeField] private float force = 10;
    [SerializeField] private float initialForce = 8;
    [SerializeField] private float k = 5;
    [SerializeField] private Rigidbody head;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        head.AddForce(GetForwardDirection() * force);
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private bool IsMoving()
    {
        return head.velocity != Vector3.zero;
    }

    private Vector3 GetForwardDirection()
    {
        // push the snake forward, use USER INPUT later
        return Vector3.forward;
    }
}
