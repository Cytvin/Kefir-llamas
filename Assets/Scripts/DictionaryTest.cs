using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DictionaryTest : MonoBehaviour
{
    [Serializable]
    private class Word
    {
        [SerializeField]
        private string _word;
        [SerializeField]
        private int _points;
    }

    [SerializeField]
    private TextAsset _textAsset;
    [SerializeField]
    private List<Word> _words = new List<Word>();

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

    // Start is called before the first frame update
    void Start()
    {
        //LoadWords();
        //DisplayAllWord(_words);
        Wrapper<Word> wrapper = new Wrapper<Word>();
        wrapper.Items = _words.ToArray();

        string json = JsonUtility.ToJson(wrapper);
        Debug.Log(json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
