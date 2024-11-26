using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpForce = 100f;
    [SerializeField] float gravityModifier;
    Rigidbody rb;
    Animator anim;
    public bool isOnGround;
    public bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
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
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isOnGround = true;
        } else if (collision.collider.CompareTag("OBS"))
        {
            isGameOver = true;
            Debug.Log("GameOver");
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);
        }
    }
}
