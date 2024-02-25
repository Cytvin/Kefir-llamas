using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ComplimentBoard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _complimentText;
    [SerializeField]
    private PlayerBlocker _playerBlocker;
    [SerializeField]
    private Button _deleteButton;
    [SerializeField]
    private Llama[] _llamas;
    [SerializeField]
    private GameObject _complimentPopup;
    [SerializeField]
    private Player _player;
    private List<WordCard> _wordCards = new List<WordCard>();

    public void PaidCompliment()
    {
        if (_wordCards.Count == 0)
        {
            MessagePopup.Instance.Show("Я определенно должен что-то сказать!");
            return;
        }

        List<string> wordForCompliment = _wordCards.Select(x => x.Word.TrimEnd('.')).ToList();

        foreach (Llama llama in _llamas)
        {
            llama.AcceptCompliment(wordForCompliment);
        }

        if(_playerBlocker.CanPlayerPass())
        {
            _playerBlocker.Pass();
            _player.SetMoveStateTo(true);
            MessagePopup.Instance.Show("Комплимент понравился ламам!\r\n" +
                "Я могу пройти!");
            _complimentPopup.gameObject.SetActive(false);
        }
        else
        {
            MessagePopup.Instance.Show("Ламам не понравился мой комплимент(\r\n" +
                "Нужно придумать что-то другое");
        }
    }

    public void AddWordCard(WordCard wordCard)
    {
        if (_wordCards.Count == 0)
        {
            _complimentText.SetText("");
        }

        _wordCards.Add(wordCard);
        _complimentText.text += $"{wordCard.Word} ";
        EnableDeleteButtonIfComplimentNotEmpty();
    }

    private void EnableDeleteButtonIfComplimentNotEmpty()
    {
        if (_wordCards.Count == 0)
        {
            _deleteButton.gameObject.SetActive(false);
        }
        else
        {
            _deleteButton.gameObject.SetActive(true);
        }   
    }

    public void DeleteLastWord()
    {
        int lastWordIndex = _wordCards.Count - 1;
        DeleteLastWordFromText(_wordCards[lastWordIndex].Word);
        _wordCards[lastWordIndex].Detach();
        _wordCards.RemoveAt(lastWordIndex);
        EnableDeleteButtonIfComplimentNotEmpty();
    }

    private void DeleteLastWordFromText(string lastWord)
    {
        _complimentText.text = _complimentText.text.TrimEnd($"{lastWord} ");
    }
}