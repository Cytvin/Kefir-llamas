using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordCard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;
    private RectTransform _rectTransform;
    private Transform _initialParrent;
    private Vector3 _lastStaticPosition;
    private float _initialHeight;
    private float _initialWidth;
    private bool _isTaken = false;
    private string _word;

    public string Word => _word;

    private void Awake()
    {
        _initialParrent = transform.parent;
        _rectTransform = GetComponent<RectTransform>();
        _initialWidth = _rectTransform.rect.width;
        _initialHeight = _rectTransform.rect.height;
    }

    private void Update()
    {
        if (_isTaken)
        {
            if (Input.GetMouseButtonUp(0))
            {
                _isTaken = false;

                if (CanAttachToComplimentBoard(out ComplimentBoard compliment))
                {
                    AttachTo(compliment);
                }
                else
                {
                    transform.SetParent(_initialParrent);
                    _lastStaticPosition = transform.localPosition;
                }
                return;
            }

            transform.position = Input.mousePosition;
        }
    }

    public void OnTaken()
    {
        GetComponent<Image>().enabled = true;
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _initialWidth);
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _initialHeight);

        _isTaken = true;
        transform.SetParent(_initialParrent.parent);
    }

    public void Detach()
    {
        gameObject.SetActive(true);
        transform.SetParent(_initialParrent);
        transform.localPosition = _lastStaticPosition;
    }

    public void SetWord(string word)
    {
        _text.SetText(word);
        _word = word;
    }

    public void SetPosition(Vector3 position)
    {
        _lastStaticPosition = position;
        transform.localPosition = position;
    }

    private bool CanAttachToComplimentBoard(out ComplimentBoard compliment)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            pointerId = -1
        };

        pointerData.position = Input.mousePosition;

        List<RaycastResult> raycastResult = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResult);

        foreach(RaycastResult result in raycastResult)
        {
            if (result.gameObject.TryGetComponent<ComplimentBoard>(out compliment))
            {
                return true;
            }
        }

        compliment = null;

        return false;
    }

    private void AttachTo(ComplimentBoard compliment)
    {
        gameObject.SetActive(false);
        transform.SetParent(compliment.transform);
        compliment.AddWordCard(this);
    }
}