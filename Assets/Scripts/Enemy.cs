using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _movedSpeed;

    public void StartMoving(Vector3 targetPosition)
    {
        StartCoroutine(MoveForward(targetPosition));
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    private IEnumerator MoveForward(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movedSpeed * Time.deltaTime);
            yield return null;
        }

        DestroyObject();
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}