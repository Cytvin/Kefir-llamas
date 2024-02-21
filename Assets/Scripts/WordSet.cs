using System;
using UnityEngine;

[Serializable]
public class WordSet
{
    [SerializeField]
    private string[] _words;
    
    public string[] Words => _words;
}
