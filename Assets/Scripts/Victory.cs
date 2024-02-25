using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Victory : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            _player.CelebrateVictory();
        }
    }
}
