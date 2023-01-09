using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public AudioClip[] clip;
    public int AttackSpeed; //공격할때 돌진
    public int BlendNum;
    public int damage; //딜
    Rigidbody2D RB;
    public int speed;
    public int angryspeed;
    public int angry;
    public int nextMove;
    public int move;
    public bool Attack;
    Animator anim;
    public float FullHp;
    public float Hp;
    public Vector3 dirVec;
    public Transform trans;//시선위치
    public Transform Playertrans; //플레이어위치
    public Transform thistrans; //소환위치

    public int isChest;

    SpriteRenderer spriteRenderer;

    public Transform pos;
    public Vector2 boxSize;


    public float ishit; //밀려나는 방향
    public float ishit2; //밀려나는 시간
    
    public Image HealthImage;//체력바
    public Image HealthImage1;//체력바 겉면
    public int Dad; //죽었나
    public int Gold; //골드
    public GameObject GoldItem;

    public int Color1; //색 빨간색으로 변하는 시간
    public float AttackTime; //공격딜레이
    public float AttackEndTime; //다음공격딜레이

    public int h;
    public GameObject Skill; //만약 던지는거있으면 함
    public int EffectSpeed; //던지는 스피드

    public bool isDading; //죽는지
    public int DadTime;//부활하는데 드는 시간

    


    void Awake()
    {
        Playertrans = GameObject.Find("Player").transform;
    }
    void OnEnable()
    {
        if (AttackEndTime == 0)
            AttackEndTime = 1.5f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (isChest == 0 )
        {
            Invoke("Angry", 1f);
        }
        Dad = 0;
        Hp = FullHp;
        HealthImage.gameObject.SetActive(true);
        HealthImage1.gameObject.SetActive(true);
        gameObject.layer = 7;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        if (BlendNum > 0)
            anim.SetFloat("EnemyBlend", BlendNum);
    }

    void FixedUpdate()
    {

        Color1 -= 1;
        if (Color1 >= 0)
            spriteRenderer.color = new Color(1, 0.5f, 0.5f);
        else
        {
            spriteRenderer.color = new Color(1, 1, 1);
            gameObject.layer = 7;
        }

        if (isChest == 0 && Dad == 0)
        {
            RB.velocity = new Vector2(nextMove, RB.velocity.y);//움직임

            Vector2 frontVec = new Vector2(RB.position.x + nextMove, RB.position.y);//바닥체크
            Debug.DrawRay(frontVec, Vector3.down * 4, new Color(0, 1, 0)); //앞쪽 아래로 빔
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 4, LayerMask.GetMask("Ground")); //땅체크



            if (nextMove == 0)//방향전환, 애니메이션
            {
                anim.SetBool("isWalk", false);
            }
            else if (nextMove > 0)
            {
                h = 1;
                anim.SetBool("isWalk", true);
                transform.localScale = new Vector3(1, 1, 1);
                HealthImage.rectTransform.localScale = new Vector3(1, 1, 1);
                HealthImage1.rectTransform.localScale = new Vector3(1, 1, 1);
                move = nextMove;
                dirVec = Vector3.right;
            }
            else
            {
                h = -1;
                anim.SetBool("isWalk", true);
                transform.localScale = new Vector3(-1, 1, 1);
                HealthImage.rectTransform.localScale = new Vector3(-1, 1, 1);
                HealthImage1.rectTransform.localScale = new Vector3(-1, 1, 1);
                move = nextMove;
                dirVec = Vector3.left;
            }

            if (ishit != 0 && ishit2 > 0 && Hp > 0)//밀려남
            {
                RB.AddForce(new Vector2(ishit * 100, 0));
            }
            
            ishit2 -= 1;
            if(ishit2 <=0)
                spriteRenderer.color = new Color(1, 1, 1);


            Debug.DrawRay(trans.position, Vector2.down*2, new Color(0, 1, 0));    //아래로빔쏨

            RaycastHit2D GroundrayHit = Physics2D.Raycast(trans.position, Vector2.down, 2f, LayerMask.GetMask("Ground"));
            if ((GroundrayHit.collider != null) && (GroundrayHit.distance < 2f))
            {
                if ((rayHit.collider == null)) //땅있으면 반대로
                {
                    nextMove *= -1;
                    CancelInvoke(); //인보크 취소
                    Invoke("Angry", 1);
                }
            }
        }
        HealthImage.fillAmount = Hp / FullHp;
        if (Hp <= 0 && Dad == 0) //죽음
        {
            ishit = 0;
            ishit2 = 0;
            CancelInvoke();
            Dad = 1;
            anim.SetTrigger("isDad");
            HealthImage.gameObject.SetActive(false);
            HealthImage1.gameObject.SetActive(false);
            Color1 = 0;


            if (isChest != 0) //상자라면
            {
                GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
                if(gamemanager.Chest[isChest] == false)
                {
                    GoldItem.SetActive(true);
                    GoldItem.GetComponent<Gold>().Gold2(Gold);
                    gamemanager.Chest[isChest] = true;
                }
                if(FullHp>100)
                    SoundManager.instance.SFXPlay("EnemyAttack", clip[2]);
            }
            else if(Gold >0)
            {
                GoldItem.SetActive(true);
                GoldItem.GetComponent<Gold>().Gold2(Gold);
            }
            if(!isDading)
                Invoke("Die", 3f);
            else
                Invoke("Replay", DadTime);

        }
        if (Dad > 0)
        {
            Color1 = 0;
            ishit = 0;
            ishit2 = 0;
            gameObject.layer = 12;
            RB.velocity = new Vector2(0, RB.velocity.y);
        }

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if ((collider.gameObject.layer == 8 || collider.gameObject.layer == 9) && Attack == false && Hp>0)
            {
                if(AttackSpeed > 0) //돌진
                {
                    ishit = h*5* AttackSpeed;
                    ishit2 = 5;
                }
                CancelInvoke();
                anim.SetTrigger("isAttack");
                Invoke("Angry", AttackEndTime);
                nextMove = 0;
                Attack = true;
                if (EffectSpeed > 0) //던지는거
                {
                    SoundManager.instance.SFXPlay("EnemyAttack", clip[1]);
                    Invoke("IsUdoAttack", AttackTime);
                }
                else if (EffectSpeed < 0)
                {
                    SoundManager.instance.SFXPlay("EnemyAttack", clip[1]);
                    Invoke("IsFlyingAttack", AttackTime);
                }
                else
                    Invoke("IsAttack", AttackTime);
            }
        }
    }

    void OnDrawGizmos() //타격박스
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    public void Angry()
    {
        Attack = false;
        int dirc = transform.position.x - Playertrans.position.x > 0 ? -1 : 1; //플레이어 방향
        nextMove = dirc * angryspeed;
        Invoke("Angry", 1);
    }
    void IsUdoAttack()
    {
        Skill.transform.position = pos.position;
        Skill.SetActive(false);
        Skill.SetActive(true);
        Skill.GetComponent<EnemySkill>().Udo();
    }
    void IsFlyingAttack()
    {
        Skill.transform.position = pos.position;
        Skill.SetActive(false);
        Skill.SetActive(true);
        Skill.transform.localScale = new Vector3(Mathf.Abs(Skill.transform.localScale.x) * gameObject.transform.localScale.x, Skill.transform.localScale.y, 1);
    }

    void IsAttack() //공격
    {
        SoundManager.instance.SFXPlay("EnemyAttack", clip[1]);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.layer == 8)
            {
                Player player = collider.GetComponent<Player>();
                player.onDamaged(trans.position);
                player.PlayerHp -= damage * 0.01f * (100 - player.Defense);
            }
        }
        gameObject.layer = 7; //무적해제
        spriteRenderer.color = new Color(1, 1, 1);
    }

    public void onDamaged(float damage)//맞았을때
    {
        SoundManager.instance.SFXPlay("EnemyHit", clip[0]);
        angry = 1;
        Color1 = 10;
        HealthImage.gameObject.SetActive(true);
        HealthImage1.gameObject.SetActive(true);
        if (Hp > 0)
        {
            nextMove = 0;
            int dirc = trans.position.x - Playertrans.position.x > 0 ? 1 : -1;
            RB.AddForce(new Vector2(0, 1) * 3, ForceMode2D.Impulse);
            ishit = dirc;
            ishit2 = 10;
            Hp = Hp - damage;
            gameObject.layer = 12; //무적
            spriteRenderer.color = new Color(1, 0.5f, 0.5f);
            Invoke("Damaged", 0.1f);
        }
    }
    void Damaged() //무적해제
        {
            gameObject.layer = 7; //무적해제
        }
    void Die()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Replay()
    {
        CancelInvoke();
        nextMove = 0;
        anim.SetTrigger("isCome");
        Invoke("Angry", 3f);
        Dad = 0;
        Hp = FullHp;
        HealthImage.gameObject.SetActive(true);
        HealthImage1.gameObject.SetActive(true);
        gameObject.layer = 7;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        if (BlendNum > 0)
            anim.SetFloat("EnemyBlend", BlendNum);
    }

}

