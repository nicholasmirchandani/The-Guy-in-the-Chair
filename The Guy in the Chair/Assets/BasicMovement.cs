using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    float horizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
    float vertical = Input.GetAxisRaw("Vertical") * moveSpeed;
    rb.velocity = new Vector2(horizontal, vertical);
    
    }
}
