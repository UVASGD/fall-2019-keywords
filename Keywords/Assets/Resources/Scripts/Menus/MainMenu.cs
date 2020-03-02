using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    private GameObject MainMenuUI;
    private GameObject CreditsUI;
    private bool creditsActive = false;

    private void Start() {
        MainMenuUI = transform.Find("MainMenu").gameObject;
        CreditsUI = transform.Find("CreditsMenu").gameObject;
    }

    public void ShowCredits() {
        MainMenuUI.SetActive(false);
        CreditsUI.SetActive(true);
        creditsActive = true;
    }

    public void ShowMainMenu() {
        CreditsUI.SetActive(false);
        MainMenuUI.SetActive(true);
        creditsActive = false;
    }

    public void ToggleCredits() {
        if (creditsActive) { ShowMainMenu(); } else { ShowCredits(); }
    }

    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TwoVTwo() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Quit() {
        Application.Quit();
    }

    public void ChangeSFXLevel(Slider slider) {

    }

    public void ChangeMusicLevel(Slider slider) {

    }
}
