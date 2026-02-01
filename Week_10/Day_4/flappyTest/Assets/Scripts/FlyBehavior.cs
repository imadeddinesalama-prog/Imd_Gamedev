using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyBehavior : MonoBehaviour
{
    [SerializeField] private float _velocity = 1.5f;
    [SerializeField] private float _roatationSpeed = 10f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check for touch input using the new Input System
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            _rb.linearVelocity = Vector2.up * _velocity;
        }

        // Also support mouse click for testing in editor
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            _rb.linearVelocity = Vector2.up * _velocity;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, _rb.linearVelocity.y * _roatationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameManager.Instance.GameOver();
    }
}