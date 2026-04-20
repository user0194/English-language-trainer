using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameObject correctResult;
    public GameObject wrongResult;

    public bool isSuccessful = false;

    private void Start()
    {
        if (isSuccessful)
        {
            if (correctResult != null)
                correctResult.SetActive(true);
        }
        else
        {
            if (wrongResult != null)
                wrongResult.SetActive(true);
        }
    } 

    public void ReturnMenuClicked()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
