using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float _destroyDelay;

    public void StartDestroying()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(_destroyDelay);
        Destroy(gameObject);
    }
}