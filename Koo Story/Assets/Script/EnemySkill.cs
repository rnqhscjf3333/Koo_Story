using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    public float speed;
    public float speedgo;
    Rigidbody2D RB;
    public AudioClip[] clip;
    public GameObject circle;

    public int damage;

    public int time; //Á¦ÇÑ½Ã°£
    public int Time1;

    public float turnSpeed;
    public bool isGoToPlayer; //À¯µµµÇ´ÂÁö¾ÈµÇ¸é ±á³É xÃàÀ¸·Î ÀÌµ¿


    public bool ishit; //Å¸°ÝÃ¼Å©
    public bool isBomb; //½Ã°£ÀÖ´Ù Æø¹ßÇÏ´ÂÁö
    public bool ishitBomb; //ÇÃ·¹ÀÌ¾îÇÑÅ× ºÎ‹HÈ÷¸é Æø¹ßÇÏ´ÂÁö
    public bool isTargetRotation; //ÇÃ·¹ÀÌ¾î ¹æÇâÀ» ÇâÇÏ´ÂÁö
    public GameObject Trans;

    public float XForce;
    public float YForce;

    public bool isNotBerk; //º®¿¡¸Â´ÂÁö

    public Vector2 boxSize; //°ø°Ý¹Ú½º

    public bool isBeam; //°¢µµÀ¯µµÇÏ´ÂÁö
    public bool isHitE; //¸Â¤·¸é »ç¶óÁø¤¤Áö

    public bool isLine;
    public bool isRotationTarget; //¹æÇâ¿¡ ¸Â°Ô È¸Àü

    LineRenderer lr;
    public Vector3 cube1Pos, cube2Pos;
    public GameObject[] LineTarget;



    void OnEnable()
    {
        if(circle != null)
        {
            circle.GetComponent<TrailRenderer>().enabled = false;
            Invoke("SetCircle", circle.GetComponent<TrailRenderer>().time+0.5f);
        }
        RB = GetComponent<Rigidbody2D>();
        RB.velocity = new Vector2(0, 0);
        Time1 = time;
        RB.AddForce(new Vector2(XForce, YForce));
        if (isTargetRotation)
        {
            Vector2 len = GameObject.Find("Player").transform.position - transform.position; //¹æÇâ À¯µµ
            float z = Mathf.Atan2(len.x, len.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, -z);
        }
        if (isLine)
        {
            lr = GetComponent<LineRenderer>();
            lr.startWidth = 0.5f;
            lr.endWidth = 0.5f;
        }


    }
    public void SetForce()
    {
        RB.AddForce(new Vector2(XForce, YForce));
    }
    void SetCircle()
    {
        circle.GetComponent<TrailRenderer>().enabled = true;
    }

    public void Udo() //À¯µµ
    {
        if(clip != null && clip.Length>0)
            SoundManager.instance.SFXPlay("BossHit", clip[0]);
        RB.velocity = new Vector2(0, 0);
        Vector3 dirVec = GameObject.Find("Player").transform.position - transform.position;
        RB.AddForce(dirVec.normalized * speed, ForceMode2D.Impulse);
        Vector2 len = GameObject.Find("Player").transform.position - transform.position; //¹æÇâ À¯µµ
        float z = Mathf.Atan2(len.x, len.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, -z);
    }
    public void InvokeUdo(float time,float speed1,bool isRotation)
    {
        Invoke("SetSpeed", time);
        Invoke("Udo", time);
        speedgo = speed1;
        if (!isRotation)
            Invoke("SetNotTurn", time);

    }
    public void SetNotTurn()
    {
        turnSpeed = 0;
    }

    public void SetSpeed()
    {
        speed = speedgo;
    }
    public void Udo1(int num) //À¯µµ
    {
        Vector3 dirVec = LineTarget[num].transform.position - transform.position;
        RB.AddForce(dirVec.normalized * speed, ForceMode2D.Impulse);
        Vector2 len = transform.position - LineTarget[num].transform.position; //¹æÇâ À¯µµ
        float z = Mathf.Atan2(len.x, len.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }
    public void Udo2(int num) //À¯µµ
    {
        Vector2 len = GameObject.Find("Player").transform.position - transform.position; //¹æÇâ À¯µµ
        float z = Mathf.Atan2(len.x, len.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, -z+num);
        Quaternion v3Rotation = Quaternion.Euler(0f, -z + num, 0);  // È¸Àü°¢

        RB.AddForce(transform.up.normalized * speed, ForceMode2D.Impulse);
    }
    public void RandomGo() //·£´ý
    {
        Vector3 renVec = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10),0);
        RB.AddForce(renVec.normalized * speed, ForceMode2D.Impulse);
        Vector2 len = renVec; //¹æÇâ À¯µµ
        float z = Mathf.Atan2(len.x, len.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }
    public void RandomGo2() //¾Æ·¡·Î ·£´ý
    {
        Vector3 renVec = new Vector3(Random.Range(-10, 10), Random.Range(-10, 0), 0);
        RB.AddForce(renVec.normalized * speed, ForceMode2D.Impulse);
        Vector2 len = renVec; //¹æÇâ À¯µµ
        float z = Mathf.Atan2(len.x, len.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }
    public void RandomGo3() //À§·Î ·£´ý
    {
        Vector3 renVec = new Vector3(Random.Range(-10, 10), Random.Range(0, 10), 0);
        RB.AddForce(renVec.normalized * speed, ForceMode2D.Impulse);
        Vector2 len = renVec; //¹æÇâ À¯µµ
        float z = Mathf.Atan2(len.x, len.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }
    public void Go(int z) //°¢µµ
    {
        transform.rotation = Quaternion.Euler(0, 0, z);
        Quaternion v3Rotation = Quaternion.Euler(0f, z, 0);  // È¸Àü°¢

        RB.AddForce(transform.up.normalized*speed, ForceMode2D.Impulse);

        //Vector3 v3Direction = Vector3.up; // È¸Àü½ÃÅ³ º¤ÅÍ(Å×½ºÆ®¿ëÀ¸·Î world forward ½èÀ½)
        //Vector3 v3RotatedDirection = v3Rotation * v3Direction;
        //RB.AddForce(v3RotatedDirection.normalized * speed, ForceMode2D.Impulse);

    }



    void Update()
    {
        if (isRotationTarget)
        {
            Vector2 len = RB.velocity; //¹æÇâ À¯µµ
            float z = Mathf.Atan2(len.x, len.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, -z);
        }
        if (isBeam)
        {
            Vector2 len = GameObject.Find("Player").transform.position- transform.position ; //¹æÇâ À¯µµ
            float z = Mathf.Atan2(len.x, len.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, -z);
        }
        transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);//È¸Àü
        if(!isGoToPlayer)
            RB.velocity = new Vector2(speed*transform.localScale.x, RB.velocity.y);
        Time1 -= 1;

        if (Time1 <= 0)
        {
            if (isBomb)//ÆøÅºÀÏ°æ¿ì
            {
                GetComponent<Animator>().SetBool("isBomb",true);
                Invoke("SetFalse", 1f);
                RB.velocity = new Vector2(0, 0);

                Time1 = 110;
                iBomb();
            }
            else
                gameObject.SetActive(false);
        }

        if (isLine)
        {
            lr.SetPosition(0, gameObject.GetComponent<Transform>().position);
            lr.SetPosition(1, LineTarget[0].transform.position);
        }
    }
    void iBomb()
    {
        if (Time1 > 50)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Trans.transform.position, boxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.gameObject.layer == 8)
                {
                    Player player = collider.GetComponent<Player>();
                    player.onDamaged(Trans.transform.position);
                    player.PlayerHp -= damage * 0.01f * (100 - player.Defense);
                }
            }

        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && ishit == false && !ishitBomb) //ÇÃ·¹ÀÌ¾îÇÑÅ× ¸Â­ŸÀ»¶§
        {
            Player player = collision.GetComponent<Player>();
            player.onDamaged(transform.position);
            player.PlayerHp -= damage * 0.01f * (100 - player.Defense);
            gameObject.SetActive(false);
        }

        if(ishitBomb && ishit == false && (collision.gameObject.layer == 8 || ((collision.gameObject.layer == 6 || collision.gameObject.layer == 4) && !isNotBerk)))
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Trans.transform.position, boxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.gameObject.layer == 8)
                {
                    Player player = collider.GetComponent<Player>();
                    player.onDamaged(Trans.transform.position);
                    player.PlayerHp -= damage * 0.01f * (100 - player.Defense);
                }
            }
            ishit = true;
            Invoke("SetFalse", 1f);
            GetComponent<Animator>().SetTrigger("isBomb");
            RB.velocity = new Vector2(0,0);
        }
        if (collision.gameObject.layer == 8  && isHitE) //ÇÃ·¹ÀÌ¾îÇÑÅ× ¸Â­ŸÀ»¶§
        {
            Player player = collision.GetComponent<Player>();
            player.onDamaged(transform.position);
            player.PlayerHp -= damage * 0.01f * (100 - player.Defense);
        }
    }
    void SetFalse()
    {
        ishit = false;
        gameObject.transform.position = GameObject.Find("Player").transform.position;
        gameObject.SetActive(false);
    }

    void OnDrawGizmos() //Å¸°Ý¹Ú½º
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Trans.transform.position, boxSize); //Å¸°Ý¹Ú½º ÀÖÀ»¶§¸¸ ¾¸
    }

    public void RePlay() //´Ù½Ã ºÎÈ°
    {

    }
}
