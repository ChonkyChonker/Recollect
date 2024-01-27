using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    private Vector3 direction;
    public Weapon weapon;

    [SerializeField] List<ChaseMovement> listOfEnemies;
    void Start()
    {
        base.lifeForce = base.maxLifeForce;
        LifeForceInteractions(); 
    }

    void Update()
    {

    }

    override
    public void DamageCharacter(int dmg, float kbForce)
    {
        base.DamageCharacter(dmg, kbForce);
        KnockBack(kbForce);

    }
    
    public void LifeForceInteractions() 
    {
        InvokeRepeating("RegenLifeForce", 3.0f, 0.2f);     
    }

    public void RegenLifeForce()
    {
        if(weapon.isAtking) {
            PauseRegen();
        }
        
        if(base.lifeForce == 0) {
            lifeForce++;
        }else if(base.lifeForce++ == base.maxLifeForce) {
            base.lifeForce = base.maxLifeForce;
        }else if(base.lifeForce == base.maxLifeForce){
            base.lifeForce = base.maxLifeForce;
        }
    }

    public void PauseRegen()
    {
        CancelInvoke("RegenLifeForce");
        LifeForceInteractions();
    }

    public void KnockBack(float kbForce)
    {
        ChaseMovement closest = closestEnemy();
        direction.x = closest.direction.x * kbForce;
        direction.y = closest.direction.y * kbForce;
        transform.position += direction;
    }

    public ChaseMovement closestEnemy()
    {
        float min = listOfEnemies[0].distance;
        ChaseMovement closest = listOfEnemies[0];
        foreach(ChaseMovement e in listOfEnemies) {
            if(e.distance < min) {
                min = e.distance;
                closest = e;
            }
        }
        return closest;
    }

    override
    public bool isHoldingWeaponTrigger()
    {
        return Input.GetMouseButton((int)MouseButton.Left);
    }

    override
    public bool isPressedWeaponTrigger()
    {
        return Input.GetMouseButtonDown((int)MouseButton.Left);
    }

}
