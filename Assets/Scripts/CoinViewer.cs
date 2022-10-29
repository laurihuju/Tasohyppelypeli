using UnityEngine;
using UnityEngine.UI;

public class CoinViewer : MonoBehaviour
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text percentText;

    private void OnEnable()
    {
        if(percentText != null)
        {
            coinText.text = CoinManager.instance.GetCoinAmount() + " / " + LevelLoader.instance.GetActiveLevel().GetCoinAmount();

            float coinsPercent = ((float)CoinManager.instance.GetCoinAmount() / (float)LevelLoader.instance.GetActiveLevel().GetCoinAmount()) * 100f;

            if (coinsPercent >= 99)
            {
                percentText.text = "Hienoa! Keräsit " + coinsPercent.ToString("0.0") + "% tason kolikoista!";
            }
            else if (coinsPercent >= 90)
            {
                percentText.text = "Keräsit " + coinsPercent.ToString("0.0") + "% tason kolikoista!";
            }
            else
            {
                percentText.text = "Keräsit " + coinsPercent.ToString("0.0") + "% tason kolikoista.";
            }
        } else
        {
            coinText.text = GameController.coinAmount.ToString();
        }
    }
}
