using UnityEngine;

public class GameController : MonoBehaviour
{
    public static int coinAmount;
    public static float playerDamage;
    public static float playerMaxHealth;
    public static int activeLevel;
    public static int highestCompletedLevel;
    public static bool isGamePaused;

    public float defaultPlayerDamage;
    public float defaultPlayerMaxHealth;

    public static GameController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(this);
        }

        LoadSavedData();

        activeLevel = -1;

        isGamePaused = false;
    }

    public void LevelCompleted()
    {
        coinAmount += CoinManager.instance.GetCoinAmount();

        PlayerPrefs.SetInt("CoinAmount", coinAmount);

        if(highestCompletedLevel < activeLevel)
        {
            highestCompletedLevel = activeLevel;
            PlayerPrefs.SetInt("HighestCompletedLevel", highestCompletedLevel);
        }
    }

    public void SetPlayerMaxHealth(float maxHealth)
    {
        playerMaxHealth = maxHealth;

        PlayerPrefs.SetFloat("PlayerMaxHealth", playerMaxHealth);
    }

    public void SetPlayerDamage(float damage)
    {
        playerDamage = damage;

        PlayerPrefs.SetFloat("PlayerDamage", playerDamage);
    }

    public void SetCoinAmount(int amount)
    {
        coinAmount = amount;

        PlayerPrefs.SetInt("CoinAmount", coinAmount);
    }

    public void LoadSavedData()
    {
        LoadCoinAmount();
        LoadHighestCompletedLevel();
        LoadPlayerDamage();
        LoadPlayerMaxHealth();
    }

    public void LoadPlayerMaxHealth()
    {
        if (PlayerPrefs.HasKey("PlayerMaxHealth"))
        {
            playerMaxHealth = PlayerPrefs.GetFloat("PlayerMaxHealth");
        }
        else
        {
            playerMaxHealth = defaultPlayerMaxHealth;
        }
    }

    public void LoadPlayerDamage()
    {
        if (PlayerPrefs.HasKey("PlayerDamage"))
        {
            playerDamage = PlayerPrefs.GetFloat("PlayerDamage");
        }
        else
        {
            playerDamage = defaultPlayerDamage;
        }
    }

    public void LoadHighestCompletedLevel()
    {
        if (PlayerPrefs.HasKey("HighestCompletedLevel"))
        {
            highestCompletedLevel = PlayerPrefs.GetInt("HighestCompletedLevel");
        }
        else
        {
            highestCompletedLevel = -1;
        }
    }

    public void LoadCoinAmount()
    {
        if (PlayerPrefs.HasKey("CoinAmount"))
        {
            coinAmount = PlayerPrefs.GetInt("CoinAmount");
        }
        else
        {
            coinAmount = 0;
        }
    }
}
