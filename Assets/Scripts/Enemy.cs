using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _movedSpeed;

    public IEnumerator MoveForward()
    {
        while (true)
        {
            transform.Translate(transform.forward * _movedSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}