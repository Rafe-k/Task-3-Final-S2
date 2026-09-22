using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.FilePathAttribute;


public class PlayerMovement : MonoBehaviour
{
    private CharacterInput controls;
    private Vector3 velocity;
    private Vector3 move;
    public Vector3 mousePosition;
    public float aimAngle;

    private CharacterController controller;

    public float moveSpeed = 5f;
    public float jumpHeight = 0.3f;
    public float gravity = -9.81f;

    public Transform ground;
    public float distanceToground = 0.4f;
    public LayerMask groundMask;
    public bool canMoveInAir = true;
    private PlayerFire gun;
    public float gunRecoilForce = 10f;

    bool canInteract;


    // Start is called before the first frame update
    private void Awake()
    {
        controls = new CharacterInput();
        controller = GetComponent<CharacterController>();
        gun = GetComponent<PlayerFire>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        aimAngle = Mathf.Atan2(move.y - mousePosition.y, move.x - mousePosition.x) * 180 / Mathf.PI;
        
        

        //Debug.DrawLine(move, mousePosition);
        Debug.DrawRay(move, mousePosition, Color.red, 5f);
        //Debug.Log(aimAngle);

        if (controls.Player.Fire.ReadValue<float>() > 0)
        {

/*            canMoveInAir = false;
            //Debug.Log("fewf");
            if (isGrounded() && gun.FireGun()) // triggers the FireGun function and checks if the player is grounded
            {
                // create an object or activate an object or something
                Debug.Log("fired and is grounded");
            } else if (gun.FireGun() && isGrounded() == false)
            {
                // create an object or activate an object or something
                Debug.Log("fired and isn't grounded");
                
                velocity.x = Mathf.Cos(aimAngle * Mathf.Deg2Rad) * gunRecoilForce;
                
                velocity.y = Mathf.Sin(aimAngle * Mathf.Deg2Rad) * gunRecoilForce;


               
            }*/

            if (gun.FireGun())
            {
                canMoveInAir = false;

                velocity.x = Mathf.Cos(aimAngle * Mathf.Deg2Rad) * gunRecoilForce;

                velocity.y = Mathf.Sin(aimAngle * Mathf.Deg2Rad) * gunRecoilForce;
            }
        }
        Debug.Log(Mathf.Sin(aimAngle * Mathf.Deg2Rad)); 
        //Debug.Log(aimAngle);


        PlayerMove();
        Grav();
        Jump();

        

    }

    public float sendAngle()
    {
        return aimAngle;
    }

    public Vector3 sendPosition()
    {
        return move;
    }


    public float sendFireControl()
    {
        return (controls.Player.Fire.ReadValue<float>());
    }
    
    private void PlayerMove()
    {
        /*       if (canMoveInAir == false)
               {
                   //controls.Player.Movement.Disable();
               } else if (canMoveInAir)
               {
                   //controls.Player.Movement.Enable();
               }



               if (canMoveInAir)
               {
                   velocity = controls.Player.Movement.ReadValue<Vector2>();

               }
               else
               {
                   move = velocity;
               }

               //Debug.Log(move);

               //move.x += velocity.x;
               //move.y += velocity.y;




               //Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
               //controller.Move(movement * moveSpeed * Time.deltaTime);




               controller.Move(velocity * moveSpeed * Time.deltaTime);

               //controller.SimpleMove(velocity * moveSpeed * Time.deltaTime);*/



        // Vector2 input = controls.Player.Movement.ReadValue<Vector2>(); // old code
        // move = new Vector3(input.x, 0, input.y);
        // move = Vector3.ClampMagnitude(move, 1f);


        Vector2 input = controls.Player.Movement.ReadValue<Vector2>();
        if (canMoveInAir) // new code
        {
            move = new Vector3(input.x, 0, input.y);
            move = Vector3.ClampMagnitude(move, 1f);
        } else
        {
            move = new Vector3(velocity.x, 0, velocity.y);
            move = Vector3.ClampMagnitude(move, maxLength);
        }
        


        // if (canMoveInAir)
        // {
        //     Vector3 finalMove = (move * moveSpeed) + (velocity.y * Vector3.up);
        //     controller.Move(finalMove * Time.deltaTime);
        // } else
        // {
        //     move.x = velocity.x;
        //     move.y = velocity.y;
        //     Vector3 finalMove = (move * moveSpeed) + (velocity.y * Vector3.up);
        // }

        Vector3 finalMove = (move * moveSpeed) + (velocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);

        Debug.Log(canMoveInAir);
    }

    /*public void recieveGunVelocity(Vector3 gunVelocity)
    {
        velocity.x = gunVelocity.x;
        velocity.y = gunVelocity.y;

        canMoveInAir = false;
        Debug.Log("triggered");
    }*/

    private bool isGrounded()
    {
        //return Physics.CheckSphere(ground.position, distanceToground, groundMask);

        

        if (canInteract)
        {
            Debug.Log("can interact");
            return true;
        } else
        {
            Debug.Log("can't interact");
            return false;

        }
    }

    private void OnTriggerEnter(Collider Ground)
    {
        canInteract = true;
    }

    private void OnTriggerExit(Collider Ground)
    {
        canInteract = false;
    }

    private void Grav()
    {
        if (isGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
            canMoveInAir = true;
            if (Mathf.Abs(velocity.x) > moveSpeed)
            {
                //velocity.x -= Mathf.Sign(velocity.x) * 0.2f;
            }
        }

        velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (controls.Player.Jump.triggered && isGrounded())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
    }

    private void OnEnable()
    {
        controls.Enable();
        

    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
