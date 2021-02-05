using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private PlayerControls _playerControls;
    
    [SerializeField] private float force = 10;
    [SerializeField] private float initialForce = 1000;
    [SerializeField] private float k = 100000;
    [SerializeField] private Rigidbody head;

    private bool _isStartedMoving = false;

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

        if (!_isStartedMoving)
        {
            head.AddForce(GetDampDirection() * initialForce);
            _isStartedMoving = true;
        }
        else
        {
            var offset = -GetDampOffset();
            head.AddForce(GetDampDirection() * (offset * k));
        }
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

    private Vector3 GetDampDirection()
    {
        var vector3 = GetForwardDirection();
        var vector2 = Vector2.Perpendicular(new Vector2(vector3.x, vector3.z));

        vector3.x = vector2.x;
        vector3.z = vector2.y;

        return vector3.normalized;
    }

    private float GetDampOffset()
    {
        return Vector3.Dot(head.position, GetDampDirection());
    }
}
