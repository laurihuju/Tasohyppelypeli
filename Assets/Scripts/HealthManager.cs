using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float healthGenerationTime;
    [SerializeField] private float healthGenerationAmount;
    [SerializeField] private bool isPlayer;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject gameOverPanel;

    private float health;
    private bool isHealthGenerating;
    private bool isDead;

    private void Start()
    {
        if (GetComponent<PlayerController>() != null)
        {
            maxHealth = GameController.playerMaxHealth;
        }

        health = maxHealth;
        isHealthGenerating = false;
        isDead = false;

        if(healthBar != null)
        {
            healthBar.Initialize(maxHealth);
        }
    }
    void Update()
    {
        if(!isHealthGenerating && health < maxHealth && !GameController.isGamePaused)
        {
            StartCoroutine(GenerateHealth());
        }
    }

    private IEnumerator GenerateHealth()
    {
        isHealthGenerating = true;

        yield return new WaitForSeconds(healthGenerationTime);

        health += healthGenerationAmount;

        if(health > maxHealth)
        {
            health = maxHealth;
        }

        isHealthGenerating = false;

        if (healthBar != null)
        {
            healthBar.SetHealth(health);
        }
    }

    public void TakeDamage(float amountOfDamage)
    {
        health -= amountOfDamage;

        if(health <= 0 && isPlayer && !isDead)
        {
            PlayerDeath();
            isDead = true;

        } else if (health <= 0 && !isPlayer && !isDead)
        {
            StartCoroutine(this.gameObject.GetComponent<EnemyDeath>().Death());
            isDead = true;
        }

        if (healthBar != null)
        {
            healthBar.SetHealth(health);
        }
    }

    public void PlayerDeath()
    {
        gameOverPanel.SetActive(true);

        GameController.isGamePaused = true;
    }
}
