using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TalkWithLlamas : MonoBehaviour
{
    [SerializeField]
    private GameObject _infoPopup;
    [SerializeField]
    private GameObject _complimentPopup;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private BoxCollider _trigger;
    private bool _canTalk = false;

    private void Update()
    {
        if (_canTalk == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _infoPopup.SetActive(false);
            _complimentPopup.SetActive(true);
            _player.SetMoveStateTo(false);
            MessagePopup.Instance.Show("Ламы не пропустям меня\r\n пока я не скажу им комплимент!\r\n" +
                "Вот что они рассказали о себе:\r\n" +
                "1 лама: Увлекается выращиванием картошки в бамбуковых горшках. Обожает персики.\r\n" +
                "2 лама: Предпочитает медитативное ткачество изысканных ковров;\r\n" +
                "3 лама: Владеет элитной химчисткой. Сильный аллергик, в том числе на персики;\r\n" +
                "4 лама: Фанатеет от археологических приключения, особенно от исследования тайных тоннелей;\r\n" +
                "5 лама: Коллекционирует экстравагантные расчески. Клаустрофоб.\r\n" +
                "В голове такая каша.\r\n" +
                "Придется что-то придумать!");
            _canTalk = false;
            _trigger.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            _infoPopup.gameObject.SetActive(true);
            _canTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            _infoPopup.gameObject.SetActive(false);
            _canTalk = false;
        }
    }
}