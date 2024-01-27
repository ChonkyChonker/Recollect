using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarrier : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Animator>().SetFloat("HP", health);
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
