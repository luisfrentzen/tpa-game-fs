using UnityEngine;


public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 15;
    public int currentHealth;

    public int level = 1;
    public int damage = 2;
    public int exp = 0;
    public int maxExp = 16;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(exp >= maxExp)
        {
            int temp = exp - maxExp;
            levelUp();
            exp += temp;
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        //Debug.Log(transform.name + " takes damage");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //Debug.Log(transform.name + " died");
    }

    public void levelUp()
    {
        damage = damage + level * 2;
        maxHealth = maxHealth + level * 15;
        maxExp = maxExp + level * 16;
        currentHealth = maxHealth;
        exp = maxExp;
    }
}
