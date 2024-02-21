using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _movedSpeed;

    public void StartMoving(Vector3 direction)
    {
        StartCoroutine(Move(direction));
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    private IEnumerator Move(Vector3 direction)
    {
        float elapsedTime = 0f;
        float duration = 10f;

        while (elapsedTime < duration)
        {
            transform.Translate(direction * _movedSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        DestroyObject();
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}