using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    [HideInInspector]
   public int _currentLevel;

    public GameObject PauseMenu;

    private void Start()

    {
        Scene scene = SceneManager.GetActiveScene();
        _currentLevel = scene.buildIndex;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PauseMenu.activeInHierarchy)
        {
            Pause();
        }
    }
    public void Play()
    {
        SceneManager.LoadScene("L1");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(_currentLevel+1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void unPause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
