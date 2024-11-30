using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public Joystick joystick;    // Reference to the Joystick script
    public float moveSpeed = 7f; // Speed for player movement
    public GameObject focalPoint;
    public GameObject powerupIndicator;
    public AudioClip enemycollideSound;
    public AudioClip powerupSound;
    bool hasPowerUp;
    float powerStrength = 20f;
    private Rigidbody rb;
    AudioSource playerAudio;
    public GameObject restartBtn;
    public GameObject MMBtn;

    void Start()
    {
        MMBtn.SetActive(false);
        restartBtn.SetActive(false);
        playerAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (transform.position.y < -10)
        {
            MMBtn.SetActive(true);
            restartBtn.SetActive(true);
        }
        powerupIndicator.transform.position=transform.position+new Vector3 (0,-0.44f,0);
        // Get input from the joystick
        float horizontal = joystick.inputDirection.x;
        float vertical = joystick.inputDirection.y;

        rb.AddForce(focalPoint.transform.right*moveSpeed* -horizontal*Time.deltaTime,ForceMode.Impulse);
        rb.AddForce(focalPoint.transform.forward*moveSpeed* -vertical*Time.deltaTime,ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PUP"))
        {
            playerAudio.PlayOneShot(powerupSound);
            hasPowerUp = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdown());
        }
    }

    IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerupIndicator.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("enemy")&& hasPowerUp)
        {
            playerAudio.PlayOneShot(enemycollideSound);
            Rigidbody enemyRB=collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRB.AddForce(awayFromPlayer*powerStrength,ForceMode.Impulse);
        }
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene("Ball");
    }
    public void MainMenuBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
