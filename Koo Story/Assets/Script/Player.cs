using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//슬라이더 이용
using Cinemachine; //시네마신
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Transform[] StartPoint;
    public float PlayerHp;
    public float FullPlayerHp;
    public float Skill;
    public float FullSkill;
    public float Skill1Power;

    public GameObject Skill1;

    public ParticleSystem Dust;


    public AudioClip[] clip;
    public float Power; //공격력
    public int Defense; //방어력
    public GameObject Manager;
    public Rigidbody2D RB;
    public float Speed;
    public float jumpPower;
    public float alive;
    public float h;
    public float move;
    public float jump;
    public float ishit;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public GameObject Sword;
    public SpriteRenderer SwordSprite;
    public SpriteRenderer ArmSprite;
    public SpriteRenderer ArmorSprite;
    public Animator Swordanim;
    public Collider2D Collid;
    bool isGround;
    public GameObject NPC; //선택 NPC
    public int NPCA; //NPC랑 대화중인지

    public float curTime;
    public float coolTime = 0.5f;

    public Transform pos; //박스위치
    public Vector2 boxSize; //공격박스
    public Vector2 jumpboxSize;//점프공격박스

    public Image HealthImage;
    public Image SkillImange;

    public int PlayerGold; //골드, gamemanager로 보낼거임

    float DashSpeed = 6;
    int DashPower = 7;

    public bool isDumble;
    public int Dumblecul;


    public CinemachineConfiner confiner; //카메라
    public PolygonCollider2D confiner1;
    public PolygonCollider2D confiner2;
    public PolygonCollider2D confiner3;
    public BoxCollider2D confinerPoint1;
    public BoxCollider2D confinerPoint2;
    public BoxCollider2D confinerPoint3;

    public BoxCollider2D MapPoint;
    public BoxCollider2D MapPoint2;
    public BoxCollider2D MapPoint3;

    public int swordnum;
    public int armornum;
    public Sprite[] SwordImage;
    public Sprite[] ArmorImage;

    

    public GameObject Dungeon;





    void Awake()
    {
        FullSkill = 100;
        Skill = 100;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        transform.position = StartPoint[GameObject.Find("GameManager").GetComponent<GameManager>().StartPoint].position;


    }

    void Update()
    {
        if (Skill > 100)
            Skill = 100;
        if(alive == 0)//정지상태
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            anim.SetBool("isWalk", false);
        }
        if (alive == 1 && move ==1) //움직임 
        {
            float axis = Input.GetAxisRaw("Horizontal"); //좌우이동
            RB.velocity = new Vector2(Speed * axis, RB.velocity.y);
            //stop speed
            if (Input.GetButtonUp("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.1f, rigid.velocity.y);
            }
            //점프
            if (Input.GetButtonDown("Jump") && anim.GetBool("isJump") == false && jump ==1)
            {
                SoundManager.instance.SoundPlay(3);
                CreateDust();
                RB.AddForce(Vector2.up * jumpPower);
            }
        }
        if (Input.GetButtonDown("Fire1") && NPC != null && NPCA ==0)//공격
        {
            CancelInvoke();
            NPC npc = NPC.GetComponent<NPC>();
            npc.TalkNPC();
            NPCA = 1;
            move = 0;
            jump = 0;
            ishit = 0;
            RB.velocity = new Vector2(0, RB.velocity.y);
            anim.SetBool("isWalk", false);
            anim.SetBool("isDumble", false);
            offDamaged();
        }

        if (curTime <= 0 && alive == 1) //공격
        {
            if (Input.GetButtonDown("Fire1") && move != 0 && curTime <= 0)
            {
                SoundManager.instance.SFXPlay("PlayerAttack", clip[0]);
                RB.velocity = new Vector2(0, RB.velocity.y);
                anim.SetFloat("Blend", 0);
                anim.SetTrigger("isAttack");

                if(anim.GetBool("isJump") == true)
                {
                    Swordanim.SetTrigger("isJumpAttack");
                }
                else
                {
                    move = 0;
                    jump = 0;
                    Swordanim.SetTrigger("isAttack");
                    Invoke("Move", 0.5f);
                }
                curTime = coolTime;
                Invoke("Attack", 0.1f);
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }


        if (curTime <= 0 && alive == 1) //스킬1
        {
            if (Input.GetButtonDown("Fire2") && move != 0 && Skill >= 50 && GameObject.Find("GameManager").GetComponent<GameManager>().Skillhave[0] ==1)
            {
                curTime = coolTime;
                SoundManager.instance.SFXPlay("PlayerAttack", clip[1]);
                RB.velocity = new Vector2(0, RB.velocity.y);
                anim.SetFloat("Blend", 1);
                anim.SetTrigger("isAttack");

                move = 0;
                jump = 0;
                Swordanim.SetTrigger("isJumpAttack");
                Invoke("Move", 0.5f);

                Skill -= 50;
                Invoke("isSkill1", 0.2f);
            }
        }
        if (curTime <= 0 && alive == 1) //스킬2
        {
            if (Input.GetButtonDown("Fire3") && move != 0 && Skill >= 50 && GameObject.Find("GameManager").GetComponent<GameManager>().Skillhave[1] == 1)
            {
                curTime = coolTime;
                SoundManager.instance.PlayerSound(0);
                RB.velocity = new Vector2(0, RB.velocity.y);
                anim.SetFloat("Blend", 2);
                anim.SetTrigger("isAttack");

                move = 0;
                jump = 0;
                Swordanim.SetTrigger("isSkill1");
                Invoke("Move", 0.5f);

                Invoke("Skill1Attack", 0.2f);
                Skill1Power = Skill / 100* Power;
                Skill = 0;
                Invoke("SkillMove", 0.15f);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && move != 0 && anim.GetBool("isJump") == false && Dumblecul == 0 && alive ==1) //대쉬
        {
            anim.SetBool("isDumble", true);
            Dumblecul = 70;
            move = 0;
            ishit = h * DashSpeed;
            Invoke("Dumble", 0.3f);
            RB.AddForce(new Vector2(0, 1) * DashPower, ForceMode2D.Impulse);
        }

            Debug.DrawRay(transform.position, new Vector2(h, 0), new Color(0, 1, 0));    //아래로빔쏨
        RaycastHit2D NPCHit = Physics2D.Raycast(rigid.position, transform.localScale, 1, LayerMask.GetMask("NPC")); 
        if ((NPCHit.collider != null) && (NPCHit.distance < 1f))//NPC가 있다면
        {
            NPC = NPCHit.collider.gameObject;
        }
        else
            NPC = null;
        
        HealthImage.fillAmount = PlayerHp / FullPlayerHp; //체력바
        SkillImange.fillAmount = Skill / FullSkill; //스킬바

        if (PlayerGold > 0) //돈
        {
            Manager manager = Manager.GetComponent<Manager>();
            GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
            manager.PlayerGold += PlayerGold;
            gamemanager.Gold += PlayerGold;
            PlayerGold = 0;
        }
    }

    void FixedUpdate()
    {
        if (alive == 1 && move == 1) //움직임, 방향전환
        {
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

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 1.2f, LayerMask.GetMask("Ground"));
        if ((rayHit.collider != null) && (rayHit.distance < 1.2f))
        {
            anim.SetBool("isJump", false);
            Swordanim.SetBool("isJump", false);
            if (isDumble == true)
            {
                RB.velocity = new Vector2(0, RB.velocity.y);
                ishit = 0;
                move = 0;
                jump = 0;
                Invoke("StopDumble", 0.3f);
            }
            if (anim.GetBool("isWalk") == true)
                CreateDust();
        }
        else
        {
            anim.SetBool("isJump", true);
            Swordanim.SetBool("isJump", true);
        }

        if (ishit != 0)
        {
            RB.AddForce(new Vector2(ishit * 10, 0));
            if (ishit > 0)
                ishit -= 0.2f;
            else
                ishit += 0.2f;

        }

        if (PlayerHp <= 0.01f && alive == 1) //죽음
        {
            PlayerHp = 0;
            CancelInvoke();
            alive = 0;
            anim.SetTrigger("isDad");
            gameObject.layer = 9;
            spriteRenderer.color = new Color(1, 1, 1,1);
            SwordSprite.color = new Color(1, 1, 1, 1);
            ArmSprite.color = new Color(1, 1, 1, 1);
            ArmorSprite.color = new Color(1, 1, 1, 1);
            rigid.velocity = new Vector2(0, 0);
            ishit = 0;
            Manager.GetComponent<Manager>().PlayerDie();
        }
        if (Dumblecul > 0)
        {
            Dumblecul -= 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 && ishit == 0) //적한테 맞았을때
        {
            if(collision.GetComponent<Enemy>() != null)
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy.isChest == 0)
                {
                    anim.SetBool("isDumble", false);
                    onDamaged(collision.transform.position);
                    PlayerHp -= enemy.damage * 0.01f * (100 - Defense);
                }
            }
            if (collision.GetComponent<Boss1>() != null && !collision.GetComponent<Boss1>().isNotHit)
            {
                anim.SetBool("isDumble", false);
                onDamaged(collision.transform.position);
                PlayerHp -= collision.GetComponent<Boss1>().damage * 0.01f * (100 - Defense);
            }
        }

        if (collision.gameObject.layer == 11) //비밀 발견
        {
            SpriteRenderer secretrenderer = collision.GetComponent<SpriteRenderer>();
            secretrenderer.color = new Color(1, 1, 1, 0.4f);
        }
        if (collision.gameObject.tag == "CM")  //카메라
        {
            collision.GetComponent<CM>().CMR();
        }
        if (collision == MapPoint || collision == MapPoint2)  //맵
        {
            Time.timeScale = 1f;
            SoundManager.instance.SFXPlay("PlayerAttack", clip[2]);
            Manager.GetComponent<Manager>().Fade = 1;
            Invoke("MapGo", 0.5f);
        }
        if (collision == MapPoint3)  //맵
        {
            if(GameObject.Find("Manager").GetComponent<Manager>().MapName == "Tutorial")
            {
                Time.timeScale = 1f;
                SoundManager.instance.SFXPlay("PlayerAttack", clip[2]);
                Manager.GetComponent<Manager>().Fade = 1;
                Manager.GetComponent<Manager>().Naration2("다음 날");
                Invoke("MapGo1", 3f);
            }
            else if(GameObject.Find("Manager").GetComponent<Manager>().MapName == "Wilderness")
            {
                Manager.GetComponent<Manager>().Fade = 1;
                Invoke("MapGo2", 1f);
            }
            else if (GameObject.Find("Manager").GetComponent<Manager>().MapName == "Cave1")
            {
                Manager.GetComponent<Manager>().Fade = 1;
                Invoke("MapGo3", 1f);
            }
            else if (GameObject.Find("Manager").GetComponent<Manager>().MapName == "Catle2.5")
            {
                Manager.GetComponent<Manager>().Fade = 1;
                Invoke("MapGo4", 1f);
            }
            else if (GameObject.Find("Manager").GetComponent<Manager>().MapName == "Robrah_Village")
            {
                Manager.GetComponent<Manager>().Fade = 1;
                Invoke("MapGo5", 1f);
            }
            else if (GameObject.Find("Manager").GetComponent<Manager>().MapName == "SkyCatle")
            {
                Manager.GetComponent<Manager>().Fade = 1;
                Invoke("MapGo6", 1f);
            }
        }
    }
    void SkillMove()
    {
        //ishit = h * SkillSpeed;
    }

    void MapGo()
    {
        SceneManager.LoadScene("Map");
        if (GameObject.Find("QuestManager") != null)
            GameObject.Find("GameManager").GetComponent<GameManager>().Quest = GameObject.Find("QuestManager").GetComponent<QuestManager>().Quest;
    }

    void MapGo1()
    {
        SceneManager.LoadScene("Dne_Village");
        if (GameObject.Find("QuestManager").GetComponent<QuestManager>() != null)
            GameObject.Find("GameManager").GetComponent<GameManager>().Quest = GameObject.Find("QuestManager").GetComponent<QuestManager>().Quest;
    }

    void MapGo2()
    {
        SceneManager.LoadScene("Wilderness3");
    }
    void MapGo3()
    {
        SceneManager.LoadScene("Ancient");
    }
    void MapGo4()
    {
        SceneManager.LoadScene("CatleEnd1");
    }
    void MapGo5()
    {
        SceneManager.LoadScene("SkyCatle");
    }
    void MapGo6()
    {
        SceneManager.LoadScene("SkyCatle1");
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4 && alive == 1) //물
        {
            PlayerHp -= 2;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11) //비밀에서 나옴
        {
            SpriteRenderer secretrenderer = collision.GetComponent<SpriteRenderer>();
            secretrenderer.color = new Color(1, 1, 1, 1);
        }
    }

    public void onDamaged(Vector2 targetPos)//맞았을때
    {
        SoundManager.instance.PlayerSound(1);
        rigid.velocity = new Vector2(0, 0);
        move = 0;
        jump = 0;
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        SwordSprite.color = new Color(1, 1, 1, 0.4f);
        ArmSprite.color = new Color(1, 1, 1, 0);
        ArmorSprite.color = new Color(1, 1, 1, 0.4f);
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        RB.velocity = new Vector2(0, 0);
        RB.AddForce(new Vector2(0, 1) *10, ForceMode2D.Impulse);
        ishit = dirc*3;

        Invoke("offDamaged", 2);
        Invoke("offDamaged2", 0.5f);

        anim.SetTrigger("isHit");
        Swordanim.SetTrigger("isHit");
    }


    void offDamaged()//무적해제
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        SwordSprite.color = new Color(1, 1, 1, 1);
        ArmSprite.color = new Color(1, 1, 1, 1);
        ArmorSprite.color = new Color(1, 1, 1, 1);
    }
    void offDamaged2()//무적해제2
    {
        ishit = 0;
        move = 1;
        jump = 1;
    }

    void OnDrawGizmos() //타격박스
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
        Gizmos.DrawWireCube(pos.position, jumpboxSize);
    }

    void Move() //움직임
    {
        move = 1;
        jump = 1;
    }

    void Attack() // 공격
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.layer == 7)
            {
                if(collider.GetComponent<Enemy>() != null) 
                    collider.GetComponent<Enemy>().onDamaged(Power);
                else if(collider.GetComponent<Boss1>() != null)
                    collider.GetComponent<Boss1>().onDamaged(Power);
                Skill += 30;
            }
        }
    }
    void Skill1Attack() // 스킬1공격
    {
        ishit = 0;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.layer == 7)
            {
                if (collider.GetComponent<Enemy>() != null)
                    collider.GetComponent<Enemy>().onDamaged(Power+ Skill1Power);
                else if (collider.GetComponent<Boss1>() != null)
                    collider.GetComponent<Boss1>().onDamaged(Power+ Skill1Power);
            }
        }
    }

    void Dumble()
    {
        isDumble = true;
    }
    void StopDumble()
    {
        anim.SetBool("isDumble", false);
        isDumble = false;
        ishit = 0;
        move = 1;
        jump = 1;
    }

    public void SwordInven(int num)
    {
        swordnum = num;
        Swordanim.SetFloat("SwordBlend", num);
    }
    public void ArmorInven(int num)
    {
        armornum = num;
        ArmorSprite.sprite = GameManager.Instance.ArmorImage[num];
    }

    void CreateDust()
    {
        //Vector2 DustVec = new Vector2(RB.position.x, RB.position.y-0.8f);
        //Dust.transform.position = DustVec;
        //Dust.transform.localScale = new Vector3(h, 1, 1);
        Dust.Play();
    }

    void isSkill1()
    {
        Skill1.SetActive(false);
        Skill1.SetActive(true);
        

        Skill1.transform.position = new Vector2(pos.transform.position.x+transform.localScale.x, pos.position.y);
        Skill1.transform.localScale = new Vector3(transform.localScale.x, Skill1.transform.localScale.y, 1);
    }
}
