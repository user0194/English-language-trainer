using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public abstract class MiniGame : MonoBehaviour
{
    protected int currentRound = 1;
    protected const int totalRounds = 5;

    protected abstract void StartNextRound();
    protected abstract void RoundFinished();
    protected abstract void GameFinished();

    protected virtual void Start()
    {
        WordPairsData wordData = GameDataManager.LoadWordPairs();
        SentencesData sentenceData = GameDataManager.LoadSentences();
        WordsData wordsData = GameDataManager.LoadWords();
    }

    protected virtual void StartNewGame()
    {
        currentRound = 0;
        StartNextRound();
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
