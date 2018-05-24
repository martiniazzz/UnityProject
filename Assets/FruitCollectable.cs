using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollectable : Collectable {

    protected override void OnRabitHit(RabbitControl rabit)
    {
        LevelController.current.addFruits(1);
        this.CollectedHide();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
