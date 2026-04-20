using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public Button wordPairsButton;
    public Button sentenceButton;
    public Button wordButton;
    public Button settingsButton;
    public Button exitButton;

    public void GameButtonClicked(Button clickedButton)
    {
        string nameButton = clickedButton.gameObject.name;
        if(nameButton != null)
        {
            switch (nameButton)
            {
                case "wordPairsButton":
                    SceneManager.LoadScene("MiniGame1Scene");
                    break;
                case "sentenceButton":
                    SceneManager.LoadScene("MiniGame2Scene");
                    break;
                case "wordButton":
                    SceneManager.LoadScene("MiniGame3Scene");
                    break;
            }
        }
    }
    public void ExitButtonClicked()
    {
        Application.Quit();
    }
}
