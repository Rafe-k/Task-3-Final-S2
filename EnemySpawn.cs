using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject prefab;
    public int enemyCountMax = 10;
    public Transform playerPosition;
    public List<Vector2> turretPosition = new List<Vector2>();
    


    private void Start()
    {
        for (int i = 0; i < enemyCountMax; i++)
        {
            turretPosition.Add(new Vector2(2*i - 4.5f, 0.5f));
        }

        for (int i = 0; i < enemyCountMax; i++)
        {
            //makeAChild(0f, 0f); // temporary numbers

            makeAChild(turretPosition[i].x, turretPosition[i].y);
        }
    }

    private void Update()
    {
        if (transform.childCount < enemyCountMax)
        {
            makeAChild(0f, 0f); // temporary numbers
        }
    }


    void makeAChild(float sentX, float sentY)
    {
        Vector2 centrePosition;

        float childX = sentX;
        float childY = sentY;
        
        Vector2 spawnPosition = new Vector2(childX, childY);

        GameObject newChild = Instantiate(prefab, spawnPosition, Quaternion.identity);
        newChild.transform.SetParent(transform);
    }
 
}


