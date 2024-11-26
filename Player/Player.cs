using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpForce = 100f;
    [SerializeField] float gravityModifier;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem dirtParticle;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] GameObject jumpBtn;
    [SerializeField] GameObject RestartBtn;

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
        jumpBtn.SetActive(true);
        RestartBtn.SetActive(false);

        Jump();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        if (isOnGround && !isGameOver)
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
            jumpBtn.SetActive(false);
            RestartBtn.SetActive(true);
        }
    }

    public void RestartBtns()
    {
        SceneManager.LoadScene("JJ");
    }
}
