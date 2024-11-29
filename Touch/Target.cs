using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Rigidbody rb;
    public float minSpeed=12f;
    public float maxSpeed=16f;
    public float xSpawnRange=4;
    public float ySpawnRange=-2;
    public float maxTorque=10;
    Gmanager gmanager;
    public int pointValue;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        gmanager = GameObject.Find("Gmanager").GetComponent<Gmanager>();
        rb = GetComponent<Rigidbody>();
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
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gmanager.UpdateScore(pointValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
