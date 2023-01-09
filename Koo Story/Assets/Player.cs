using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public Rigidbody2D RB;
    public float Speed;
    public float maxSpeed;
    public float jumpPower;
    public float alive;
    public float h;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public Collider2D Collid;
    bool isGround;
    public BoxCollider2D boxCollider;

    public int[] SkinArray = new int[] { 0, 0, 0 }; // 무기/방어구/스킬


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (alive == 1)
        {
            float axis = Input.GetAxisRaw("Horizontal");
            RB.velocity = new Vector2(Speed * axis, RB.velocity.y);
            //stop speed
            if (Input.GetButtonUp("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.1f, rigid.velocity.y);
            }
            //점프
            if (Input.GetButtonDown("Jump") && anim.GetBool("isJump") == false)
            {
                RB.AddForce(Vector2.up * 700);
            }
        }
    }


    void FixedUpdate()
    {
        if (alive == 1)
        {
            //움직임
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                h = 1;
                anim.SetBool("isWalk", true);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                h = -1;
                anim.SetBool("isWalk", true);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else { anim.SetBool("isWalk", false); }


        }

        //땅
        Debug.DrawRay(rigid.position, Vector2.down, new Color(0, 1, 0));    //아래로빔쏨

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 1.3f, LayerMask.GetMask("Ground"));
        if ((rayHit.collider != null) && (rayHit.distance < 1.5f))
        {
            anim.SetBool("isJump", false);
        }
        else
        {
            anim.SetBool("isJump", true);
        }
    }

    
}
