using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : LevelEnter {

	protected override void OnRabitEnter(RabbitControl rabit)
    {
        SceneManager.LoadScene("Level1");
    }
}
