using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    private Vector2 position;
    private Vector2 target;
    private float targetAngle;
    private bool killed = false;
    private int health;
    // Start is called before the first frame update

    public class EnemyTurret2 : EnemyClass
    {
        public Vector2 GetValue()
        {
            //Vector2 egr = Vector2(10, 1);
            //this.health = 20; 
            return position;
        }
    }

    
}


