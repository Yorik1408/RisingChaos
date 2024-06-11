using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    private AudioSource[] allAudioSources;

    private void Start()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // ѕоказываем все текстовые блоки, которые наход€тс€ вне меню паузы
        Text[] allTexts = FindObjectsOfType<Text>();
        foreach (Text text in allTexts)
        {
            if (!IsChildOfPauseMenu(text.gameObject))
            {
                text.enabled = true;
            }
        }

        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.UnPause();
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // —крываем все текстовые блоки, которые наход€тс€ вне меню паузы
        Text[] allTexts = FindObjectsOfType<Text>();
        foreach (Text text in allTexts)
        {
            if (!IsChildOfPauseMenu(text.gameObject))
            {
                text.enabled = false;
            }
        }

        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Pause();
        }
    }

    private bool IsChildOfPauseMenu(GameObject obj)
    {
        // ѕровер€ем, €вл€етс€ ли родитель переданного объекта меню паузы
        Transform parent = obj.transform.parent;
        while (parent != null)
        {
            if (parent.gameObject == pauseMenuUI)
            {
                return true;
            }
            parent = parent.parent;
        }
        return false;
    }

    public void LoadMenu()
    {
        Debug.Log("Load");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
