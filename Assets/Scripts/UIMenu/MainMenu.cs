using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    public string TestArea;
    public GameObject OptionScreen;
    void Start()
    {
        ResetGameInfo();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }
    public void OpenOptions()
    {
        OptionScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        OptionScreen.GetComponent<OptionScreen>().DisableMenu();
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void GoTestArea()
    {
        SceneManager.LoadScene(TestArea);
    }
    public void ResetGameInfo()//重製所有遊戲數據
    {
        CardSystem.acquiredCards.Clear(); //清除所有已獲得的塔羅牌
        CardSystem.ResetCardInfo(); //重製所有塔羅牌的資料
    }
}
