using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    
    public AudioSource audioSourceUI;
    public AudioSource audioSourceSound;

    enum Screen
    {
        None,
        Main,
        Settings,
        Level
    }

    public CanvasGroup mainScreen;
    public CanvasGroup settingsScreen;
    public CanvasGroup levelScreen;

    void SetCurrentScreen(Screen screen)
    {
        Utility.SetCanvasGroupEnabled(mainScreen, screen == Screen.Main);
        Utility.SetCanvasGroupEnabled(settingsScreen, screen == Screen.Settings);
        Utility.SetCanvasGroupEnabled(levelScreen, screen == Screen.Level);
    }
    
    void Start()
    {
        SetCurrentScreen(Screen.Main);
        
    }

    public void StartNewGame()
    {
        if (audioSourceUI) audioSourceUI.Play();
        SetCurrentScreen(Screen.Level);
    }

    public void OpenSettings()
    {
        if (audioSourceUI) audioSourceUI.Play();
        SetCurrentScreen(Screen.Settings);
    }

    public void CloseSettings()
    {
        if (audioSourceUI) audioSourceUI.Play();
        SetCurrentScreen(Screen.Main);
    }

    public void ExitGame()
    {
        if (audioSourceUI) audioSourceUI.Play();
        Application.Quit();
    }

    public void LevelOne()
    {
        if (audioSourceUI) audioSourceUI.Play();
        SetCurrentScreen(Screen.None);
        LoadingScreen.instance.LoadScene("SampleScene");
        
    }

    public void LevelTwo()
    {
        if (audioSourceUI) audioSourceUI.Play();
        SetCurrentScreen(Screen.None);
        LoadingScreen.instance.LoadScene("SecondScene");
        
    }

    public void TestUI()
    {
        if (audioSourceUI) audioSourceUI.Play();
    }

    public void TestEffect()
    {
        if (audioSourceSound) audioSourceSound.Play();
    }
}
