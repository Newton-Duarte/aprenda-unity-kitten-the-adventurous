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
        GameObject temp = Instantiate(_gameController.getCoinPrefab, transform.position, transform.rotation);
        Destroy(temp.gameObject, 0.25f);
        _gameController.addCoins(1);
        Destroy(gameObject);
    }
}
