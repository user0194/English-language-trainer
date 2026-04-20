using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class WordGame : MiniGame
{
    public Text wordText;
    public Text roundText;
    public TMP_InputField translationInputField;
    public Sprite normalBg;
    public Sprite correctBg;
    public Sprite wrongBg;
    private Color normalColor = new Color(140f/255f,112f/255f,233f/255f);
    private Color correctColor = new Color(121f / 255f, 154f / 255f, 67f / 255f);
    private Color wrongColor = new Color(240f / 255f, 114f / 255f, 74f / 255f);
    public GameObject uiManagerObject;
    private Word currentWord;
    private WordsData wordsData;
    private int correctRounds = 0;

    protected override void Start()
    {
        base.Start();
        LoadGameData();
        SetupRound();
    }

    /// <summary>
    /// Загрузка данных игры из WordsData
    /// </summary>
    private void LoadGameData()
    {
        wordsData = GameDataManager.LoadWords();
        if(wordsData == null || wordsData.words == null || wordsData.words.Length == 0)
        {
            wordsData = GameDataManager.GetDefaultWords();
        }
    }

    /// <summary>
    /// Настройка раунда
    /// </summary>
    private void SetupRound()
    {
        if(wordsData.words.Length > 0)
        {
            int randomIndex = Random.Range(0, wordsData.words.Length);
            currentWord = wordsData.words[randomIndex];

            if(wordText != null)
                wordText.text = currentWord.word;
            
            if(translationInputField != null)
            {
                translationInputField.text = "";
                SetViewInput(translationInputField, normalBg, normalColor);
                translationInputField.interactable = true;
                translationInputField.onEndEdit.RemoveAllListeners();
                translationInputField.onEndEdit.AddListener(OnSubmitTranslation);
            }
        }
    }

    /// <summary>
    /// Вызывается при нажатии Enter в поле ввода
    /// </summary>
    /// <param name="input"></param>
    private void OnSubmitTranslation(string input)
    {
        CheckTranslation(input);
    }

    /// <summary>
    /// Проверка введённого перевода
    /// </summary>
    /// <param name="userTranslation"></param>
    private void CheckTranslation(string userTranslation)
    {
        if(currentWord != null)
        {
            string correctTranslation = currentWord.translation.Trim().ToLower();
            string userInput = userTranslation.Trim().ToLower();

            if(userInput == correctTranslation)
            {
                SetViewInput(translationInputField,correctBg, correctColor);
                correctRounds++;
            }
            else
                SetViewInput(translationInputField, wrongBg, wrongColor);
            if(translationInputField != null)
            {
                translationInputField.interactable = false;
                ColorBlock colors = translationInputField.colors;
                colors.normalColor = Color.white;
                colors.highlightedColor = Color.white;
                colors.pressedColor = Color.white;
                colors.selectedColor = Color.white;
                colors.disabledColor = Color.white;
                translationInputField.colors = colors;
            }
            RoundFinished();
        }
    }

    /// <summary>
    /// Установка внешнего вида
    /// </summary>
    /// <param name="tmpInputField"></param>
    /// <param name="sprite"></param>
    /// <param name="textColor"></param>
    private void SetViewInput(TMP_InputField tmpInputField, Sprite sprite, Color textColor)
    {
        if(tmpInputField != null)
        {
            UnityEngine.UI.Image bgImage = tmpInputField.GetComponent<UnityEngine.UI.Image>();
            if(bgImage != null && sprite != null) 
                bgImage.sprite = sprite;
            if(tmpInputField.textComponent != null) 
                tmpInputField.textComponent.color = textColor;
            ColorBlock colors = tmpInputField.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.white;
            colors.pressedColor = Color.white;
            colors.selectedColor = Color.white;
            colors.disabledColor = Color.white;
            tmpInputField.colors = colors;
        }
    }


    protected override void StartNextRound()
    {
        currentRound++;
        if (currentRound > totalRounds)
            GameFinished();
        else
            SetupRound();
        UpdateRoundText();
    }

    protected override void RoundFinished()
    {
        Invoke("StartNextRound", 1.0f);
    } 

    protected override void GameFinished()
    {
        if(translationInputField != null)
        {
            translationInputField.interactable = false;
            ColorBlock colors = translationInputField.colors;
            colors.disabledColor = Color.white;
            translationInputField.colors = colors;
        }
        bool isSuccess = correctRounds > (totalRounds / 2.0f);
        if(uiManagerObject  != null)
        {
            UIManager uiManager = uiManagerObject.GetComponent<UIManager>();
            if(uiManager != null )
            {
                uiManager.isSuccessful = isSuccess;
            }
        }
        if(uiManagerObject != null)
        {
            uiManagerObject.SetActive(true);
        }
    }

    private void UpdateRoundText()
    {
        if (roundText != null)
            roundText.text = $"Раунд {currentRound}";
    }
}
