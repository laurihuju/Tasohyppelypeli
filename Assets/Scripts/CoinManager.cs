using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    private int coins;
    [SerializeField] private Text coinViewText;

    public static CoinManager instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        coins = 0;

        coinViewText.text = "0 / " + LevelLoader.instance.GetActiveLevel().GetCoinAmount();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            coins++;
            coinViewText.text = coins + " / " + LevelLoader.instance.GetActiveLevel().GetCoinAmount();
            HintSystem.instance.ShowHint("+ " + 1 + " kolikkoa", 5f, 2);
            Destroy(collision.gameObject);
        }
    }

    public int GetCoinAmount()
    {
        return coins;
    }

    public void AddCoins(int coinAmount)
    {
        coins += coinAmount;
        coinViewText.text = coins + " / " + LevelLoader.instance.GetActiveLevel().GetCoinAmount();
    }
}
