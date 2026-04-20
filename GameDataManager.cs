using System;

public class GameDataManager : MonoBehaviour
{
	public GameDataManager()
	{
	}
    public static WordPairsData LoadWordPairs()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("wordPairsFile");
        if(!CheckData(jsonFile, "wordPairsFile"))
        {
            UIManager.ShowMessage("Файл не найден, используем стандартные слова");
            return GetDefaultWordPairs();
        }
        else return JsonUtility.FromJson<WordPairsData>(jsonFile.text) ?? GetDefaultWordPairs();
    }

    public static SentencesData LoadSentences()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("sentencesFile");
        if (!CheckData(jsonFile, "sentencesFile"))
        {
            UIManager.ShowMessage("Файл не найден, используем стандартные слова");
            return GetDefaultSentences();
        }
        else return JsonUtility.FromJson<SentencesData>(jsonFile.text) ?? GetDefaultSentences();
    }

    public static WordsData LoadWords()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("wordsFile");
        if (!CheckData(jsonFile, "wordsFile"))
        {
            UIManager.ShowMessage("Файл не найден, используем стандартные слова");
            return GetDefaultWords();
        }
        else return JsonUtility.FromJson<WordsData>(jsonFile.text) ?? GetDefaultWords();
    }

    private static bool CheckData(TextAsset file, string fileName)
    {
        if (file == null)
        {
            Debug.LogError($"{fileName} не найден");
            return false;
        }
        if(string.IsNullOrEmpty(file.text))
        {
            Debug.LogError($"{fileName} пустой");
            return false;
        }
        return true;
    }

    private static WordPairsData GetDefaultWordPairs()
    {
        var data = new WordPairsFile();
        data.wordPairs = new WordPair[]
        {
            new WordPair { word = "Hello", translation = "Привет" },
            new WordPair { word = "Goodbye", translation = "До свидания" },
            new WordPair { word = "Please", translation = "Пожалуйста" },
            new WordPair { word = "Sorry", translation = "Извините" },
            new WordPair { word = "Thank you", translation = "Спасибо" },
            new WordPair { word = "Name", translation = "Имя" },
            new WordPair { word = "Family", translation = "Семья" },
            new WordPair { word = "Friend", translation = "Друг" },
            new WordPair { word = "Home", translation = "Дом" },
            new WordPair { word = "School", translation = "Школа" },
            new WordPair { word = "Work", translation = "Работа" },
            new WordPair { word = "Money", translation = "Деньги" },
            new WordPair { word = "Cash", translation = "Наличные" },
            new WordPair { word = "Letter", translation = "Письмо" },
            new WordPair { word = "Food", translation = "Еда" },
            new WordPair { word = "Drink", translation = "Напиток" },
            new WordPair { word = "Meat", translation = "Мясо" },
            new WordPair { word = "Chicken", translation = "Курица" },
            new WordPair { word = "Salad", translation = "Салат" },
            new WordPair { word = "Fruit", translation = "Фрукты" }
        };
        return data;
    }

    private static SentencesData GetDefaultSentences()
    {
        var data = new SentencesFile();
        data.sentences = new Sentence[]
        {
        new Sentence {
            sentences = "They {0} to the school.",
            correctWord = "go",
            wrongWords = new string[] { "eat", "sleep", "drink" },
            translation = "Они ходят в школу."
        },
        new Sentence {
            sentences = "I {0} apples.",
            correctWord = "like",
            wrongWords = new string[] { "open", "read", "listen" },
            translation = "Я люблю яблоки."
        },
        new Sentence {
            sentences = "She {0} a book.",
            correctWord = "reads",
            wrongWords = new string[] { "plays", "cooks", "drinks" },
            translation = "Она читает книгу."
        },
        new Sentence {
            sentences = "I {0} English.",
            correctWord = "study",
            wrongWords = new string[] { "jump", "run", "open" },
            translation = "Я изучаю английский."
        },
        new Sentence {
            sentences = "We {0} the violins.",
            correctWord = "play",
            wrongWords = new string[] { "cooks", "read", "swim" },
            translation = "Мы играем на скрипках."
        },
        new Sentence {
            sentences = "I {0} apples.",
            correctWord = "like",
            wrongWords = new string[] { "open", "read", "listen" },
            translation = "Я люблю яблоки."
        },
        new Sentence {
            sentences = "It {0} in winter.",
            correctWord = "snows",
            wrongWords = new string[] { "walks", "talks", "sings" },
            translation = "Зимой идёт снег."
        },
        new Sentence {
            sentences = "Cats {0} milk.",
            correctWord = "like",
            wrongWords = new string[] { "drive", "listen", "jump" },
            translation = "Кошки любят молоко."
        },
        new Sentence {
            sentences = "She {0} in a hospital.",
            correctWord = "works",
            wrongWords = new string[] { "sleeps", "study", "flies" },
            translation = "Она работает в больнице."
        },
        new Sentence {
            sentences = "I {0} the letter.",
            correctWord = "read",
            wrongWords = new string[] { "go", "play", "eat" },
            translation = "Она работает в больнице."
        }
        };
        return data;
    }

    private static WordsData GetDefaultWords()
    {
        var data = new WordsFile();
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

public class WordPair
{
    public string word;
    public string translation;
}

public class WordPairsData
{
    public WordPair[] wordPairs;
}

public class Sentence
{
    public string sentences;
    public string correctWord;
    public string[] wrongWords;
    public string translation;
}

public class SentencesData
{
    public Sentence[] sentences;
}

public class Word
{
    public string word;
    public string translation;
}

public class WordsData
{
    public Word[] words;
}