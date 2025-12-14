using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;
    public GameObject _particalEffect;

    void Start()
    {
        //Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.collider.gameObject);
        }
    }

    void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        AudioManager.Instance.CollideSound();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        _particalEffect.SetActive(true);
        Invoke(nameof(OffObject), 0.3f);
    }

    void OffObject()
    {
        Destroy(gameObject);
    }
}
