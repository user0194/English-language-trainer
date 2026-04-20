using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SentencesGame : MiniGame
{
    public Button[] answerButtons;
    public Sprite normalBg;
    public Sprite correctBg;
    public Sprite wrongBg;
    private Color normalColor = new Color(140f / 255f, 112f / 255f, 233f / 255f);
    private Color waitColor = new Color(246f / 255f, 171f / 255f, 66f / 255f);
    private Color correctColor = new Color(121f / 255f, 154f / 255f, 67f / 255f);
    private Color wrongColor = new Color(240f / 255f, 114f / 255f, 74f / 255f);
    public Text sentence;
    public Text roundText;
    public GameObject uiManagerObject;
    private SentencesData answers;
    private Sentence currentSentence;
    private string correctAnswer;
    private int correctSentenceInRound = 0;
    private int resultTotalRounds = 0;

    protected override void Start()
    {
        base.Start();
        LoadGameData();
        SetupRound();
    }

    /// <summary>
    /// Загрузка данных игры
    /// </summary>
    private void LoadGameData()
    {
        answers = GameDataManager.LoadSentences();
    }

    private void SetupRound()
    {
        if(answers.sentences.Length == 0)
        {
            GameFinished();
            return;
        }

        currentSentence = null;
        correctAnswer = null;

        int randomIndex = Random.Range(0, answers.sentences.Length);
        currentSentence = answers.sentences[randomIndex];
        correctAnswer = currentSentence.correctWord;

        if (sentence != null)
            sentence.text = currentSentence.sentence;

        List<string> allAnswers = new List<string>(currentSentence.wrongWords);
        allAnswers.Add(correctAnswer);
        for(int i = 0;i < allAnswers.Count;i++)
        {
            int ri = Random.Range(0, allAnswers.Count);
            string tmp = allAnswers[i];
            allAnswers[i] = allAnswers[ri];
            allAnswers[ri] = tmp;
        }

        for(int j = 0; j < answerButtons.Length; j++)
        {
            if(j < allAnswers.Count)
            {
                string tmp = allAnswers[j];
                Text buttonText = answerButtons[j].GetComponentInChildren<Text>();
                if(buttonText != null) 
                    buttonText.text = tmp;
                ResetViewButton(answerButtons[j]);
                answerButtons[j].interactable = true;
            }
            else
            {
                if(answerButtons[j] != null)
                {
                    answerButtons[j].interactable = false;
                    Text buttonText = answerButtons[j].GetComponentInChildren<Text>();
                    if (buttonText != null)
                        buttonText.text = "";
                    ResetViewButton(answerButtons[j]);
                }
            }
        }
        UpdateRoundText();
    }

    /// <summary>
    /// Обработчик клика по кнопке ответа
    /// </summary>
    /// <param name="clickedButton"></param>
    public void OnAnswerButtonClicked(Button clickedButton)
    {
        foreach(var btn in answerButtons)
        {
            if(btn != null) btn.interactable = false;
        }
        Text buttonText = clickedButton.GetComponentInChildren<Text>();
        string selectedAnswer = buttonText != null ? buttonText.text : "";
        bool isCorrect = selectedAnswer == correctAnswer;
        if (isCorrect)
        {
            SetViewButton(clickedButton, correctBg, correctColor);
            correctSentenceInRound++;
        }
        else
            SetViewButton(clickedButton, wrongBg, wrongColor);
        Invoke("RoundFinished", 1f);
    }

    /// <summary>
    /// Установка внешнего вида кнопки
    /// </summary>
    /// <param name="button"></param>
    /// <param name="sprite"></param>
    /// <param name="textColor"></param>
    private void SetViewButton(Button button, Sprite sprite, Color textColor)
    {
        if (button == null) return;
        Image buttonImage = button.GetComponent<Image>();
        Text buttonText = button.GetComponentInChildren<Text>();
        if(buttonImage != null && sprite != null)
            buttonImage.sprite = sprite;
        if (buttonText != null)
        {
            buttonText.color = textColor;
            ColorBlock colors = button.colors;
            colors.disabledColor = Color.white;
            button.colors = colors;
        }
    }

    /// <summary>
    /// Сброс внешнего вида кнопки
    /// </summary>
    /// <param name="button"></param>
    private void ResetViewButton(Button button)
    {
        if(button != null) 
            SetViewButton(button,normalBg,normalColor);
    }

    protected override void StartNextRound()
    {
        currentRound++;
        if(currentRound > totalRounds)
        {
            GameFinished();
            return;
        }
        correctSentenceInRound = 0;
        currentSentence = null;
        correctAnswer = null;

        foreach(var button in answerButtons)
        {
            ResetViewButton(button);
            if (button != null) button.interactable = true;
        }
        SetupRound();
    }

    /// <summary>
    /// Завершение раунда
    /// </summary>
    protected override void RoundFinished()
    {
        if(correctSentenceInRound >= 1)
            resultTotalRounds++;
        correctSentenceInRound = 0;
        Invoke("StartNextRound", 2f);
    }

    /// <summary>
    /// Завершение игры
    /// </summary>
    protected override void GameFinished()
    {
        bool isSuccessful = (resultTotalRounds > (totalRounds/2));
        if(uiManagerObject != null)
        {
            UIManager uiManager = uiManagerObject.GetComponent<UIManager>();
            if(uiManager != null)
            {
                uiManager.isSuccessful = isSuccessful;
                uiManagerObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Обновление текста раунда
    /// </summary>
    private void UpdateRoundText()
    {
        if (roundText != null)
            roundText.text = $"Раунд {currentRound}";
    }
}
