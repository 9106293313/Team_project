using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerUI;
    public GameObject OptionScreen;
    public bool gameIsPaused = false;

    bool IsClosingMenu = false;
    bool IsOpeningMenu = false;

    bool IsGameOver = false;
    public GameObject GameOverMenu;

    public GameObject TabMenu;
    bool IsTabMenu = false;

    public List<AudioSource> audioSources;

    public int TotalSceneNum = 0; //�x�s�g�L�F�X���A�C�������d��+1
    [HideInInspector] public string targetSceneName;
    [HideInInspector]public Vector2 TPpos;

    public int MiniBossPercent = 30; //�H�����d�ɬO�pboss�������v�A�w�]��30

    private void OnLevelWasLoaded(int level)
    {
        switch (TotalSceneNum)
        {
            case 0:
                targetSceneName = "CardSceneA";
                break;
            case 1:
                RandomSceneBamboo();
                break;
            case 2:
                targetSceneName = "CardSceneA";
                break;
            case 3:
                RandomSceneBamboo();
                break;
            case 4:
                targetSceneName = "CardSceneB";
                break;
            case 5:
                targetSceneName = "Store";
                TPpos.x = 0f;
                TPpos.y = 0f;
                break;
            case 6:
                targetSceneName = "Boss2";
                TPpos.x = 0f;
                TPpos.y = 0f;
                break;
            case 7:
                targetSceneName = "CardSceneA";
                break;
            case 8:
                RandomSceneIce();
                break;
            case 9:
                targetSceneName = "CardSceneA";
                break;
            case 10:
                RandomSceneIce();
                break;
            case 11:
                targetSceneName = "CardSceneB";
                break;
            case 12:
                targetSceneName = "Store";
                TPpos.x = 0f;
                TPpos.y = 0f;
                break;
            case 13:
                targetSceneName = "Boss3";
                TPpos.x = 0f;
                TPpos.y = 0f;
                break;
            case 14:
                targetSceneName = "CardSceneA";
                break;
            case 15:
                RandomSceneFire();
                break;
            case 16:
                targetSceneName = "CardSceneA";
                break;
            case 17:
                RandomSceneFire();
                break;
            case 18:
                targetSceneName = "CardSceneB";
                break;
            case 19:
                targetSceneName = "Store";
                TPpos.x = 0f;
                TPpos.y = 0f;
                break;
            case 20:
                targetSceneName = "Boss1";
                TPpos.x = 0f;
                TPpos.y = 0f;
                break;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {

        if (GameObject.FindWithTag("CardSelectPanel") != null) //�p�G�������t��CardSelectPanel��tag������A�{�����~�����
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && IsGameOver==false)
        {
            if(IsTabMenu)
            {
                ToggleTabMenu();
            }
            else if(OptionScreen.activeInHierarchy != true && IsClosingMenu == false)
            {
                OpenOptions();
            }
            else if(OptionScreen.activeInHierarchy == true && IsOpeningMenu == false)
            {
                CloseOptions();
            }

        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleTabMenu();
        }
    }

    public void OpenOptions()
    {
        if (IsOpeningMenu == false)
        {
            StartCoroutine(OpenOptionDelay());
        }
        else
        {
            return;
        }
    }

    IEnumerator OpenOptionDelay()
    {
        if (IsTabMenu == true)
        {
            TabMenu.SetActive(false);
            IsTabMenu = false;
        }

        IsOpeningMenu = true;

        OptionScreen.SetActive(true);
        ClosePlayerUI();
        PauseGame();

        float endPauseTime = Time.realtimeSinceStartup + 1f;
        yield return new WaitWhile(() => Time.realtimeSinceStartup < endPauseTime); //�ۭq�q��waitTime�A���Qtimescale�v�T

        IsOpeningMenu = false;
    }
    public void CloseOptions()
    {
        if(IsClosingMenu==false)
        {
            StartCoroutine(CloseOptionDelay());
        }
        else
        {
            return;
        }
    }
    IEnumerator CloseOptionDelay()
    {
        IsClosingMenu = true;

        OptionScreen.GetComponent<OptionScreen>().DisableMenu();

        float endPauseTime = Time.realtimeSinceStartup + 0.6f;
        yield return new WaitWhile(() => Time.realtimeSinceStartup < endPauseTime); //�ۭq�q��waitTime�A���Qtimescale�v�T

        OpenPlayerUI();
        ResumeGame();

        float endPauseTime2 = Time.realtimeSinceStartup + 0.6f;
        yield return new WaitWhile(() => Time.realtimeSinceStartup < endPauseTime2); //�ۭq�q��waitTime�A���Qtimescale�v�T

        IsClosingMenu = false;
    }
    public void BackToTitle()
    {
        StartCoroutine(BackToTitleDelay());
    }
    IEnumerator BackToTitleDelay()
    {
        if(IsGameOver)
        {
            ResumeGame();
            SceneManager.LoadScene(0);
        }
        else
        {
            IsClosingMenu = true;

            OptionScreen.GetComponent<OptionScreen>().DisableMenu();

            float endPauseTime = Time.realtimeSinceStartup + 0.6f;
            yield return new WaitWhile(() => Time.realtimeSinceStartup < endPauseTime); //�ۭq�q��waitTime�A���Qtimescale�v�T

            ResumeGame();
            IsClosingMenu = false;
            SceneManager.LoadScene(0);
        }
        
    }
    void OpenPlayerUI()
    {
        playerUI.SetActive(true);
    }
    void ClosePlayerUI()
    {
        playerUI.SetActive(false);
    }

    void FindAudioSources()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        audioSources = new List<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            if (!audioSource.CompareTag("Music"))
            {
                audioSources.Add(audioSource);
            }
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0;
        gameIsPaused=true;

        FindAudioSources();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Pause();
        }
    }
    void ResumeGame()
    {
        if(!IsGameOver)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().FixedBugForResumeGame(); //�״_����ɼȰ��A�Ѱ��Ȱ��ɷ|Ĳ�o��bug
        }

        Time.timeScale = 1;
        gameIsPaused = false;

        FindAudioSources();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.UnPause();
        }
    }

    public void GameOver()
    {
        IsGameOver=true;
        StartCoroutine(StartGameOver());
    }
    IEnumerator StartGameOver()
    {
        yield return new WaitForSeconds(1f);
        GameOverMenu.SetActive(true);
        PauseGame();
    }

    public void ToggleTabMenu()
    {
        if(OptionScreen.activeInHierarchy || IsGameOver) //�p�G�ﶵ��楴�}�F�άO�C�������F�A��^
        {
            return;
        }

        if (IsTabMenu==false)
        {
            TabMenu.SetActive(true);
            IsTabMenu = true;
            PauseGame();
        }
        else
        {
            TabMenu.SetActive(false);
            IsTabMenu = false;
            ResumeGame();
        }
    }

    void RandomScene()
    {
        int RandomNum = UnityEngine.Random.Range(1, 4); //1~3
        switch (RandomNum)
        {
            case 1:
                targetSceneName = "A1";
                TPpos.x = 0f;
                TPpos.y = 0f;
                break;
            case 2:
                targetSceneName = "A2";
                TPpos.x = -25f;
                TPpos.y = 1f;
                break;
            case 3:
                targetSceneName = "A3";
                TPpos.x = -25f;
                TPpos.y = 1f;
                break;
        }  
    }
    void RandomSceneBamboo()
    {
        int RandomNum = UnityEngine.Random.Range(1, 101); //1~100
        if(RandomNum <= MiniBossPercent)
        {
            targetSceneName = "BambooMiniBoss";
            TPpos.x = 0f;
            TPpos.y = 0f;
            MiniBossPercent = 10;
        }
        else
        {
            targetSceneName = "Bamboo";
            TPpos.x = 0f;
            TPpos.y = 0f;
        }
    }
    void RandomSceneIce()
    {
        int RandomNum = UnityEngine.Random.Range(1, 101); //1~100
        if (RandomNum <= MiniBossPercent)
        {
            targetSceneName = "IceMiniBossScene";
            TPpos.x = 0f;
            TPpos.y = 0f;
            MiniBossPercent = 10;
        }
        else
        {
            targetSceneName = "IceScene";
            TPpos.x = 0f;
            TPpos.y = 0f;
        }
    }
    void RandomSceneFire()
    {
        int RandomNum = UnityEngine.Random.Range(1, 101); //1~100
        if (RandomNum <= MiniBossPercent)
        {
            targetSceneName = "FireMiniBossScene";
            TPpos.x = 0f;
            TPpos.y = 0f;
            MiniBossPercent = 10;
        }
        else
        {
            int randomNum2 = UnityEngine.Random.Range(1, 3); //1~2
            if (randomNum2==1)
            {
                targetSceneName = "FireScene1";
                TPpos.x = 0f;
                TPpos.y = 12f;
            }
            else
            {
                targetSceneName = "FireScene2";
                TPpos.x = 0f;
                TPpos.y = 12f;
            }
            
        }
    }

}
