using UnityEngine;
using UnityEngine.UI;

public class UpgradeDialog : MonoBehaviour
{
    [SerializeField] private Text title;

    [SerializeField] private Image upgradeImage;
    [SerializeField] private Text upgradeOrgAmountText;
    [SerializeField] private Text upgradeAmountText;
    [SerializeField] private Text upgradeNewAmountText;
    [SerializeField] private Button upgradeButton;

    [SerializeField] private Image coinImage;
    [SerializeField] private Text coinOrgAmountText;
    [SerializeField] private Text coinConsumptionText;
    [SerializeField] private Text coinNewAmountText;

    [SerializeField] private Sprite coinSprite;
    [SerializeField] private Sprite healthSprite;
    [SerializeField] private Sprite damageSprite;

    [SerializeField] private float upgradeAmount;
    [SerializeField] private int upgradePrice;

    public static UpgradeDialog instance;

    private int whatToUpgrade;

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

        this.gameObject.SetActive(false);
        whatToUpgrade = 0;
        Prepare(0);
    }

    public void Prepare(int whatToUpgrade)
    {
        this.whatToUpgrade = whatToUpgrade;

        coinImage.sprite = coinSprite;
        coinOrgAmountText.text = GameController.coinAmount.ToString();
        coinConsumptionText.text = "- " + upgradePrice.ToString();
        coinNewAmountText.text = "= " + (GameController.coinAmount - upgradePrice);

        if (whatToUpgrade == 0)
        {
            title.text = "Päivitä elämiä";

            upgradeImage.sprite = healthSprite;

            upgradeOrgAmountText.text = GameController.playerMaxHealth.ToString();
            upgradeAmountText.text = "+ " + upgradeAmount.ToString();
            upgradeNewAmountText.text = "= " + (GameController.playerMaxHealth + upgradeAmount);
        } else if (whatToUpgrade == 1)
        {
            title.text = "Päivitä vahinkoa";

            upgradeImage.sprite = damageSprite;

            upgradeOrgAmountText.text = GameController.playerDamage.ToString();
            upgradeAmountText.text = "+ " + upgradeAmount.ToString();
            upgradeNewAmountText.text = "= " + (GameController.playerDamage + upgradeAmount);
        }

        if(GameController.coinAmount - upgradePrice >= 0)
        {
            upgradeButton.interactable = true;
        } else
        {
            upgradeButton.interactable = false;
        }
    }

    public void Upgrade()
    {
        if(whatToUpgrade == 0)
        {
            GameController.instance.SetPlayerMaxHealth(GameController.playerMaxHealth + upgradeAmount);
        } else if (whatToUpgrade == 1)
        {
            GameController.instance.SetPlayerDamage(GameController.playerDamage + upgradeAmount);
        }

        GameController.instance.SetCoinAmount(GameController.coinAmount - upgradePrice);
        Prepare(whatToUpgrade);
    }
}
