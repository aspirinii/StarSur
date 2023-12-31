using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive;
    public float gameTimer;
    public float maxGameTime = 0.1f * 60f;

    [Header("# Game Player Info")]
    public int playerId;
    public int level;
    public int kill;
    public int exp;
    public float health;
    public float maxHealth = 100;
    public int[] nextExp = { 3, 6, 10, 50, 150, 210, 280, 360, 460, 600};


    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public Transform uiJoystick;
    public GameObject enemyCleaner;
    public GameObject inputDirectionArrow;

    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;

    }
    

    public void GameStart(int id)
    {
        playerId = id;
        health = 70;
        player.gameObject.SetActive(true);
        if (playerId == 2)
        {
            uiLevelUp.Select(5); // 삽 총 선택  하여 시작시점 부터 삽 총을 사용할 수 있음
        }
        else if(playerId == 3){
            uiLevelUp.Select(6);  

        }
        else if(playerId == 0){
            uiLevelUp.Select(5);  
            // check itemData Class 
            //index 5 is 5th item in itemData, 

        }
        else
        {
            uiLevelUp.Select(playerId % 2); 
        }
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);

        GameObject InputDirectionArrow = GameObject.Instantiate(inputDirectionArrow, player.transform);
        
    }

    void FixedUpdate()
    {
        //if ketboard input q, upgrade weapon
        if(Input.GetKeyDown(KeyCode.Q)){
            uiLevelUp.Select(5);  
        }
        //test code 

    }

    public void GameOver()
    {

        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(2f);
        uiResult.gameObject.SetActive(true);
        uiResult.SetResult(0); // 0 Lose Title
        Stop();
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
       
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());

    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(2f);
        uiResult.gameObject.SetActive(true);
        uiResult.SetResult(1);// 1 win Title
        Stop();
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
       
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(1); 

    }
    public void GameQuit()
    {
        Application.Quit(); // In Editor , it is not working
    }

    void Update()
    {
        if(!isLive)
            return;
        gameTimer += Time.deltaTime;

        if(gameTimer > maxGameTime){
            gameTimer = maxGameTime;
            GameVictory();
        }

    }

    public void GetExp()
    {
        if(!isLive)
            return;
        exp++;
        if(exp == nextExp[Mathf.Min(level, nextExp.Length - 1)]){
            level++;
            exp = 0;
            // uiLevelUp.Show();
            // 테스트를 위해 블록처리 
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
        uiJoystick.localScale = Vector3.zero;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
        uiJoystick.localScale = Vector3.one;

    }
        
    
}
