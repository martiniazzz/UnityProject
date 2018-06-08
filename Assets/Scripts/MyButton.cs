using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyButton : MonoBehaviour
{
    public UnityEvent signalOnClick = new UnityEvent();

    public void _onClick()
    {
        this.signalOnClick.Invoke();
    }
}
