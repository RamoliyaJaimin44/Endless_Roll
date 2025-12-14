using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Boundary Settings")]
    public GameObject groundObject;
    private float minX;
    private float maxX;

    [Header("Movement Settings")]
    public float forwardSpeed = 5f;
    public float normalSpeed = 5f;
    public float boostedSpeed = 10f;
    public float jumpForce = 5f;

    [Header("Speed Boost Settings")]
    public Slider speedBar;
    public float maxSpeedValue = 100f;
    public float drainRate = 20f;
    private float currentSpeedValue = 0f;
    private bool isSpeedBoosting = false;

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public float meterScaleFactor = 1f;
    public float score;
    private float startZ;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public GameObject gamePanel;

    private Rigidbody rb;
    private bool isGrounded = true;

    private bool ismove;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startZ = transform.position.z;
        forwardSpeed = normalSpeed;

        if (speedBar != null)
        {
            speedBar.maxValue = maxSpeedValue;
            speedBar.value = currentSpeedValue;
        }

        if (groundObject != null)
        {
            Renderer groundRenderer = groundObject.GetComponent<Renderer>();
            if (groundRenderer != null)
            {
                minX = groundRenderer.bounds.min.x;
                maxX = groundRenderer.bounds.max.x;
            }
        }
        ismove = true;
    }

    void Update()
    {
        if (ismove)
        {
            HandleSpeedBoost();

            Vector3 currentVelocity = rb.velocity;
            rb.velocity = new Vector3(currentVelocity.x, currentVelocity.y, forwardSpeed);

            float horizontal = Input.GetAxis("Horizontal");
            rb.velocity = new Vector3(horizontal * forwardSpeed, rb.velocity.y, forwardSpeed);
        }

        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        JumpButton();
        UpdateScore();
    }

    void HandleSpeedBoost()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (currentSpeedValue > 0f)
            {
                isSpeedBoosting = true;
                forwardSpeed = boostedSpeed;
                currentSpeedValue -= drainRate * Time.deltaTime;
                currentSpeedValue = Mathf.Max(currentSpeedValue, 0f);
            }
            else
            {
                isSpeedBoosting = false;
                forwardSpeed = normalSpeed;
            }
        }
        else
        {
            isSpeedBoosting = false;
            forwardSpeed = normalSpeed;
        }

        if (speedBar != null)
        {
            speedBar.value = currentSpeedValue;
        }
    }

    public void AddSpeedValue(float amount)
    {
        currentSpeedValue += amount;
        currentSpeedValue = Mathf.Min(currentSpeedValue, maxSpeedValue);

        if (speedBar != null)
        {
            speedBar.value = currentSpeedValue;
        }
    }

    void JumpButton()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && ismove)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void UpdateScore()
    {
        score = (transform.position.z - startZ) * meterScaleFactor;
        scoreText.text = score.ToString("F1") + " m";
    }
    public bool IsMoving()
    {
        return ismove;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            ismove = false;
            gameOverPanel.SetActive(true);
            gamePanel.SetActive(false);
        }
    }
}
