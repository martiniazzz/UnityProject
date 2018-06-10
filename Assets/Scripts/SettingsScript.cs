using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {

    public Sprite soundOn;
    public Sprite soundOff;
    public Sprite musicOn;
    public Sprite musicOff;

    Button closeButton;
    Button musicButton;
    Button soundButton;
    Button bgButton;

    bool music = true;
    bool sound = true;

    void Start () {
		closeButton = GameObject.Find("CloseSettings").GetComponent<Button>();
        musicButton = GameObject.Find("MusicSettings").GetComponent<Button>();
        soundButton = GameObject.Find("SoundSettings").GetComponent<Button>();
        bgButton = GameObject.Find("BgButton").GetComponent<Button>();

        closeButton.onClick.AddListener(this.onClose);
        bgButton.onClick.AddListener(this.onClose);

        musicButton.onClick.AddListener(this.onMusic);
        soundButton.onClick.AddListener(this.onSound);
    }

    public void onClose()
    {
        Destroy(this.gameObject);
    }

    void onMusic()
    {
        music = !music;
        if (music)
            getMusicButton().GetComponent<Image>().sprite = musicOn;
        else
            getMusicButton().GetComponent<Image>().sprite = musicOff;
        SoundManager.Instance.setMusicOn(music);
    }

    void onSound()
    {
        sound = !sound;
        if (sound)
            getSoundButton().GetComponent<Image>().sprite = soundOn;
        else
            getSoundButton().GetComponent<Image>().sprite = soundOff;
        SoundManager.Instance.setSoundOn(sound);
    }

    public Button getCloseButton()
    {
        return closeButton;
    }

    public Button getMusicButton()
    {
        return musicButton;
    }

    public Button getSoundButton()
    {
        return soundButton;
    }

    public Button getBgButton()
    {
        return bgButton;
    }
}
