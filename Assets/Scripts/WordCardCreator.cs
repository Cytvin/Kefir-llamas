using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class WordCardCreator : MonoBehaviour
{
    [Serializable]
    private class WordWithPoints
    {
        public string Word;
        public int Points;
    }

    [Serializable]
    private class WordWithPointsDictionary
    {
        public WordWithPoints[] WordWithPoints;
    }

    [SerializeField]
    private GameObject _mindCloud;
    [SerializeField]
    private TextAsset _dictionary;
    [SerializeField]
    private GameObject _wordBoardPrefab;
    [SerializeField]
    private Llama[] _llamas = new Llama[5];
    [SerializeField]
    private TextAsset[] _wordWithPoints = new TextAsset[5];
    [SerializeField]
    private float _xOffset = 100;
    [SerializeField]
    private float _yOffset = 100;
    [SerializeField]
    private float _yExtraBottomOffset = 200;
    private float _maxX;
    private float _maxY;
    private WordSet _wordSet;

    private void Start()
    {
        RectTransform rectTransform = _mindCloud.GetComponent<RectTransform>();
        _maxX = rectTransform.rect.width / 2 - _xOffset;
        _maxY = rectTransform.rect.height / 2 - _yOffset;

        WordSet loadedWordSet = (WordSet)JsonUtility.FromJson(_dictionary.text, typeof(WordSet));
        _wordSet = loadedWordSet;

        LoadLlamasDictionaryFromJson();
        CreateWordCards();
    }

    private void LoadLlamasDictionaryFromJson()
    {
        int llamaIndex = 0;
        foreach (TextAsset json in _wordWithPoints)
        {
            WordWithPointsDictionary wordsWithPoints = (WordWithPointsDictionary)JsonUtility.FromJson(json.text, typeof(WordWithPointsDictionary));
            _llamas[llamaIndex].SetWordWithPoints(wordsWithPoints.WordWithPoints.ToDictionary(word => word.Word, word => word.Points));
            llamaIndex++;
        }
    }

    private void CreateWordCards()
    {
        foreach(string word in _wordSet.Words)
        {
            Vector3 position = new Vector3();
            position.y = UnityEngine.Random.Range(-_maxY + _yExtraBottomOffset, _maxY);
            position.x = UnityEngine.Random.Range(-_maxX, _maxX);

            _wordBoardPrefab.transform.InverseTransformPoint(position);

            WordCard wordBoard = Instantiate(_wordBoardPrefab, _mindCloud.transform).GetComponent<WordCard>();
            wordBoard.SetPosition(position);
            wordBoard.SetWord(word);
        }
    }
}