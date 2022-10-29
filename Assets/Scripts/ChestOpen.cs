using System.Collections;
using UnityEngine;

public class ChestOpen : EnemyDeath
{
    [SerializeField] private Sprite openSprite;

    [SerializeField] private int coinAmount;
    public override IEnumerator Death()
    {
        GetComponent<SpriteRenderer>().sprite = openSprite;
        CoinManager.instance.AddCoins(coinAmount);
        HintSystem.instance.ShowHint("+ " + coinAmount + " kolikkoa", 5f, 2);
        yield return null;
    }
}
