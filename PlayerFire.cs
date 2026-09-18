using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerFire : PlayerInput
{
    private CharacterInput controls;
    public float range = 10f;
    private bool fired;
    private int fireCooldown = 0;
    private int shells = 2;
    // Start is called before the first frame update
    //public float rotation = GetComponent<PlayerMovement>();
    private float rotation;
    private PlayerMovement gunRotation;
    


    private void Start()
    {

    }

    private void Awake()
    {
        gunRotation = GetComponent<PlayerMovement>();
        controls = new CharacterInput();
        

    }

    private void Update()
    {
        rotation = gunRotation.sendAngle();

        transform.rotation = quaternion.Euler(0f, 0f, rotation * Mathf.Deg2Rad);

    }

    public bool FireGun()
    {
        Vector3 velocity;
        velocity.z = 0;
        if ((gunRotation.sendFireControl() > 0) && shells > 0) // checks if the fire button is clicked and at least one shell is remaining
        {
            



            if (fireCooldown <= 0)
            {
                fired = true;
                fireCooldown = 100;
                Debug.Log(fired);
                shells -= 1;


                velocity.x = 90 - rotation;
                velocity.y = rotation;
                shells = 2; // temporary
                return true;
            }

            
        }
        return false; 
    }

}
