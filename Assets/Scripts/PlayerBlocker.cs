using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerBlocker : MonoBehaviour
{
    [SerializeField]
    private int _mimimumPointsToPass;
    [SerializeField]
    private Llama[] _lamas;
    [SerializeField]
    private BoxCollider _collider;

    public bool CanPlayerPass()
    {
        int pointsSum = 0;
        foreach (var llama in _lamas)
        {
            pointsSum += llama.ComplimentPoints;
        }

        Debug.Log(pointsSum / _lamas.Length);

        if (pointsSum / _lamas.Length > _mimimumPointsToPass)
        {
            return true;
        }

        return false;
    }

    public void Pass()
    {
        foreach (var llama in _lamas)
        {
            llama.Pass();
        }

        _collider.enabled = false;
    }
}