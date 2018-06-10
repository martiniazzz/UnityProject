using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseClicked : MonoBehaviour
{

    GameObject popupObj;
    SettingsScript popup;

    public GameObject prefab;
    public Canvas canvas;

    void showSettings()
    {
        Debug.Log("Pause clicked");
        popupObj = Instantiate(prefab);
        popupObj.transform.SetParent(canvas.transform, false);
        popup = popupObj.GetComponent<SettingsScript>();
    }

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(this.showSettings);
    }
}