using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    public int bulletAmount = 10;   // +10 bullets each pickup

    void Start()
    {
        //Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooter = other.GetComponent<PlayerShooting>();
            if (shooter != null)
                shooter.AddBullets(bulletAmount);

            Destroy(gameObject);
        }
    }
    
}

