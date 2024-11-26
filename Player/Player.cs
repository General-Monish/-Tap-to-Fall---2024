using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpForce = 100f;
    [SerializeField] float gravityModifier;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem dirtParticle;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip deathSound;

    AudioSource playerAudio;
    Rigidbody rb;
    Animator anim;
    public bool isOnGround;
    public bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Physics.gravity*=gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isGameOver)
        {
            anim.SetTrigger("Jump_trig");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        } else if (collision.collider.CompareTag("OBS"))
        {
            isGameOver = true;
            Debug.Log("GameOver");
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);
            explosion.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(deathSound, 1f);
        }
    }
}
