using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _movedSpeed;

    public void StartMoving(Vector3 targetPoint)
    {
        StartCoroutine(Move(targetPoint));
    }

    private IEnumerator Move(Vector3 targetPoint)
    {
        float minDistance = 0.1f;

        while (Vector3.Distance(transform.position, targetPoint) >= minDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, _movedSpeed * Time.deltaTime);
            yield return null;
        }
    }
}