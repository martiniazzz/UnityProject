using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnter : MonoBehaviour {

    protected virtual void OnRabitEnter(RabbitControl rabit)
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        RabbitControl rabit = collider.GetComponent<RabbitControl>();
        if (rabit != null)
        {
            this.OnRabitEnter(rabit);
        }
    }

}
