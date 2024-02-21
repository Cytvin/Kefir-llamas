using System;
using UnityEngine;

[Serializable]
public class Dictionary
{
    [SerializeField]
    private WordSet[] _wordSets;

    public WordSet[] WordSets => _wordSets;
}
