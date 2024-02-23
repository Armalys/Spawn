using UnityEngine;

[RequireComponent(typeof(Destroyer))]
[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour
{
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}