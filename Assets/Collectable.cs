using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    protected virtual void OnRabitHit(RabbitControl rabit)
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        RabbitControl rabit = collider.GetComponent<RabbitControl>();
        if (rabit != null)
        {
            this.OnRabitHit(rabit);
        }
    }

    public void CollectedHide()
    {
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
    }
}
