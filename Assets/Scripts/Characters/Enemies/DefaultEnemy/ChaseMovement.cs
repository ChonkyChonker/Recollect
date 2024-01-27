using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseMovement : Enemy
{  
    public GameController game;
    private PlayerController player;
    private Animator EnemyAnimator;
    public float speed;
    
    public float distance;  
    public Vector2 direction;  
    // Start is called before the first frame update
    void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        player = game.currentController;
        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = player.transform.position - transform.position;
        
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        Vector3 dpos = player.transform.position - this.transform.position;
        if(dpos.x > 0)  EnemyAnimator.SetFloat("dx", 1);
        else if(dpos.x < 0) EnemyAnimator.SetFloat("dx", -1);
        else if(dpos.y > 0) EnemyAnimator.SetFloat("dy", 1);
        else if(dpos.y < 0) EnemyAnimator.SetFloat("dy", -1);
    }

    public override bool isHoldingWeaponTrigger()
    {
        throw new System.NotImplementedException();
    }

    public override bool isPressedWeaponTrigger()
    {
        throw new System.NotImplementedException();
    }
}
