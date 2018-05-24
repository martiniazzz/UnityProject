using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController current;
    private Vector3 startingPosition;

    int coins;
    int fruits;
    int crystals;

    void Awake()
    {
        current = this;
        coins = 0;
        fruits = 0;
        crystals = 0;
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

    public void addCoins(int val)
    {
        coins += val;
    }

    public void addFruits(int val)
    {
        fruits += val;
    }

    public void addCrystals(int val)
    {
        crystals += val;
    }

}
