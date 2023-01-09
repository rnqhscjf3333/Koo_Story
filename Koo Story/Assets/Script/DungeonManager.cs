using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; //시네마신
using Light2DE = UnityEngine.Experimental.Rendering.Universal.Light2D;//라이트
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject manager;
    public GameObject NPC;
    public AudioClip[] clip;
    public int Dungeon = 1; //던전 진행상황

    public GameObject[] Monster1;
    public GameObject[] Monster2;
    public GameObject[] Monster3;

    public PolygonCollider2D Bossconfiner;

    public GameObject Boss;
    public Transform BossTrans;
    public GameObject BossFront;
    public GameObject HP;

    public CinemachineVirtualCamera Follower;
    public GameObject Group;
    public GameObject Wall;

    public string BossName;

    public bool[] isBoss; //보스중복등장방지

    public ParticleSystem[] particle;//파티클

    public GameObject[] Back; //배경

    public GameObject[] Tile; //타일

    public AudioSource[] audiosource;

    public CinemachineVirtualCamera CMCamera;
    public float ShakeTime;

    float shakepower;
    float shaketime;
    public float t;

    public GameObject FakePlayer;
    public GameObject FakeSword;
    public GameObject FakeArmor;
    public GameObject FakeSword1;


    void Awake()
    {
        if (BossTrans == null)
            BossTrans = Boss.transform;
        if (Boss != null)
            Boss.SetActive(false);
    }

    void Update()
    {
        if (ShakeTime > 0)
        {
            ShakeTime -= 1;
            if (ShakeTime == 0 && CMCamera != null)
                CMCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;

        }
    }


    public void DungeonEnter(int num)
    {
        Dungeon = num;
        if(num == 1)
        {
            for (int i = 0; i < Monster1.Length; i++)
            {
                if(Monster1[i] != null)
                {
                    Monster1[i].SetActive(true);
                    Monster1[i].transform.position = Monster1[i].GetComponent<Enemy>().thistrans.position;
                }
            }
            for (int i = 0; i < Monster2.Length; i++)
            {
                if (Monster2[i] != null)
                    Monster2[i].SetActive(false);
            }

            for (int i = 0; i < Monster3.Length; i++)
                if (Monster3[i] != null)
                    Monster3[i].SetActive(false);
        }
        if (num == 2)
        {
            for (int i = 0; i < Monster1.Length; i++)
                if (Monster1[i] != null)
                    Monster1[i].SetActive(false);
            for (int i = 0; i < Monster2.Length; i++)
            {
                if (Monster2[i] != null)
                {
                    Monster2[i].SetActive(true);
                    Monster2[i].transform.position = Monster2[i].GetComponent<Enemy>().thistrans.position;
                }
            }
            for (int i = 0; i < Monster3.Length; i++)
                if (Monster3[i] != null)
                    Monster3[i].SetActive(false);
            if(BossName == "용들어온다" && GameObject.Find("QuestManager").GetComponent<QuestManager>().Quest[0] == 20)
            {
                Player.GetComponent<Player>().alive = 0;
                Boss.SetActive(true);
                Invoke("BreakTile", 9.8f);
            }
            else if (BossName == "용들어온다")
            {
                Tile[0].SetActive(false);
                Tile[1].SetActive(false);
                Tile[3].SetActive(false);
            }
            if (BossName == "용들어온다2" && GameObject.Find("QuestManager").GetComponent<QuestManager>().Quest[0] == 28)
            {
                manager.GetComponent<Manager>().Fade = 1f;
                Invoke("Dragon2Start", 0.5f);
                Player.GetComponent<Player>().alive = 0;
            }

            if (BossName == "마야온다" && GameObject.Find("QuestManager").GetComponent<QuestManager>().Quest[0] == 52)
            {
                manager.GetComponent<Manager>().Fade = 1f;
                Invoke("CatleStart", 1f);
                Player.GetComponent<Player>().alive = 0;
            }
            else if (BossName == "마야온다")
            {
                manager.GetComponent<Manager>().Fade = 1f;
                SceneManager.LoadScene("CatleEnd1");
                Player.GetComponent<Player>().alive = 0;
            }

            if (BossName == "마왕온다")
            {
                manager.GetComponent<Manager>().Fade = 1f;
                Invoke("CatleStart", 1f);
                Player.GetComponent<Player>().alive = 0;
            }

        }
        if (num == 3)
        {
            for (int i = 0; i < Monster1.Length; i++)
                if (Monster1[i] != null)
                    Monster1[i].SetActive(false);
            for (int i = 0; i < Monster2.Length; i++)
                if (Monster2[i] != null)
                    Monster2[i].SetActive(false);
            for (int i = 0; i < Monster3.Length; i++)
            {
                if (Monster3[i] != null)
                {
                    Monster3[i].SetActive(true);
                    Monster3[i].transform.position = Monster3[i].GetComponent<Enemy>().thistrans.position;
                }
            }
        }
        if (num == 4 && isBoss[4] == false)
        {
            audiosource[0].Stop();
            isBoss[4] = true;
            manager.GetComponent<Manager>().Fade = 1;
            Invoke("GoToBoss", 1f);
            
        }
        if (num == 5 && isBoss[5] == false)
        {
            Boss.SetActive(true);
            HP.SetActive(true);
            Follower.Follow = BossTrans.transform;

            manager.GetComponent<Manager>().CMconfiner.m_BoundingShape2D = Bossconfiner;

            Player.GetComponent<Player>().alive = 0;
            Invoke("BossCome", 3);
            isBoss[5] = true;
            GameManager.Instance.StartPoint = 2;  
        }
        if (num == 6 && isBoss[6] == false) //늑대아이 등장
        {
            Follower.Follow = Group.transform;
            isBoss[6] = true;
            Player.GetComponent<Player>().alive = 0;
            BossFront.GetComponent<NPC>().TalkNPC();
        }
    }
    void Dragon2Start()
    {
        manager.GetComponent<Manager>().Fade = -1f;
        Boss.SetActive(true);
        FakePlayer.SetActive(true);
        Player.SetActive(false);
        FakeInven();
        Invoke("Dragon2", 7f);
        Invoke("FakePlayerAttack", 10.2f);
        Invoke("FakePlayerDie", 13f);
        Invoke("Dragon2End", 22f);
        Follower.Follow = BossTrans.transform;
    }
    void Dragon2()
    {
        ShakeCamera(10, 30);
    }
    void FakePlayerAttack()
    {
        FakePlayer.GetComponent<Animator>().SetTrigger("isAttack");
        FakeSword.GetComponent<Animator>().SetTrigger("isAttack");
    }
    void FakePlayerDie()
    {
        FakePlayer.GetComponent<Animator>().SetTrigger("isDown");
    }
    void Dragon2End()
    {
        manager.GetComponent<Manager>().Fade = 0.1f;
        Invoke("GoTostarPoint2", 3f);
    }
    void GoTostarPoint2()
    {
        Follower.Follow = Player.transform;
        Player.SetActive(true);
        Player.transform.position = Player.GetComponent<Player>().StartPoint[1].position;
        manager.GetComponent<Manager>().Fade = -0.5f;
        manager.GetComponent<Manager>().Naration2("정신을 차리니 또 후드를 쓴 소녀가 눈 앞에 있다.");
        Player.GetComponent<Player>().alive = 1;
    }
    void BreakTile()
    {
        ShakeCamera(10, 30);
        Tile[0].SetActive(false);
        Tile[1].SetActive(false);
        //Tile[0].GetComponent<Animator>().SetTrigger("isBreak");
        //Tile[0].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //Tile[1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        manager.GetComponent<Manager>().Fade = 0.5f;
        Invoke("Static", 3f);
    }
    void Static()
    {
        Player.transform.position = new Vector3(-2f, Player.transform.position.y, 1f);
        manager.GetComponent<Manager>().Fade = -0.1f;
        Tile[2].SetActive(true);
        Invoke("StopParticle", 8f);
    }
    void StopParticle()
    {
        particle[0].Stop();
        manager.GetComponent<Manager>().Fade = 0.1f;
        Invoke("GoToCliff2", 3f);
    }
    void GoToCliff2()
    {
        Player.transform.position = Player.GetComponent<Player>().StartPoint[1].position;
        manager.GetComponent<Manager>().Fade = -0.5f;
        manager.GetComponent<Manager>().Naration2("정신을 차리니 후드를 쓴 소녀가 눈 앞에 있다.");
        Player.GetComponent<Player>().alive = 1;
    }

    void GoToBoss()
    {
        manager.GetComponent<Manager>().Fade = -1;
        for (int i = 0; i < Monster1.Length; i++)
            if (Monster1[i] != null)
                Monster1[i].SetActive(false);
        for (int i = 0; i < Monster2.Length; i++)
            if (Monster2[i] != null)
                Monster2[i].SetActive(false);
        for (int i = 0; i < Monster3.Length; i++)
            if (Monster3[i] != null)
                Monster3[i].SetActive(false);
        

        Player.transform.position = Player.GetComponent<Player>().StartPoint[1].position;
        manager.GetComponent<Manager>().CMconfiner.m_BoundingShape2D = Bossconfiner;
    }
    void BossCome()
    {
        audiosource[1].Play();
        Follower.Follow = Group.transform;
        Boss.GetComponent<Boss1>().Angry();
        Player.GetComponent<Player>().alive = 1;
        manager.GetComponent<Manager>().Naration2(BossName);
    }

    public void BossDie()
    {
        HP.SetActive(false);
        Wall.SetActive(false);
        Follower.Follow = Player.transform;
        audiosource[1].Stop();
        Time.timeScale = 0.5f;
        Invoke("Time1", 1f);
    }

    public void TalkEnd(int num) //늑대아이와의 대화 끝
    {
        if(num == 0) //늑대아이
        {
            BossFront.GetComponent<Animator>().SetBool("isStand",true);
            DungeonEnter(5);
            Invoke("StartEffect", 2);
        }

        if (num == 1) //바다
        {
            audiosource[0].Stop();
            SoundManager.instance.SFXPlay("BossHit", clip[0]);
            particle[0].Play();
            particle[1].Play();
            Invoke("SeaDungeonEnter", 5);
            Tile[0].SetActive(false);
            NPC.layer = 9;
            NPC.GetComponent<NPC>().TalkingNPC("자! 출항한다! 싸울 준비하라고!",5);

            foreach (GameObject i in Back)
                i.GetComponent<BackGround>().isBackMove = true;
        }
        if (num == 2) //바다
        {
            audiosource[0].Stop();
            SoundManager.instance.SFXPlay("BossHit", clip[0]);
            particle[0].Play();
            particle[1].Play();
            Invoke("DeadSeaDungeonEnter", 5);
            Tile[0].SetActive(false);
            NPC.layer = 0;
            NPC.GetComponent<NPC>().TalkingNPC("자! 출항한다! 전방에 안개다!",5);

            foreach (GameObject i in Back)
                i.GetComponent<BackGround>().isBackMove = true;
            t = 200;
            Light();
        }
        if (num == 3) //드래곤
        {
            audiosource[0].Stop();
            NPC.GetComponent<Animator>().enabled = true;
            Invoke("DragonDungeonEnter", 5);
            Tile[0].SetActive(false);
            NPC.layer = 0;
            NPC.GetComponent<NPC>().TalkingNPC("갈게! 준비하고 있어!", 2);
        }
        if (num == 4) //고대
        {
            audiosource[0].Stop();
            Invoke("AncientDungeonEnter", 1);
            Tile[0].SetActive(false);
            NPC.layer = 0;
            NPC.GetComponent<NPC>().TalkingNPC("치지직... 적으로 간주하고 방어 시스템을 가동합니다.", 2);
        }
        if (num == 5) //시르케
        {
            BossFront.GetComponent<Animator>().SetBool("isStand", true);
            Invoke("WizardDungeonEnter", 2);
        }
        if (num == 6) //마야
        {
            Boss.GetComponent<Animator>().SetBool("isStand", true);
            Invoke("MayaEnd", 32);
            Player.GetComponent<Player>().alive = 0;
        }
        if (num == 7) //스태락1
        {
            BossFront.GetComponent<Animator>().SetTrigger("isStand");
            Player.GetComponent<Player>().alive = 0;
            Invoke("StarakEnter", 4);
        }
        if (num == 8) //마왕1
        {
            if(GameManager.Instance.Armornum == 8)
            {
                Boss.GetComponent<Animator>().SetTrigger("isStand1");
                Invoke("DevilEnd1", 9);
            }
            else
            {
                Boss.GetComponent<Animator>().SetTrigger("isStand");
                Invoke("DevilEnd", 25);
            }
            Player.GetComponent<Player>().alive = 0;
        }
    }
    void Light()
    {
        if(t > 20)
        {
            t -= 1;
            Tile[1].GetComponent<Light2DE>().pointLightOuterRadius= t;
            Invoke("Light", 0.02f);
        }

    }


    void StartEffect()
    {
        Boss.GetComponent<Boss1>().Effect.GetComponent<Animator>().SetTrigger("isEffect");
    }

    void SeaDungeonEnter()
    {
        NPC.GetComponent<NPC>().TalkingNPC("녀석이 왔다!",5);
        isBoss[5] = true;
        Boss.SetActive(true);
        HP.SetActive(true);
        Follower.Follow = Group.transform;
        Invoke("Music1", 5f);
    }

    void DeadSeaDungeonEnter()
    {
        NPC.GetComponent<NPC>().TalkingNPC("유령선이다!!",5);
        isBoss[5] = true;
        Boss.SetActive(true);
        HP.SetActive(true);
        Follower.Follow = Group.transform;
        Invoke("Music1", 5f);
    }
    void DragonDungeonEnter()
    {
        isBoss[5] = true;
        Boss.SetActive(true);
        HP.SetActive(true);
        Follower.Follow = Group.transform;
        Invoke("Music1", 5f);
    }
    void AncientDungeonEnter()
    {
        Back[0].SetActive(false);
        isBoss[5] = true;
        Boss.SetActive(true);
        HP.SetActive(true);
        Follower.Follow = Group.transform;
        Invoke("Music1", 5f);
    }

    void Music1()
    {
        audiosource[1].Play();
    }
    void Time1()
    {
        if (Time.timeScale < 1f)
        {
            Time.timeScale += 0.1f;
            Invoke("Time1", 0.1f);
        }
    }
    public void ShakeCamera(float intensity, float time)
    {
        CMCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        ShakeTime = time;
    }
    public void FakeInven()
    {
        FakeArmor.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.ArmorImage[GameManager.Instance.Armornum];
        FakeSword.GetComponent<Animator>().SetFloat("SwordBlend", GameManager.Instance.Swordnum);
    }
    public void FakeArmor1()
    {
        FakeArmor.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.ArmorImage[10];
    }
    public void FakeSworda()
    {
        FakeSword1.GetComponent<Animator>().SetFloat("SwordBlend", GameManager.Instance.Swordnum);
    }
    void WizardDungeonEnter()
    {
        BossFront.SetActive(false);
        DungeonEnter(5);
    }

    void CatleStart()
    {
        manager.GetComponent<Manager>().Fade = -1f;
        Boss.SetActive(true);
        FakePlayer.SetActive(true);
        Player.transform.position = new Vector3(-11, -100, 0);
        FakeInven();
        Follower.Follow = Group.transform;
        BossFront.GetComponent<NPC>().TalkNPC();
    }
    void MayaEnd()
    {
        GameManager.Instance.Quest[0] = 53;
        Tile[0].SetActive(true);
    }
    void StarakEnter()
    {
        BossFront.SetActive(false);
        DungeonEnter(5);
    }
    void DevilEnd()
    {
        GameManager.Instance.Quest[0] = 54;
        Tile[0].SetActive(true);
    }
    void DevilEnd1()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().Armorhave[9] = 1;
        GameObject.Find("GameManager").GetComponent<GameManager>().Armornum = 9;
        GameObject.Find("Player").GetComponent<Player>().armornum = 9;
        GameObject.Find("Player").GetComponent<Player>().ArmorInven(9);
        manager.GetComponent<Manager>().Naration2("새로운 갑옷을 얻었다.[힘을 얻은 마왕의 갑옷]");
        Invoke("DevilEnd2", 3);
    }
    void DevilEnd2()
    {
        Tile[0].SetActive(true);
    }
}
