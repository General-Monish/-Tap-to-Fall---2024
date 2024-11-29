using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Rigidbody rb;
    public float minSpeed=11f;
    public float maxSpeed=17f;
    public float xSpawnRange=4;
    public float ySpawnRange=-0.5f;
    public float maxTorque=10;
    Gmanager gmanager;
    public int pointValue;
    public ParticleSystem explosionParticle;
    public List<AudioClip> clickSounds;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        gmanager = GameObject.Find("Gmanager").GetComponent<Gmanager>();
        rb = GetComponent<Rigidbody>();
        audioSource =gmanager.GetComponent<AudioSource>();
        RandomForce();
        RandomTorque();
        RandomSpawnPos();
    }

    private void RandomSpawnPos()
    {
        transform.position = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawnRange);
    }

    private void RandomTorque()
    {
        rb.AddTorque(Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque), ForceMode.Impulse);
    }

    private void RandomForce()
    {
        rb.AddForce(Vector3.up * Random.Range(minSpeed, maxSpeed), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gmanager.isGameActive)
        {
            PlayRandomAudioClip();
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gmanager.UpdateScore(pointValue);
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!other.gameObject.CompareTag("OBS"))
        {
            gmanager.GameOver();
        }
    }

    public void PlayRandomAudioClip()
    {
        if (clickSounds.Count > 0)
        {
            // Choose a random audio clip from the list
            AudioClip randomClip = clickSounds[Random.Range(0, clickSounds.Count)];

            // Play the random audio clip
            audioSource.PlayOneShot(randomClip,1f); // Adjust volume as needed
        }
        else
        {
            Debug.LogWarning("No audio clips assigned to the list!");
        }
    }
}
