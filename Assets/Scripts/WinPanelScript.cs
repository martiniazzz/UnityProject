using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPanelScript : MonoBehaviour {

    public AudioClip winSound = null;
    AudioSource winSource = null;

    public Sprite blueCrSprite;
    public Sprite greenCrSprite;
    public Sprite redCrSprite;

    Button closeButton;
    Button bgButton;
    Button toMenuButton;
    Button replayButton;

    Text fruitsInfo;
    Text coinsInfo;

    Image blueCr;
    Image greenCr;
    Image redCr;

    void Awake () {
        
		closeButton = GameObject.Find("WinCloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(this.onMenu);
        bgButton = GameObject.Find("WinBgButton").GetComponent<Button>();
        bgButton.onClick.AddListener(this.onMenu);
        toMenuButton = GameObject.Find("WinToMenuButton").GetComponent<Button>();
        toMenuButton.onClick.AddListener(this.onMenu);
        replayButton = GameObject.Find("WinRepeatButton").GetComponent<Button>();
        replayButton.onClick.AddListener(this.onReplay);

        fruitsInfo = GameObject.Find("FruitsInfoText").GetComponent<Text>();
        coinsInfo = GameObject.Find("CoinsInfoText").GetComponent<Text>();

        blueCr = GameObject.Find("BlueCrystalCol").GetComponent<Image>();
        greenCr = GameObject.Find("GreenCrystalCol").GetComponent<Image>();
        redCr = GameObject.Find("RedCrystalCol").GetComponent<Image>();
    }

    void Start()
    {
        Debug.Log("Win music play");
        winSource = gameObject.AddComponent<AudioSource>();
        winSource.clip = winSound;
        winSource.loop = false;
        winSource.Play();
    }

    public void showStatistic(int coins, int fruits, int maxFruits, bool blue, bool green, bool red)
    {
        if (blue)
            blueCr.sprite = blueCrSprite;
        if (green)
            greenCr.sprite = greenCrSprite;
        if (red)
            redCr.sprite = redCrSprite;

        string filler = "";
        filler += "+";
        filler += coins.ToString();
        coinsInfo.text = filler;
        filler = fruits.ToString() + "/" + maxFruits.ToString();
        fruitsInfo.text = filler;
    }

    void onReplay()
    {
        SceneManager.LoadScene("Level1");
    }

    void onMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
