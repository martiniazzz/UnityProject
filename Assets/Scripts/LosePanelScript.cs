using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanelScript : MonoBehaviour {

    public Sprite blueCrSprite;
    public Sprite greenCrSprite;
    public Sprite redCrSprite;

    Button closeButton;
    Button bgButton;
    Button toMenuButton;
    Button replayButton;

    Image blueCr;
    Image greenCr;
    Image redCr;

    void Awake()
    {

        closeButton = GameObject.Find("LoseCloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(this.onMenu);
        bgButton = GameObject.Find("LoseBgButton").GetComponent<Button>();
        bgButton.onClick.AddListener(this.onMenu);
        toMenuButton = GameObject.Find("LoseToMenuButton").GetComponent<Button>();
        toMenuButton.onClick.AddListener(this.onMenu);
        replayButton = GameObject.Find("LoseRepeatButton").GetComponent<Button>();
        replayButton.onClick.AddListener(this.onReplay);

        blueCr = GameObject.Find("BlueCrystalCol").GetComponent<Image>();
        greenCr = GameObject.Find("GreenCrystalCol").GetComponent<Image>();
        redCr = GameObject.Find("RedCrystalCol").GetComponent<Image>();
    }

    public void showStatistic(bool blue, bool green, bool red)
    {
        if (blue)
            blueCr.sprite = blueCrSprite;
        if (green)
            greenCr.sprite = greenCrSprite;
        if (red)
            redCr.sprite = redCrSprite;
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
