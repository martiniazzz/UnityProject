using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController current;
    private Vector3 startingPosition;

    void Awake()
    {
        current = this;
    }

    void Start () {	
	}

	void Update () {
	}

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public void onRabitDeath(RabbitControl rabit)
    {
        rabit.transform.position = this.startingPosition;
    }

}
