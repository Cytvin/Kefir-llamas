using TMPro;
using UnityEngine;

public class MessagePopup : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private TextMeshProUGUI _messageText;
    private static MessagePopup _instance;
    private bool _lastPlayerMoveState;

    public static MessagePopup Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance == this) 
        {
            Destroy(gameObject);
        }

        gameObject.SetActive(false);
    }

    public void Show(string message)
    {
        gameObject.SetActive(true);
        _messageText.SetText(message);
        _lastPlayerMoveState = _player.IsMoveEnable;
        _player.SetMoveStateTo(false);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        _messageText.SetText("");
        _player.SetMoveStateTo(_lastPlayerMoveState);
    }
}