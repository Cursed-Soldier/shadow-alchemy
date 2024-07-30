using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool isMain = false;
    public GameObject mainMenuUI;
    public GameObject settingsMenuUI;
    public GameObject ChooseGameTypeUI;
    public GameObject ChooseGameLevelUI;

    public void Play()
    {
        //mainMenuUI.SetActive(false);
        //ChooseGameTypeUI.SetActive(true);
        SceneManager.LoadScene("Level_1");
    }

    public void BackInPlay()
    {
        mainMenuUI.SetActive(true);
        ChooseGameTypeUI.SetActive(false);
    }

    public void ChooseGameLevel()
    {
        ChooseGameLevelUI.SetActive(true);
        ChooseGameTypeUI.SetActive(false);
    }

    public void BackInChooseLevel()
    {
        ChooseGameTypeUI.SetActive(true);
        ChooseGameLevelUI.SetActive(false);
    }

    public void MainToSettings()
    {
        mainMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void SettingsToMain()
    {
        mainMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }

    public void Exit()
    {
        Debug.Log("Exit");
    }

}
