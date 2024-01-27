using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoogieWoogie : MonoBehaviour
{
    public Enemy todo;
    public GameController game;

    void Start()
    {

    }

    void Update()
    {
        if(todo.midDamage) 
        {
            Vector3 todoPos = transform.position;
            transform.position = game.currentController.transform.position;
            game.currentController.transform.position = todoPos;
            todo.midDamage = false;
        }
    }

}
