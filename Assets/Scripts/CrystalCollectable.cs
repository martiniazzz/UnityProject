using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalCollectable : Collectable {

    private void Start()
    {
        string str = PlayerPrefs.GetString("stats" + LevelController.current.lvl, null);
        LevelStatus levelStatus = JsonUtility.FromJson<LevelStatus>(str);
        string name = this.gameObject.name;
        if (name.Equals("gem-2") && levelStatus.hasBlueCrystal)
            this.CollectedHide();
        else if (name.Equals("gem-3") && levelStatus.hasGreenCrystal)
            this.CollectedHide();
        else if (name.Equals("gem-1") && levelStatus.hasRedCrystal)
            this.CollectedHide();
    }

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
