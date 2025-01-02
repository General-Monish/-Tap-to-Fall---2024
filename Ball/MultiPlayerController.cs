using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class MultiPlayerController : MonoBehaviour
{
    // Reference to the Joystick script
    private jj joystick;
    public float moveSpeed = 7f; // Speed for player movement
    private GameObject focalPoint;
    /*public GameObject powerupIndicator;*/
    public AudioClip enemycollideSound;
    public AudioClip powerupSound;
    bool hasPowerUp;
    float powerStrength = 20f;
    private Rigidbody rb;
    AudioSource playerAudio;

    public bool isGameOver = false;
    public GameObject boomPrefab;
    public GameObject hitPrefab;
    public GameObject powerGlowupPrefab;
    PhotonView photonView;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        joystick = GameObject.FindAnyObjectByType<jj>();
        photonView = GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
        if (transform.position.y < -10)
        {
            Debug.Log("plyr dead");
            isGameOver = true;
        }
        /*powerupIndicator.transform.position=transform.position+new Vector3 (0,-0.44f,0);*/
        powerGlowupPrefab.transform.position = transform.position + new Vector3(0, -.4f, 0);
        // Get input from the joystick

        if (photonView.IsMine)
        {
            float horizontal = joystick.inputDirection.x;
            float vertical = joystick.inputDirection.y;


            rb.AddForce(focalPoint.transform.right * moveSpeed * horizontal * Time.deltaTime, ForceMode.Impulse);
            rb.AddForce(focalPoint.transform.forward * moveSpeed * vertical * Time.deltaTime, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PUP"))
        {
            playerAudio.PlayOneShot(powerupSound);
            hasPowerUp = true;

            powerGlowupPrefab.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdown());
        }
    }

    IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        /*powerupIndicator.SetActive(false);*/
       powerGlowupPrefab.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("enemy") && hasPowerUp)
        {
            PhotonNetwork.Instantiate(boomPrefab.name, transform.position, boomPrefab.transform.rotation);
            PhotonNetwork.Instantiate(hitPrefab.name, transform.position, hitPrefab.transform.rotation);
            playerAudio.PlayOneShot(enemycollideSound);
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRB.AddForce(awayFromPlayer * powerStrength, ForceMode.Impulse);
        }
    }
}
