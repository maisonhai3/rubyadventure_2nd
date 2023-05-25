using UnityEngine;

/// <summary>
///     This class handle Enemy behaviour. It make them walk back & forth as long as they aren't fixed, and then just idle
///     without being able to interact with the player anymore once fixed.
/// </summary>
public class Enemy : MonoBehaviour
{
    // ====== ENEMY MOVEMENT ========
    public float speed;
    public float timeToChange;
    public bool horizontal;

    public GameObject smokeParticleEffect;
    public ParticleSystem fixedParticleEffect;

    public AudioClip hitSound;
    public AudioClip fixedSound;

    // ===== ANIMATION ========
    private Animator animator;

    // ================= SOUNDS =======================
    private AudioSource audioSource;
    public Vector2 direction = Vector2.right;
    private float remainingTimeToChange;

    private bool repaired;

    private Rigidbody2D rigidbody2d;

    public void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        remainingTimeToChange = timeToChange;

        direction = horizontal ? Vector2.right : Vector2.down;

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (repaired)
            return;

        remainingTimeToChange -= Time.deltaTime;

        if (remainingTimeToChange <= 0)
        {
            remainingTimeToChange += timeToChange;
            GetDirection();
        }

        animator.SetFloat("ForwardX", direction.x);
        animator.SetFloat("ForwardY", direction.y);
    }

    public virtual void FixedUpdate()
    {
        rigidbody2d.MovePosition(rigidbody2d.position + direction * speed * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (repaired)
            return;

        var controller = other.collider.GetComponent<RubyController>();

        if (controller != null)
            controller.ChangeHealth(-1);
    }

    public void Fix()
    {
        animator.SetTrigger("Fixed");
        repaired = true;

        smokeParticleEffect.SetActive(false);

        Instantiate(fixedParticleEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);

        //we don't want that enemy to react to the player or bullet anymore, remove its rigidbody from the simulation
        rigidbody2d.simulated = false;

        audioSource.Stop();
        audioSource.PlayOneShot(hitSound);
        audioSource.PlayOneShot(fixedSound);
    }

    public virtual void GetDirection()
    {
        direction *= -1; // Reverse direction.
    }
}