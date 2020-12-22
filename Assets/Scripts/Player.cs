using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] int playerHealth = 100;
    [SerializeField] int playerLives = 3;
    [SerializeField] int respawnTime = 2;

    HealthBar healthBar;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    FloatingJoystick joystick;

    [Header("Player Weapon")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] ParticleSystem laserVFX;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float fireRate = 0.1f;

    [Header("Death FX")]
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.6f;

    Coroutine firingCoroutine;

    // Movement Variables
    Vector2 startPosition;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float xPadding;
    float yPadding;

    void Start()
    {
        SetUpMoveBoundaries();
        SetupHealthBar();
        startPosition = new Vector2(transform.position.x, transform.position.y);
    }


    void Update()
    {
        MoveWithJoystick();
        MoveWithKeyboard();
        Fire();
    }
    void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xPadding = GetComponent<RectTransform>().rect.width / 2;
        yPadding = GetComponent<RectTransform>().rect.height / 2;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }
    void MoveWithJoystick()
    {
        if (!joystick) { joystick = FindObjectOfType<FloatingJoystick>(); }
        var horizontal = joystick.Horizontal * Time.deltaTime * moveSpeed;
        var vertical =   joystick.Vertical * Time.deltaTime * moveSpeed;

        var newHorizontal = Mathf.Clamp(transform.position.x + horizontal, xMin, xMax);
        var newVertical =   Mathf.Clamp(transform.position.y + vertical, yMin, yMax);

        transform.position = new Vector2(newHorizontal, newVertical);
        Debug.Log(joystick.Horizontal);
        Debug.Log(joystick.Vertical);
    }
    void MoveWithKeyboard()
    {
        var horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var vertical = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newHorizontal = Mathf.Clamp(transform.position.x + horizontal, xMin, xMax);
        var newVertical = Mathf.Clamp(transform.position.y + vertical, yMin, yMax);

        transform.position = new Vector2(newHorizontal, newVertical);
    }

    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(Firing());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (firingCoroutine == null) { return; }
            StopCoroutine(firingCoroutine);
        }
    }
    IEnumerator Firing()
    {
        while (true)
        {
            Vector2 firePoint = new Vector2(transform.position.x, transform.position.y + yPadding);
            GameObject laser = Instantiate(laserPrefab, firePoint, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            Instantiate(laserVFX, firePoint, Quaternion.identity);
            yield return new WaitForSeconds(fireRate);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(collision, damageDealer);
    }

    private void ProcessHit(Collider2D collision, DamageDealer damageDealer)
    {
        playerHealth -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (!healthBar) { healthBar = FindObjectOfType<HealthBar>(); }
        healthBar.UpdateHealth(Mathf.RoundToInt(playerHealth));
        if (playerHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        if (playerLives <= 0)
        {
            FindObjectOfType<SceneNav>().GetComponent<SceneNav>().LoadGameOver();
        }
        else
        {
            SpawnPlayer();
        }
        Destroy(gameObject);
    }
    public void SetPlayerLives(int lives)
    {
        playerLives = lives;
    }
    void SpawnPlayer()
    {
        new WaitForSeconds(respawnTime);
        Player player = Instantiate(playerPrefab, startPosition, transform.rotation).GetComponent<Player>();
        player.SetPlayerLives(--playerLives);
    }

    void SetupHealthBar()
    {
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetupHealthBar(Mathf.RoundToInt(playerHealth));
        healthBar.UpdateHealth(Mathf.RoundToInt(playerHealth));
        Debug.Log("Health bar setup!");
    }
}
