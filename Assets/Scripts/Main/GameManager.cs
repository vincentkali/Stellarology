using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int LEVEL;
    
    //public PlayerCollision playerCollision;
    //public PlayerMove playerMove;
    private GameObject player;
    private GameObject destination;
    private PlayerCollision playerCollision;
    private PlayerMove playerMove;

    private bool levelCleared = false;
    [SerializeField] private const int NUM_OF_REWARD = 3;

    private float _timeUsed = 0;

    public float TimeUsed
    {
        get => _timeUsed;

        set
        {
            _timeUsed = value;
        }
    }
    public static float totalTimeUsed;

    [SerializeField] private GameObject pauseMenuUI;

    [SerializeField] private ScoreBoard scoreBoardUI;

    private IEnumerator Start ()
    {
        //FindObjectOfType<AudioManager>().Play("ThemeForLevels");

        while (!levelCleared)
        {
            player = GameObject.Find("Player");
            destination = GameObject.Find("Destination");
            playerCollision = player.GetComponent<PlayerCollision>();
            playerMove = player.GetComponent<PlayerMove>();
            yield return PlayGame();
        }
    }

    private IEnumerator PlayGame ()
    {
        //#region Debug
        
        //    Debug.Log("rewardCount: " + playerCollision.RewardCount);
        
        //#endregion

        _timeUsed += Time.deltaTime;
        playerMove.Move();

        #region Level Finished

        if (playerCollision.HasReachedDestination)
            {
                playerMove.enabled = false;
                
                player.transform.position = new Vector3 (destination.transform.position.x, destination.transform.position.y + 2f, destination.transform.position.z);
                //gameObject.transform.Rotate(0.0f, 0.0f, 0.0f,Space.World);
                player.transform.rotation = Quaternion.identity;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                levelCleared = true;

                if (LEVEL <= 4)
                {
                    MapManager.canAccessLevelN[LEVEL + 1] = true;
                }
            

                totalTimeUsed = TimeUsed;
                scoreBoardUI.Setup(playerCollision.RewardCount, totalTimeUsed);
                Debug.Log(totalTimeUsed);

                FindObjectOfType<AudioManager>().Stop("ThemeForLevels");
                FindObjectOfType<AudioManager>().Play("ScoreBoard");
            }

        #endregion


        yield return null;
    }

    public void PauseButton ()
    {
        FindObjectOfType<AudioManager>().Stop("ThemeForLevels");
        FindObjectOfType<AudioManager>().Play("ClickButton");

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
