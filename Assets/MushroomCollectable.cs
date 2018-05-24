using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomCollectable : Collectable {

    protected override void OnRabitHit(RabbitControl rabit)
    {
        rabit.setBig();
        rabit.setAfterMushroom(true);
        this.CollectedHide();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
