
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float startSpeed = 10f;

    [HideInInspector]

    public float speed;

    public float health = 100;

    public int worth = 50;

    public GameObject deathEffect;


    private void Start()
    {
        speed = startSpeed; 
    }

    // Start is called before the first frame update

    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health<=0)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        PlayerStat.Money += worth;
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);

    }

    // Update is called once per frame
   
}
