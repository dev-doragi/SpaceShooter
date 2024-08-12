using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Transform[] children;

    private Rigidbody2D rb;

    public float moveSpeed;

    public Vector2 moveInput;

    public Transform topLeft;
    public Transform bottomRight;

    private Animator anim;

    public GameObject bullet;
    public Transform bulletPoint;

    public float shotDelay; // ������ ��Ÿ��
    private float shotCounter;

    public GameObject shield;

    public bool doubleShot;
    public Transform doubleShot1;
    public Transform doubleShot2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        children = gameObject.GetComponentsInChildren<Transform>();
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
            if (doubleShot)
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx_1.PlayerLaser);
                Instantiate(bullet, doubleShot1.position, doubleShot1.rotation);
                Instantiate(bullet, doubleShot2.position, doubleShot2.rotation);
            }
            else
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx_1.PlayerLaser);
                Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
            }
    
            shotCounter = shotDelay;
        }

        if (Input.GetButton("Fire1"))
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                if(doubleShot)
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx_1.PlayerLaser);
                    Instantiate(bullet, doubleShot1.position, doubleShot1.rotation);
                    Instantiate(bullet, doubleShot2.position, doubleShot2.rotation);
                }
                else
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx_1.PlayerLaser);
                    Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
                }

                shotCounter = shotDelay;
            }
        }
    }

    public void OnDamaged()
    {
        //layer��ȭ

        for(int i=0; i < children.Length; i++)
        {
            children[i].gameObject.layer = 10;
        }

        //�ǰݽ� ����ȭ
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);// 4��° ���ڴ� ����
        Invoke("OffDamaged", 2);
    }

    public void OffDamaged()
    {
        for (int i = 0; i < children.Length; i++)
        {
            children[i].gameObject.layer = 7;
        }

        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
