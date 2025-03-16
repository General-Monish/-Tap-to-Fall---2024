using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
      rb =  GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       float hor = Input.GetAxis("Horizontal");
       float ver = Input.GetAxis ("Vertical");

        rb.AddForce(transform.forward * ver * speed * Time.deltaTime, ForceMode.Impulse);
        rb.AddForce(transform.right * hor * speed * Time.deltaTime,ForceMode.Impulse);
    }
}
