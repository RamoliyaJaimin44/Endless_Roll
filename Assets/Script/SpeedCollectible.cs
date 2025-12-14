using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCollectible : MonoBehaviour
{
    public float speedAmount = 20f;

    void Start()
    {
        //Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.AddSpeedValue(speedAmount);
            }

            Destroy(gameObject);
        }

    }
}
