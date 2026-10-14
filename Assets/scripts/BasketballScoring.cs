using UnityEngine;

public class BasketScoring : MonoBehaviour
{
    public BasketballGameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball"))
        {
            gameManager.MadeBasket();
        }
    }
}
