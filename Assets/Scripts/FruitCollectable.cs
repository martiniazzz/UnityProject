using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollectable : Collectable {

    bool canCollect = true;

    protected override void OnRabitHit(RabbitControl rabit)
    {
        if (canCollect)
        {
            LevelController.current.addFruits(1);
            string name = this.gameObject.name;
            LevelController.current.levelStatus.collectedFruitsList.Add(name);
            this.CollectedHide();
        }
        
    }

    void Start () {
        string str = PlayerPrefs.GetString("stats" + LevelController.current.lvl, null);
        LevelStatus levelStatus = JsonUtility.FromJson<LevelStatus>(str);
        string name = this.gameObject.name;
        foreach (string n in LevelController.current.levelStatus.collectedFruitsList)
        {
            if (n.Equals(name))
            {
                canCollect = false;
                Color oldColor = this.GetComponent<Renderer>().material.color;
                Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f);
                this.GetComponent<Renderer>().material.color = newColor;
            }
        }  

    }
}
