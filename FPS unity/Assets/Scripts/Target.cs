using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Enemymovement enemymovement;

    private void Start()
    {
        enemymovement = gameObject.GetComponent<Enemymovement>();
    }
    public float health = 50f;
   
    public void TakeDamage(float amount)
    {
        if (health <= 0f)
        {
            enemymovement._die = true;
            Invoke("Die", 1.5f);
            
        }
        else
        {
            health -= amount;
            enemymovement._getHit = true;
            Debug.Log("Hit");
        }
        
        
    }
    
    public void Die()
    {
        
        Destroy(gameObject);
    }
}
