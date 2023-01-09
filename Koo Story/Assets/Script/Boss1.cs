using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Boss1 : MonoBehaviour
{
    public AudioClip[] clip;

    public int damage; //딜
    public int Skilldamage; //딜
    Rigidbody2D RB;
    public int speed;
    public int angryspeed;
    public int nextMove;
    public int move;
    Animator anim;
    public float FullHp;
    public float Hp;
    public float CurrentHp;
    public Vector3 dirVec; //방향
    public Transform trans;//시선위치
    public Transform Playertrans; //플레이어위치

    public Vector2 boxSize;
    public Vector2 SkillboxSize;
    public float curTime;
    public float coolTime = 1f;

    public Image HealthImage;//체력바
    public Image HealthImage1;//체력바 겉면

    public int Dad; //죽었나
    public int Gold; //골드
    public GameObject GoldItem;

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer[] HitspriteRenderer;
    public Sprite[] BossSprite;

    public bool Attack; //공격중인지
    public int Color1; //색 빨간색으로 변하는 시간

    public Transform pos;
    public Transform pos2;

    public GameObject Dungeon;
    public GameObject Skill;
    public GameObject Effect;
    public GameObject Sword;

    public int skiilcul; //스킬쿨
    public int skiilcar; //현재스킬쿨

    public int skiilcul2; //스킬쿨
    public int skiilcar2; //현재스킬쿨

    public int Num; //보스넘버 

    public float ishit; //밀려나는 방향

    public bool SkillJump; //늑대 점프중인지
    public float JumpPower;
    public float JumpSpeed;

    public bool isSkill2; //스킬2썼는지

    public ParticleSystem[] particle;

    public GameObject[] Monster1;//소환몬스터
    public GameObject[] Monster2;//소환몬스터
    public GameObject[] Monster3;//소환몬스터

    public float AttackDelay; //어택델레이
    public float AttackEndDelay; //어택후 분노까지의 딜레이

    public int DieCount; //0되야죽음

    public GameObject NPC;

    public int SkillSpeed;
    public int SkillCount;//스킬쓰는횟수
    public int SkillCount2;//스킬쓰는횟수
    public int SkillCount3;//스킬쓰는횟수

    public Image ShipHealthImage;//체력바


    public float FullShipHP;
    public float ShipHP;

    public GameObject[] ETC; //기타 오브텍즈

    public CinemachineVirtualCamera CMCamera;
    public float ShakeTime;

    float shakepower;
    float shaketime;

    public bool isNotHit; //닿는거 공격
    public CinemachineVirtualCamera Follower;

    bool isflying;

    public float[] ETCFlat;

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(Num == 17)
        {
            HitspriteRenderer[0].sprite = BossSprite[0];
            HitspriteRenderer[1].sprite = BossSprite[1];
        }
    }

    void OnEnable()
    {
        if (Num == 5)//참치
        {
            SkillCount = 10;
            ChamChiCome();
            Invoke("ChamChiThink", 7);
            shakepower = 30;
            shaketime = 30;
            Invoke("ShakeCamera1", 4.6f);
        }
        if (Num == 6)
        {
            Invoke("monstercome", 2f);
        }
        if (Num == 7)//유령선
        {
            SkillCount = 10;
            Invoke("GhostThink", 10);
            shakepower = 30;
            shaketime = 30;
            Invoke("ShakeCamera1", 3f);
            Monster1[7].SetActive(false);
            particle[0].Play();
        }
        if (Num == 10) //드래곤
        {
            SkillCount = 3;
            Invoke("DragonThink", 10);
            shakepower = 10;
            shaketime = 10;
            Invoke("ShakeCamera1", 1.3f);
        }
        if (Num == 12) //고대병기
        {
            SkillCount = 3;
            Invoke("AncientThink", 5);
            shakepower = 10;
            shaketime = 10;
            Invoke("ShakeCamera1", 2f);
        }
        if (Num == 15) //시르케
        {
            Invoke("WizardThink", 5);
        }
        if (Num == 17) //블랜더
        {
            DieCount = GameManager.Instance.BlenderDieCount;
            if (DieCount == 5)
                BlenderDieSkill00();
            else if (DieCount == 4)
                BlenderDieSkill0();
            else if (DieCount == 3)
                BlenderDieSkill01();
            else if (DieCount == 2)
            {
                BlenderDieSkill02();
                anim.SetTrigger("Z1");
                AttackDelay = 0;
                speed = 0;
                Invoke("isBlenderAngry", 3f);
            }
        }
    }

    void FixedUpdate()
    {
        if(Num == 17 && DieCount <= 2)
        {
            GameObject.Find("Player").GetComponent<Player>().ArmorInven(10);
        }
        if (ShakeTime > 0)
        {
            ShakeTime -= 1;
            if (ShakeTime == 0)
                CMCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;

        }
        if (ishit != 0 && Hp > 0.1f)//밀려남
        {
            RB.AddForce(new Vector2(ishit * 100, 0));
        }

        skiilcar += 1;
        skiilcar2 += 1;
        Color1 -= 1;
        if (Color1 >= 0)
        {
            spriteRenderer.color = new Color(1, 0.5f, 0.5f);
            if (HitspriteRenderer != null)
                foreach (SpriteRenderer i in HitspriteRenderer)
                    i.color = new Color(1, 0.5f, 0.5f);
            if (Num == 10)//드래곤
                HealthImage.GetComponent<Image>().color = new Color(1, 0.5f, 0.5f);
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1);
            if(HitspriteRenderer != null)
                foreach (SpriteRenderer i in HitspriteRenderer)
                    i.color = new Color(1, 1, 1);
            if (Num == 10)//드래곤
                HealthImage.GetComponent<Image>().color = new Color(0, 0f, 0f);
        }
        if (Dad == 0)
        {
            RB.velocity = new Vector2(nextMove, RB.velocity.y);//움직임

            int a = nextMove > 0 ? 1 : -1;

            Vector2 frontVec = new Vector2(RB.position.x + a, RB.position.y);//바닥체크
            Debug.DrawRay(frontVec, Vector3.down * 4, new Color(0, 1, 0)); //앞쪽 아래로 빔
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 8, LayerMask.GetMask("Ground")); //땅체크



            if ((rayHit.collider != null) && (rayHit.distance < 4) && SkillJump == true)
            {
                anim.SetTrigger("isSkill1End");
                Effect.GetComponent<Animator>().SetTrigger("isEffect");
                RB.velocity = new Vector2(0, RB.velocity.y);
                ishit = 0;
                SkillJump = false;
                IsSkillAttack();
                Invoke("Angry", 1f);
                ShakeCamera(30, 10);
            }

            if (rayHit.collider == null && isflying && Num != 19)
            {
                nextMove *= -1;
            }
            if (rayHit.collider == null && isflying && Num == 19)
            {
                SkillCount2 *= -1;
                isflying = false;
            }

            if ((rayHit.collider != null) && (rayHit.distance < 8) && SkillJump == true && Num ==14)
            {
                anim.SetTrigger("isSkill1End");
                Effect.GetComponent<Animator>().SetTrigger("isEffect");
                RB.velocity = new Vector2(0, RB.velocity.y);
                ishit = 0;
                SkillJump = false;
                IsSkillAttack();
                Invoke("Angry", 1f);
                ShakeCamera(30, 10);
            }

            if (nextMove == 0)//방향전환, 애니메이션
            {
                anim.SetBool("isWalk", false);
            }
            else if (nextMove > 0)
            {
                anim.SetBool("isWalk", true);
                transform.localScale = new Vector3(1, 1, 1);
                move = nextMove;
                dirVec = Vector3.right;
            }
            else
            {
                anim.SetBool("isWalk", true);
                transform.localScale = new Vector3(-1, 1, 1);
                move = nextMove;
                dirVec = Vector3.left;
            }
        }

        if (skiilcar >= skiilcul && Attack == false && Hp > 0.1f) //스킬공격
        {
            if (Num == 0) //꽃
            {
                SoundManager.instance.SFXPlay("BossHit", clip[2]);
                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;
                anim.SetTrigger("isSkill1");
                Skill.GetComponent<Animator>().SetTrigger("isSkill");
                Invoke("IsSkill", 0.5f);
                Invoke("Angry", 1.5f);
            }
            if (Num == 1) //차우
            {
                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;
                Invoke("isChaUSkill", 0.5f);
                Invoke("Angry", 2f);
                Invoke("isChaUSkil4", 2f);
            }
            if (Num == 2) //늑대
            {

                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;
                anim.SetTrigger("isSkill1");
                Invoke("isWolfSkill1", 2f);
            }
            if (Num == 3) //늑대아이
            {
                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;
                anim.SetTrigger("isSkill1");
                Skill.SetActive(false);
                Skill.SetActive(true);
                Invoke("isWolfBoySkill1", 0.5f);
                Invoke("Angry", 3f);

                Skill.transform.position = pos2.position;
                if (DieCount == 0)
                {
                    Vector2 SkillVec = new Vector2(transform.position.x + transform.localScale.x * 2, pos2.position.y);
                    Skill.transform.position = SkillVec;
                }
                Skill.transform.localScale = new Vector3(transform.localScale.x, Skill.transform.localScale.y, 1);
                Skill.GetComponent<EnemySkill>().damage = Skilldamage;
                Skill.GetComponent<Animator>().SetTrigger("isCome");
                Invoke("IsAttack", AttackDelay);
                Skill.GetComponent<EnemySkill>().speed = 0;
            }
            if (Num == 4) //검은수염
            {
                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;
                anim.SetTrigger("isSkill1");
                Invoke("IsBlackSkill", 0);
                Invoke("Angry", 3f);
                SkillCount = 3;
            }
            if (Num == 6) //보스아머
            {
                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;
                Invoke("IsArmorSkill", 0);
                Invoke("Angry", 5f);
                SkillCount = 3;
            }
            if (Num == 8) //골렘
            {
                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;
                Invoke("isChaUSkill", 1f);
                Invoke("Angry", 2f);
                Invoke("isChaUSkil4", 2f);
                anim.SetBool("isSkill1", true);
            }
            if (Num == 9) //슬라임
            {
                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;
                Invoke("IsSlimeSkill", 0.5f);
                Invoke("Angry", 5f);
                SkillCount = 5;
                anim.SetTrigger("isSkill1");
            }
            if (Num == 11) //광석
            {
                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;
                Invoke("isMineralSkill", 0.5f);
                Invoke("Angry", 3f);
                anim.SetTrigger("isSkill");
                Monster1[1].GetComponent<EnemySkill>().isBeam = true;
            }
            if (Num == 13) //솔져
            {
                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;
                Invoke("isSoldierSkill", 2f);
                Invoke("Angry", 10f);
                anim.SetTrigger("isSkill");
                SoundManager.instance.SFXPlay("BossHit", clip[2]);
            }
            if (Num == 14) //과물
            {
                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;

                for (int i = 0; i < 6; i++)
                {
                    Monster1[i].SetActive(false);
                    Monster1[i].transform.position = pos2.position;
                }

                if (SkillCount2 < 3)
                {
                    SkillCount2 += 1;
                    int i = Random.Range(0, 2);
                    SkillCount = 0;
                    if (i == 0)
                    {
                        Invoke("isBeastSkill", 0.5f);
                        anim.SetTrigger("isSkill1");
                        Invoke("Angry", 2f);
                    }
                    else if (i == 1)
                    {
                        Invoke("isBeastSkil2", 2f);
                        anim.SetTrigger("isSkill2");
                        Invoke("Angry", 3f);
                    }
                }
                else
                {
                    anim.SetTrigger("isSkill3");
                    Invoke("isWolfSkill1", 1f);
                    SkillCount2 = 0;
                }

            }
            if (Num == 16) //스태락1
            {

                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;

                for (int i1 = 0; i1 < 3; i1++)
                {
                    Monster1[i1].SetActive(false);
                    Monster1[i1].transform.position = ETC[0].transform.position;
                }

                int i = Random.Range(0, 3);
                SkillCount = 0;
                if (i == 0)
                {
                    Invoke("isStarakSkill", 1f);
                    anim.SetTrigger("isSkill1");
                    Invoke("Angry", 4f);
                }
                else if (i == 1)
                {
                    Invoke("IsSkillAttack", 2f);
                    anim.SetTrigger("isSkill2");
                    if (DieCount == 0)
                        Invoke("isStarakSound", 3f);
                    Invoke("Angry", 4f);
                }
                else if (i == 2)
                {
                    Invoke("isStarakSkill3", 1f);
                    anim.SetTrigger("isSkill3");
                    Invoke("Angry", 3f);
                    if (DieCount == 0)
                        Invoke("isStarakSkill3A", 2f);
                }
            }

            if (Num == 17 &&  DieCount > 2) //블랜더
            {

                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;

                for (int i1 = 0; i1 < 3; i1++)
                {
                    Monster1[i1].SetActive(false);
                    Monster1[i1].transform.position = ETC[0].transform.position;
                }

                int i = Random.Range(0, 3);
                SkillCount = 0;
                if (i == 0)
                {
                    Invoke("isBlenderSkill1", 0.67f);
                    anim.SetTrigger("isSkill1");
                    Invoke("Angry", 2f);
                }
                else if (i == 1)
                {
                    isBlenderSkill4();
                    Invoke("isBlenderSkill4A", 1f);
                    anim.SetTrigger("isSkill4");
                    Invoke("Angry", 2.5f);
                }
                else if (i == 2)
                {
                    if (DieCount < 4)
                        angryspeed = 4;
                    Invoke("isChaUSkill", 1f);
                    Invoke("Angry", 2f);
                    //Invoke("isChaUSkil4", 2.5f);
                    anim.SetTrigger("isSkill5");
                    Invoke("Angry", 3f);
                }


            }
            if (Num == 18) //드락사르
            {
                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;

                if(SkillCount < 3)
                {
                    SkillCount += 1;
                    int i = Random.Range(0, 2);
                    if (i == 0)
                    {
                        SkillCount3 = transform.position.x - Playertrans.position.x > 0 ? -1 : 1;
                        Invoke("isDRAKSkill", 1.5f);
                        anim.SetTrigger("S");
                        Invoke("Angry", 2.5f);
                    }
                    else if (i == 1)
                    {
                        Invoke("isDRAKSkill1", 1.5f);
                        anim.SetTrigger("S1");
                        Invoke("Angry", 1.6f);
                    }
                }
                else
                {
                    SkillCount = 0;
                    SkillCount2 = 0;
                    Invoke("isDRAKSkill2", 1.83f);
                    Invoke("isDRAKSkill2", 2.16f);
                    Invoke("isDRAKSkill2", 3);
                    Invoke("Angry", 4f);
                    //Invoke("isChaUSkil4", 2.5f);
                    anim.SetTrigger("S2");
                }

            }
            if (Num == 19) //이시도르
            {
                Attack = true;
                nextMove = 0;
                CancelInvoke();
                skiilcar = 0;

                int i = Random.Range(0, 2);
                //i = 1;
                if (i == 0 && DieCount ==1)
                {
                    Invoke("isISkill", 1f);
                    Invoke("IsSkillAttack", 2.5f);
                    Invoke("IsAttack", 2.5f);
                    anim.SetTrigger("isSkill2");
                    Invoke("Angry", 5f);
                }
                else if (i == 1 && DieCount == 1)
                {
                    Invoke("isISkil2", 1.5f);
                    anim.SetTrigger("isSkill1");
                    Invoke("Angry", 3f);
                }
                if (i == 0 && DieCount == 0)
                {
                    Invoke("isISkill", 0.5f);
                    Invoke("IsSkillAttack", 2f);
                    Invoke("IsAttack", 2f);
                    anim.SetTrigger("isSkill2A");
                    Invoke("Angry", 3.5f);
                }
                else if (i == 1 && DieCount == 0)
                {
                    Invoke("isISkil2", 1f);
                    anim.SetTrigger("isSkill1A");
                    Invoke("isISkil2B", 0.67f);
                    Invoke("Angry", 2f);
                }

            }
        }

        if (Hp <= FullHp / 2 && !isSkill2 && Num == 2 && !Attack && Hp > 0.1f) //늑대스킬2
        {
            CancelInvoke();
            gameObject.layer = 12;
            isSkill2 = true;
            anim.SetTrigger("isSkill2");
            Attack = true;
            nextMove = 0;
            Invoke("isWolfSkill2", 2f);
            Invoke("Angry", 3f);
        }

        if (Hp <= FullHp / 2 && Num == 4 && !Attack && skiilcar2 >= skiilcul2 && Hp > 0.1f) //블랙스킬2
        {
            skiilcar2 = 0;
            CancelInvoke();
            gameObject.layer = 12;
            anim.SetTrigger("isSkill3");
            isSkill2 = true;
            Attack = true;
            nextMove = 0;
            Invoke("IsBlackSkill2", 0.5f);
            Invoke("Angry", 1f);
            particle[1].Play();
        }

        if (Hp <= FullHp / 2 && Num == 6 && !Attack && skiilcar2 >= skiilcul2 && Hp > 0.1f) //아머스킬2
        {
            skiilcar2 = 0;
            CancelInvoke();
            anim.SetTrigger("isSkill2");
            isSkill2 = true;
            Attack = true;
            nextMove = 0;
            IsArmorSkill2();
            Invoke("Angry", 3f);
        }

        if (Num == 17 && !Attack && skiilcar2 >= skiilcul2 && Hp > 0.1f && DieCount < 5 && DieCount > 2) //블랜더스킬2
        {
            Attack = true;
            nextMove = 0;
            CancelInvoke();
            skiilcar2 = 0;

            int i = Random.Range(0, 3);
            //SkillCount2 = 0;
            if (i == 0)
            {
                Invoke("isBlenderSkill2", 1f);
                anim.SetTrigger("isSkill2");
                Invoke("Angry", 1f);
            }
            else if (i == 1)
            {
                Invoke("isBlenderSkill3", 0.67f);
                anim.SetTrigger("isSkill3");
                Invoke("Angry", 1.5f);
            }
            else if (i == 2)
            {
                Invoke("isBlenderSkill6", 0.67f);
                Invoke("Angry", 1f);
                anim.SetTrigger("isSkill6");
            }
        }



        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if ((collider.gameObject.layer == 8 || collider.gameObject.layer == 9) && Attack == false && AttackDelay > 0 && Dad == 0 && Hp > 1f) //공격
            {

                CancelInvoke();
                //anim.SetTrigger("isAttack");
                if(Num == 19 && DieCount ==0)
                    anim.SetTrigger("isAttackA");
                else
                    anim.SetTrigger("isAttack");
                Invoke("Angry", AttackEndDelay);
                Invoke("IsAttack", AttackDelay);
                if (Num == 18)
                    Invoke("IsAttack", 1.5f);
                nextMove = 0;
                Attack = true;
                if (Sword != null)
                    Sword.GetComponent<Animator>().SetTrigger("isAttack");
                if (Num == 3 && DieCount == 0)
                {
                    int dirc = transform.position.x - Playertrans.position.x > 0 ? -1 : 1;
                    ishit = dirc * 7;
                    Invoke("CancleIshit", 0.5f);
                }

                if (Num == 8)
                {
                    SkillCount = 4;
                }

            }
        }



        if (HealthImage.fillAmount - Hp / FullHp < -0.01f)
            HealthImage.fillAmount += 0.01f;
        else
            HealthImage.fillAmount = Hp / FullHp;

        if (FullShipHP > 0)
            ShipHealthImage.fillAmount = ShipHP / FullShipHP;



        if (Hp <= 0 && Dad == 0 && DieCount == 0) //죽음
        {
            CancelInvoke();
            Dad = 1;
            anim.SetTrigger("isDad");
            HealthImage.gameObject.SetActive(false);
            HealthImage1.gameObject.SetActive(false);
            GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
            GoldItem.SetActive(true);
            GoldItem.GetComponent<Gold>().Gold2(Gold);
            Color1 = 0;
            if(Num != 19)
                Dungeon.GetComponent<DungeonManager>().BossDie();
            if (gamemanager.Quest[0] == 3 && Num == 0) //퀘스트
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                gamemanager.Quest[0] = 4;
            }
            if (gamemanager.Quest[2] == 1 && Num == 1) //퀘스트
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                gamemanager.Quest[2] = 2;
            }
            if (gamemanager.Quest[0] == 5 && Num == 2) //늑대퀘스트
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                gamemanager.Quest[0] = 6;
            }
            if (gamemanager.Quest[3] == 1 && Num == 3)//늑대아이
            {
                NPC.SetActive(true);
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
            }
            if (Num == 3)//늑대아이
            {
                Effect.SetActive(false);
                spriteRenderer.sprite = BossSprite[0];
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
            }
            if (gamemanager.Quest[0] == 8 && Num == 4) //해적
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                gamemanager.Quest[0] = 9;
            }
            if (gamemanager.Quest[0] == 10 && Num == 5) //참치
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                NPC.GetComponent<NPC>().TalkingNPC("해치웠다!! 드엔마을 사람들이 할 말이 있다는데 일단 마을로 돌아가자.", 5);
                gamemanager.Quest[0] = 11;
            }
            if (Num == 5)
                Invoke("SeaMap", 5);
            if (Num == -1)//마야
            {
                NPC.SetActive(true);
                gamemanager.Quest[0] = 0;
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
            }
            if (Num == 6 && gamemanager.Armorhave[8] == 0)
            {
                gamemanager.Armorhave[8] = 1;
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("새로운 방어구를 획득했다.[힘을 잃은 마왕의 갑옷]");
                GameObject.Find("Manager").GetComponent<Manager>().Naration3("컬렉션에 새로운 보스의 정보가 기록되었다.");
                SoundManager.instance.Click2();
                GameObject.Find("GameManager").GetComponent<GameManager>().Armorhave[8] = 1;
                GameObject.Find("GameManager").GetComponent<GameManager>().Armornum = 8;
                GameObject.Find("Player").GetComponent<Player>().armornum = 8;
                GameObject.Find("Player").GetComponent<Player>().ArmorInven(8);
                GameObject.Find("Player").GetComponent<Player>().Defense = GameObject.Find("Manager").GetComponent<Manager>().ArmorDefense[8];
            }
            if (Num == 6)
            {
                Monster1[3].SetActive(false);
                Monster1[4].SetActive(false);
            }

            if (gamemanager.Quest[4] == 1 && Num == 7) //참치
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                NPC.GetComponent<NPC>().TalkingNPC("... 마을로 돌아가자.", 30);
                gamemanager.Quest[4] = 2;
            }
            if (Num == 7)
                Invoke("SeaMap", 18);

            if (gamemanager.Quest[0] == 23 && Num == 8) //골렘
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                gamemanager.Quest[0] = 24;
            }
            if (gamemanager.Quest[0] == 25 && Num == 9) //슬라임
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                gamemanager.Quest[0] = 26;
            }

            if (gamemanager.Quest[0] == 34 && Num == 10) //드래곤
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                gamemanager.Quest[0] = 35;
            }
            if (Num == 10)
            {
                Playertrans.position = ETC[1].transform.position;
                GameObject.Find("Player").GetComponent<Player>().alive = 0;
                Invoke("DragonMap", 15);
            }
            if (gamemanager.Quest[6] == 1 && Num == 11) //광산
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                gamemanager.Quest[6] = 2;
            }
            if (gamemanager.Key[2] == 0 && Num == 12) //고대병기
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("새로운 아이템을 획득했다.[파란 보석]");
                GameObject.Find("Manager").GetComponent<Manager>().Naration3("컬렉션에 새로운 보스의 정보가 기록되었다.");
                gamemanager.Key[2] = 1;
            }
            if (Num == 12)
                Invoke("DragonMap", 5);

            if (gamemanager.Quest[0] == 45 && Num == 13) //솔져
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                gamemanager.Quest[0] = 46;
            }

            if (gamemanager.Quest[0] == 47 && Num == 14) //괴물
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                gamemanager.Quest[0] = 48;
            }

            if (gamemanager.Quest[0] == 49 && Num == 15) //시르케
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                gamemanager.Quest[0] = 50;
            }

            if (Num == 16) //스태락
            {
                GameObject.Find("Player").GetComponent<Player>().alive = 0;
                spriteRenderer.sprite = BossSprite[1];
                if (gamemanager.Quest[0] == 53)
                {
                    GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                    GameObject.Find("Manager").GetComponent<Manager>().Naration3("새로운 무기를 획득했다.[힘을 잃은 마왕의 검]");
                    GameObject.Find("GameManager").GetComponent<GameManager>().Swordhave[8] = 1;
                    GameObject.Find("GameManager").GetComponent<GameManager>().Swordnum = 8;
                    GameObject.Find("Player").GetComponent<Player>().swordnum = 8;
                    GameObject.Find("Player").GetComponent<Player>().SwordInven(8);
                    Invoke("StarakEnd", 6);
                }
                else
                    Invoke("StarakEnd", 2);
            }
            if (gamemanager.Quest[9] == 1 && Num == 19) //이시도르
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration2("컬렉션에 새로운 보스의 정보가 기록되었다.");
                GameObject.Find("Manager").GetComponent<Manager>().Naration3("새로운 방어구를 획득했다.[광전사의 갑주]");
                GameObject.Find("GameManager").GetComponent<GameManager>().Armorhave[7] = 1;
                GameObject.Find("GameManager").GetComponent<GameManager>().Armornum = 7;
                GameObject.Find("Player").GetComponent<Player>().armornum = 7;
                GameObject.Find("Player").GetComponent<Player>().ArmorInven(7);
                gamemanager.Quest[9] = 2;
            }
            if (Num == 19) //이시도르
            {
                RB.transform.localScale = new Vector2(-1, 1);
                spriteRenderer.sprite = BossSprite[0];
                ETC[1].SetActive(false);
                GameObject.Find("Manager").GetComponent<Manager>().audiosource[2].Stop();
                GameObject.Find("Manager").GetComponent<Manager>().audiosource[0].Play();
            }

            foreach (ParticleSystem i in particle)
                i.Stop();
        }
        if (Hp <= 0 && Dad == 0 && DieCount > 0)
        {
            Attack = true;
            if (Num == 3)
            {
                RB.velocity = new Vector2(0, RB.velocity.y);
                CancelInvoke();
                anim.SetTrigger("isDie1");
                Hp = 0.1f;
                DieCount -= 1;
                Effect.GetComponent<Animator>().SetBool("isLighting", true);
                Invoke("BoyAngry", 2);
                Skill.SetActive(false);
            }
            else if (Num == 16)
            {
                RB.transform.localScale = new Vector2(-1, 1);
                nextMove = 0;
                RB.velocity = new Vector2(0, RB.velocity.y);
                CancelInvoke();
                anim.SetTrigger("isDad1");
                Hp = 0.1f;
                DieCount -= 1;
                Invoke("StarakAngry1", 2);
                Invoke("StarakAngry2", 7f);
                Invoke("StarakAngry", 12f);
                Dungeon.GetComponent<DungeonManager>().audiosource[1].Stop();
            }
            else if (Num == 17)
            {
                nextMove = 0;
                RB.velocity = new Vector2(0, RB.velocity.y);
                Hp = 0.1f;
                CancelInvoke();

                if (DieCount == 5)
                {
                    anim.SetTrigger("isDad1");
                    Invoke("BlenderDieSkill1", 7);
                    Invoke("BlenderDieSkill0", 4);
                    Dungeon.GetComponent<DungeonManager>().audiosource[1].Stop();
                    DieCount -= 1;
                }
                else if (DieCount == 4)
                {
                    anim.SetTrigger("isDad1");
                    Invoke("BlenderDieSkill1", 7);
                    Invoke("BlenderDieSkill01", 4);
                    Dungeon.GetComponent<DungeonManager>().audiosource[1].Stop();
                    DieCount -= 1;
                }
                else if (DieCount == 3)
                {
                    anim.SetTrigger("isDad2");
                    Invoke("BlenderDie2", 2);
                    Dungeon.GetComponent<DungeonManager>().audiosource[1].Stop();
                }
                else if (DieCount == 2)
                {
                    anim.SetTrigger("isDad3");
                    Invoke("BlenderDie2", 2);
                    Dungeon.GetComponent<DungeonManager>().audiosource[1].Stop();
                }
            }
            else if (Num == 18) // 드락사르
            {
                RB.transform.localScale = new Vector2(-1, 1);
                nextMove = 0;
                RB.velocity = new Vector2(0, RB.velocity.y);
                CancelInvoke();
                anim.SetTrigger("D");
                Hp = 0.1f;
                Dungeon.GetComponent<DungeonManager>().audiosource[1].Stop();
                GameManager.Instance.Quest[8] = 1;
                SceneManager.LoadScene("SkyCatle1");
            }
            else if (Num == 19) // 이시도르
            {
                //RB.transform.localScale = new Vector2(-1, 1);
                nextMove = 0;
                RB.velocity = new Vector2(0, RB.velocity.y);
                CancelInvoke();
                anim.SetTrigger("isDad1");
                Hp = 0.1f;
                GameObject.Find("Manager").GetComponent<Manager>().audiosource[1].Stop();
                Invoke("IDie1", 2);
                Invoke("IAngry1", 3);
                DieCount = 0;
            }
        }
        if (Dad > 0)
        {
            gameObject.layer = 12;
            Color1 = 0;
            RB.velocity = new Vector2(0, RB.velocity.y);
        }

        if (ShipHP < 0 && Dad == 0)
        {
            Dad = 1;
            foreach (GameObject i in ETC)
                i.SetActive(false);
            Monster1[5].SetActive(true);
            NPC.SetActive(false);
            anim.SetTrigger("isVictory");
            ShipHP = 0;
        }
    }

    void OnDrawGizmos() //타격박스
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
        Gizmos.DrawWireCube(pos2.position, SkillboxSize);
        if (Skill != null)
            Gizmos.DrawWireCube(Skill.transform.position, SkillboxSize);
    }

    void SeaMap()
    {
        Monster1[6].SetActive(true);
    }

    void DragonMap()
    {
        ETC[2].SetActive(true);
    }


    public void Angry()
    {
        isflying = false;
        int dirc = transform.position.x - Playertrans.position.x > 0 ? -1 : 1; //플레이어 방향
        nextMove = dirc * speed;
        Invoke("Angry", 1);
        Attack = false;
        if (Num == 1 || Num == 8) //차우
        {
            anim.SetBool("isSkill1", false);
            foreach (ParticleSystem i in particle)
                i.Stop();
        }
        gameObject.layer = 7;
    }

    void IsAttack() //공격
    {
        SoundManager.instance.SFXPlay("BossHit", clip[1]);
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
        if (Num == 8)
            particle[1].Play();

        if (Num == 8 && SkillCount > 0)//골렘
        {
            Invoke("IsAttack", 0.3f);
            SkillCount -= 1;
        }
        if (Num == 11)
        {
            Monster1[0].SetActive(true);
            Monster1[0].transform.position = Playertrans.position - Vector3.down * 1f;
            Monster1[0].GetComponent<Animator>().SetTrigger("isCome");
        }
        if(Num ==16 && (DieCount == 0))
        {
            SoundManager.instance.SFXPlay("BossHit", clip[2]);
            Monster1[3].SetActive(false);
            Monster1[3].SetActive(true);
            Monster1[3].transform.position = ETC[3].transform.position;
            Monster1[3].GetComponent<EnemySkill>().Udo();
        }
    }

    void IsSkill() //스킬
    {
        SoundManager.instance.SFXPlay("BossSkillAttack", clip[3]);
        Vector3 swap = Skill.transform.position; //위치조정
        swap.x = Playertrans.position.x;
        Skill.transform.position = swap;

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Skill.transform.position, SkillboxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.layer == 8)
            {
                Player player = collider.GetComponent<Player>();
                player.onDamaged(trans.position);
                player.PlayerHp -= Skilldamage * 0.01f * (100 - player.Defense);
            }
        }
    }

    void isChaUSkill()
    {
        if (Hp > 0.1f)
        {
            int dirc = transform.position.x - Playertrans.position.x > 0 ? -1 : 1; //플레이어 방향
            nextMove = dirc * angryspeed * 8;
            particle[0].Play();
            SkillCount = 1;
            isflying = true;
            if (Num != 17)
            {
                isChaUSkill2();
                anim.SetBool("isSkill1", true);
            }
        }
    }
    void isChaUSkill2()
    {
        if (SkillCount == 1)
        {
            SoundManager.instance.SFXPlay("BossHit", clip[2]);
            Invoke("isChaUSkill3", 0.1f);
        }
    }

    void isChaUSkill3()
    {
        if (SkillCount == 1)
        {
            SoundManager.instance.SFXPlay("BossHit", clip[3]);
            Invoke("isChaUSkill2", 0.1f);
        }
    }

    void isChaUSkil4()
    {
        SkillCount = 0;
    }


    void isWolfSkill1()
    {
        RB.AddForce(new Vector2(0, 1) * JumpPower, ForceMode2D.Impulse);
        float dirc = Playertrans.position.x - transform.position.x; //플레이어 방향
        ishit = dirc * JumpSpeed;
        Invoke("isWolfSkill1Go", 0.3f);
    }

    void isWolfSkill2()
    {
        for (int i = 0; i < Monster1.Length; i++)
        {
            Monster1[i].SetActive(true);
            Monster1[i].transform.position = Monster1[i].GetComponent<Enemy>().thistrans.position;
            Monster1[i].GetComponent<Enemy>().Angry();
        }
    }

    void isWolfSkill1Go()
    {
        SkillJump = true;
    }


    public void onDamaged(float damage)//맞았을때
    {
        SoundManager.instance.SFXPlay("BossHit", clip[0]);
        HealthImage.gameObject.SetActive(true);
        HealthImage1.gameObject.SetActive(true);
        if (Hp > 0.1f)
        {
            int dirc = trans.position.x - Playertrans.position.x > 0 ? 1 : -1;
            Hp = Hp - damage;
            gameObject.layer = 12; //무적
            Invoke("Damaged", 0.1f);
            Color1 = 10;
        }
    }
    void Damaged() //무적해제
    {
        gameObject.layer = 7; //무적해제
        spriteRenderer.color = new Color(1, 1, 1);
    }
    void Die()
    {
        gameObject.SetActive(false);
    }
    void IsSkillAttack() //스킬공격
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos2.position, SkillboxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.layer == 8)
            {
                Player player = collider.GetComponent<Player>();
                player.onDamaged(trans.position);
                player.PlayerHp -= Skilldamage * 0.01f * (100 - player.Defense);
            }
        }


    }

    void isWolfBoySkill1()
    {
        SoundManager.instance.SFXPlay("BossHit", clip[2]);
        Skill.GetComponent<EnemySkill>().speed = SkillSpeed;
    }

    void BoyAngry() //늑대아이 부활
    {
        Hp = FullHp;
        spriteRenderer.sprite = BossSprite[1];
        anim.SetTrigger("isDie2");
        anim.SetBool("isAngry", true);
        Effect.GetComponent<Animator>().SetBool("isLighting", false);
        //Effect.GetComponent<Animator>().SetTrigger("isEffect");
        Invoke("Angry", 3.5f);
        AttackDelay = 0.2f;
        AttackEndDelay = 2.5f;
        SkillSpeed = 15;
        Skill.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
    }

    void CancleIshit()
    {
        ishit = 0;
    }
    void IsBlackSkill() //검은수염 스킬
    {
        if (SkillCount > 0)
        {
            anim.SetTrigger("isSkill1");
            Monster1[SkillCount - 1].SetActive(true);
            Monster1[SkillCount - 1].transform.position = Playertrans.position - Vector3.down * 1f;
            Monster1[SkillCount - 1].GetComponent<Animator>().SetTrigger("isCome");
            SkillCount -= 1;
            Invoke("IsBlackSkill", 0.7f);
        }
        else
        {
            SkillCount = 3;
            IsBalckSkillEnd();
            anim.SetTrigger("isSkill2");
            particle[0].Play();
            IsAttack();
        }
    }
    void IsBalckSkillEnd()
    {
        if (SkillCount > 0)
        {
            Monster1[SkillCount - 1].GetComponent<EnemySkill>().Time1 = 0;
            SkillCount -= 1;
            Invoke("IsBalckSkillEnd", 0.2f);
        }
    }
    void IsBlackSkill2() //검은수염 스킬2
    {
        for (int i = 3; i <= 5; i++)
        {
            Monster1[i].SetActive(true);
            Monster1[i].transform.position = Playertrans.position + new Vector3(Random.Range(20, -20), 15 + Random.Range(5, -5), 0);
            Monster1[i].GetComponent<EnemySkill>().Udo();

        }
    }

    void ChamChiThink() //참치생각
    {
        int a = Random.Range(0, 3);
        if (a == 0)
        {
            NPC.GetComponent<NPC>().TalkingNPC("오른쪽을 조심해!", 4);
            transform.position = new Vector2(120, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            Invoke("ChamChiAttack1", 1);
        }
        else if (a == 1)
        {
            NPC.GetComponent<NPC>().TalkingNPC("왼쪽을 조심해!", 4);
            transform.position = new Vector2(34, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
            Invoke("ChamChiAttack1", 1);
        }
        else if (a == 2)
        {
            NPC.GetComponent<NPC>().TalkingNPC("가운데를 조심해!!!", 4);
            Invoke("ChamChiAttack2", 1);
        }
    }

    void ChamChiAttack1()
    {
        anim.SetTrigger("isAttack1");
        Invoke("ChamChiAttack1ing", 1.25f);
        CurrentHp = Hp;
        shakepower = 30;
        shaketime = 10;
        Invoke("ShakeCamera1", 1.25f);
    }

    void ChamChiAttack1ing()
    {
        if (Hp > CurrentHp - 200)
        {
            if (ShipHP >= 0)
            {
                SoundManager.instance.SFXPlay("PlayerAttack", clip[1]);
                ShipHP -= 10;
                Invoke("ChamChiAttack1ing", 0.5f);


                ShipHealthImage.GetComponent<Image>().color = new Color(1, 0.5f, 0.5f);
                Invoke("ReturnColor", 0.25f);

                ShakeCamera(10, 10);
            }
        }
        else
        {
            anim.SetTrigger("isAttack1End");
            Invoke("ChamChiWater", 1.2f);
            Invoke("ChamChiThink", 4f);

            shakepower = 30;
            shaketime = 30;
            Invoke("ShakeCamera1", 1f);
        }
    }

    void ChamChiAttack2()
    {
        anim.SetTrigger("isAttack2");
        Invoke("ChamChiAttack1ing2", 0.85f);
        Invoke("ChamChiThink", 4);

    }

    void ChamChiAttack1ing2()
    {
        ShipHP -= 10;
        ShipHealthImage.GetComponent<Image>().color = new Color(1, 0.5f, 0.5f);
        Invoke("ReturnColor", 0.5f);

        ShakeCamera(30, 30);
    }

    void ChamChiWater()
    {
        for (int i = 0; i < 5; i++)
        {
            Monster1[i].GetComponent<EnemySkill>().XForce = 500 * transform.localScale.x;
            Monster1[i].SetActive(true);
            Monster1[i].transform.position = pos2.position + new Vector3(Random.Range(5, -5), 15 + Random.Range(5, -5), 0);
            Monster1[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(200, -200), Random.Range(100, -100)));
        }
    }

    void ChamChiCome()
    {
        SoundManager.instance.SFXPlay("Come", clip[2]);
        SkillCount -= 1;
        if (SkillCount > 0)
            Invoke("ChamChiCome", 0.25f);
    }

    void IsArmorSkill() //아머 스킬
    {
        if (SkillCount > 0)
        {
            anim.SetTrigger("isSkill1");
            Monster1[SkillCount - 1].SetActive(false);
            Monster1[SkillCount - 1].SetActive(true);
            Monster1[SkillCount - 1].transform.localScale = new Vector3(0.1f, 0.1f, 1);
            Monster1[SkillCount - 1].transform.position = pos.position;
            SkillCount -= 1;
            IsArmorSkillBigger();
            Invoke("IsArmorSkill", 1.5f);
        }
        else
        {
            SkillCount = 3;
        }
    }

    void IsArmorSkill2() //아머 스킬2
    {
        SkillCount = 5;
        anim.SetTrigger("isSkill1");
        Monster1[SkillCount].SetActive(false);
        Monster1[SkillCount].SetActive(true);
        Monster1[SkillCount].transform.localScale = new Vector3(0.1f, 0.1f, 1);
        Monster1[SkillCount].transform.position = pos.position;
        IsArmorSkillBigger2();
    }

    void IsArmorSkillBigger()
    {
        if (Monster1[SkillCount].transform.localScale.x < 1)
        {
            Monster1[SkillCount].transform.localScale = new Vector3(Monster1[SkillCount].transform.localScale.x + 0.1f, Monster1[SkillCount].transform.localScale.x + 0.1f, 1);
            Invoke("IsArmorSkillBigger", 0.1f);
        }
        else
            Invoke("IsArmorSkillEnd", 0.2f);
    }
    void IsArmorSkillBigger2()
    {
        if (Monster1[SkillCount].transform.localScale.x < 2)
        {
            Monster1[SkillCount].transform.localScale = new Vector3(Monster1[SkillCount].transform.localScale.x + 0.1f, Monster1[SkillCount].transform.localScale.x + 0.1f, 1);
            Invoke("IsArmorSkillBigger2", 0.1f);
        }
        else
            Invoke("IsArmorSkillEnd", 0.2f);
    }

    void IsArmorSkillEnd()
    {
        Monster1[SkillCount].GetComponent<EnemySkill>().Udo();
    }


    void IsSlimeSkill() //슬라임 스킬
    {
        if (SkillCount > 0)
        {
            SoundManager.instance.SFXPlay("BossSkill", clip[2]);
            particle[0].Play();
            Monster1[SkillCount - 1].SetActive(false);
            Monster1[SkillCount - 1].SetActive(true);
            Monster1[SkillCount - 1].transform.position = pos2.position;
            Monster1[SkillCount - 1].GetComponent<EnemySkill>().Udo();
            SkillCount -= 1;
            Invoke("IsSlimeSkill", 0.5f);
        }
        else
        {
            SkillCount = 3;
            anim.SetTrigger("isSkill1End");
        }
    }


    void ReturnColor()
    {
        ShipHealthImage.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
    }

    public void ShakeCamera(float intensity, float time)
    {
        CMCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        ShakeTime = time;
    }

    void ShakeCamera1()
    {
        ShakeCamera(shakepower, shaketime);
    }

    void monstercome()
    {
        Monster1[6].SetActive(false);
        Monster1[7].SetActive(false);
        Monster1[3].SetActive(true);
        Monster1[4].SetActive(true);
        Monster1[3].GetComponent<Enemy>().Replay();
        Monster1[4].GetComponent<Enemy>().Replay();
    }

    void GhostThink() //고스트생각
    {
        particle[0].Play();
        particle[1].Stop();
        particle[2].Play();
        particle[3].Stop();

        int a = Random.Range(0, 4);
        if (a == 0)
        {
            NPC.GetComponent<NPC>().TalkingNPC("오른쪽에서 온다!", 4);
            transform.position = new Vector2(130, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            Invoke("GhostAttack1", 1);
        }
        else if (a == 1)
        {
            NPC.GetComponent<NPC>().TalkingNPC("왼쪽에서 온다!", 4);
            transform.position = new Vector2(22, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
            Invoke("GhostAttack1", 1);
        }
        else if (a == 2)
        {
            NPC.GetComponent<NPC>().TalkingNPC("오른쪽을 조심해!!!", 4);
            transform.position = new Vector2(130, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            Invoke("GhostAttack2", 1);
        }
        else if (a == 3)
        {
            NPC.GetComponent<NPC>().TalkingNPC("왼쪽을 조심해!!!", 4);
            transform.position = new Vector2(22, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
            Invoke("GhostAttack2", 1);
        }
    }

    void GhostAttack1()
    {
        anim.SetTrigger("isAttack1");
        Invoke("GhostAttack1ing", 2f);
        CurrentHp = Hp;
        shakepower = 30;
        shaketime = 10;
        Invoke("ShakeCamera1", 2f);
    }

    void GhostAttack1ing()
    {
        particle[2].Stop();
        particle[3].Play();
        particle[2].transform.localScale = new Vector3(transform.localScale.x * -1, particle[3].transform.localScale.y, 1);
        particle[3].transform.localScale = new Vector3(transform.localScale.x * -1, particle[3].transform.localScale.y, 1);

        if (transform.localScale.x < 0)
        {
            particle[0].Stop();
            particle[1].Play();
            particle[3].GetComponent<AreaEffector2D>().forceAngle = 0;
            particle[3].GetComponent<AreaEffector2D>().forceMagnitude = 100;
        }
        else
        {
            particle[0].Play();
            particle[1].Stop();
            particle[3].GetComponent<AreaEffector2D>().forceAngle = 180;
            particle[3].GetComponent<AreaEffector2D>().forceMagnitude = 100;
        }
        if (Hp > CurrentHp - 400)
        {
            if (ShipHP >= 0)
            {
                SoundManager.instance.SFXPlay("PlayerAttack", clip[1]);
                ShipHP -= 8;
                Invoke("GhostAttack1ing", 0.5f);


                ShipHealthImage.GetComponent<Image>().color = new Color(1, 0.5f, 0.5f);
                Invoke("ReturnColor", 0.25f);

                ShakeCamera(10, 10);
            }
        }
        else
        {
            particle[0].Play();
            particle[1].Stop();
            particle[2].Play();
            particle[3].Stop();
            anim.SetTrigger("isAttack1End");
            Invoke("GhostThink", 10f);
            particle[3].GetComponent<AreaEffector2D>().forceMagnitude = 0;
            ShakeCamera(30, 30);
        }
    }

    void GhostAttack2()
    {
        anim.SetTrigger("isAttack2");
        Invoke("GhostAttack1ing2", 2.67f);
        Invoke("GhostThink", 4);

    }

    void GhostAttack1ing2()
    {
        ShipHP -= 10;
        ShipHealthImage.GetComponent<Image>().color = new Color(1, 0.5f, 0.5f);
        Invoke("ReturnColor", 0.5f);

        ShakeCamera(30, 30);
    }

    void DragonThink() //드래곤생각
    {
        //SkillCount = 0;
        if (SkillCount > 0)
        {
            int a = Random.Range(0, 3);
            if (a == 0)
            {
                Invoke("DragonAttack1", 2);
                anim.SetTrigger("isAttack1");
                Invoke("DragonThink", 5);
            }
            else if (a == 1)
            {
                anim.SetTrigger("isAttack2");
                Invoke("DragonThink", 7);
            }
            else if (a == 2)
            {
                anim.SetTrigger("isAttack3");
                Invoke("DragonThink", 7);
            }
            SkillCount -= 1;
        }
        else
        {
            anim.SetTrigger("isSkill1");
            Invoke("DragonSkill", 5);
            SkillCount = 3;
        }
    }
    void DragonAttack1()
    {
        ShakeCamera(10, 10);
        IsAttack();
        for (int i = 0; i <= 9; i++)
        {
            Monster1[i].SetActive(true);
            Monster1[i].transform.position = Playertrans.position + new Vector3(Random.Range(20, -20), 15 + Random.Range(20, 10), 0);
        }
    }
    void DragonSkill()
    {
        if (SkillCount > 0)
        {
            SkillCount -= 1;
            int a = Random.Range(0, 4);
            if (a == 0)
            {
                anim.SetTrigger("isSkill2");
                Follower.Follow = pos2.transform;
                CMCamera.m_Lens.OrthographicSize = 10;
                Invoke("DragonGroupCamera", 4);
                Invoke("DragonSkill2End", 5);
                Invoke("DragonSkill", 10);
            }
            else if (a == 1)
            {
                SkillSpeed = 10;
                anim.SetTrigger("isSkill3");
                Follower.Follow = pos2.transform;
                CMCamera.m_Lens.OrthographicSize = 10;
                Invoke("DragonGroupCamera", 2);
                Invoke("DragonSkill3End", 3);

                Invoke("DragonSkill", 13);
            }
            else if (a == 2)
            {
                anim.SetTrigger("isSkill4");
                Invoke("DragonSkill", 5);
            }
            else if (a == 3)
            {
                anim.SetTrigger("isSkill5");
                Invoke("DragonSkill", 5);
            }
        }
        else
        {
            anim.SetTrigger("isSkill1End");
            Invoke("DragonThink", 10);
            SkillCount = 3;
        }
    }
    void DragonGroupCamera()
    {
        Follower.Follow = Dungeon.GetComponent<DungeonManager>().Group.transform;
    }

    void DragonSkill2End()
    {
        ETC[0].GetComponent<Animator>().SetTrigger("isBomb");
        CMCamera.m_Lens.OrthographicSize = 5;
        for (int i = 10; i <= 16; i++)
        {
            Monster1[i].SetActive(true);
            Monster1[i].transform.position = ETC[0].transform.position;
            Monster1[i].GetComponent<EnemySkill>().speed = 10;
            Monster1[i].GetComponent<EnemySkill>().RandomGo2();
        }
    }
    void DragonSkill3End()
    {
        CMCamera.m_Lens.OrthographicSize = 5;
        if (SkillSpeed < 20)
        {
            Monster1[SkillSpeed].SetActive(true);
            Monster1[SkillSpeed].transform.position = ETC[0].transform.position;
            Monster1[SkillSpeed].GetComponent<EnemySkill>().speed = 30;
            Monster1[SkillSpeed].GetComponent<EnemySkill>().Udo();
            SkillSpeed += 1;
            Invoke("DragonSkill3End", 1f);
        }
    }
    void isMineralSkill()
    {
        Monster1[1].GetComponent<EnemySkill>().isBeam = false;
    }

    void AncientThink() //고대병기 생각
    {
        Follower.Follow = ETC[4].transform;
        //SkillCount = 0;
        if (Hp > FullHp / 2 || SkillCount2<=3)
        {
            SkillCount2 += 1;
            int a = Random.Range(0, 4);
            //a = 3;
            if (a == 0)
            {
                anim.SetTrigger("isAttack1");
                Invoke("AncientThink", 3);
            }
            else if (a == 1)
            {
                anim.SetTrigger("isAttack2");
                Invoke("AncientThink", 3);
            }
            else if (a == 2)
            {
                SkillCount = 0;
                anim.SetTrigger("isAttack3");
                int b = Random.Range(0, 2);

                if(b == 0)
                    Invoke("AncientAttack3", 3);
                else
                    Invoke("AncientAttack3s", 3);
                Invoke("AncientThink", 8);

                for (int i = 0; i < 10; i++)
                {
                    Monster1[i].SetActive(false);
                    Monster1[i].transform.position = pos.transform.position;
                }
            }
            else if (a == 3)
            {
                anim.SetTrigger("isAttack4");
                Invoke("AncientAttack4", 3);
                SkillCount3 = 0;
                Invoke("AncientThink", 10);
                for (int i = 10; i < 13; i++)
                {
                    Monster1[i].SetActive(false);
                    Monster1[i].transform.position = pos.transform.position;
                }
            }
        }
        else
        {
            anim.SetTrigger("isSkill1");
            Invoke("AncientSkill", 5);
            for (int i = 0; i < 3; i++)
            {
                Monster1[i].SetActive(false);
                Monster1[i].transform.position = pos.transform.position;
            }
            SkillCount2 = 0;
        }
    }

    void AncientAttack3()
    {
        Follower.Follow = ETC[3].transform;
        if (SkillCount < 9)
        {
            SoundManager.instance.SFXPlay("Boss", clip[1]);
            Monster1[SkillCount].SetActive(true);
            Monster1[SkillCount].transform.position = pos.transform.position;
            Monster1[SkillCount].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[SkillCount].GetComponent<EnemySkill>().speed = 10;
            Monster1[SkillCount].GetComponent<EnemySkill>().Go(-70 + (SkillCount* 20));
            SkillCount += 1;
            Invoke("AncientAttack3", 0.1f);
        }
        else
        {
            SkillCount = 0;
            AncientAttack3End();
        }
    }
    void AncientAttack3s()
    {
        Follower.Follow = ETC[3].transform;
        if (SkillCount < 9)
        {
            SoundManager.instance.SFXPlay("Boss", clip[1]);
            Monster1[SkillCount].SetActive(true);
            Monster1[SkillCount].transform.position = pos.transform.position;
            Monster1[SkillCount].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[SkillCount].GetComponent<EnemySkill>().speed = 10;
            Monster1[SkillCount].GetComponent<EnemySkill>().Go(70 - (SkillCount * 20));
            SkillCount += 1;
            Invoke("AncientAttack3s", 0.1f);
        }
        else
        {
            SkillCount = 0;
            AncientAttack3End();
        }
    }

    void AncientAttack3End()
    {
        if (SkillCount < 9)
        {
            Monster1[SkillCount].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[SkillCount].GetComponent<EnemySkill>().speed = 30;
            Monster1[SkillCount].GetComponent<EnemySkill>().Udo();
            SkillCount += 1;
            Invoke("AncientAttack3End", 0.2f);
            SoundManager.instance.SFXPlay("Boss", clip[2]);
        }
    }
    void AncientAttack4()
    {
        Follower.Follow = ETC[5].transform;
        SoundManager.instance.SFXPlay("Boss", clip[1]);
        for (int i = 10; i < 13; i++)
        {
            Monster1[i].SetActive(true);
            Monster1[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[i].transform.position = pos.transform.position;
            Monster1[i].GetComponent<EnemySkill>().speed =5;
            Monster1[i].GetComponent<EnemySkill>().Go(120*(i-10));
        }
        SkillCount3 = 1;
        Invoke("AncientAttack4End", 1f);
    }
    void AncientAttack4End()
    {
        if (SkillCount3 < 5)
        {
            for (int i = 10; i < 13; i++)
            {
                Monster1[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Monster1[i].GetComponent<EnemySkill>().speed = 10 * SkillCount3;
                Monster1[i].GetComponent<EnemySkill>().Udo();
            }
            SoundManager.instance.SFXPlay("Boss", clip[2]);
            Invoke("AncientAttack4End", 1f);
            SkillCount3 += 1;
        }
        else
        {
            for (int i = 10; i < 13; i++)
            {
                Monster1[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Monster1[i].GetComponent<Animator>().SetTrigger("isBomb");
            }
            Follower.Follow = ETC[4].transform;
        }
    }
    void AncientSkill()
    {
        SkillCount = 0;
        SkillCount3 = 0;
        anim.SetTrigger("isSkill2");
        Invoke("AncientThink", 10);
        Invoke("AncientAttack3", 2);
        //Invoke("AncientAttack4", 2);
    }
    void isSoldierSkill()
    {
        Monster1[0].SetActive(false);
        Monster1[0].SetActive(true);
        Monster1[0].transform.position = pos2.position;
        Monster1[0].GetComponent<EnemySkill>().Udo();
        anim.SetTrigger("isSkill1");
        Invoke("isSoldierSkill1", 0.5f);
    }
    void isSoldierSkill1()
    {
        Monster1[0].SetActive(false);
        Monster1[0].SetActive(true);
        Monster1[0].GetComponent<EnemySkill>().Udo1(1);
        anim.SetTrigger("isSkill2");
        Invoke("isSoldierSkill2", 0.5f);
    }
    void isSoldierSkill2()
    {
        Monster1[0].SetActive(false);
        Invoke("Angry", 1f);
    }
    void isBeastSkill()
    {
        if (SkillCount <= 2)
        {
            Monster1[SkillCount].SetActive(false);
            Monster1[SkillCount].SetActive(true);
            Monster1[SkillCount].transform.position = pos2.position;
            Monster1[SkillCount].GetComponent<EnemySkill>().Udo();
            Invoke("isBeastSkill", 0.1f);
            SkillCount += 1;
            SoundManager.instance.SFXPlay("BossHit", clip[2]);
        }
    }

    void isBeastSkil2()
    {
        Monster1[5].SetActive(false);
        Monster1[5].SetActive(true);
        Monster1[5].transform.position = pos2.position;
        Monster1[5].GetComponent<EnemySkill>().Udo();
        SoundManager.instance.SFXPlay("BossHit", clip[3]);
    }

    void WizardThink()
    {
        if ( SkillCount2 <= 2)
        {
            SkillCount2 += 1;
            int a = Random.Range(0, 2);
            //a = 3;
            if (a == 0)
            {
                anim.SetTrigger("isAttack1");
                Invoke("WizardThink", 5);
            }
            else if (a == 1)
            {
                anim.SetTrigger("isAttack2");
                Invoke("WizardAttack2", 2);
                Invoke("WizardThink", 5);
                for (int i = 0; i < 3; i++)
                {
                    Monster1[i].SetActive(false);
                    Monster1[i].transform.position = ETC[i].transform.position;
                }
            }
            else if (a == 2)
            {
                SkillCount = 0;
                anim.SetTrigger("isAttack3");
                Invoke("AncientAttack3", 3);
                Invoke("WizardThink", 8);

                for (int i = 0; i < 10; i++)
                {
                    Monster1[i].SetActive(false);
                    Monster1[i].transform.position = pos.transform.position;
                }
            }
            else if (a == 3)
            {
                anim.SetTrigger("isAttack4");
                Invoke("AncientAttack4", 3);
                SkillCount3 = 0;
                Invoke("AncientThink", 10);
                for (int i = 10; i < 13; i++)
                {
                    Monster1[i].SetActive(false);
                    Monster1[i].transform.position = pos.transform.position;
                }
            }
        }
        else
        {
            anim.SetTrigger("isSkill");
            Invoke("isWizardSkillEnd", 3f);
            SkillCount2 = 0;
        }
    }
    void WizardAttack2()
    {
        for (int i = 0; i < 3; i++)
        {
            Monster1[i].SetActive(true);
            Monster1[i].transform.position = ETC[i].transform.position;
            Monster1[i].GetComponent<EnemySkill>().Udo();
        }
    }
    void isWizardSkillEnd()
    {
        anim.SetTrigger("isSkillEnd");
        JumpSpeed = JumpSpeed * (-1) + 1;
        anim.SetFloat("Blend", JumpSpeed);
        Invoke("WizardThink", 5);
    }
    void isStarakSkill()
    {
        if (SkillCount < 3)
        {
            SoundManager.instance.SFXPlay("BossHit", clip[4]);
            Monster1[SkillCount].SetActive(false);
            Monster1[SkillCount].SetActive(true);
            Monster1[SkillCount].transform.position = ETC[0].transform.position;
            Monster1[SkillCount].GetComponent<EnemySkill>().speed = 10;
            Monster1[SkillCount].GetComponent<EnemySkill>().Udo();
            SkillCount += 1;
            Invoke("isStarakSkill", 0.67f);
            if(DieCount == 0)
            {
                Invoke("isStarakSkillA", 0.33f);
            }
        }

    }
    void isStarakSkillA()
    {
        SoundManager.instance.SFXPlay("BossHit", clip[2]);
        Monster1[SkillCount + 2].SetActive(false);
        Monster1[SkillCount + 2].SetActive(true);
        Monster1[SkillCount + 2].transform.position = ETC[3].transform.position;
        Monster1[SkillCount + 2].GetComponent<EnemySkill>().Udo();
    }
    void isStarakSkill3()
    {
        ETC[1].GetComponent<Animator>().SetTrigger("isSkill");
        ETC[1].transform.position = new Vector2(Playertrans.position.x, ETC[1].transform.position.y);
    }

    void StarakAngry() //스태락 부활
    {
        Invoke("Angry", 1f);
    }
    void StarakAngry1()
    {
        ETC[2].SetActive(true);
        spriteRenderer.sprite = BossSprite[1];
        ETC[4].SetActive(true);
    }
    void StarakAngry2()
    {
        Hp = FullHp;
        spriteRenderer.sprite = BossSprite[2];
        Dungeon.GetComponent<DungeonManager>().audiosource[2].Play();
    }

    void isStarakSkill3A()
    {
        SoundManager.instance.SFXPlay("BossHit", clip[2]);
        for (int i = 6; i < 11; i++)
        {
            Monster1[i].SetActive(true);
            Monster1[i].transform.position = ETC[3].transform.position;
            Monster1[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[i].GetComponent<EnemySkill>().Go((5+5*(6-i))*(int)gameObject.transform.localScale.x);
        }
    }

    void isStarakSound()
    {
        SoundManager.instance.SFXPlay("BossHit", clip[3]);
    }
    void StarakEnd()
    {
        GameObject.Find("Manager").GetComponent<Manager>().Fade = 1;
        Invoke("StarakEnd1", 1);
    }
    void StarakEnd1()
    {
        SceneManager.LoadScene("CatleEnd2");
    }
    void isBlenderSkill1()
    {
        ishit = transform.localScale.x * -30;
        Invoke("CancleIshit", 0.67f);
        Invoke("isBlenderSkill1A", 0.33f);
    }
    void isBlenderSkill1A()
    {
        if (DieCount >= 4)
        {
            SoundManager.instance.SFXPlay("BossHit", clip[2]);
            for (int i = 0; i < 5; i++)
            {
                Monster1[i].SetActive(true);
                Monster1[i].transform.position = ETC[0].transform.position;
                Monster1[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Monster1[i].GetComponent<EnemySkill>().speed = 20;
                Monster1[i].GetComponent<EnemySkill>().Udo2(-10 + i * 5);
                Monster1[i].GetComponent<EnemySkill>().Time1 = 50;
            }
        }
        else
        {
            SoundManager.instance.SFXPlay("BossHit", clip[2]);
            for (int i = 0; i < 5; i++)
            {
                Monster1[i].SetActive(true);
                Monster1[i].transform.position = ETC[0].transform.position;
                Monster1[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Monster1[i].GetComponent<EnemySkill>().speed = 20;
                Monster1[i].GetComponent<EnemySkill>().Udo2(-20 + i * 10);
                Monster1[i].GetComponent<EnemySkill>().Time1 = 80;
                Monster1[i].GetComponent<EnemySkill>().InvokeUdo(0.5f, 30, false);
            }
        }
    }
    void isBlenderSkill4()
    {
        int n = 3;
        if (DieCount < 4)
            n = 5;

        if (SkillCount < n)
        {
            Monster1[SkillCount + 10].transform.localScale = new Vector2(0.5f, 0.5f);
            Monster1[SkillCount + 10].SetActive(true);
            Monster1[SkillCount + 10].transform.position = new Vector2(ETC[1].transform.position.x, ETC[1].transform.position.y-1*SkillCount);

            Monster1[SkillCount + 10].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[SkillCount + 10].GetComponent<EnemySkill>().isBeam = true;
            SkillCount += 1;
            Invoke("isBlenderSkill4", 0.1f);
            SoundManager.instance.SFXPlay("BossHit", clip[4]);
        }
        else
            SkillCount = 0;
    }
    void isBlenderSkill4A()
    {
        int n = 3;
        if (DieCount < 4)
            n = 5;
        if (SkillCount < n)
        {
            Monster1[SkillCount+10].GetComponent<EnemySkill>().isBeam = false;
            Monster1[SkillCount + 10].GetComponent<EnemySkill>().Udo();
            Monster1[SkillCount + 10].GetComponent<EnemySkill>().Time1 = 70;
            SkillCount += 1;
            Invoke("isBlenderSkill4A", 0.1f);
            SoundManager.instance.SFXPlay("BossHit", clip[2]);
        }
        else
            SkillCount = 0;
    }
    void isBlenderSkill2()
    {
        Monster2[0].SetActive(true);
        Monster2[0].transform.position = new Vector2(transform.position.x, 0);
        Monster2[0].GetComponent<EnemySkill>().Udo();
        if (DieCount < 4)
        {
            Monster2[0].GetComponent<EnemySkill>().InvokeUdo(2, 20, false);
            Monster2[0].GetComponent<EnemySkill>().InvokeUdo(3, 20, false);
            Monster2[0].GetComponent<EnemySkill>().InvokeUdo(4, 20, false);
            Monster2[0].GetComponent<EnemySkill>().InvokeUdo(5, 20, false);
            Monster2[0].GetComponent<EnemySkill>().InvokeUdo(6, 20, false);
        }
    }
    void isBlenderSkill3()
    {
        int n = 2;
        if (DieCount < 4)
            n = 4;
        if (SkillCount2 < n)
        {
            Monster1[SkillCount2 + 15].transform.localScale = new Vector2(0.25f, 0.25f);
            Monster1[SkillCount2+15].SetActive(true);
            Monster1[SkillCount2+15].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[SkillCount2 + 15].transform.position = ETC[0].transform.position;
            Monster1[SkillCount2 + 15].GetComponent<EnemySkill>().speed = 3;
            Monster1[SkillCount2 + 15].GetComponent<EnemySkill>().RandomGo3();
            Monster1[SkillCount2 + 15].GetComponent<EnemySkill>().turnSpeed = 500;
            Monster1[SkillCount2 + 15].GetComponent<EnemySkill>().Time1 = 300;
            Monster1[SkillCount2 + 15].GetComponent<EnemySkill>().isRotationTarget = false;
            Monster1[SkillCount2 + 15].GetComponent<EnemySkill>().InvokeUdo(2, 30, false);
            Invoke("isBlenderSkill3", 0.33f);
            SoundManager.instance.SFXPlay("BossHit", clip[4]);
            SkillCount2 += 1;
        }
        else
        {
            SkillCount2  = 0;
        }
    }
    void isBlenderSkill6()
    {
        //SoundManager.instance.SFXPlay("BossHit", clip[5]);
        Monster2[5].SetActive(true);
        Monster2[5].transform.position = ETC[0].transform.position;
        Monster2[5].GetComponent<EnemySkill>().Time1 = 500;
        Monster2[5].GetComponent<EnemySkill>().speed = 5;
        Monster2[5].GetComponent<EnemySkill>().Udo();

        Monster2[5].GetComponent<EnemySkill>().InvokeUdo(1, 10, true);
        Monster2[5].GetComponent<EnemySkill>().InvokeUdo(2, 10, true);
        Monster2[5].GetComponent<EnemySkill>().InvokeUdo(3, 10, true);
        if (DieCount < 4)
        {
            Monster2[5].GetComponent<EnemySkill>().InvokeUdo(4, 10, true);
            Monster2[5].GetComponent<EnemySkill>().InvokeUdo(5, 10, true);
        }
    }
    void BlenderDieSkill00()
    {
        HitspriteRenderer[0].sprite = BossSprite[0];
        HitspriteRenderer[1].sprite = BossSprite[1];
        ETC[10].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        ETC[11].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }
    void BlenderDieSkill0()
    {
        HitspriteRenderer[0].sprite = BossSprite[2];
        HitspriteRenderer[1].sprite = BossSprite[3];
        ETC[10].GetComponent<SpriteRenderer>().color = new Color(0.85f, 0.85f, 0.85f);
        ETC[11].GetComponent<SpriteRenderer>().color = new Color(0.85f, 0.85f, 0.85f);
    }
    void BlenderDieSkill01()
    {
        HitspriteRenderer[0].sprite = BossSprite[4];
        HitspriteRenderer[1].sprite = BossSprite[5];
        ETC[10].GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
        ETC[11].GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
    }
    void BlenderDieSkill02()
    {
        HitspriteRenderer[0].sprite = BossSprite[6];
        HitspriteRenderer[1].sprite = BossSprite[7];
        ETC[10].GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0);
        ETC[11].GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0);

    }
    void BlenderDieSkill1()
    {
        
        transform.position = new Vector2(62, transform.position.y);
        anim.SetTrigger("isDadSkill1");
        Invoke("BlenderDieSkill2", 1.5f);
    }
    void BlenderDieSkill2()
    {
        anim.SetTrigger("isDadSkill2");
        Invoke("BlenderDieSkill3", 2f);
        if(DieCount < 4)
            Invoke("BlenderDieSkill3A", 2f);
    }
    void BlenderDieSkill3()
    {
        SkillCount = 0;
        SoundManager.instance.SFXPlay("BossHit", clip[2]);
        for (int i = 0; i < 10; i++)
        {
            Monster1[i].SetActive(true);
            Monster1[i].transform.position = ETC[2].transform.position;
            Monster1[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[i].GetComponent<EnemySkill>().speed = 20;
            Monster1[i].GetComponent<EnemySkill>().Go(45 + 30 * i);
            Monster1[i].GetComponent<EnemySkill>().Time1 = 80;
        }
        Invoke("BlenderDieSkill4", 3f);
        if (DieCount < 4)
            Invoke("BlenderDieSkill4A", 3f);
    }
    void BlenderDieSkill3A()
    {
        for (int i = 10; i < 13; i++)
        {
            Monster1[i].SetActive(true);
            Monster1[i].transform.position = ETC[0].transform.position;
            Monster1[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[i].transform.localScale = new Vector2(0.25f, 0.25f);
            Monster1[i].GetComponent<EnemySkill>().Udo2(-20 + (i-10) * 20);
            Monster1[i].GetComponent<EnemySkill>().Time1 = 80;
        }
    }
    void BlenderDieSkill4()
    {
        SkillCount = 0;
        SkillCount2 = 0;
        SoundManager.instance.SFXPlay("BossHit", clip[2]);
        for (int i = 0; i < 10; i++)
        {
            Monster1[i].SetActive(false);
            Monster1[i].SetActive(true);
            Monster1[i].transform.position = ETC[2].transform.position;
            Monster1[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[i].GetComponent<EnemySkill>().Go(30 + 30 * i);
            Monster1[i].GetComponent<EnemySkill>().Time1 = 80;
        }
        Invoke("BlenderDieSkill5", 3f);
        Invoke("BlenderDieSkill5A", 3f);
        if (DieCount < 4)
        {
            Invoke("BlenderDieSkill3A", 3f);
            Invoke("BlenderDieSkill4A", 4f);
        }
    }
    void BlenderDieSkill4A()
    {
        for (int i = 13; i < 16; i++)
        {
            Monster1[i].SetActive(true);
            Monster1[i].transform.position = ETC[0].transform.position;
            Monster1[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[i].transform.localScale = new Vector2(0.25f, 0.25f);
            Monster1[i].GetComponent<EnemySkill>().Udo2(-20 + (i - 13) * 20);
            Monster1[i].GetComponent<EnemySkill>().Time1 = 80;
        }
    }
    void BlenderDieSkill5()
    {
        if (SkillCount < 10)
        {
            Monster1[SkillCount].SetActive(false);
            Monster1[SkillCount].SetActive(true);
            Monster1[SkillCount].transform.position = ETC[2].transform.position;
            Monster1[SkillCount].GetComponent<EnemySkill>().Go(99 + 18 * SkillCount);
            Monster1[SkillCount].GetComponent<EnemySkill>().Time1 = 80;
            SkillCount += 1;
            Invoke("BlenderDieSkill5", 0.1f);
            SoundManager.instance.SFXPlay("BossHit", clip[2]);
        }
        else
        {
            SkillCount = 0;
            Invoke("BlenderDieSkill6", 2f);
            if (DieCount < 4)
            {
                Invoke("BlenderDieSkill3A", 3f);
                Invoke("BlenderDieSkill4A", 2f);
            }
        }
    }

    void BlenderDieSkill5A()
    {
        if (SkillCount2 < 10)
        {
            Monster3[SkillCount2].SetActive(false);
            Monster3[SkillCount2].SetActive(true);
            Monster3[SkillCount2].transform.position = ETC[2].transform.position;
            Monster3[SkillCount2].GetComponent<EnemySkill>().Go(261 - 18 * SkillCount2);
            Monster3[SkillCount2].GetComponent<EnemySkill>().Time1 = 80;
            SkillCount2 += 1;
            Invoke("BlenderDieSkill5A", 0.1f);
            SoundManager.instance.SFXPlay("BossHit", clip[2]);
        }
        else
        {
            SkillCount2 = 0;
            Invoke("BlenderDieSkill6A", 2f);
        }
    }

    void BlenderDieSkill6()
    {
        if (SkillCount < 10)
        {
            Monster1[SkillCount].SetActive(false);
            Monster1[SkillCount].SetActive(true);
            Monster1[SkillCount].transform.position = ETC[2].transform.position;
            Monster1[SkillCount].GetComponent<EnemySkill>().Go(261 - 18 * SkillCount);
            Monster1[SkillCount].GetComponent<EnemySkill>().Time1 = 80;
            SkillCount += 1;
            Invoke("BlenderDieSkill6", 0.1f);
            SoundManager.instance.SFXPlay("BossHit", clip[2]);
        }
        else
        {
            SkillCount = 0;
            Invoke("BlenderDieSkill7", 2f);
        }
    }
    void BlenderDieSkill6A()
    {
        if (SkillCount2 < 10)
        {
            Monster3[SkillCount2].SetActive(false);
            Monster3[SkillCount2].SetActive(true);
            Monster3[SkillCount2].transform.position = ETC[2].transform.position;
            Monster3[SkillCount2].GetComponent<EnemySkill>().Go(99 + 18 * SkillCount2);
            Monster3[SkillCount2].GetComponent<EnemySkill>().Time1 = 80;
            SkillCount2 += 1;
            Invoke("BlenderDieSkill6A", 0.1f);
            SoundManager.instance.SFXPlay("BossHit", clip[2]);
        }
        else
        {
            SkillCount2 = 0;
        }
    }
    void BlenderDieSkill7()
    {
        anim.SetTrigger("isDadSkill3");
        Hp = FullHp;
        Invoke("Angry", 6f);
        Invoke("DiecountSave", 6f);
        BlenderDieSkill8();
        Dungeon.GetComponent<DungeonManager>().audiosource[1].Play();
    }
    void BlenderDieSkill8()
    {
        int n = 3;
        if (DieCount < 4)
        {
            n = 5;
        }
        if (SkillCount < n)
        {
            Monster1[SkillCount + 13].SetActive(true);
            Monster1[SkillCount + 13].transform.localScale = new Vector2(0.25f, 0.25f);
            Monster1[SkillCount + 13].transform.position = ETC[0].transform.position;
            Monster1[SkillCount + 13].GetComponent<EnemySkill>().speed = 4;
            Monster1[SkillCount + 13].GetComponent<EnemySkill>().RandomGo3();
            Monster1[SkillCount + 13].GetComponent<EnemySkill>().turnSpeed = 500;
            Monster1[SkillCount + 13].GetComponent<EnemySkill>().Time1 = 300;
            Monster1[SkillCount + 13].GetComponent<EnemySkill>().isRotationTarget = false;
            Monster1[SkillCount + 13].GetComponent<EnemySkill>().InvokeUdo(2, 30, false);
            Invoke("BlenderDieSkill8", 0.1f);
            SoundManager.instance.SFXPlay("BossHit", clip[4]);
            SkillCount += 1;
        }
        else
        {
            SkillCount = 0;
        }
    }
    void DiecountSave()
    {
        if(GameObject.Find("Player").GetComponent<Player>().PlayerHp > 0)
        {
            GameManager.Instance.BlenderDieCount = DieCount;
            GameObject.Find("Player").GetComponent<Player>().PlayerHp = GameObject.Find("Player").GetComponent<Player>().FullPlayerHp;
        }
    }
    void BlenderDie2()
    {
        GameObject.Find("Manager").GetComponent<Manager>().Fade = 1;
        Invoke("BlenderDie2A", 1f);
    }
    void BlenderDie2A()
    {
        SceneManager.LoadScene("LastTower2");
    }
    void isBlenderAngry()
    {
        Invoke("BlenderGo", 2);
        Invoke("BlenderThink", 22);
        Dungeon.GetComponent<DungeonManager>().CancelInvoke();
        Dungeon.GetComponent<DungeonManager>().audiosource[1].Stop();
        Dungeon.GetComponent<DungeonManager>().audiosource[2].Play();
        GameObject.Find("Player").GetComponent<Player>().alive = 1;
        Dungeon.GetComponent<DungeonManager>().Follower.Follow = Dungeon.GetComponent<DungeonManager>().Group.transform;
    }
    void BlenderGo()
    {
        anim.SetTrigger("Z7");
        Invoke("BlenderGo1", 15.16f);
        Invoke("BlenderGo2", 16.5f);
        Invoke("BlenderGo3", 17.5f);
        Invoke("BlenderASkill5A", 5f);
        SkillCount3 = 5;
    }
    void BlenderThink() //블랜더생각
    {
        if (SkillCount3 <= 4)
        {
            ETC[16].SetActive(false);
            ETC[17].SetActive(false);
            SkillCount3 += 1;
            int a = Random.Range(0, 4);
            //a = 3;
            if (a == 0)
            {
                SkillCount = 0;
                anim.SetTrigger("Z3");
                Invoke("BlenderASkill1", 2.4f);
                Invoke("BlenderASkill2", 4.7f);
                Invoke("BlenderASkill3", 6.5f);
                Invoke("BlenderThink", 10);
                shakepower = 30;
                shaketime = 30;
                Invoke("ShakeCamera1", 1f);
                Invoke("ShakeCamera1", 3.16f);
                Invoke("ShakeCamera1", 5.33f);
            }
            else if (a == 1)
            {
                anim.SetTrigger("Z5");
                Invoke("BlenderASkill5A", 5);
                Invoke("BlenderThink", 12);
            }
            else if (a == 2)
            {
                SkillCount = 0;
                anim.SetTrigger("Z4");
                Invoke("BlenderASkill4", 1);
                Invoke("BlenderASkill41", 4.5f);
                Invoke("BlenderThink", 12);
            }
            else if (a == 3)
            {
                anim.SetTrigger("Z4A");
                ETC[16].SetActive(true);
                ETC[17].SetActive(true);
                Invoke("AncientAttack4A1", 1);
                Invoke("AncientAttack4A4", 2);
                Invoke("AncientAttack4A1", 6);
                Invoke("AncientAttack4A4", 6);
                Invoke("BlenderThink", 10);
            }
        }
        else
        {
            anim.SetTrigger("Z2");
            Invoke("BlenderAngry1", 8f);
            SkillCount3 = 0;
        }
    }
    void BlenderAngry1()
    {
        anim.SetTrigger("Z1");
        Invoke("BlenderThink", 4f);
    }
    void BlenderASkill1()
    {
        if (SkillCount < 3)
        {
            Monster1[SkillCount + 10].transform.localScale = new Vector2(0.25f, 0.25f);
            Monster1[SkillCount + 10].SetActive(false);
            Monster1[SkillCount + 10].SetActive(true);
            Monster1[SkillCount + 10].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[SkillCount + 10].transform.position = ETC[2].transform.position;
            Monster1[SkillCount + 10].GetComponent<EnemySkill>().speed = 3;
            Monster1[SkillCount + 10].GetComponent<EnemySkill>().RandomGo2();
            Monster1[SkillCount + 10].GetComponent<EnemySkill>().turnSpeed = 1000;
            Monster1[SkillCount + 10].GetComponent<EnemySkill>().Time1 = 300;
            Monster1[SkillCount + 10].GetComponent<EnemySkill>().isRotationTarget = false;
            Monster1[SkillCount + 10].GetComponent<EnemySkill>().InvokeUdo(2, 30, false);
            Invoke("BlenderASkill1", 0.1f);
            SoundManager.instance.SFXPlay("BossHit", clip[4]);
            SkillCount += 1;
        }
        else
        {
            SkillCount = 0;
        }
    }
    void BlenderASkill2()
    {
        if (SkillCount < 3)
        {
            Monster1[SkillCount + 13].transform.localScale = new Vector2(0.25f, 0.25f);
            Monster1[SkillCount + 13].SetActive(false);
            Monster1[SkillCount + 13].SetActive(true);
            Monster1[SkillCount + 13].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[SkillCount + 13].transform.position = ETC[2].transform.position;
            Monster1[SkillCount + 13].GetComponent<EnemySkill>().speed = 3;
            Monster1[SkillCount + 13].GetComponent<EnemySkill>().RandomGo2();
            Monster1[SkillCount + 13].GetComponent<EnemySkill>().turnSpeed = 1000;
            Monster1[SkillCount + 13].GetComponent<EnemySkill>().Time1 = 300;
            Monster1[SkillCount + 13].GetComponent<EnemySkill>().isRotationTarget = false;
            Monster1[SkillCount + 13].GetComponent<EnemySkill>().InvokeUdo(2, 30, false);
            Invoke("BlenderASkill2", 0.1f);
            SoundManager.instance.SFXPlay("BossHit", clip[4]);
            SkillCount += 1;
        }
        else
        {
            SkillCount = 0;
        }
    }
    void BlenderASkill3()
    {
        if (SkillCount < 6)
        {
            Monster1[SkillCount + 16].transform.localScale = new Vector2(0.25f, 0.25f);
            Monster1[SkillCount + 16].SetActive(false);
            Monster1[SkillCount + 16].SetActive(true);
            Monster1[SkillCount + 16].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[SkillCount + 16].transform.position = ETC[2].transform.position;
            Monster1[SkillCount + 16].GetComponent<EnemySkill>().speed = 3;
            Monster1[SkillCount + 16].GetComponent<EnemySkill>().RandomGo2();
            Monster1[SkillCount + 16].GetComponent<EnemySkill>().turnSpeed = 1000;
            Monster1[SkillCount + 16].GetComponent<EnemySkill>().Time1 = 300;
            Monster1[SkillCount + 16].GetComponent<EnemySkill>().isRotationTarget = false;
            Monster1[SkillCount + 16].GetComponent<EnemySkill>().InvokeUdo(2, 30, false);
            Invoke("BlenderASkill3", 0.1f);
            SoundManager.instance.SFXPlay("BossHit", clip[4]);
            SkillCount += 1;
        }
        else
        {
            SkillCount = 0;
        }
    }
    void BlenderASkill5A()
    {
        for (int i = 5; i < 10; i++)
        {
            Monster2[i].SetActive(true);
            Monster2[i].transform.position = ETC[0].transform.position;
            Monster2[i].GetComponent<EnemySkill>().speed = 10;
            Monster2[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster2[i].transform.localScale = new Vector2(1f, 1f);
            Monster2[i].GetComponent<EnemySkill>().Go(20*i);
            Monster2[i].GetComponent<EnemySkill>().Time1 = 80;
        }
    }

    void BlenderASkill4()
    {
        if (SkillCount < 10)
        {
            Monster2[SkillCount + 5].transform.localScale = new Vector2(0.25f, 0.25f);
            Monster2[SkillCount + 5].SetActive(false);
            Monster2[SkillCount + 5].SetActive(true);
            Monster2[SkillCount + 5].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster2[SkillCount + 5].transform.position = new Vector2(Random.Range(70f, 110f), Random.Range(-27f, -10f));
            Monster2[SkillCount + 5].GetComponent<EnemySkill>().speed = 0;
            Monster2[SkillCount + 5].GetComponent<EnemySkill>().turnSpeed = 1000;
            Monster2[SkillCount + 5].GetComponent<EnemySkill>().Time1 = 300;
            Monster2[SkillCount + 5].transform.localScale = new Vector2(1f, 1f);
            Monster2[SkillCount + 5].GetComponent<EnemySkill>().isRotationTarget = false;
            Invoke("BlenderASkill4", 0.1f);
            SkillCount += 1;
        }
        else
        {
            SkillCount = 0;
        }
    }
    void BlenderASkill41()
    {
        for (int i = 5; i < 15; i++)
        {
            Monster2[i].GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 0);
            Monster2[i].GetComponent<EnemySkill>().Time1 = 250;
        }
    }
    void AncientAttack4A1()
    {
        ETCFlat[0] = (Playertrans.position.x- ETC[16].transform.position.x)/100;
        ETC[18].GetComponent<Animator>().SetTrigger("HandAttack");
        Invoke("AncientAttack2A", 1.5f);
        SkillCount = 0;
        AncientAttack4A2();
    }
    void AncientAttack4A2()
    {
        if (SkillCount < 100)
        {
            ETC[16].transform.position = new Vector2(ETC[16].transform.position.x + ETCFlat[0], ETC[16].transform.position.y);
            Invoke("AncientAttack4A2", 0.01f);
            SkillCount += 1;
        }
        else
        {
            ETCFlat[0] = (52 - ETC[16].transform.position.x) / 100;
            SkillCount = 0;
            Invoke("AncientAttack4A3", 1f);
        }
    }
    void AncientAttack2A()
    {
        for (int i = 0; i < 5; i++)
        {
            Monster1[i].SetActive(true);
            Monster1[i].transform.position = new Vector2(ETC[18].transform.position.x, -25);
            Monster1[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[i].transform.localScale = new Vector2(0.25f, 0.25f);
            Monster1[i].GetComponent<EnemySkill>().Go(-90 + (i) * 50);
            Monster1[i].GetComponent<EnemySkill>().Time1 = 80;
            Monster1[i].GetComponent<SpriteRenderer>().color = new Color(0, 1, 1);
            Monster1[i].GetComponent<EnemySkill>().circle.GetComponent<TrailRenderer>().time = 0;
            //Monster3[i].GetComponent<EnemySkill>().SetForce();
        }
    }
    void AncientAttack4A3()
    {
        if (SkillCount < 100)
        {
            ETC[16].transform.position = new Vector2(ETC[16].transform.position.x + ETCFlat[0], ETC[16].transform.position.y);
            Invoke("AncientAttack4A3", 0.01f);
            SkillCount += 1;
        }
    }
    void AncientAttack4A4()
    {
        ETCFlat[1] = (Playertrans.position.x - ETC[17].transform.position.x) / 100;
        ETC[19].GetComponent<Animator>().SetTrigger("HandAttack");
        SkillCount2 = 0;
        AncientAttack4A5();
        Invoke("AncientAttack4A", 1.5f);
    }
    void AncientAttack4A5()
    {
        if (SkillCount2 < 100)
        {
            ETC[17].transform.position = new Vector2(ETC[17].transform.position.x + ETCFlat[1], ETC[17].transform.position.y);
            Invoke("AncientAttack4A5", 0.01f);
            SkillCount2 += 1;
        }
        else
        {
            ETCFlat[1] = (72 - ETC[17].transform.position.x) / 100;
            SkillCount2 = 0;
            Invoke("AncientAttack4A6", 1f);
        }
    }
    void AncientAttack4A()
    {
        for (int i = 0; i < 5; i++)
        {
            Monster3[i].SetActive(true);
            Monster3[i].transform.position = new Vector2(ETC[19].transform.position.x, -25);
            Monster3[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster3[i].transform.localScale = new Vector2(0.25f, 0.25f);
            Monster3[i].GetComponent<EnemySkill>().Go(-80 + (i) * 40);
            Monster3[i].GetComponent<EnemySkill>().Time1 = 50;
            Monster3[i].GetComponent<SpriteRenderer>().color = new Color(0, 1, 1);
            Monster3[i].GetComponent<EnemySkill>().circle.GetComponent<TrailRenderer>().time = 0;
        }
    }
    void AncientAttack4A6()
    {
        if (SkillCount2 < 100)
        {
            ETC[17].transform.position = new Vector2(ETC[17].transform.position.x + ETCFlat[1], ETC[17].transform.position.y);
            Invoke("AncientAttack4A6", 0.01f);
            SkillCount2 += 1;
        }
    }
    void BlenderGo1()
    {
        ShakeCamera(10, 10);
        IsAttack();
        for (int i = 20; i <= 29; i++)
        {
            Monster3[i].SetActive(true);
            Monster3[i].transform.position = Playertrans.position + new Vector3(Random.Range(42, 82)-62, 15 + Random.Range(10, 20),1);
        }
    }
    void BlenderGo2()
    {
        ShakeCamera(10, 10);
        IsAttack();
        for (int i = 10; i <= 19; i++)
        {
            Monster3[i].SetActive(true);
            Monster3[i].transform.position = Playertrans.position + new Vector3(Random.Range(42, 82)-62, 15 + Random.Range(10,20),1);
        }
    }
    void BlenderGo3()
    {
        ShakeCamera(10, 10);
        IsAttack();
        for (int i = 30; i <= 49; i++)
        {
            Monster3[i].SetActive(true);
            Monster3[i].transform.position = Playertrans.position + new Vector3(Random.Range(42, 82) - 62, 15 + Random.Range(10, 20), 1);
        }
    }
    void isDRAKSkill()
    {
        int dirc = transform.position.x - Playertrans.position.x > 0 ? -1 : 1;
        float dirc1 = transform.position.x + SkillCount3 * 10;
        if (dirc1 < 35)
        {
            dirc1 = 35;
        }
        if (dirc1 > 80)
        {
            dirc1 = 80;
        }
        transform.position = new Vector2(dirc1, transform.position.y);
        //IsSkillAttack();
    }
    void isDRAKSkill1()
    {
        for (int i = 0; i < 3; i++)
        {
            Monster1[i].SetActive(true);
            Monster1[i].transform.position = ETC[5].transform.position;
            Monster1[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Monster1[i].GetComponent<EnemySkill>().speed = 10;
            Monster1[i].GetComponent<EnemySkill>().Udo2(-20 + i * 20);
            Monster1[i].GetComponent<EnemySkill>().Time1 = 150;
        }
    }
    void isDRAKSkill2()
    {
        Monster1[SkillCount2 + 10].SetActive(true);
        Monster1[SkillCount2 + 10].transform.position = ETC[0].transform.position;
        Monster1[SkillCount2 + 10].GetComponent<EnemySkill>().speed = 20;
        Monster1[SkillCount2 + 10].GetComponent<EnemySkill>().Udo();
        Monster1[SkillCount2 + 10].GetComponent<EnemySkill>().Time1 = 50;

        Monster1[SkillCount2+5].SetActive(true);
        Monster1[SkillCount2+5].transform.position = ETC[SkillCount2 + 5].transform.position;
        Monster1[SkillCount2+5].GetComponent<EnemySkill>().speed = 10;
        Monster1[SkillCount2+5].GetComponent<EnemySkill>().Udo();
        Monster1[SkillCount2+5].GetComponent<EnemySkill>().Time1 = 300;
        SkillCount2 += 1;
    }
    void isISkill()
    {
        SkillCount = 0;
        SkillCount2 = transform.position.x - Playertrans.position.x > 0 ? -1 : 1;
        isISkillA();
        isflying = true;
        Invoke("isISkillB", 1.5f);
    }
    void isISkillA()
    {
        if (SkillCount < 150)
        {
            transform.position = new Vector2(transform.position.x + SkillCount2*0.1f, transform.position.y);
            Invoke("isISkillA", 0.01f);
        }
    }
    void isISkillB()
    {
        SkillCount = 150;
        isflying = false;
    }
    void isISkil2()
    {
        Monster1[0].SetActive(true);
        Monster1[0].transform.position = ETC[0].transform.position;
        Monster1[0].GetComponent<EnemySkill>().speed = 20;
        Monster1[0].GetComponent<EnemySkill>().Udo();
        Monster1[0].GetComponent<EnemySkill>().Time1 = 10;
    }
    void isISkil2A()
    {
        Monster1[0].SetActive(true);
        Monster1[0].transform.position = ETC[0].transform.position;
        Monster1[0].GetComponent<EnemySkill>().speed = 20;
        Monster1[0].GetComponent<EnemySkill>().Udo();
        Monster1[0].GetComponent<EnemySkill>().Time1 = 10;

    }
    void isISkil2B()
    {
        spriteRenderer.sprite = BossSprite[6];
        Invoke("isISkil2C", 1f);
    }
    void isISkil2C()
    {
        spriteRenderer.sprite = BossSprite[5];
    }
    void IDie1() //이시도르 부활
    {
        SkillCount3 += 1;
        spriteRenderer.sprite = BossSprite[SkillCount3];
        if (SkillCount3<5)
            Invoke("IDie1", 0.1f);
    }
    void IAngry1() //이시도르
    {
        Hp = FullHp;
        AttackDelay = 0.83f;
        AttackEndDelay = 2.5f;
        Angry();
        GameObject.Find("Manager").GetComponent<Manager>().audiosource[2].Play();
    }
}
