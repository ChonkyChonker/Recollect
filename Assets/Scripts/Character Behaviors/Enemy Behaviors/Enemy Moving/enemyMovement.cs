using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    private float xPos;
    private float yPos;
    // Start is called before the first frame update
    void Start()
    {
        xPos = -4;
        yPos = 0;
        transform.position = new Vector2(xPos, yPos);
    }

    // Update is called once per frame
    void Update()
    {
        MovementLeft();
        MovementRight();
    }

    void MovementLeft() 
    {
        xPos = xPos + 1;
        yPos = yPos + 1;
        transform.position = new Vector2(xPos, yPos);
    }

    void MovementRight() 
    {
        xPos = xPos - 1;
        yPos = yPos - 1;
        transform.position = new Vector2(xPos, yPos);
    }
}
