using System.Collections.Generic;
using UnityEngine;

public class Llama : MonoBehaviour
{
    [SerializeField]
    private Vector3 _passPosition;
    [SerializeField]
    private Vector3 _passRotation;
    private Dictionary<string, int> _wordWithPoints = new Dictionary<string, int>();
    private int _points;

    public int ComplimentPoints => _points;

    public void AcceptCompliment(List<string> compliment)
    {
        _points = compliment.Count;

        foreach (string word in compliment)
        {
            if (_wordWithPoints.ContainsKey(word))
            {
                _points -= 1;
                _points += _wordWithPoints[word];
            }
        }

        Debug.Log(_points);
    }

    public void SetWordWithPoints(Dictionary<string, int> dictionary)
    {
        _wordWithPoints = dictionary;
    }

    public void Pass()
    {
        transform.localPosition = _passPosition;
        transform.rotation = Quaternion.Euler(_passRotation);
    }
}