using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameController game;
    private PlayerController currentController;
    

    // Update is called once per frame
    void Update () {
        currentController = game.currentController;
        transform.position = currentController.transform.position + new Vector3(0, 1, -5);
    }
}
