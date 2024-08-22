using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamagable
{
    [SerializeField]private float maxHealth = 5f;
    private float currentHealth;
    private void Start() {
        currentHealth=maxHealth;
    }
   public void Damage(float damageAmount){
        currentHealth-=maxHealth;
        if (currentHealth<=0)
        {
            Destroy(gameObject);
        }
   }
}
