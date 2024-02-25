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
            MessagePopup.Instance.Show("���� �� ��������� ����\r\n ���� � �� ����� �� ����������!\r\n" +
                "��� ��� ��� ���������� � ����:\r\n" +
                "1 ����: ���������� ������������ �������� � ���������� �������. ������� �������.\r\n" +
                "2 ����: ������������ ������������ ��������� ���������� ������;\r\n" +
                "3 ����: ������� ������� ����������. ������� ��������, � ��� ����� �� �������;\r\n" +
                "4 ����: �������� �� ��������������� �����������, �������� �� ������������ ������ ��������;\r\n" +
                "5 ����: ��������������� ��������������� ��������. �����������.\r\n" +
                "� ������ ����� ����.\r\n" +
                "�������� ���-�� ���������!");
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