using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField]
    private GameObject _mindCloud;
    [SerializeField]
    private TextAsset _dictionary;
    [SerializeField]
    private Button _button;
    private WordSet[] _wordSets;
    private int _wordSetIndex = 0;

    // Start is called before the first frame update
    private void Start()
    {
        Dictionary dictionary = (Dictionary)JsonUtility.FromJson(_dictionary.text, typeof(Dictionary));

        _wordSets = dictionary.WordSets;

        ShowWords();
    }

    private void ShowWords()
    {
        foreach(string word in _wordSets[_wordSetIndex].Words)
        {
            Vector3 position = new Vector3();
            position.y = Random.Range(-210, 310);
            position.x = Random.Range(-375, 375);

            _button.transform.InverseTransformPoint(position);

            Button currentButton = Instantiate(_button, _mindCloud.transform);
            currentButton.transform.localPosition = position;
            currentButton.GetComponentInChildren<TextMeshProUGUI>().SetText(word);
        }

        _wordSetIndex++;
    }
}
