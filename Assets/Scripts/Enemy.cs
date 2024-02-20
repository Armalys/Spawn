using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _movedSpeed;

    public void StartMoving()
    {
        StartCoroutine(MoveForward());
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    private IEnumerator MoveForward()
    {
        while (true)
        {
            transform.Translate(Vector3.forward * _movedSpeed * Time.deltaTime);
            yield return null;
        }
    }
}