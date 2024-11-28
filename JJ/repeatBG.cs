using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatBG : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidhth;
    // Start is called before the first frame update
    void Start()
    {

        startPos = transform.position;
        repeatWidhth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - repeatWidhth)
        {
            transform.position = startPos;
        }
    }
}
