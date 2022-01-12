using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private Text _diamondText;
    [SerializeField] private Text _timeText;
    public float animationTime = 1f;

    private int NEXTLEVEL;

    private void Start()
    {

        //this.gameObject.SetActive(false);
        NEXTLEVEL = FindObjectOfType<GameManager>().LEVEL + 1;
    }


    public void Setup (int numOfDiamond, double timeUsed)
    {
        this.gameObject.SetActive(true);
        _diamondText.text =  numOfDiamond.ToString();
        StartCoroutine(SlidingNumber(0f, GameManager.totalTimeUsed));
        //Debug.Log("Board Setup");

    }

    public void BackButton ()
    {
        SceneManager.LoadScene("Map");
        FindObjectOfType<AudioManager>().Play("ClickButton");
        FindObjectOfType<AudioManager>().Play("ThemeForMap");
        Debug.Log("back");
    }
    public void NextLevelButton ()
    {
        SceneManager.LoadScene("Level" + NEXTLEVEL.ToString());
        FindObjectOfType<AudioManager>().Play("ClickButton");
        FindObjectOfType<AudioManager>().Play("ThemeForLevels");
    }


    public IEnumerator SlidingNumber(float initNum, float targetNum)
    {
        for(float showNum = 0f; showNum < targetNum;
            showNum += (animationTime * Time.deltaTime) * (targetNum - initNum))
        {
             _timeText.text = showNum.ToString();
            yield return null;
        }
    }


}
