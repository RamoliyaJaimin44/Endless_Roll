using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bullet Info")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 20f;

    [Header("UI")]
    public TextMeshProUGUI bulletCountText;

    public int bulletCount = 50;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            FireBullet();

        UpdateUI();
    }

    public void AddBullets(int amount)
    {
        bulletCount += amount;
    }

    void FireBullet()
    {
        if (bulletCount <= 0) return;

        bulletCount--;

        GameObject b = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = b.GetComponent<Rigidbody>();
        rb.velocity = shootPoint.forward * bulletSpeed;
    }

    void UpdateUI()
    {
        if (bulletCountText != null)
            bulletCountText.text = bulletCount.ToString();
    }
}

