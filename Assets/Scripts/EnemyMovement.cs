using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeedX;
    public float moveSpeedY;
    public float rotateSpeed;

    private Rigidbody2D rb;

    public float XTarget; // Ư�� ��ġ�� �ٴٸ��� ��� �ൿ�϶�� ��ħ�ϱ� ���� ����Ʈ
    public float YTarget;
    public int moveType; // 0 - �Ʒ��� ������, 1 - ���� ������, 2 - Ư�� ��ġ�� ������

    public bool moveUp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + rotateSpeed * Time.deltaTime);

        switch (moveType)
        {
            case 0: // �Ʒ��� ������
                if (transform.position.x < XTarget)
                {
                    rb.velocity = new Vector2(-moveSpeedX, -moveSpeedY);
                }
                else
                {
                    rb.velocity = new Vector2(-moveSpeedX, 0f);
                }
                break;

            case 1: // ���� ������
                if (transform.position.x < XTarget)
                {
                    rb.velocity = new Vector2(-moveSpeedX, moveSpeedY);
                }
                else
                {
                    rb.velocity = new Vector2(-moveSpeedX, 0f);
                }
                break;
            case 2: // Ư�� ��ġ�� ������
                if (transform.position.x < XTarget)
                {
                    if (moveUp)
                    {
                        if (transform.position.y > YTarget)
                        {
                            rb.velocity = new Vector2(-moveSpeedX, 0f);
                        }
                        else
                        {
                            rb.velocity = new Vector2(-moveSpeedX, moveSpeedY);
                        }
                    }
                    else
                    {
                        if (transform.position.y < YTarget)
                        {
                            rb.velocity = new Vector2(-moveSpeedX, 0f);
                        }
                        else
                        {
                            rb.velocity = new Vector2(-moveSpeedX, -moveSpeedY);
                        }
                    }
                }
                else
                {
                    rb.velocity = new Vector2(-moveSpeedX, 0f);
                }
                break;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
