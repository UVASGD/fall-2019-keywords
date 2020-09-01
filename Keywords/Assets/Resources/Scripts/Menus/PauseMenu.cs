using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    private bool isPaused = false;
    private GameObject PauseMenuUI;
    private GameObject ResumeButton;

    private void Start() {
        transform.Find("PauseMenu").gameObject.SetActive(true);
        PauseMenuUI = transform.Find("PauseMenu").gameObject;
        ResumeButton = GameObject.Find("ResumeButton");
        Resume();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Toggle();
        }
    }

    public void Toggle() {
        if (isPaused) { Resume(); } else { Pause(); }
    }

    public void Resume() {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause() {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        EventSystem.current.SetSelectedGameObject(ResumeButton);
    }

    public void Restart() {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }

    public bool GetPaused() {
        return isPaused;
    }
}
