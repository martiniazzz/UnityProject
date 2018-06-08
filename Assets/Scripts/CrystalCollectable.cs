using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalCollectable : Collectable {

    protected override void OnRabitHit(RabbitControl rabit)
    {
        string name = this.gameObject.name;
        int type = 0;
        if (name.Equals("gem-2"))
            type = 1;
        else if(name.Equals("gem-3"))
            type = 2;
        else
            type = 3;
        LevelController.current.addCrystals(type);
        this.CollectedHide();
    }

}
