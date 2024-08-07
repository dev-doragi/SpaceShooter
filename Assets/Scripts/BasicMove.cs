using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour
{
    public float moveSpeedX;
    public float moveSpeedY;
    public float rotateSpeed;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(moveSpeedX/2f, moveSpeedX), moveSpeedY);
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (Random.Range(rotateSpeed/5f, rotateSpeed) * Time.deltaTime));
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
