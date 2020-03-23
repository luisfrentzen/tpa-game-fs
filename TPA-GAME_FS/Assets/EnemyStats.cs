using UnityEngine;


public class EnemyStats : MonoBehaviour
{

    public CharacterStats playerStats;
    public int level;
    public int health;
    public int damage;
    public int exp;

    Transform target;

    void Awake()
    {
        playerStats = PlayerManager.instance.player.GetComponent<CharacterStats>();
        level = playerStats.level;
        health = level * 17;
        damage = level * 2;
        exp = level * 9;
    }

    void Update()
    {

    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        //Debug.Log(transform.name + " takes damage");

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        playerStats.exp += exp;
        Destroy(target);
        //Debug.Log(transform.name + " died");
    }
}
