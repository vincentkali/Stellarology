using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ResumeButton()
    {
        FindObjectOfType<AudioManager>().Play("ClickButton");
        FindObjectOfType<AudioManager>().Play("ThemeForLevels");
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }
    public void RestartButton()
    {
        FindObjectOfType<AudioManager>().Play("ClickButton");
        FindObjectOfType<AudioManager>().Play("ThemeForLevels");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitButton()
    {
        FindObjectOfType<AudioManager>().Play("ClickButton");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map");
        FindObjectOfType<AudioManager>().Play("ThemeForMap");
    }
}