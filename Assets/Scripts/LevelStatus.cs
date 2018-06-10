using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelStatus{

    public bool hasAllCrystals = false;

    public bool hasBlueCrystal = false;
    public bool hasGreenCrystal = false;
    public bool hasRedCrystal = false;

    public bool hasAllFruits = false;
    public int maxFruits = 11;
    public int collectedFruits = 0;

    public bool levelPassed = false;
    public List<string> collectedFruitsList = new List<string>();

}
