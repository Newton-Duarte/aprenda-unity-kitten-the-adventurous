using UnityEngine;

public class OnInvisibleDestroy : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
