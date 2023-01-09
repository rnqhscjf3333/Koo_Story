using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public bool isBackMove; //배경이동하는지

    public bool isStop; //이동후 멈추는지
    public bool isGone; //이동후 사라지는지
    public bool isFast; // z누르면 빨라지는지
    public float speed;
    public float Downspeed;
    public int EndPosition;
    public int EndPositionY;
    public int StartPosition;
    public int StartPositionY;
    public bool isGround; //플레이어한테 닿으면 이동


    void Awake()
    {
    }

    void Update()
    {
        if (isBackMove)
        {
            if (Input.GetButton("Fire1") && isFast)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed*2);
                transform.Translate(Vector3.down * Time.deltaTime * Downspeed*2);
            }
            transform.Translate(Vector3.left * Time.deltaTime*speed);
            transform.Translate(Vector3.down * Time.deltaTime * Downspeed);

            if (transform.position.x < EndPosition)
            {
                if (!isStop)
                    transform.position = new Vector2(StartPosition, transform.position.y);
                else 
                    isBackMove = false;
                if (isGone)
                    gameObject.SetActive(false);

            }
            if (Downspeed>0 && transform.position.y < EndPositionY)
            {
                if (!isStop)
                    transform.position = new Vector2(transform.position.x, StartPositionY);
                else
                    isBackMove = false;
                if (isGone)
                    gameObject.SetActive(false);

            }
            else if (Downspeed < 0 && transform.position.y > EndPositionY)
            {
                if (!isStop)
                    transform.position = new Vector2(transform.position.x, StartPositionY);
                else
                    isBackMove = false;
                if (isGone)
                    gameObject.SetActive(false);

            }
        }
        




    }
    void OnCollisionStay2D(Collision2D collision)

    {
        if ((collision.gameObject.layer == 8 || collision.gameObject.layer == 7) && isGround)
        {
            isBackMove = true;
            collision.transform.Translate(Vector3.left * Time.deltaTime * speed);
            collision.transform.Translate(Vector3.down * Time.deltaTime * Downspeed);
        }

    }
}
