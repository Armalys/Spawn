using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _movedSpeed;

    public void StartMoving(Vector3 direction)
    {
        StartCoroutine(Move(direction));
    }

    private IEnumerator Move(Vector3 direction)
    {
        while (true)
        {
            transform.Translate(direction * _movedSpeed * Time.deltaTime);
            yield return null;
        }
    }
}