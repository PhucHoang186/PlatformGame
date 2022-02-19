using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class GameManagement : MonoBehaviour
{
    //Check if is there a save file
    private static int hasSaveFile =0;
    public float waitTime = 2f;
    //public UnityEvent events;
    public static GameManagement instance;
    [HideInInspector]
    public bool gameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public Animator ani;
    [HideInInspector]
    public bool isLoss =false;
    [SerializeField]private static bool isLoadGame = false;
    [HideInInspector]
    //public bool Wait = false;
    private void Awake()
    {
        if(pauseMenu == null)
        {
            return;
        }
        pauseMenu.transform.localPosition = new Vector3(Screen.width,0f,0f);
        if(pauseMenu!=null)
        {
            pauseMenu.SetActive(true);
        }
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void OnEnable()
    {
        if(isLoadGame)
        {
            LoadProgress();
            isLoadGame = false;

        }
        hasSaveFile = PlayerPrefs.GetInt("HasSaveFile");
    }
    public IEnumerator Load(string scenename)
    {   
        Time.timeScale = 1f;
        ani.SetTrigger("StartFade");
        yield return new WaitForSecondsRealtime(waitTime);
        isLoss = false;
        SceneManager.LoadScene(scenename);
    }
    public void LoadingScreen(string scenename)
    {
        StartCoroutine(Load(scenename));
    }
    public void ExitGame()
    {   
        Application.Quit();
        Debug.Log("Exitting...");
    }

    private void Update()
    {
        // open / close pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if(pauseMenu == null)
            {
                return;
            }
            
            if (!gameIsPaused)
            {
                PauseGame();

            }
            else if(!isLoss && gameIsPaused)
            {
                ResumeGame();
            }
        }
    }
    //resume menu
    private void ResumeGame()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenu.SetActive(false);
        FindObjectOfType<ShootingProjectile>().enabled = true;
    }
    //pause menu

    public void PauseGame()
    {
        gameIsPaused = true;
        pauseMenu.transform.DOLocalMoveX(0f, 1f).SetUpdate(true);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        FindObjectOfType<ShootingProjectile>().enabled = false;
    }
    //replay
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void OpenOption()// options for set resolution , vsync =>> On working
    {
        optionsMenu.SetActive(true);
    }
    public void CloseOption()
    {
        optionsMenu.SetActive(false);

    }
    //save game
    public void SaveProgress()
    {
        hasSaveFile = 1;
        //save player properties
        GameObject player = GameObject.Find("Player");
        PlayerPrefs.SetFloat("PlayerX",player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);

        //save enemies properties
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            PlayerPrefs.SetFloat(enemy.name + "x", enemy.transform.position.x);
            PlayerPrefs.SetFloat(enemy.name + "y", enemy.transform.position.y);
            PlayerPrefs.SetInt(enemy.name + "_health", enemy.GetComponent<Enemy>().currentHealth);
            PlayerPrefs.SetInt(enemy.name + "_direction", enemy.GetComponent<Enemy>().moveDirection);
            PlayerPrefs.SetInt(enemy.name +"isDestroy",enemy.GetComponent<Enemy>().isDestroy);
            Debug.Log(enemy.GetComponent<Enemy>().isDestroy);
        }
        //save the curent level
        PlayerPrefs.SetString("currentlevel", SceneManager.GetActiveScene().name);
        Debug.Log("Save!");
        //save the file

        PlayerPrefs.SetInt("HasSaveFile", hasSaveFile);
    }
    //load game, scene
    public void LoadProgressScene()
    {
  
        if (hasSaveFile == 1)
        {
            //save the cureent level
            Debug.Log("Load");
            LoadingScreen(PlayerPrefs.GetString("currentlevel"));
            isLoadGame = true;
        }
        else
        {
            Debug.Log("There is no save file");
        }


    }
    private void LoadProgress()
    {
        //load player properties
        GameObject player = GameObject.Find("Player");
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"));
        //load enemies properties
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.transform.position = new Vector3(PlayerPrefs.GetFloat(enemy.name + "x"), PlayerPrefs.GetFloat(enemy.name + "y"));
            Enemy enemyscript = enemy.GetComponent<Enemy>();
            StartCoroutine(enemyscript.SetHealth( PlayerPrefs.GetInt(enemy.name + "_health")));
            enemyscript.moveDirection = PlayerPrefs.GetInt(enemy.name + "_direction");
            enemy.transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, PlayerPrefs.GetInt(enemy.name + "_direction")));
            enemyscript.isDestroy = PlayerPrefs.GetInt(enemy.name + "isDestroy");
            Debug.Log(enemy.name);
            Debug.Log(enemyscript.currentHealth);

        }
    }
    public void StartNewGame()
    {
        hasSaveFile = 0;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("HasSaveFile", hasSaveFile);
        PlayerPrefs.SetInt("LevelReached", 1);
    }
    public void WinRound(int currentLevel)
    {
        int lastLevel = PlayerPrefs.GetInt("LevelReached");
        if(lastLevel < currentLevel) // check if we play at previous level, if true than we don't save progress
        {
            PlayerPrefs.SetInt("LevelReached", currentLevel);
        }
    }
}