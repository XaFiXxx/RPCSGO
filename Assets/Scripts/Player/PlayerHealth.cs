using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;

    [Header("Armor")]
    [SerializeField] private int maxArmor = 100;
    [SerializeField] private int currentArmor = 0;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    public int MaxArmor => maxArmor;
    public int CurrentArmor => currentArmor;

    public bool IsDead => currentHealth <= 0;

    void Awake()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentArmor = Mathf.Clamp(currentArmor, 0, maxArmor);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || IsDead)
            return;

        int remainingDamage = damage;

        if (currentArmor > 0)
        {
            int absorbedDamage =
                Mathf.Min(currentArmor, remainingDamage);

            currentArmor -= absorbedDamage;
            remainingDamage -= absorbedDamage;
        }

        if (remainingDamage > 0)
        {
            currentHealth -= remainingDamage;
            currentHealth = Mathf.Max(currentHealth, 0);
        }

        Debug.Log(
            "Dégâts reçus : " + damage +
            " | Vie : " + currentHealth +
            " | Kevlar : " + currentArmor
        );

        if (IsDead)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || IsDead)
            return;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        Debug.Log("Vie : " + currentHealth);
    }

    public void AddArmor(int amount)
    {
        if (amount <= 0 || IsDead)
            return;

        currentArmor += amount;
        currentArmor = Mathf.Min(currentArmor, maxArmor);

        Debug.Log("Kevlar : " + currentArmor);
    }

    private void Die()
    {
        Debug.Log("Le joueur est mort.");
    }
}