using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    Smanager smanager;
    Rigidbody rb;// Start is called before the first frame update
    void Start()
    {
        smanager = FindObjectOfType<Smanager>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        MoveTowradsPlayer();
        OnDestroy();
        //yes 
    }

    private void MoveTowradsPlayer()
    {
        Vector3 lookDir = (player.transform.position - transform.position).normalized;
        rb.AddForce(lookDir * speed);
        // boom 

    }

    private void OnDestroy()
    {
        if (transform.position.y < -10)
        {
            smanager.DisplayScore(1);
            Destroy(gameObject);
        }
    }
}
