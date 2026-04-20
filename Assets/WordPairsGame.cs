using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class WordPairsGame : MiniGame
{
    public Button[] leftButtons;
    public Button[] rightButtons;

    private Button selectedButtonLeft = null;
    private Button selectedButtonRight = null;

    public Sprite normalBg;
    public Sprite waitBg;
    public Sprite correctBg;
    public Sprite wrongBg;

    private Color normalColor = new Color(140f / 255f, 112f / 255f, 233f / 255f);
    private Color waitColor = new Color(246f / 255f, 171f / 255f, 66f / 255f);
    private Color correctColor = new Color(121f / 255f, 154f / 255f, 67f / 255f);
    private Color wrongColor = new Color(240f / 255f, 114f / 255f, 74f / 255f);

    public Text roundText;
    public GameObject uiManagerObject;

    private List<CardPair> leftCards = new List<CardPair>();
    private List<CardPair> rightCards = new List<CardPair>();
    private WordPairsData pairs;

    private CardPair selectedLeftCard;
    private CardPair selectedRightCard;

    private int foundPairsCount = 0;
    private int correctPairsInRound = 0;
    private int wrongPairsInRound = 0;
    private int resultTotalRounds = 0;
    private bool canSelect = true;

    protected override void Start()
    {
        base.Start();
        LoadGameData();
        SetupRound();
    }

    /// <summary>
    /// Загрузка данных игр
    /// </summary>
    private void LoadGameData()
    {
        pairs = GameDataManager.LoadWordPairs();
    }

    /// <summary>
    /// Получение случайных пар слов
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    private List<WordPair> GetRandomWordPairs(int count)
    {
        List<WordPair> result = new List<WordPair>();
        List<WordPair> copyPairs = new List<WordPair>(pairs.wordPairs);
        for(int i = 0; i < count && copyPairs.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, copyPairs.Count);
            result.Add(copyPairs[randomIndex]);
            copyPairs.RemoveAt(randomIndex);
        }
        return result;
    }

    /// <summary>
    /// Клик по левой кнопке
    /// </summary>
    /// <param name="clickedButton"></param>
    public void OnLeftButtonClicked(Button clickedButton)
    {
        int index = -1;
        for(int i = 0; i < leftButtons.Length; i++)
        {
            if(leftButtons[i] == clickedButton)
            {
                index = i;
                break;
            }
        }

        if(!canSelect || index < 0 || index >= leftButtons.Length || leftButtons[index] == null)
        {
            return;
        }

        CardPair clickedCard = leftCards[index];
        if(clickedCard.isMatched || clickedCard.isWrong)
        {
            return;
        } 

        if(selectedButtonLeft == leftButtons[index])
        {
            ResetViewButton(selectedButtonLeft);
            selectedButtonLeft = null;
            selectedLeftCard = null;
            return;
        }

        if(selectedButtonLeft != null)
        {
            ResetViewButton(selectedButtonLeft);
        }

        selectedButtonLeft = leftButtons[index];
        selectedLeftCard = clickedCard;

        SetViewButton(selectedButtonLeft, waitBg, waitColor);

        if(selectedButtonRight != null && selectedRightCard != null)
        {
            CheckPair();
        }
    }

    /// <summary>
    /// Клик по правой кнопке
    /// </summary>
    /// <param name="clickedButton"></param>
    public void OnRightButtonClicked(Button clickedButton)
    {
        int index = -1;
        for (int i = 0; i < rightButtons.Length; i++)
        {
            if (rightButtons[i] == clickedButton)
            {
                index = i;
                break;
            }
        }

        if (!canSelect || index < 0 || index >= rightButtons.Length || rightButtons[index] == null)
        {
            return;
        }

        CardPair clickedCard = rightCards[index];
        if (clickedCard.isMatched || clickedCard.isWrong)
        {
            return;
        }

        if (selectedButtonRight == rightButtons[index])
        {
            ResetViewButton(selectedButtonRight);
            selectedButtonRight = null;
            selectedRightCard = null;
            return;
        }

        if (selectedButtonRight != null)
        {
            ResetViewButton(selectedButtonRight);
        }

        selectedButtonRight = rightButtons[index];
        selectedRightCard = clickedCard;

        SetViewButton(selectedButtonRight, waitBg, waitColor);

        if (selectedButtonLeft != null && selectedLeftCard != null)
        {
            CheckPair();
        }
    }

    /// <summary>
    /// Проверка выбранной пары
    /// </summary>
    private void CheckPair()
    {
        if (selectedButtonLeft == null || selectedButtonRight == null || selectedLeftCard == null || selectedRightCard == null)
            return;
        canSelect = false;
        bool isCorrect = selectedLeftCard.wordPairID == selectedRightCard.wordPairID;
        if (isCorrect)
        {
            SetViewButton(selectedButtonLeft, correctBg, correctColor);
            SetViewButton(selectedButtonRight, correctBg, correctColor);

            selectedLeftCard.isMatched = true;
            selectedRightCard.isMatched = true;

            foundPairsCount++;
            correctPairsInRound++;

            selectedButtonLeft.interactable = false;
            selectedButtonRight.interactable = false;

            selectedButtonLeft = null;
            selectedButtonRight = null;
            selectedLeftCard = null;
            selectedRightCard= null;
            canSelect = true;
        }
        else
        {
            SetViewButton(selectedButtonLeft, wrongBg, wrongColor);
            SetViewButton(selectedButtonRight, wrongBg, wrongColor);

            selectedLeftCard.isWrong = true;
            selectedRightCard.isWrong = true;

            wrongPairsInRound++;

            selectedButtonLeft.interactable = false;
            selectedButtonRight.interactable = false;

            selectedButtonLeft = null;
            selectedButtonRight = null;
            selectedLeftCard = null;
            selectedRightCard = null;
            canSelect = true;
        }

        if (foundPairsCount >= 4)
        {
            Invoke("RoundFinished", 1f);
        }
        else
            CheckContinueRound();
    }

    /// <summary>
    /// Проверка возможности продолжения раунда
    /// </summary>
    private void CheckContinueRound()
    {
        bool availableCards = false;

        foreach(var card in leftCards)
        {
            if(!card.isMatched && !card.isWrong)
            {
                availableCards = true; 
                break;
            }
        }

        if (availableCards)
        {
            foreach(var card in rightCards)
            {
                if(!card.isMatched && !card.isWrong)
                    return;
            }
        }

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
        if(buttonText != null)
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
        if(button != null && button.interactable)
            SetViewButton(button, normalBg, normalColor);
    }

    /// <summary>
    /// Начало следующего раунда
    /// </summary>
    protected override void StartNextRound()
    {
        currentRound++;
        if(currentRound > totalRounds)
        {
            GameFinished();
            return;
        }
        SetupRound();
    }

    /// <summary>
    /// Настройка раунда
    /// </summary>
    private void SetupRound()
    {
        if(pairs.wordPairs.Length == 0)
        {
            GameFinished();
            return;
        }
        UpdateRoundText();

        foundPairsCount = 0;
        correctPairsInRound = 0;
        wrongPairsInRound = 0;
        canSelect = true;
        selectedButtonLeft = null;
        selectedButtonRight = null;
        selectedLeftCard = null;
        selectedRightCard = null;

        foreach(var button in leftButtons)
        {
            if(button != null)
            {
                button.interactable = true;
                ResetViewButton(button);
            }
        }
        foreach(var button in rightButtons)
        {
            if (button != null)
            {
                button.interactable = true;
                ResetViewButton(button);
            }
        }
        List<WordPair> randomPairs = GetRandomWordPairs(4);
        leftCards.Clear();
        rightCards.Clear();

        for(int i = 0;i<4 && i < leftButtons.Length; i++)
        {
            WordPair pair = randomPairs[i];
            CardPair card = new CardPair(leftButtons[i], pair.word, pair.wordPairID);
            leftCards.Add(card);

            Text buttonText = leftButtons[i].GetComponentInChildren<Text>();
            if(buttonText != null)
                buttonText.text = pair.word;
        }

        List<CardPair> rightColumnCards = new List<CardPair>();
        for(int i = 0;i < 4 && i < rightButtons.Length; i++)
        {
            WordPair pair = randomPairs[i];
            CardPair card = new CardPair(rightButtons[i], pair.translation, pair.wordPairID);
            rightColumnCards.Add(card);
        }

        for(int i = 0; i < rightColumnCards.Count; i++)
        {
            int randomIndex = Random.Range(0, rightColumnCards.Count);
            CardPair temp = rightColumnCards[i];
            rightColumnCards[i] = rightColumnCards[randomIndex];
            rightColumnCards[randomIndex] = temp;
        }

        for(int i = 0; i < rightColumnCards.Count && i < rightButtons.Length;i++)
        {
            CardPair card = rightColumnCards[i];
            rightCards.Add(card);

            Text buttonText = rightButtons[i].GetComponentInChildren<Text>();
            if(buttonText != null)
                buttonText.text = card.word;
        }

        foreach(var leftCard in leftCards)
        {
            foreach(var rightCard in rightCards)
            {
                if (leftCard.wordPairID == rightCard.wordPairID)
                    break;
            }
        }
    }

    /// <summary>
    /// Завершение раунда
    /// </summary>
    protected override void RoundFinished()
    {
        if (correctPairsInRound > 1)
            resultTotalRounds++;
        Invoke("StartNextRound", 2f);
    } 

    /// <summary>
    /// Завершение игры
    /// </summary>
    protected override void GameFinished()
    {
        bool isSuccessful = (resultTotalRounds > (totalRounds/2f));
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
    /// Объявление текста раунда
    /// </summary>
    private void UpdateRoundText()
    {
        if (roundText != null)
            roundText.text = $"Раунд {currentRound}";
    }
}
