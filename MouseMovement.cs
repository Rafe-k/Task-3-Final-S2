using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMovement : PlayerInput
{
    private Vector2 move;
    private CharacterInput controls;
    public Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    private CharacterController controller;

    // Start is called before the first frame update

    private void Awake()
    {
        controls = new CharacterInput();
        controller = new CharacterController(); // both of these may not be necessary
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseMove();
    }

    private void MouseMove()
    {


        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public Vector3 GetMousePos()
    {
        return mousePosition;
    }
}
