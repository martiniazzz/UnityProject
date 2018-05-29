using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollectable : Collectable {

    protected override void OnRabitHit(RabbitControl rabit)
    {
        if(rabit.getAfterMushroom() == true)
            rabit.addBomb();
        if (rabit.getBig() == false && rabit.getBombBonus() == false)
        {
            rabit.die();
        }
        rabit.setSmall();
        if(rabit.getBombBonus() == false)
            this.CollectedHide();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
