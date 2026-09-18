using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{

    public Vector2 turretPosition;
    public float currentRotation;
    public float targetRotation;
    public Transform playerPosition;

    private void Start()
    {
        
    }

    private void Awake()
    {
        
    }

    private void Update()
    {
        Tracking();
    }

    public void Tracking()
    {

        //currentRotation = Mathf.MoveTowardsAngle
        targetRotation = Mathf.Atan2(turretPosition.y, playerPosition.position.x) * Mathf.Rad2Deg;
        //Debug.DrawRay();
        Debug.DrawRay(turretPosition, playerPosition.position, Color.red, 5f);
    }






}


