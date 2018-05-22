using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHere : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        RabbitControl rabit = collider.GetComponent<RabbitControl>();
        if (rabit != null)
        {
            LevelController.current.onRabitDeath(rabit);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
