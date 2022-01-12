using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    //public static bool[] hasPassedLevelN = new bool[3];
    public static bool[] canAccessLevelN = new bool[6];

    [SerializeField] private GameObject[] levelNButton;
    
    void Start ()
    {
        canAccessLevelN[0] = true;
        
        for(int n = 0; n < canAccessLevelN.Length; n++)
        {
            if(!canAccessLevelN[n])
            {
                Image image = levelNButton[n].GetComponent<Image>();
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0.4f);
            }
        }
    }
    
    public void BackButton ()
    {
        SceneManager.LoadScene("Start");
        FindObjectOfType<AudioManager>().Play("ClickButton");
    }

    public void LevelNButton (int n)
    {
        if (canAccessLevelN[n])
        {
            SceneManager.LoadScene("Level" + n.ToString());
            FindObjectOfType<AudioManager>().Play("ClickButton");
            FindObjectOfType<AudioManager>().Stop("ThemeForMap");
            FindObjectOfType<AudioManager>().Play("ThemeForLevels");
        }
    }
}
