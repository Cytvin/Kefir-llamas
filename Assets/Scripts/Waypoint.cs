using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _destinationPoint;
    [SerializeField]
    private GameObject _player;

    private void Update()
    {
        transform.position = _player.transform.position;
        transform.LookAt(_destinationPoint.transform);
    }
}
