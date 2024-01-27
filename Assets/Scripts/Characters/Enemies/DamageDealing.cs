using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealing : MonoBehaviour
{
    
    public GameController game;
    public Collider2D enemyCollider;
    public PlayerController playerController;
    public Collider2D playerCollider;
    public Player player;

    public int damage;

    void Start()
    {
        InvokeRepeating("DealDamage", 0f, 1.0f);
    }

    void Update()
    {
        playerController = game.currentController;
        playerCollider = playerController.GetComponent<Collider2D>();
        player = game.currentPlayer;
        
    }

    private void DealDamage() 
    {
        if(Physics2D.Distance(playerCollider, enemyCollider).distance < 0.1)
        {
            player.DamageCharacter(damage, player.knockBackForce);
        }
    }
}
