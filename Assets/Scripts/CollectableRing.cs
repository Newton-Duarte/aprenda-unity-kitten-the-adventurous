using UnityEngine;

public class CollectableRing : MonoBehaviour
{
    GameController _gameController;

    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    internal void collect()
    {
        _gameController.addCoins(1);
        Destroy(gameObject);
    }
}
