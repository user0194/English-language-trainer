using UnityEngine;
using UnityEngine.UI;

public class CardPair
{
    public Button cardButton;
    public string word;
    public string wordPairID;
    public bool isMatched = false;
    public bool isWrong = false;
    public bool IsRevealed = false;

    public CardPair(Button button, string cardWord, string pairID)
    {
        cardButton = button;
        word = cardWord;
        wordPairID = pairID;
    }
    public Button ReturnButton()
    {
        return cardButton;
    }
}
