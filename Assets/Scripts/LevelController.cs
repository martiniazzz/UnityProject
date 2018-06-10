using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LevelController : MonoBehaviour {

    public AudioClip music = null;
    AudioSource musicSource = null;

    public static LevelController current;
    private Vector3 startingPosition;

    public int lvl;

    Text coins_amount;
    Text all_coins_amount;
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

    public Sprite appleSprite;
    public Sprite passedSprite;
    public Sprite lockSprite;

    public int coins;
    public int fruits;
    public int fruitsMax;
    public bool crystalBlue;
    public bool crystalGreen;
    public bool crystalRed;

    public Button pause;
    public GameObject prefabSettings;
    public Canvas canvas;

    public GameObject prefabLosePanel;

    GameObject popupObj;
    SettingsScript popup;

    GameObject losePanelObj;
    LosePanelScript losePanel;

    public LevelStatus levelStatus;

    Image lvl1Passed;
    Image lvl1AllCrystals;
    Image lvl1AllFruits;

    Image lvl2Passed;
    Image lvl2AllCrystals;
    Image lvl2AllFruits;

    void Awake()
    {
        current = this;
        string str = PlayerPrefs.GetString("stats" + lvl, null);
        this.levelStatus = JsonUtility.FromJson<LevelStatus>(str);
        if (this.levelStatus == null)
        {
            this.levelStatus = new LevelStatus();
            fruitsMax = this.levelStatus.maxFruits;
            coins = 0;
            fruits = 0;
            crystalBlue = false;
            crystalGreen = false;
            crystalRed = false;
            return;
        }
        Debug.Log("Read  " + str);
        fruitsMax = this.levelStatus.maxFruits;
        coins = 0;
        fruits = levelStatus.collectedFruits;
        crystalBlue = levelStatus.hasBlueCrystal;
        crystalGreen = levelStatus.hasGreenCrystal;
        crystalRed = levelStatus.hasRedCrystal;
    }

    private void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = music;
        musicSource.loop = true;
        
        if (SoundManager.Instance.isMusicOn())
        {
            musicSource.Play();
        }

        string name = SceneManager.GetActiveScene().name;

        if (name.Equals("Levels"))
        {
            showAllCoins();
            lvl1Passed = GameObject.Find("Lvl1Passed").GetComponent<Image>();
            lvl1AllCrystals = GameObject.Find("Lvl1Crystals").GetComponent<Image>();
            lvl1AllFruits = GameObject.Find("Lvl1Fruits").GetComponent<Image>();

            lvl2Passed = GameObject.Find("Lvl2Passed").GetComponent<Image>();
            lvl2AllCrystals = GameObject.Find("Lvl2Crystals").GetComponent<Image>();
            lvl2AllFruits = GameObject.Find("Lvl2Fruits").GetComponent<Image>();
        }
        if (name.Equals("Level1"))
        {
            pause.onClick.AddListener(this.showSettings);

            coins_amount = GameObject.Find("Amount").GetComponent<Text>();
            fruits_amount = GameObject.Find("CollectedFruits").GetComponent<Text>();

            life1 = GameObject.Find("L1").GetComponent<Image>();
            life2 = GameObject.Find("L2").GetComponent<Image>();
            life3 = GameObject.Find("L3").GetComponent<Image>();

            blue = GameObject.Find("BlueCry").GetComponent<Image>();
            green = GameObject.Find("GreenCry").GetComponent<Image>();
            red = GameObject.Find("RedCry").GetComponent<Image>();
        }

    }

    void Update()
    {
        bool is_music_on = PlayerPrefs.GetInt("music", 1) == 1;

        if (!is_music_on)
            musicSource.Pause();
        else if (!musicSource.isPlaying)
            musicSource.Play();

        string name = SceneManager.GetActiveScene().name;
        if (name.Equals("Level1"))
        {
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
        if (name.Equals("Levels"))
        {
            string str = PlayerPrefs.GetString("stats" + lvl, null);
            LevelStatus levelStatus1 = JsonUtility.FromJson<LevelStatus>(str);
            if (levelStatus1 == null)
            {
                lvl2Passed.sprite = lockSprite;
            }
            else
            {
                if (levelStatus1.levelPassed)
                {
                    lvl1Passed.sprite = passedSprite;
                }
                    
                if (levelStatus.hasAllFruits)
                    lvl1AllFruits.sprite = appleSprite;
                if (levelStatus1.hasBlueCrystal && levelStatus1.hasGreenCrystal && levelStatus1.hasRedCrystal)
                    lvl1AllCrystals.sprite = blueSprite;
            }

            str = PlayerPrefs.GetString("stats" + (lvl+1), null);
            LevelStatus levelStatus2 = JsonUtility.FromJson<LevelStatus>(str);
            if (levelStatus2 != null)
            { 
                if (levelStatus2.levelPassed)
                {
                    lvl2Passed.sprite = passedSprite;
                }
                else
                {
                    Color oldColor = lvl2Passed.GetComponent<Renderer>().material.color;
                    Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f);
                    lvl2Passed.GetComponent<Renderer>().material.color = newColor;
                }

                if (levelStatus2.hasAllFruits)
                    lvl2AllFruits.sprite = appleSprite;
                if (levelStatus2.hasBlueCrystal && levelStatus2.hasGreenCrystal && levelStatus2.hasRedCrystal)
                    lvl2AllCrystals.sprite = blueSprite;
            }
        }
    }

        void showSettings()
        {
            Debug.Log("Pause clicked");
            popupObj = Instantiate(prefabSettings);
            popupObj.transform.SetParent(canvas.transform, false);
            popup = popupObj.GetComponent<SettingsScript>();
        }

        public void setStartPosition(Vector3 pos)
        {
            this.startingPosition = pos;
        }

        public void onRabitDeath(RabbitControl rabit)
        {
            int currLifes = rabit.getLifes();

            if (currLifes == 2)
                life3.sprite = empty_life;
            else if (currLifes == 1)
                life2.sprite = empty_life;
            else
                life1.sprite = empty_life;

            if (currLifes > 0)
            {
                rabit.transform.position = this.startingPosition;
            }
            else
            {
                levelStatus.levelPassed = false;
                string str = JsonUtility.ToJson(levelStatus);
                PlayerPrefs.SetString("stats" + lvl, str);
                Debug.Log("Written  " + str);
                losePanelObj = Instantiate(prefabLosePanel);
                losePanelObj.transform.SetParent(canvas.transform, false);
                losePanel = losePanelObj.GetComponent<LosePanelScript>();
                losePanel.showStatistic(crystalBlue, crystalGreen, crystalRed);
            }
        }

        public void addCoins(int val)
        {
            coins += val;
        }

        public void addFruits(int val)
        {
            fruits += val;
            levelStatus.collectedFruits++;
        }

        public void addCrystals(int type)
        {
            if (type == 1)
            {
                crystalBlue = true;
                levelStatus.hasBlueCrystal = true;
            }
            else if (type == 2)
            {
                crystalGreen = true;
                levelStatus.hasGreenCrystal = true;
            }
            else
            {
                crystalRed = true;
                levelStatus.hasRedCrystal = true;
            }
        }

        public void showAllCoins()
        {
            all_coins_amount = GameObject.Find("AllAmount").GetComponent<Text>();

            string allCoins = PlayerPrefs.GetString("allCoins", null);
            if (allCoins == null)
                allCoins = "0";

            string filler = "";
            int zeros = 4 - allCoins.ToString().Length;
            for (int i = 0; i < zeros; i++)
                filler += "0";
            filler += allCoins.ToString();
            all_coins_amount.text = filler;
        }

    }
