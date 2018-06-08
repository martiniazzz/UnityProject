using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public static LevelController current;
    private Vector3 startingPosition;

    Text coins_amount;
    Text fruits_amount;

    Image life1;
    Image life2;
    Image life3;

    public Sprite empty_life;

    Image blue;
    Image green;
    Image red;

    public Sprite blueSprite;
    public Sprite greenSprite;
    public Sprite redSprite;

    int coins;
    int fruits;
    bool crystalBlue;
    bool crystalGreen;
    bool crystalRed;

    void Awake()
    {
        current = this;
        coins = 0;
        fruits = 0;
        crystalBlue = false;
        crystalGreen = false;
        crystalRed = false;
    }

    private void Start()
    {
        coins_amount = GameObject.Find("Amount").GetComponent<Text>();
        fruits_amount = GameObject.Find("CollectedFruits").GetComponent<Text>();

        life1 = GameObject.Find("L1").GetComponent<Image>();
        life2 = GameObject.Find("L2").GetComponent<Image>();
        life3 = GameObject.Find("L3").GetComponent<Image>();

        blue = GameObject.Find("BlueCry").GetComponent<Image>();
        green = GameObject.Find("GreenCry").GetComponent<Image>();
        red = GameObject.Find("RedCry").GetComponent<Image>();
    }

    void Update () {
        string filler = "";
        int zeros = 4 - coins.ToString().Length;
        for (int i = 0; i < zeros; i++)
            filler += "0";
        filler += coins.ToString();
        coins_amount.text = filler;

        fruits_amount.text = fruits.ToString();

        if (crystalBlue)
            blue.sprite = blueSprite;
        if (crystalGreen)
            green.sprite = greenSprite;
        if (crystalRed)
            red.sprite = redSprite;

    }

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }
    //crystal-not-found
    public void onRabitDeath(RabbitControl rabit)
    {
        int currLifes = rabit.getLifes();
        if (currLifes > 0)
        {
            rabit.transform.position = this.startingPosition;
        }
        else
            SceneManager.LoadScene("Levels");

        if (currLifes == 2)
            life3.sprite = empty_life;
        else if(currLifes == 1)
            life2.sprite = empty_life;
        else
            life1.sprite = empty_life;
    }

    public void addCoins(int val)
    {
        coins += val;
    }

    public void addFruits(int val)
    {
        fruits += val;
    }

    public void addCrystals(int type)
    {
        if (type == 1)
            crystalBlue = true;
        else if (type == 2)
            crystalGreen = true;
        else
            crystalRed = true;
    }

}
