using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public static class GameDataManager
{
    public static WordPairsData LoadWordPairs()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("WordPairsData");
        if (!CheckData(jsonFile, "WordPairsData"))
        {
            return GetDefaultWordPairs();
        }
        else return JsonUtility.FromJson<WordPairsData>(jsonFile.text) ?? GetDefaultWordPairs();
    }

    public static SentencesData LoadSentences()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("SentencesData");
        if (!CheckData(jsonFile, "SentencesData"))
        {
            return GetDefaultSentences();
        }
        else return JsonUtility.FromJson<SentencesData>(jsonFile.text) ?? GetDefaultSentences();
    }

    public static WordsData LoadWords()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("WordsData");
        if (!CheckData(jsonFile, "WordsData"))
        {
            return GetDefaultWords();
        }
        else return JsonUtility.FromJson<WordsData>(jsonFile.text) ?? GetDefaultWords();
    }

    public static bool CheckData(TextAsset file, string fileName)
    {
        if(file == null)
        {
            Debug.LogError($"{fileName} не найден");
            return false;
        }
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError($"{fileName} пустой");
            return false;
        }
        return true;
    }

    public static WordPairsData GetDefaultWordPairs() 
    {
        var data = new WordPairsData();
        data.wordPairs = new WordPair[]
        {
            new WordPair { wordPairID = "wordPairID_001", word = "Hello", translation = "Привет" },
            new WordPair { wordPairID = "wordPairID_002", word = "Goodbye", translation = "До свидания" },
            new WordPair { wordPairID = "wordPairID_003", word = "Please", translation = "Пожалуйста" },
            new WordPair { wordPairID = "wordPairID_004", word = "Sorry", translation = "Извините" },
            new WordPair { wordPairID = "wordPairID_005", word = "Thank you", translation = "Спасибо" },
            new WordPair { wordPairID = "wordPairID_006", word = "Name", translation = "Имя" },
            new WordPair { wordPairID = "wordPairID_007", word = "Family", translation = "Семья" },
            new WordPair { wordPairID = "wordPairID_008", word = "Friend", translation = "Друг" },
            new WordPair { wordPairID = "wordPairID_009", word = "Home", translation = "Дом" },
            new WordPair { wordPairID = "wordPairID_010", word = "School", translation = "Школа" },
            new WordPair { wordPairID = "wordPairID_011", word = "Work", translation = "Работа" },
            new WordPair { wordPairID = "wordPairID_012", word = "Money", translation = "Деньги" },
            new WordPair { wordPairID = "wordPairID_013", word = "Cash", translation = "Наличные" },
            new WordPair { wordPairID = "wordPairID_014", word = "Letter", translation = "Письмо" },
            new WordPair { wordPairID = "wordPairID_015", word = "Food", translation = "Еда" },
            new WordPair { wordPairID = "wordPairID_016", word = "Drink", translation = "Напиток" },
            new WordPair { wordPairID = "wordPairID_017", word = "Meat", translation = "Мясо" },
            new WordPair { wordPairID = "wordPairID_018", word = "Chicken", translation = "Курица" },
            new WordPair { wordPairID = "wordPairID_019", word = "Salad", translation = "Салат" },
            new WordPair { wordPairID = "wordPairID_020", word = "Fruit", translation = "Фрукты" }
        };
        return data;
    }

    public static SentencesData GetDefaultSentences() 
    {
        var data = new SentencesData();
        data.sentences = new Sentence[]
        {
        new Sentence {
            sentence = "They {0} to the school.",
            correctWord = "go",
            wrongWords = new string[] { "eat", "sleep", "drink" },
            translation = "Они ходят в школу."
        },
        new Sentence {
            sentence = "I {0} apples.",
            correctWord = "like",
            wrongWords = new string[] { "open", "read", "listen" },
            translation = "Я люблю яблоки."
        },
        new Sentence {
            sentence = "She {0} a book.",
            correctWord = "reads",
            wrongWords = new string[] { "plays", "cooks", "drinks" },
            translation = "Она читает книгу."
        },
        new Sentence {
            sentence = "I {0} English.",
            correctWord = "study",
            wrongWords = new string[] { "jump", "run", "open" },
            translation = "Я изучаю английский."
        },
        new Sentence {
            sentence = "We {0} the violins.",
            correctWord = "play",
            wrongWords = new string[] { "cooks", "read", "swim" },
            translation = "Мы играем на скрипках."
        },
        new Sentence {
            sentence = "I {0} apples.",
            correctWord = "like",
            wrongWords = new string[] { "open", "read", "listen" },
            translation = "Я люблю яблоки."
        },
        new Sentence {
            sentence = "It {0} in winter.",
            correctWord = "snows",
            wrongWords = new string[] { "walks", "talks", "sings" },
            translation = "Зимой идёт снег."
        },
        new Sentence {
            sentence = "Cats {0} milk.",
            correctWord = "like",
            wrongWords = new string[] { "drive", "listen", "jump" },
            translation = "Кошки любят молоко."
        },
        new Sentence {
            sentence = "She {0} in a hospital.",
            correctWord = "works",
            wrongWords = new string[] { "sleeps", "study", "flies" },
            translation = "Она работает в больнице."
        },
        new Sentence {
            sentence = "I {0} the letter.",
            correctWord = "read",
            wrongWords = new string[] { "go", "play", "eat" },
            translation = "Она работает в больнице."
        }
        };
        return data;
    }

    public static WordsData GetDefaultWords() 
    {
        var data = new WordsData();
        data.words = new Word[]
        {
            new Word { word = "TIME", translation = "Время"},
            new Word { word = "LIFE", translation = "Жизнь" },
            new Word { word = "FOOD", translation = "Еда" },
            new Word { word = "DOOR", translation = "Дверь"},
            new Word { word = "BOOK", translation = "Книга"},
            new Word { word = "CITY", translation = "Город" },
            new Word { word = "HAND", translation = "Рука" },
            new Word { word = "WORK", translation = "Работа"},
            new Word { word = "FIRE", translation = "Огонь"},
            new Word { word = "WIND", translation = "Ветер"}
        };
        return data;
    }
}
