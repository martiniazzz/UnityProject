﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHere : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        RabbitControl rabit = collider.GetComponent<RabbitControl>();
        if (rabit != null)
        {
            rabit.fall();
            LevelController.current.onRabitDeath(rabit);
        }
    }

}
