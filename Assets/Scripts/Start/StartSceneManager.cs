using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    void Start ()
    {
        FindObjectOfType<AudioManager>().Play("ThemeForMap");
    }
    
    public void StartButton ()
    {
        FindObjectOfType<AudioManager>().Play("ClickButton");
        SceneManager.LoadScene("Map");
    }
    public void QuitButton ()
    {
        FindObjectOfType<AudioManager>().Play("ClickButton");
        Debug.Log("Quit");
        Application.Quit();
    }
}
