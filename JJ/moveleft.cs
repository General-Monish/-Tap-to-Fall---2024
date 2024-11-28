using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class moveleft : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Player player;
    private float leftBound = -6f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isGameOver == false)
        {
            BackgroundMoving();
        }
        if (transform.position.x < leftBound && gameObject.CompareTag("OBS"))

        {
            Destroy(gameObject);

        }
    }

    private void BackgroundMoving()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
