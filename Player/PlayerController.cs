using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float normalFallSpeed = 2f;
    [SerializeField] float fastFallSpeed = 8f;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0, -normalFallSpeed, 0);
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity=new Vector3(0,-fastFallSpeed, 0);
        }
    }
}
