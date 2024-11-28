using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityModifier = 2f;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem dirtParticle;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private GameObject jumpBtn;
    [SerializeField] private GameObject restartBtn;

    private AudioSource playerAudio;
    private Rigidbody rb;
    private Animator anim;

    public bool isOnGround = true;
    public bool isGameOver = false;

    private static bool gravityInitialized = false;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        // Initialize gravity only once
        if (!gravityInitialized)
        {
            Physics.gravity *= gravityModifier;
            gravityInitialized = true;
        }

        // Initialize UI buttons
        jumpBtn.SetActive(true);
        restartBtn.SetActive(false);

        // Reset game state
        ResetPlayerState();
    }

    public void Jump()
    {
        if (isOnGround && !isGameOver)
        {
            anim.SetTrigger("Jump_trig");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            if (dirtParticle != null) dirtParticle.Stop();
            if (playerAudio != null) playerAudio.PlayOneShot(jumpSound, 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isOnGround = true;
            if (dirtParticle != null) dirtParticle.Play();
        }
        else if (collision.collider.CompareTag("OBS"))
        {
            isGameOver = true;
            Debug.Log("Game Over");

            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);

            if (explosion != null) explosion.Play();
            if (dirtParticle != null) dirtParticle.Stop();
            if (playerAudio != null) playerAudio.PlayOneShot(deathSound, 1f);

            jumpBtn.SetActive(false);
            restartBtn.SetActive(true);
        }
    }

    public void RestartBtns()
    {
        // Reset Rigidbody velocity and other states before reloading
        ResetPlayerState();

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetPlayerState()
    {
        // Reset critical state variables
        isOnGround = true;
        isGameOver = false;

        // Reset Rigidbody velocity
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Reset animations
        if (anim != null)
        {
            anim.SetBool("Death_b", false);
            anim.SetInteger("DeathType_int", 0);
        }

        // Reset particle systems
        if (dirtParticle != null) dirtParticle.Play();
        if (explosion != null) explosion.Stop();
    }
}
