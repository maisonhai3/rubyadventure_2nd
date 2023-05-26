using Manager;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // ========= MOVEMENT =================
    public float speed = 4;

    // ======== HEALTH ==========
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public Transform respawnPosition;
    public ParticleSystem hitParticle;

    // ======== PROJECTILE ==========
    public GameObject projectilePrefab;

    // ======== AUDIO ==========
    public AudioClip hitSound;
    public AudioClip shootingSound;

    // ==== ANIMATION =====
    private Animator animator;

    // ================= SOUNDS =======================
    private AudioSource audioSource;

    // ======== HEALTH ==========
    private Vector2 currentInput;
    private float invincibleTimer;
    private bool isInvincible;
    private Vector2 lookDirection = new(1, 0);

    // =========== MOVEMENT ==============
    private Rigidbody2D rigidbody2d;

    // ======== HEALTH ==========
    public int health { get; private set; }

    private void Start()
    {
        // =========== MOVEMENT ==============
        rigidbody2d = GetComponent<Rigidbody2D>();

        // ======== HEALTH ==========
        invincibleTimer = -1.0f;
        health = maxHealth;

        // ==== ANIMATION =====
        animator = GetComponent<Animator>();

        // ==== AUDIO =====
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // ================= HEALTH ====================
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        // ============== MOVEMENT ======================
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        currentInput = move;

        // ============== ANIMATION =======================
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        // ============== PROJECTILE ======================
        if (Input.GetKeyDown(KeyCode.C))
            LaunchProjectile();

        // ======== DIALOGUE ==========
        if (Input.GetKeyDown(KeyCode.X))
        {
            var hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f,
                1 << LayerMask.NameToLayer("NPC"));
            if (hit.collider != null)
            {
                var character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null) character.DisplayDialog();
            }
        }
    }

    private void FixedUpdate()
    {
        var position = rigidbody2d.position;

        position = position + currentInput * speed * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    // ===================== HEALTH ==================
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            animator.SetTrigger("Hit");
            audioSource.PlayOneShot(hitSound);

            Instantiate(hitParticle, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }

        health = Mathf.Clamp(health + amount, 0, maxHealth);

        if (health == 0)
            Respawn();

        UIHealthBar.Instance.SetValue(health / (float)maxHealth);
    }

    private void Respawn()
    {
        ChangeHealth(maxHealth);
        transform.position = respawnPosition.position;
    }

    // =============== PROJECTILE ========================
    private void LaunchProjectile()
    {
        // var projectileObject =
        //     Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        var projectileObject = PoolingManager.Instance.GetPooledObject();
        if (projectileObject != null)
        {
            projectileObject.transform.position = rigidbody2d.position + Vector2.up * 0.5f;
            projectileObject.transform.rotation = Quaternion.identity;
            projectileObject.SetActive(true);
        }

        var projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        audioSource.PlayOneShot(shootingSound);
    }

    // =============== SOUND ==========================
    //Allow to play a sound on the player sound source. used by Collectible
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}