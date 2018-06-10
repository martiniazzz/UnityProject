using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : LevelEnter {

    public GameObject prefabWinPanel;
    public Canvas canvas;

    GameObject winPanelObj;
    WinPanelScript winPanel;

    protected override void OnRabitEnter(RabbitControl rabit)
    {
        LevelController.current.levelStatus.levelPassed = true;
        bool allCrystals = LevelController.current.levelStatus.hasBlueCrystal && LevelController.current.levelStatus.hasRedCrystal && LevelController.current.levelStatus.hasGreenCrystal;
        if (allCrystals)
            LevelController.current.levelStatus.hasAllCrystals = true;
        string str = JsonUtility.ToJson(LevelController.current.levelStatus);
        PlayerPrefs.SetString("stats"+ LevelController.current.lvl, str);
        Debug.Log("Written  " + str);
        string allCoins = PlayerPrefs.GetString("allCoins", null);
        if (allCoins == null)
            allCoins = "0";
        bool result;
        int number;
        result = int.TryParse(allCoins, out number);
        if (result)
        {
            number = Int32.Parse(allCoins.ToString());
        }
        number += LevelController.current.coins;
        PlayerPrefs.SetString("allCoins", number.ToString());
        winPanelObj = Instantiate(prefabWinPanel);
        winPanelObj.transform.SetParent(canvas.transform, false);
        winPanel = winPanelObj.GetComponent<WinPanelScript>();
        winPanel.showStatistic(LevelController.current.coins, LevelController.current.fruits, LevelController.current.fruitsMax, LevelController.current.crystalBlue, LevelController.current.crystalGreen, LevelController.current.crystalRed);
    }
}
