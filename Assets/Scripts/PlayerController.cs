using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed;

    public Vector2 moveInput;

    public Transform topLeft;
    public Transform bottomRight;

    private Animator anim;

    public GameObject bullet;
    public Transform bulletPoint;

    public float shotDelay; // 레이저 쿨타임
    private float shotCounter;

    public GameObject shield;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.velocity = moveInput * moveSpeed;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, topLeft.position.x, bottomRight.position.x),
                                         Mathf.Clamp(transform.position.y, bottomRight.position.y, topLeft.position.y),
                                         transform.position.z);

        anim.SetFloat("Movement", moveInput.y);

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);

            shotCounter = shotDelay;
        }

        if (Input.GetButton("Fire1"))
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);

                shotCounter = shotDelay;
            }
        }
    }
}
