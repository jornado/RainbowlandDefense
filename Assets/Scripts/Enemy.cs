using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 15f;
    [HideInInspector]
    public float speed;
    public float startHealth = 100f;
    private float health;
    public int worth = 5;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start() {
        speed = startSpeed;
        health = GetStartHealth();
    }

	float GetStartHealth()
	{
		int wave = WaveSpawner.instance.GetCurrentWave();

		if (wave >= 15)
		{
			return startHealth * 1.4f;
		}
		else if (wave >= 25)
		{
			return startHealth * 1.6f;
		}
		else if (wave >= 35)
		{
			return startHealth * 1.8f;
		}
		else if (wave >= 45)
		{
			return startHealth * 2.0f;
		}
		else if (wave >= 55)
		{
			return startHealth * 2.2f;
		}
		else if (wave >= 65)
		{
			return startHealth * 3.5f;
		}
		else if (wave >= 85)
		{
			return startHealth * 5f;
		}
		else
		{
			return startHealth;
		}
	}

	public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += worth;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }

    public void Slow(float slowAmount)
    {
        speed = startSpeed * (1f - slowAmount);
    }
}
