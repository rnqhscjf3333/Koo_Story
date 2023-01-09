using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine; //시네마신
using TMPro;

public class Manager : MonoBehaviour
{

    public string PointName;
    public string MapName; //로드할 맵 이름
    public PolygonCollider2D[] confiner; //컨파이너 개수

    public CinemachineConfiner CMconfiner; //컨파이너

    public GameObject Dungeon; //던전매니저

    public GameObject Player;
    public GameManager gamemanager;
    public int PlayerGold;
    public Text GoldText;

    public GameObject Naration;
    public GameObject NPCChat;
    public Text NarationText;

    public int isNar; //나레이션중인지

    public GameObject DieNaration;
    public bool isDie = false;

    public int[] Swordhave; //0:나무검 1:녹슨검 2:도끼 3:창
    public int[] SwordSale; //가격
    public int[] SwordPower; //공격력
    public int[] Armorhave; // 0:나무갑옷 1:녹슨갑옷
    public int[] ArmorSale; //가격
    public int[] ArmorDefense; //방어력

    public int[] Skillhave; // 0:검기날리기 1:강타
    public int[] SkillSale; //가격
    public int[] Itemhave; // 0:촌장집키 1: 지하열쇠
    public int[] ItemSale; //가격

    public string[] SwordText;
    public string[] ArmorText;
    public string[] SkillText;
    public string[] ItemText;
    public Text Itemtext;
    public Text Inventext;
    public int Itemnum;
    public int Swordnum;
    public int Armornum;

    public Image[] SwordSprite;
    public Image[] ArmorSprite;
    public Image[] SkillSprite;
    public Image[] ItemSprite;

    public Image Black;
    public float Fade; //1이면 페이드아웃, -1이면 인
    public float Fade2; //블랙 색

    public GameObject[] NPC;
    public GameObject[] StartPoint;
    public int[] NPCCount;


    public TextMeshPro text;
    public GameObject quad;

    public CinemachineVirtualCamera Follower;

    public GameObject Pause;
    public bool isPause;

    public GameObject[] InvenObject;

    public string sentence1;

    Player player;
    public bool isNotNaration;
    public bool isNotClick;

    public AudioSource[] audiosource;


    private void Awake()
    {
        NPCCount = new int[10];
        SwordPower[0] = 5;
        SwordText[0] = "연습용 목검(공격력+5)\n" +
            "주먹으로 치는 것보다 조금 더 아프다";

        SwordPower[1] = 10;
        SwordText[1] = "몽둥이(공격력+10)\n" +
            "역사적으로 이게 약이었다";
        SwordSale[1] = 300;

        SwordPower[2] = 20;
        SwordText[2] = "오래된 도끼(공격력+20)\n" +
            "나무를 베는 데에 더 적합한 도끼";
        SwordSale[2] = 1000;

        SwordPower[3] = 25;//창
        SwordText[3] = "늑대아이의 창(공격력+25)\n" +
            "수많은 늑대들의 피가 스며들어 있다";//창

        SwordPower[4] = 50;//이름없는 기사의 검
        SwordText[4] = "이름없는 기사의 검(공격력+50)\n" +
            "용족과의 전쟁 당시에 쓰였던 검이다";
        SwordSale[4] = 3000;

        SwordPower[5] = 75;
        SwordText[5] = "정열의 검(공격력+75)\n" +
            "정열을 원하는 사람에게 추천";
        SwordSale[5] = 5000;

        SwordPower[6] = 200;
        SwordText[6] = "황금의 검(공격력+200)\n" +
            "돈으로 강함을 사고싶은 자에게 추천";
        SwordSale[6] = 30000;

        SwordPower[7] = 100;
        SwordText[7] = "이집트 노예의 한이 서린 곡괭이(공격력+100)" +
            "";

        SwordPower[8] = 100;//마검1
        SwordText[8] = "힘을 잃은 마왕의 검(공격력+100)\n" +
            "힘이 느껴지지 않는다.";

        SwordPower[9] = 150;//마검2
        SwordText[9] = "정화된 마왕의 검(공격력+150)\n" +
            "강력한 힘이 느껴진다.";

        ArmorDefense[0] = 5;
        ArmorText[0] = "맨몸(방어력+0)";

        ArmorDefense[1] = 10;
        ArmorText[1] = "나무갑옷(방어력+10)\n" +
            "없는 것보단 낫다";
        ArmorSale[1] = 200;

        ArmorDefense[2] = 20;
        ArmorText[2] = "녹슨 갑옷(방어력+20)";
        ArmorSale[2] = 500;

        ArmorDefense[3] = 40;
        ArmorText[3] = "이름없는 기사의 갑옷(방어력+40)\n" +
            "용족과의 전쟁 당시에 쓰였던 갑옷이다.";
        ArmorSale[3] = 2000;

        ArmorDefense[4] = 50;
        ArmorText[4] = "정열의 옷(방어력+50)\n" +
            "정열을 원하는 사람에게 추천";
        ArmorSale[4] = 4000;

        ArmorDefense[5] = 80;
        ArmorText[5] = "트래져 헌터 최우수 회원복(방어력+80)\n" +
            "진정한 트래져 헌터에게 수여합니다.-뉴 트래져 헌터협회 회장 벤 게이츠";

        ArmorDefense[6] = 90;
        ArmorText[6] = "황금의 갑옷(방어력+90)\n" +
            "돈을 자랑하고 싶은 사람에게 추천";
        ArmorSale[6] = 20000;

        ArmorDefense[7] = 95;
        ArmorText[7] = "광전사의 갑옷(방어력+95)\n" +
            "슬픔이 느껴진다.";
        ArmorDefense[8] = 0;
        ArmorText[8] = "힘을 잃은 마왕의 갑옷(방어력+0)\n" +
            "힘이 거의 느껴지지 않는다.";

        ArmorDefense[9] = 80;
        ArmorText[9] = "힘을 얻은 마왕의 갑옷(방어력+80)\n" +
            "힘이 넘치는 것이 느껴진다.";

        SkillText[1] = "50이상의 기력을 사용, 기력에 비례해서 데미지를 준다. 최대 데미지 : 공격력의 2배 (사용 : C키)";
        SkillSale[1] = 1000;
        SkillText[2] = "기력 100을 사용하여 2초간 정신집중 후 공격력의 5배의 데미지를 준다. (사용 : V키)";
        SkillSale[2] = 3000;

        ItemText[3] = "드락사르의 검조각이다.";
        ItemText[4] = "특이한 문양이 새겨진 부적이다.";

        GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Swordnum = gamemanager.Swordnum; //동기화
        Armornum = gamemanager.Armornum;
        PlayerGold = gamemanager.Gold;
        if (PointName != "" && !isNotNaration)
            Naration2(PointName);
        Swordhave = gamemanager.Swordhave;
        Armorhave = gamemanager.Armorhave;
        Skillhave = gamemanager.Skillhave;
        Itemhave = gamemanager.Key;

        if (Player != null)
        {
            player = Player.GetComponent<Player>();
            player.ArmorInven(Armornum);
            player.Defense = ArmorDefense[Armornum];
            player.SwordInven(Swordnum);
            player.Power = SwordPower[Swordnum];
        }

        if (gamemanager.Quest[0] == 11 && PointName == "드엔 마을")
        {
            Player.transform.position = StartPoint[0].transform.position;
            player.alive = 0;
            NPCCount[0] = 1;
            CMRA(4);
        }

        if (gamemanager.Quest[0] == 50 && PointName == "트타스 마을")
        {
            Player.transform.position = StartPoint[0].transform.position;
            player.alive = 0;
            NPCCount[1] = 1;
            CMRA(4);
        }

        if (gamemanager.Quest[0] == 54 && PointName == "최후의 탑")
        {
            Player.transform.position = StartPoint[0].transform.position;
            player.alive = 0;
            NPCCount[2] = 1;
        }
        else if (PointName == "최후의 탑")
        {
            NPC[0].SetActive(false);
        }
        if (PointName == "최후의 탑1" && gamemanager.BlenderDieCount == 3)
        {
            Player.SetActive(false);
            NPCCount[3] = 1;
            Dungeon.GetComponent<DungeonManager>().FakeInven();
        }
        if (PointName == "최후의 탑1" && gamemanager.BlenderDieCount == 2)
        {
            Player.SetActive(false);
            NPCCount[4] = 1;
            Dungeon.GetComponent<DungeonManager>().FakeInven();
            NPC[0].GetComponent<Animator>().SetTrigger("200");
            Dungeon.GetComponent<DungeonManager>().FakeArmor1();
            Dungeon.GetComponent<DungeonManager>().FakeSworda();
        }
        if (PointName == "천공의 성 중심" && gamemanager.Quest[8] == 0)
        {
            NPCCount[5] = 1;
            player.alive = 0;
        }
        if (PointName == "천공의 성 중심" && gamemanager.Quest[8] == 1)
        {
            NPC[0].GetComponent<Animator>().SetTrigger("D1");
            Invoke("anyway", 3);
            player.alive = 0;
        }
    }

    void anyway()
    {
        NPCCount[5] = 100;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//일시정지
        {
            isPause = !isPause;
            Pause.SetActive(isPause);
            if (isPause)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

        if(NPCCount[0] > 0)
        {
            GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

            if (NPCCount[0] == 1)
            {
                Follower.Follow = NPC[0].transform;
                NPCTalking(0, "다이스!");
            }
            else if (NPCCount[0] == 2)
                NPCTalking(0, "인사도 안하고 가려고 하면 어떡해!");
            else if (NPCCount[0] == 3)
                NPCTalking(0, "모두 너에게 인사하기 위해서 모인거야~");
            else if (NPCCount[0] == 4 && gamemanager.Quest[3] >= 3)
            {
                Follower.Follow = NPC[1].transform;
                NPCTalking(1, "다이스. 생각해보니 저번에 너에게\n 감사인사를 못했구나. 고맙다.\n 촌장으로서 부끄러운 일이지만 모든\n 사실을 마을 사람들에게도 말해줬단다.\n 내 아들에게도 미안하구나."); //촌장
            }
            else if (NPCCount[0] == 4)
            {
                Follower.Follow = NPC[1].transform;
                NPCTalking(1, "다이스! 어쩌면 너가 드락사르를 이어서 드엔 마을의 자랑거리가 될 수도 \n있겠구나!");
            }
            else if (NPCCount[0] == 5 && gamemanager.Quest[2] == 3)
            {
                Follower.Follow = NPC[2].transform;
                NPCTalking(2, "고마워... \n나도 언젠가 형처럼 강해질 수 있겠지?");//상점아들
            }
            else if (NPCCount[0] == 5)
            {
                Follower.Follow = NPC[2].transform;
                NPCTalking(2, "... ");
            }
            else if (NPCCount[0] == 6 && gamemanager.Quest[2] == 3)
            {
                Follower.Follow = NPC[3].transform;
                NPCTalking(3, "내 아들을 도와줬다며? \n아들이 다 설명해줬단다. 고맙다!");//상점
            }
            else if (NPCCount[0] == 6)
            {
                Follower.Follow = NPC[3].transform;
                NPCTalking(3, "이런... 너가 고물들을 사주는 유일한\n 호갱... 아니 고객이었는데 이제는\n 안오겠구나. 아쉽네...");
            }
            else if (NPCCount[0] == 7 )
            {
                Follower.Follow = NPC[4].transform;
                NPCTalking(4, "너 덕분에 내 꿈을 이룰 수 있게 되었어! 정말 고마워!");//강태공
            }
            else if (NPCCount[0] == 8)
            {
                Follower.Follow = NPC[0].transform;
                NPCTalking(0, "대륙으로 가도 우리를 잊지 말고 가끔\n 마을에 들려~");
            }
            else if (NPCCount[0] == 9)
                NPCTalking(0, "그리고.. 마지막으로 할 말이 있으니까 \n마을 사람들이 간 후에 와 줘.");
            if (NPCCount[0] == 10)
            {
                gamemanager.Quest[0] = 12;
                Fade = 1;
                Invoke("PlayerDie1", 1f);
            }
            if (Input.GetButtonDown("Fire1"))
            {
                NPCCount[0] += 1;
                SoundManager.instance.Click();
            }
        }

        if (NPCCount[1] > 0)
        {
            GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

            if (NPCCount[1] == 1)
                NPCTalking(1, "다이스. 저주가 사라지고 있어. \n성공한 모양이네?");
            else if (NPCCount[1] == 2)
                NPCTalking(1, "너가 간 사이에 손님이 한분 오셨어.");
            else if (NPCCount[1] == 3)
                NPCTalking(0, "다이스! 오랫만이야!");
            else if (NPCCount[1] == 4)
                NPCTalking(0, "왜 왔냐고?? ... \n생각보다 반갑지는 않나 보네.");
            else if (NPCCount[1] == 5)
                NPCTalking(1, "미안하지만 다이스는 너보다 신경 써야 할 게 많아서 말이야.");
            else if (NPCCount[1] == 6)
                NPCTalking(1, "이제 곧 사악한 국왕을 죽이고 새로운 \n영웅이 될 예정이거든.");
            else if (NPCCount[1] == 7)
                NPCTalking(0, "... 불길한 예감이 적중했구나... \n사실은 그거 때문에 여기까지 온 거야.");
            else if (NPCCount[1] == 8)
                NPCTalking(0, "이미 무슨 일인지는 대충 알고 왔어. \n이제 멈춰주면 안될까? \n뒷일은 내가 어떻게든 해 볼게...");
            else if (NPCCount[1] == 9)
                NPCTalking(1, "후후후.. 그 말을 믿는 건 아니겠지? \n다이스?");
            else if (NPCCount[1] == 10)
                NPCTalking(1, "곧 스태락은 정신이 잠식당해서 마왕의 \n꼭두각시가 될 거야. 그걸 막을 수 있는 \n사람은 너밖에 없어 다이스!");
            else if (NPCCount[1] == 11)
                NPCTalking(0, "다이스! 친누나는 아니지만 어릴 때부터 너와 지낸 누나로서 부탁할게! \n제발 그만둬...");
            else if (NPCCount[1] == 12)
                NPCTalking(1, "후후후,,,");
            else if (NPCCount[1] == 13)
                NPCTalking(1, "너는 아직도 진실을 숨기고 있어. 마야.");
            else if (NPCCount[1] == 14)
                NPCTalking(0, "어떻게 내 이름을...?");
            else if (NPCCount[1] == 15)
                NPCTalking(1, "난 모든 걸 알고 있어. 물론...");
            else if (NPCCount[1] == 16)
                NPCTalking(1, "너가 드락사르의 딸이라는 것까지!");
            else if (NPCCount[1] == 17)
                NPCTalking(0, "!!!!");
            else if (NPCCount[1] == 11)
                NPCTalking(0, "그걸 어떻게?");
            else if (NPCCount[1] == 18)
                NPCTalking(1, "다이스! 지금까지 너와 자라면서 자신의 정체마저도 밝히지 않은 사람을\n 믿을 생각은 아니겠지?");
            else if (NPCCount[1] == 19)
                NPCTalking(1, "마야. 너의 생각은 뻔해. 왕국으로 가서 \n스태락의 바짓가랑이를 붙잡고 정신을 \n잃지 말라고 징징거리는 게 너의 \n최선이겠지.");
            else if (NPCCount[1] == 20)
                NPCTalking(1, "과연 스태락이 너의 말을 듣고 정신을 \n차릴까? 이제는 원수나 다름없는 \n드락사르의 자식의 이야기를?");
            else if (NPCCount[1] == 21)
                NPCTalking(1, "스태락은 평생 드락사르에게 열등감이 \n있었지. 그리고 마지막 결투에서도 \n결국 드락사르를 이기지 못했어.");
            else if (NPCCount[1] == 22)
                NPCTalking(1, "마야. 나는 너와 달라. 나는 모든 것을 \n알고 있어. 그래서 너의 바람이 \n헛된 것이라는 것도 알고 있지.");
            else if (NPCCount[1] == 23)
                NPCTalking(1, "다이스! 이제 선택할 시간이야... 마야를 믿고 기다릴지...  아니면 너가 직접 이 \n긴 비극의 종지부를 찍을 지...");
            else if (NPCCount[1] == 24)
                NPCTalking(1, "만약 너의 손으로 스태락을 죽인다면 \n감사의 표시로 너에게 내가 알고 있는 \n모든 것을 알려주겠어.");
            else if (NPCCount[1] == 25)
                NPCTalking(0, "다이스... 제발...");
            else if (NPCCount[1] == 26)
                NPCTalking(1, "...");
            else if (NPCCount[1] == 27)
            {
                NPCTalking(0, "...!");
                PlayerMove();
            }
            else if (NPCCount[1] == 28)
                NPCTalking(0, "다이스!!");
            else if (NPCCount[1] == 29)
            {
                NPCTalking(1, "...");
                NPC[1].transform.localScale = new Vector2(-1,1);
            }

            if (NPCCount[1] == 30)
            {
                gamemanager.Quest[0] = 51;
                Fade = 1;
                Invoke("PlayerDie1", 1f);
            }
            if (Input.GetButtonDown("Fire1"))
            {
                NPCCount[1] += 1;
                SoundManager.instance.Click();
            }
        }

        if (NPCCount[2] > 0)
        {
            GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

            if (NPCCount[2] == 1)
            {
                Dungeon.GetComponent<DungeonManager>().audiosource[0].Play();
                NPCTalking(0, "왔구나..");
            }
            else if (NPCCount[2] == 2)
                NPCTalking(0, "여기가 어딘지 알아?");
            else if (NPCCount[2] == 3)
                NPCTalking(0, "최후의 탑이라고 불리는 곳이야. \n과거에는 바다 건너의 용족들의 침입을 감시하던 감시탑이였지.");
            else if (NPCCount[2] == 4)
                NPCTalking(0, "과거에 용족에게 대항한 인간들의 \n반란이 일어나고 패배한 용족들은\n 바다 건너 시그마 대륙으로 도망갔어.");
            else if (NPCCount[2] == 5)
                NPCTalking(0, "어리거나 부상을 입었던 용족은 스스로\n 날 수 없었기에 바다를 건너려면 \n다른 용족의 도움을 받아야 했지.");
            else if (NPCCount[2] == 6)
                NPCTalking(0, "하지만 용왕과 그의 부대는 겁에 질린 채 먼저 도망가 버리고... 날지 못하는 \n용족들은 도망치다가 결국 이 감시탑으로 몰려들었어.");
            else if (NPCCount[2] == 7)
            {
                NPCTalking(0, "...");
                NPC[0].GetComponent<Animator>().SetTrigger("2");
            }
            else if (NPCCount[2] == 8)
                NPCTalking(0, "나도 그 중 한명이였어.");
            else if (NPCCount[2] == 9)
                NPCTalking(0, "정식으로 소개할게 나는 용족국가인 \n매크로린 왕국의 용왕, \n맥스웰 3세의 딸인 맥시아야.");
            else if (NPCCount[2] == 10)
                NPCTalking(0, "나는 내 남동생과 시종들에게 들려서 \n성을 떠나고 있었어.");
            else if (NPCCount[2] == 11)
                NPCTalking(0, "하지만 인간들의 화살에 나를 들고있던 시종이 맞아서 그대로 \n땅으로 떨어지고 말았어.");
            else if (NPCCount[2] == 12)
                NPCTalking(0, "나는 내 비명소리를 들은 아버지가 잠깐 멈추더니 그대로 날아가는 걸 봤지....");
            else if (NPCCount[2] == 13)
                NPCTalking(0, "나는 살기를 품은 채 내게 다가오는 \n인간들이 무서워서 도망치다가\n 결국 감시탑까지 왔어.");
            else if (NPCCount[2] == 14)
                NPCTalking(0, "이 탑의 꼭대기에서 우리들은 고향을 \n볼 수는 있었지만 갈 수가 없었어.");
            else if (NPCCount[2] == 15)
                NPCTalking(0, "결국 우리들은 이 탑에서 \n최후의 항쟁을 벌이기로 다짐했지.");
            else if (NPCCount[2] == 16)
                NPCTalking(0, "인간들이 탑까지 다가왔고.... \n말 그대로 학살이 벌어졌어.");
            else if (NPCCount[2] == 17)
                NPCTalking(0, "그들이 꼭대기까지 올라와서도 \n차례차례... 학살이 이어졌고 나는 바로 앞에 있는 용족 아이까지 죽는 걸 보고 \n내 차례가 왔음을 깨달았지.");
            else if (NPCCount[2] == 18)
                NPCTalking(0, "모든 걸 포기한 순간 어머니께서 \n날아오셔서 나를 구해줬어.");
            else if (NPCCount[2] == 19)
                NPCTalking(0, "도망치라는 아버지의 명을 무시하고 \n나를 구하러 오신 거였지.");
            else if (NPCCount[2] == 20)
                NPCTalking(0, "나는 기뻐서 어머니의 품 속에서 펑펑 \n울었어... 눈물이 흘러내려와서 \n어머니께서도 울고 계신다는 것을 알 수 있었어.");
            else if (NPCCount[2] == 21)
                NPCTalking(0, "시그마 대륙에 거의 도착하고... \n그제서야 알 수 있었어.");
            else if (NPCCount[2] == 22)
                NPCTalking(0, "흘러내린 건 눈물이 아니라... \n어머니의 피라는 것을.");
            else if (NPCCount[2] ==23)
                NPCTalking(0, "어머니께서는 나를 구하는 과정에서\n 검에 찔린 상태였던 거야. 오직 나를 \n구하겠다는 일념으로 날고 계신 거였지.");
            else if (NPCCount[2] ==24)
                NPCTalking(0, "땅에 내려왔고... 어머니께서는 그대로 \n세상을 떠나셨어. 나를 꼭 안고 계신 \n어머니를 병사들 10명이 붙어서 \n겨우 떼어냈지.");
            else if (NPCCount[2] == 25)
                NPCTalking(0, "의사들 말로는 날고 있을 때 이미 세상을 떠났는데 대륙까지 온 게 기적이라고 \n하더라고.");
            else if (NPCCount[2] == 26)
                NPCTalking(0, "나는 나를 버렸던 아버지에게 갔지만 \n돌아온 건 실망 뿐이였어.");
            else if (NPCCount[2] == 27)
                NPCTalking(0, "패배를 경험한 아버지는 이미 인간이 \n두려워서 복수할 생각도 못하는 상태였어.");
            else if (NPCCount[2] == 28)
                NPCTalking(0, "다음 날 인간들과 평화협정을 맺고 \n양국에 있는 마법사들이 모여서 시그마 대륙과 퓨리에 대륙 사이에 \n거대한 마법장벽을 세웠어..");
            else if (NPCCount[2] == 29)
                NPCTalking(0, "협정에 의해 새워진 장벽은 \n서로의 왕국이 존재하는 한 용족과 \n인간이 서로 넘지 못하게 막았지.");
            else if (NPCCount[2] == 30)
                NPCTalking(0, "이 마법장벽 때문에 누구도 인간들에게 접근할 수가 없었어. 나는 이날부터 \n아버지를 증오했지.");
            else if (NPCCount[2] == 31)
                NPCTalking(0, "복수를 위해서는 남의 도움 없이 스스로 힘을 키워야 되겠다고 생각해서 열심히 수련에 매진했어.");
            else if (NPCCount[2] == 32)
                NPCTalking(0, "마법도 연마해서 마침내 넓은 \n마법장벽에서 마법이 약해진 곳을 찾아 작은 구멍을 낸 뒤 이 저주스러운 성에 \n들어왔지.");
            else if (NPCCount[2] == 33)
                NPCTalking(0, "하지만 복수만 생각해 온 나와 달리 \n성 안팎의 분위기는 달랐어.");
            else if (NPCCount[2] == 34)
                NPCTalking(0, "비록 세 용사 중 한명만 남았지만 성 밖의 모든 시민들이 스태락을 왕으로서 \n존경하고 나라는 안정을 되찾은 뒤였지.");
            else if (NPCCount[2] == 35)
                NPCTalking(0, "성 안에서도 스태락은 아내가 아들을 \n낳은 뒤라 누구보다 행복한 시절을 보내고 있었어.");
            else if (NPCCount[2] == 36)
                NPCTalking(0, "나는 생각했어. 만약 이런 상황에서 \n복수를 한다고 해도 스태락은 영원히\n 인간들에게 영웅의 이름으로 새겨질 \n것이라고.");
            else if (NPCCount[2] == 37)
                NPCTalking(0, "진정한 복수를 위해서는... \n스태락의 이름을 더럽히고 모든 사람들이 그를 미워하는 상황에서 비극적인 최후를 맞이해야 됐어.");
            else if (NPCCount[2] == 38)
                NPCTalking(0, "고민하는 나에게... 누군가가 다가왔어.");
            else if (NPCCount[2] == 39)
                NPCTalking(0, "마왕의 검에 깃들어있는... 마왕의 사념이 내게 말을 걸었지.");
            else if (NPCCount[2] == 40)
                NPCTalking(0, "그는 내 마음을 읽고 스태락의 정신을 \n흔들어 주면 그 틈에 몸을 지배해 \n주겠다고 했어.");
            else if (NPCCount[2] == 41)
                NPCTalking(0, "물론 그가 나를 이용한다는 것은 알고 \n있었지만 나는 밤에 스태락이 \n가장 사랑하는 아들을 납치했지.");
            else if (NPCCount[2] == 42)
                NPCTalking(0, "아들을 성에서 먼 섬에 버리고 성으로 \n돌아갔는데 문제가 생겼어.");
            else if (NPCCount[2] == 43)
                NPCTalking(0, "왕의 조력자인 시르케라는 마법사가 \n나와 마왕의 존재를 알아채고 성 지하에 씌여진 고대의 마법을 발동시켜서 \n성에 왕족 이외의 존재를 막는 보호막을 씌운 거야.");
            else if (NPCCount[2] == 44)
                NPCTalking(0, "게다가 마왕의 힘을 억눌러서 \n스태락을 지배하는 것을 막았지.");
            else if (NPCCount[2] == 45)
                NPCTalking(0, "위력이 큰 만큼 굉장한 부작용이 따르는 고대의 흑마법을 사용했기 때문에 나는 성 근처에 접근조차 할 수 없었어.");
            else if (NPCCount[2] == 46)
                NPCTalking(0, "성에 접근하지 못하면 답이 없었기 \n때문에 나는 왕의 명성이라도 떨어뜨리기 위해서 부하들을 이용했어.");
            else if (NPCCount[2] == 47)
                NPCTalking(0, "후후후... 그 중 하나는 사람들이 \n거대 참치라고 부르던 모양인데 사실은 \n우리나라에서 데려온 블라슈라는 \n바다괴물이야.");
            else if (NPCCount[2] == 48)
                NPCTalking(0, "나머지 하나는... 너가 죽인 용이야. \n용족들 사이에서는 하이 드래건이라고 \n불리는 무서운 존재지.");
            else if (NPCCount[2] == 49)
                NPCTalking(0, "약간 무리해서라도 마법장벽에 틈을 \n만들어서 둘을 데려오고 보이는 인간들을 모두 죽이라고 지시했어. \n시민들은 속사정을 모르니 자신을 도와주지 않는 스태락을 원망하게 될 거라는 \n생각했지.");
            else if (NPCCount[2] == 50)
                NPCTalking(0, "마침 성에서 흑마법의 부작용으로 \n생각되는 저주가 흘러나왔기 때문에 \n사람들은 점점 내 생각처럼 스태락을 \n미워하기 시작했어.");
            else if (NPCCount[2] == 51)
                NPCTalking(0, "나는 성 안 마법사의 힘이 다하기를 \n기다라면서 내 계획을 방해할 만한 \n존재들을 찾아다녔어.");
            else if (NPCCount[2] == 52)
                NPCTalking(0, "그 존재는 세 용사 중 스태락을 제외한 \n나머지 두명인 드락사르와 스태틱, \n그리고 마법사를 제외한 두 조력자 중 \n나머지 한명인 이시도르였어.");
            else if (NPCCount[2] == 53)
                NPCTalking(0, "결국 이시도르는 찾지 못했지만\n 드락사르와 스태틱의 소재는 확인했어. 그들에게 내 계획을 들키지 않게 \n조심했지. 뭐 스태틱은 어느정도 \n눈치챈 거 같았지만.");
            else if (NPCCount[2] == 54)
                NPCTalking(0, "걔네가 어디 있는지까지는 굳이 말하지 않아도 되겠지.");
            else if (NPCCount[2] == 55)
            {
                NPC[0].GetComponent<Animator>().SetTrigger("3");
                Dungeon.GetComponent<DungeonManager>().audiosource[0].Stop();
                NPCTalking(0, "너는 여기서 죽을꺼니까.");
            }
            else if (NPCCount[2] == 56)
                NPCTalking(0, "나는 마법사의 힘이 다할 날을 기다렸어. 완벽한 복수를 위해서는 평생이라도 \n기다릴 수 있었어.");
            else if (NPCCount[2] == 57)
                NPCTalking(0, "근데 변수가 생겼어. \n오래 전 먼 섬에 버렸던 스태락의 아들이 바다괴물 블라슈를 잡고 \n용이 있는 곳까지 온 거야.");
            else if (NPCCount[2] == 58)
                NPCTalking(0, "그게 누군지는 알겠지? 나는 너가 \n드락사르의 딸과 자라는 것을 한동안 \n지켜봤었지만 변수가 안된다고 판단해서 신경쓰지 않고 있었지.");
            else if (NPCCount[2] == 59)
                NPCTalking(0, "하지만... 너는 실망스러웠어. 내 용에게 상대조차 되지 않을 정도로 약했지.");
            else if (NPCCount[2] == 60)
                NPCTalking(0, "나는 너가 시몬이라는 아이와 굴을 파고 있다는 것을 알고 용과 계획을 세웠어.");
            else if (NPCCount[2] == 61)
                NPCTalking(0, "후후후... 너는 전부 내 계획대로 \n움직여 줬어. 용에게 시켜서 시몬을 \n죽이고 그 복수심 덕분에 너는 용을 \n죽일 정도로 강해졌어.");
            else if (NPCCount[2] == 62)
                NPCTalking(0, "물론 충실한 내 부하가 죽을 때는 \n가슴이 아팠지만 복수심에 휩싸인 너는 깨닫지 못했지.");
            else if (NPCCount[2] == 63)
                NPCTalking(0, "나는 너라면 성의 보호막을 통과할 수\n 있을 것이라고 생각했어. 왕의 핏줄인 \n너라면... 뭐 너를 납치할 때 챙겨둔 \n부적이 추가로 필요했지만 말이야.");
            else if (NPCCount[2] == 64)
            {
                NPCTalking(0, "부적에는 이 검과 같은 문양이 있지. \n이 검은 바로 우리 어머니의 몸에 \n박혀있었던 검이야. \n그때는 무슨 의민지 몰랐지만... \n스태락의 가문을 의미하는 \n문양이더라고?");
            }
            else if (NPCCount[2] == 65)
                NPCTalking(0, "다이스. 아니... 너의 진짜 이름인 스태온. 원래 너가 마법사를 죽여서 보호막이 \n사라지면 내가 직접 복수를 하려고 \n했지만 아들에게 죽는 것이 \n더 스태락에게 어울린다고 생각했어.");
            else if (NPCCount[2] == 66)
                NPCTalking(0, "이미 테일러 왕국의 왕과 왕비가 죽었고 남은 건 너뿐이야. \n너만 죽으면 왕의 핏줄이 없으니 왕국은 사라지고 평화협정과 \n마법장벽도 사라지지.");
            else if (NPCCount[2] == 67)
            {
                NPCTalking(0, "아버지에게 오늘 마법장벽이 \n사라질 거라고 말했어. \n딸로서 처음이자 마지막 부탁으로 \n병사를 보내달라고 했지.");
            }
            else if (NPCCount[2] == 68)
                NPCTalking(0, "마법장벽에 가려서 볼 수 없지만 탑 앞에 용족병사들이 모여있는 게 느껴져.");
            else if (NPCCount[2] == 68)
                NPCTalking(0, "여기서는 병사들은 물론이고 건너편의 \n시그마 대륙도 볼 수 없어. 왜냐하면 \n마법장벽을 만들면서 건너편을 \n볼 수 없게 막았거든.");
            else if (NPCCount[2] == 68)
                NPCTalking(0, "서로 보지 못하면 전쟁을 막을 수 있을 \n거라고 생각했나 보지. 참 한심해. \n 여기서 보이는 태양과 하늘, 바다가 모두 마법으로 그려낸 가짜야.");
            else if (NPCCount[2] == 68)
                NPCTalking(0, "너가 죽으면 장벽이 부숴지고 \n용족 병사들이 들이닥쳐서 \n인간들에게 복수를 할 거야. \n즉 너의 죽음으로 모든 게 완성되지.");
            else if (NPCCount[2] == 69)
                NPCTalking(0, "그러면... 작별이야...");




            if (NPCCount[2] == 70)
            {
                text.transform.position = new Vector2(1000, 0);

                NPC[0].GetComponent<Animator>().SetTrigger("4");
                gamemanager.Quest[0] = 55;
                Invoke("DungeonStart", 4f);
                NPCCount[2] = 0;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                NPCCount[2] += 1;
                SoundManager.instance.Click();
            }
        }

        if (NPCCount[3] > 0)
        {
            GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

            if (NPCCount[3] == 1)
                NPCTalking(0, "크... 나름 잘 버티네.");
            else if (NPCCount[3] == 2)
                NPCTalking(0, "용 한마리도 못잡던 꼬마가 \n이렇게 성장하다니");
            else if (NPCCount[3] == 3)
                NPCTalking(0, "누나로서 기쁘네. 후후후");
            else if (NPCCount[3] == 4)
                NPCTalking(0, "그거 알아? \n용족들은 과거에는 모두 인간이였어.");
            else if (NPCCount[3] == 1)
                NPCTalking(0, "그 중에 용의 피를 받아들인 강한 \n인간만이 인간보다 상위의 존재인 \n용족이 된 거지.");
            else if (NPCCount[3] == 5)
                NPCTalking(0, "평소엔 너희 인간과 큰 차이가 없지만 \n몸 안에 있는 용의 피에서 힘을 끌어낼 \n수 있는거지. 그리고 용족중에서도 극소수의 강자만이 용의 힘을 100% 끌어낼 \n수 있어.");
            else if (NPCCount[3] == 6)
                NPCTalking(0, "우린 이걸 해방시킨다고 하는데 \n현재 용의 힘을 해방시킬 수 있는 용족은 공식적으로는 아버지밖에 없어. \n나 역시 해방시킬 수는 있지만... ");
            else if (NPCCount[3] == 7)
                NPCTalking(0, "이 모습은 아버지에게도 보여준 적이 \n없어. 그리고...");
            else if (NPCCount[3] == 8)
                NPCTalking(0, "너에게도 보여주지 않을거야.");
            else if (NPCCount[3] == 9)
            {
                NPCTalking(0, "스태온! 나는 두팔 벌리고 있을테니까 \n나를 죽이고 싶으면 칼로 찌르면 돼!");
                NPC[0].GetComponent<Animator>().SetBool("11", true);
            }
            else if (NPCCount[3] == 10)
                NPCTalking(0, "당황한 거 같은데 함정같은 건 아니야.");
            else if (NPCCount[3] == 11)
                NPCTalking(0, "나 참...  망설이는 거야?");
            else if (NPCCount[3] == 12)
                NPCTalking(0, "어디보자... \n지금까지 너한테 어디까지 말했지?");
            else if (NPCCount[3] == 13)
                NPCTalking(0, "너를 드엔마을에 버렸고... \n시몬을 죽이고... 너의 엄마를 죽이고... \n너의 손으로 아빠를 죽이게 했지.");
            else if (NPCCount[3] == 14)
                NPCTalking(0, "아!  한 가지 말을 안했구나.");
            else if (NPCCount[3] == 15)
                NPCTalking(0, "나는 너가 어릴 때 드엔 마을에 \n들렸다고 했었지?");
            else if (NPCCount[3] == 16)
                NPCTalking(0, "그 당시의 너는 너무 약해서 \n커서도 내 계획을 방해할 만큼의 변수가 되지 않을 것이라고 판단했었어.");
            else if (NPCCount[3] == 17)
                NPCTalking(0, "하지만... 너 옆에 있었던 드락사르의 딸 마야는 달랐어.");
            else if (NPCCount[3] == 18)
                NPCTalking(0, "대륙으로 가서 아버지를 찾기위해서 \n열심히 수련하고 있었지.");
            else if (NPCCount[3] == 19)
                NPCTalking(0, "물론 나에 비하면 약했지만 드락사르의 자식인 만큼 나중에 변수가 될 수도 \n있다고 판단했어.");
            else if (NPCCount[3] == 20)
                NPCTalking(0, "그래서... 다른 쪽에 신경을 쓰게 만들어서 굳센 마음을 한 번은 꺾어야 한다고 \n판단했지.");
            else if (NPCCount[3] == 21)
                NPCTalking(0, "너도 알지 모르겠지만 마야의 어머니는 갑작스러운 '사고'로 죽었지. \n 마야는 충격을 받고 다시는 검을 들지 \n못했어.");
            else if (NPCCount[3] == 22)
                NPCTalking(0, "후후후... 그게 과연 사고였을까?");
            else if (NPCCount[3] == 23)
            {
                NPCTalking(0, "...");
                isNotClick = true;
                Invoke("SetisNotClick3", 4);
                Invoke("SwordAttack", 2);
                NPC[0].GetComponent<Animator>().SetBool("12", true);
                NPCCount[3] += 1;
            }
            else if (NPCCount[3] == 25)
                NPCTalking(0, "그게 너의 한계야. \n너의 검에는 살의가 없어.\n이런 무딘 각오로는 절대 나를 벨 수 없어.");
            else if (NPCCount[3] == 26)
                NPCTalking(0, "나는 너의 원수지만... 너를 도와줬지.");
            else if (NPCCount[3] == 27)
                NPCTalking(0, "너가 시몬의 원수를 갚게 도와줬고 \n너가 죽을 위기일 때마다 구해줬어.");
            else if (NPCCount[3] == 28)
                NPCTalking(0, "뭐 너도 알듯이 너를 이용한 거긴 하지만.");
            else if (NPCCount[3] == 29)
                NPCTalking(0, "너의 마음 속에는 아직 그때의 나에 대한 \n감정이 남아있는 거야. \n그래서 죽이지 못하는 거지.");
            else if (NPCCount[3] == 30)
                NPCTalking(0, "사실은 나도 짧은 시간동안 너와 \n지내면서 정말 즐거웠어. 너가 성장하는 걸 지켜보는 재미도 있었지.");
            else if (NPCCount[3] == 31)
                NPCTalking(0, "살면서 이렇게 이야기를 많이 \n나눈 사람도 너가 처음이었어. \n지금까지 모든 걸 나 혼자 했거든.");
            else if (NPCCount[3] == 32)
                NPCTalking(0, "하지만...",false);
            else if (NPCCount[3] == 33)
            {
                NPC[0].GetComponent<Animator>().SetBool("13", true);
                isNotClick = true;
                Invoke("SetisNotClick3", 3);
                NPCCount[3] += 1;
                NPCFalse();
                Follower.Follow = NPC[1].transform;
            }
            else if (NPCCount[3] == 35)
                NPCTalking(0, "나는 너를 죽일 수 있어.");
            else if (NPCCount[3] == 36)
                NPCTalking(0, "이게 너와 나의 차이야. \n나는 평생 이 순간을 기다려왔어.");
            else if (NPCCount[3] == 37)
                NPCTalking(0, "너와 내 증오의 깊이의 차이라고도 \n할 수 있지.");
            else if (NPCCount[3] == 38)
                NPCTalking(0, "정말로... 미안해... \n너는 정말 좋은 친구였어.");
            else if (NPCCount[3] == 39)
            {
                if(GameManager.Instance.Armornum == 9)
                {
                    NPCCount[3] = 99;
                    NPC[0].GetComponent<Animator>().SetBool("100", true);
                    isNotClick = true;
                    Invoke("SetisNotClick3", 6);
                    Follower.Follow = NPC[1].transform;
                    NPCFalse();
                }
                else
                {
                    NPC[0].GetComponent<Animator>().SetBool("15", true);
                    NPCCount[3] = 40;
                    isNotClick = true;
                    Invoke("SetisNotClick3", 3);
                }
            }

            else if (NPCCount[3] == 41) NPCTalking(0, "드디어 파란색 장벽이 모습을 드러냈네. \n너가 죽어갈수록 장벽의 힘이 약해질 \n것이고 결국은 부숴질거야.");
            else if (NPCCount[3] == 42) NPCTalking(0, "그동안 정으로.... \n너의 마지막은 지켜봐 줄게. ");
            else if (NPCCount[3] == 43) NPCTalking(0, "잘 가... 친구.");
            else if (NPCCount[3] == 44)
            {
                NPC[0].GetComponent<Animator>().SetBool("14", true);
                text.transform.position = new Vector2(1000, 0);
                isNotClick = true;
                Invoke("Ending1", 38);
                NPCCount[3] += 1;
                NPCFalse();
                Follower.Follow = NPC[1].transform;
            }

            else if (NPCCount[3] == 100)
                NPCTalking(0, "내 팔이!!  너는!!");
            else if (NPCCount[3] == 101)
                NPCTalking(0, "마왕...");
            else if (NPCCount[3] == 102)
                NPCTalking(2, "크크크... 맥시아 다시 만났구나.");
            else if (NPCCount[3] == 103)
                NPCTalking(2, "너에게 다시 만날 거라고 했지?");
            else if (NPCCount[3] == 104)
                NPCTalking(0, "이런... 너를 예상하지 못하다니. \n내 실수야.");
            else if (NPCCount[3] == 105)
                NPCTalking(2, "크크크.. 나 역시 예상 못했으니 \n너의 잘못이 아니지.");
            else if (NPCCount[3] == 106)
                NPCTalking(2, "예전에 너에게 스태락의 정신을 흔들어 달라고 요청했을 때  너가 나를 이용해 먹고 버릴 것이란 것을 알고 있었단다.");
            else if (NPCCount[3] == 107)
                NPCTalking(2, "하지만 나 역시 계획이 있었지. \n스태락의 몸을 지배하면 바로 어디선가 나를 부르고 있는 내 갑옷에게 가서 \n내 힘을 완전히 회복할 생각이였어.");
            else if (NPCCount[3] == 108)
                NPCTalking(2, "뭐.... 계획은 빗나가 버렸지만... \n이 순진한 녀석이 내 갑옷을 가지고 와 준 덕분에 갑옷으로 들어가서 내 힘을 \n회복할 수 있었으니까 상관없지.");
            else if (NPCCount[3] == 109)
                NPCTalking(2, "여하튼 틈을 보고 있다가 드디어 \n이 녀석을 지배할 수 있게 된 거다.");
            else if (NPCCount[3] == 110)
                NPCTalking(0, "후후후... 그래서 너는 과거처럼 용족과 \n인간을 지배할 생각이야?");
            else if (NPCCount[3] == 111)
                NPCTalking(2, "일단 바다 속에 처박힌 내 성부터 다시 \n세워야겠지. 그러면 봉인된 내 병사들이 깨어나서 너희를 다 죽일것이야.");
            else if (NPCCount[3] == 112)
                NPCTalking(2, "물론... 너를 죽이고 나서 말이야.");
            else if (NPCCount[3] == 113)
                NPCTalking(0, "...");
            else if (NPCCount[3] == 114)
                NPCTalking(0, "전제가 잘못됐네.");
            else if (NPCCount[3] == 115)
                NPCTalking(0, "시르케라는 마법사가 죽었을 때 \n나는 성 안으로 들어가서 너가 스태락을 지배하기 전에 죽일 수 있었어.");
            else if (NPCCount[3] == 116)
                NPCTalking(0, "내가 왜 너가 스태락을 지배하기 전에 \n죽이지 않았는지 알아?");
            else if (NPCCount[3] == 117)
                NPCTalking(0, "그건 너가 스태락의 몸을 지배하더라도 \n나보다 약하기 때문이야.");
            else if (NPCCount[3] == 118)
            {
                NPC[0].GetComponent<Animator>().SetBool("101", true);
                text.transform.position = new Vector2(1000, 0);
                isNotClick = true;
                Invoke("SetisNotClick3", 6);
                NPCCount[3] += 1;
                NPCFalse();
                text.transform.position = new Vector2(1000, 0);
            }
            else if (NPCCount[3] == 120)
                NPCTalking(2, "해방인가...");
            else if (NPCCount[3] == 121)
                NPCTalking(2, "너의 아버지도 너 나이 때 아직 해방을 \n배우지 못했는데... 대단하구나. \n이건 진심이란다.");
            else if (NPCCount[3] == 122)
                NPCTalking(0, "오늘 모든 게 결정나.");
            else if (NPCCount[3] == 123)
                NPCTalking(2, "크크크 오늘 새로운 역사가 생기겠군.");
            else if (NPCCount[3] == 124)
            {
                GameManager.Instance.StartPoint = 2;
                GameManager.Instance.BlenderDieCount = 2;
                isNotClick = true;
                NPCCount[3] += 1;
                Fade = 1;
                Invoke("GoToBlender", 1f);
            }




            if (Input.GetButtonDown("Fire1") && !isNotClick)
            {
                NPCCount[3] += 1;
                SoundManager.instance.Click();
            }
        }

        if (NPCCount[4] > 0)
        {
            GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

            if (NPCCount[4] == 1)
                NPCTalking(0, "...");
            else if (NPCCount[4] == 2)
                NPCTalking(0, "정말 충격적이네...");
            else if (NPCCount[4] == 2)
                NPCTalking(0, "끙... 일어나지도 못하겠어...");
            else if (NPCCount[4] == 3)
                NPCTalking(2, "해방이라는 것은 큰 힘을 얻는 대신 \n거대한 대가를 요구한다.");
            else if (NPCCount[4] == 4)
                NPCTalking(2, "너는 아직 너무 어려. \n몸이 그 힘을 견뎌내지 못하는 거지.");
            else if (NPCCount[4] == 5)
                NPCTalking(2, "포기해라 소녀여. \n나는 계속 강해지지만 \n너는 몸의 한계를 이미 넘었다.");
            else if (NPCCount[4] == 6)
                NPCTalking(2, "지금이라도 항복한다면 죽이지는 \n않을거다.");
            else if (NPCCount[4] == 7)
                NPCTalking(2, "나는 혼란스러운 이 세상의 질서를 \n다시 바로잡겠다.");
            else if (NPCCount[4] == 8)
                NPCTalking(0, "...");
            else if (NPCCount[4] == 9)
                NPCTalking(0, "내 계획은 완벽했어...");
            else if (NPCCount[4] == 10)
                NPCTalking(0, "여기서 끝날 수는...");
            else if (NPCCount[4] == 11)
                NPCTalking(0, "없단 말이야!!!!");
            else if (NPCCount[4] == 12)
            {
                Follower.Follow = NPC[1].transform;
                NPCFalse();
                isNotClick = true;
                Invoke("SetisNotClick4", 6);
                NPC[0].GetComponent<Animator>().SetBool("201", true);
                NPCCount[4] += 1;
            }
            else if (NPCCount[4] == 14)
                NPCTalking(2, "정신력 만으로 일어선 건가...");
            else if (NPCCount[4] == 15)
                NPCTalking(2, "이제는 정말로 너를 죽일 수 밖에 없구나.");
            else if (NPCCount[4] == 16)
            {
                if (GameManager.Instance.Swordnum == 9)
                {
                    NPCCount[4] = 100;
                    NPC[0].GetComponent<Animator>().SetBool("210", true);
                }
                else
                {
                    NPC[0].GetComponent<Animator>().SetBool("202", true);
                }
                Follower.Follow = NPC[1].transform;
                NPCFalse();
                isNotClick = true;
                Invoke("SetisNotClick4", 4);
                NPCCount[4] += 1;
            }
            else if (NPCCount[4] == 18)
                NPCTalking(2, "작별이구나.");
            else if (NPCCount[4] == 19)
                NPCTalking(0, "...");
            else if (NPCCount[4] == 20)
            {
                NPC[0].GetComponent<Animator>().SetBool("203", true);
                Follower.Follow = NPC[1].transform;
                NPCFalse();
                isNotClick = true;
                Invoke("SetisNotClick4", 4);
                NPCCount[4] += 1;
            }
            else if (NPCCount[4] == 22)
                NPCTalking(2, "윽! 이 힘은??");
            else if (NPCCount[4] == 23)
                NPCTalking(0, "...");
            else if (NPCCount[4] == 24)
                NPCTalking(0, "너는 내 팔을 잘라냈어.");
            else if (NPCCount[4] == 25)
                NPCTalking(0, "그러면서 너의 몸 안에는 \n내 피가 스며들었지.");
            else if (NPCCount[4] == 26)
                NPCTalking(0, "우리는 피에서 용의 힘을 끌어내.\n 그리고... 힘을 소모한 내 몸과 달리 \n너의 몸 안에 있는 내 피에는 \n아직 용의 힘이 남아있지.");
            else if (NPCCount[4] == 27)
                NPCTalking(2, "크으윽.... 안돼...!");
            else if (NPCCount[4] == 28)
                NPCTalking(0, "남아있는 모든 힘으로 너의 몸 안에 있는 용의 힘을 터트릴 거야... \n이게... 내 최후의 발악이야...");
            else if (NPCCount[4] == 29)
                NPCTalking(2, "크아아악!! ");
            else if (NPCCount[4] == 30)
                NPCTalking(0, "...");
            else if (NPCCount[4] == 31)
            {
                Follower.Follow = NPC[1].transform;
                NPCFalse();
                isNotClick = true;
                //Invoke("SetisNotClick4", 45);
                NPC[0].GetComponent<Animator>().SetBool("204", true);
                NPCCount[4] += 1;
                Invoke("Ending1", 47);
            }

            else if (NPCCount[4] == 102)
                NPCTalking(2, "이건... 무슨 일이 벌어진거지?");
            else if (NPCCount[4] == 103)
                NPCTalking(2, "분명 완벽하게 통제하고 있는 \n이 녀석의 몸이 움직이지 않아!");
            else if (NPCCount[4] == 103)
                NPCTalking(0, "무슨 일이 벌어진거지?");
            else if (NPCCount[4] == 103)
                NPCTalking(2, "으아아아악!!!");
            else if (NPCCount[4] == 104)
            {
                Follower.Follow = NPC[1].transform;
                NPCFalse();
                isNotClick = true;
                Invoke("SetisNotClick4", 15);
                NPC[0].GetComponent<Animator>().SetBool("211", true);
                NPCCount[4] += 1;
            }
            else if (NPCCount[4] == 106)
                NPCTalking(0, "...");
            else if (NPCCount[4] == 107)
                NPCTalking(0, "다이스?");
            else if (NPCCount[4] == 108)
                NPCTalking(0, "너 맞구나...");
            else if (NPCCount[4] == 109)
                NPCTalking(0, "그 검... 마왕이 빠져나간 마왕의 검에 \n드락사르의 검의 힘을 넣은 거구나.");
            else if (NPCCount[4] == 110)
                NPCTalking(0, "안에 있는 드락사르의 힘이 \n마왕을 몰아낸 것 같네.");
            else if (NPCCount[4] == 111)
                NPCTalking(0, "그 녀석은 우리를 마지막까지 \n방해하는구나...");
            else if (NPCCount[4] == 112)
                NPCTalking(0, "너가 나를 찌르는 순간을 노렸었는데... \n너의 무딘 각오가 이번에는 너를 구했네.");
            else if (NPCCount[4] == 113)
            {
                Follower.Follow = NPC[1].transform;
                NPCFalse();
                isNotClick = true;
                Invoke("SetisNotClick4", 2);
                NPC[0].GetComponent<Animator>().SetBool("212", true);
                NPCCount[4] += 1;
            }
            else if (NPCCount[4] == 115)
                NPCTalking(0, "그래서 어떻게 할 거야?");
            else if (NPCCount[4] == 116)
                NPCTalking(0, "...");
            else if (NPCCount[4] == 117)
                NPCTalking(0, "됐다.");
            else if (NPCCount[4] == 118)
                NPCTalking(0, "너에겐 질렸어... ");
            else if (NPCCount[4] == 119)
                NPCTalking(0, "정말 수고했어. 다이스.");
            else if (NPCCount[4] == 120)
                NPCTalking(0, "인생이 계획대로 흘러가지는 않네.");
            else if (NPCCount[4] == 121)
                NPCTalking(0, "여러 결말을 생각했는데 이런 결말은\n 상상도 하지 못했어.");
            else if (NPCCount[4] == 122)
            {
                Follower.Follow = NPC[1].transform;
                NPCFalse();
                isNotClick = true;
                NPC[0].GetComponent<Animator>().SetBool("213", true);
                NPCCount[4] += 1;
                Invoke("Ending2", 5);
            }

            if(NPCCount[4]>=106)
                Follower.Follow = NPC[1].transform;

            if (Input.GetButtonDown("Fire1") && !isNotClick)
            {
                NPCCount[4] += 1;
                SoundManager.instance.Click();
            }
        }
        if (NPCCount[5] > 0)
        {
            GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

            if (NPCCount[5] == 1)
                NPCTalking(0, "...");
            else if (NPCCount[5] == 2)
                NPCTalking(0, "너는 누구지?");
            else if (NPCCount[5] == 3)
                NPCTalking(0, "...");
            else if (NPCCount[5] == 4)
                NPCTalking(0, "너도 그 돌을 가지고 있구나.");
            else if (NPCCount[5] == 5)
                NPCTalking(0, "...");
            else if (NPCCount[5] == 6)
                NPCTalking(0, "내 이름도 밝혀야겠지.");
            else if (NPCCount[5] == 7)
            {
                NPCFalse();
                isNotClick = true;
                Invoke("SetisNotClick5", 2);
                NPC[0].GetComponent<Animator>().SetBool("0", true);
                NPCCount[5] += 1;
            }
            else if (NPCCount[5] == 9)
                NPCTalking(0, "내 이름은 드락사르. \n너도 알지 모르겠구나.");
            else if (NPCCount[5] == 10)
                NPCTalking(0, "나는 과거에 용족에 반란을 일으켜서 \n승리했었다.");
            else if (NPCCount[5] == 11)
                NPCTalking(0, "하지만 이시도르가 그 돌과 같은 파란 \n돌을 가지고 오면서 분란이 시작됐지.");
            else if (NPCCount[5] == 12)
                NPCTalking(0, "그 돌은 천상석이라고 불리는 하늘성으로 갈 수 있는 열쇠였다. \n나와 스태락, 스태릭, 그리고 이시도르와 시르케가 모두 하늘성으로 올라왔지.");
            else if (NPCCount[5] == 13)
                NPCTalking(0, "이 곳은 먼 과거 마왕이 등장하기 전\n 모든 인간이 천상인과 지하인으로 \n나뉘어서 영원한 전쟁을 치르고 있을 때 천상인들이 거주했던 거대한 \n인공섬이다.");
            else if (NPCCount[5] == 14)
                NPCTalking(0, "하늘성에는 오면서 봤겠지만 강력한 \n병기들과 무기가 잠들어 있었다. \n파란 돌을 이용해서 그들을 깨운다면 국가 하나 정도는 쉽게 파괴할 수 있는 \n전력이었다.");
            else if (NPCCount[5] == 15)
                NPCTalking(0, "그리고 하늘성의 중심인 이곳에는 \n가슴에 검을 찔러서 자살한 수많은 \n천상인들의 시체와 함께 편지가 \n쓰여져 있었다.");
            else if (NPCCount[5] == 16)
                NPCTalking(0, "편지에는 자신들의 욕심 때문에 \n마왕이 부활했다면서 기술력을 맹신한 \n자신들을 자책하는 내용이 있었다. ");
            else if (NPCCount[5] == 17)
                NPCTalking(0, "그리고 마왕의 군대에 최후의 방어선이 \n뚫리고 있으니 하늘성을 조종할 수 있는 파란 돌을 지상에 숨기고 모두 자살해서 아무도 그 돌의 위치를 알 수 없게 \n막겠다고 쓰여져 있었지.");
            else if (NPCCount[5] == 18)
                NPCTalking(0, "마지막으로 이 편지를 본다면 \n숨겨둔 파란 돌을 찾아서 왔을 테니 \n자신들과 같은 실수를 하지 말고 \n파란 돌을 놓고 즉시 하늘섬을 떠나라고 쓰여있었다.");
            else if (NPCCount[5] == 19)
                NPCTalking(0, "스태락은 하늘성의 병기들을 이용해서 \n용족들이 도망간 시그마 대륙을 불태워 버리자고 주장했고 나는 반대했다.");
            else if (NPCCount[5] == 20)
                NPCTalking(0, "천상인들의 경고에 따라서 하늘성을 \n떠나자고 했지만 분노한 스태락이 내게 검을 들이댔지.");
            else if (NPCCount[5] == 21)
                NPCTalking(0, "우리는 싸웠고 마왕의 검을 가진 \n스태락을 상대로 장기전을 가면 \n이기지 못할 것을 알기 때문에 \n내 검과 팔을 일부러 내줘서 스태락이 \n방심한 틈에 승리했다.");
            else if (NPCCount[5] == 22)
                NPCTalking(0, "내 앞에 있는 검조각이 그 때 \n부숴진 내 검이다. \n나는 매일 이걸 보면서 각오를 다진다.");
            else if (NPCCount[5] == 23)
                NPCTalking(0, "나는 패배한 스태락에게 하늘섬을 \n떠나라고 했고 그 후로 다른 누군가가\n  다른 파란 돌을 찾아서 하늘섬을 \n와서 병기들을 깨울 것이 걱정되서 \n이 곳을 지키고 있었다.");
            else if (NPCCount[5] == 24)
                NPCTalking(0, "너의 선택은 뭐지? \n파란 돌을 내게 주고 이 곳을 \n떠날 것인가?");
            else if (NPCCount[5] == 25)
                NPCTalking(0, "...");
            else if (NPCCount[5] == 26)
                NPCTalking(0, "그럴 생각은 없는 것 같군");
            else if (NPCCount[5] == 27)
                NPCTalking(0, "검신의 경지에 이른 검사는 무기에\n 상관없이 강력한 검기를 내보낼 수 \n있다고 하지.");
            else if (NPCCount[5] == 28)
            {
                NPCFalse();
                isNotClick = true;
                Invoke("SetisNotClick5", 2);
                NPC[0].GetComponent<Animator>().SetBool("1", true);
                NPCCount[5] += 1;
            }
            else if (NPCCount[5] == 30)
                NPCTalking(0, "설령 그게 나뭇가지라도 말이야.");
            else if (NPCCount[5] == 31)
                NPCTalking(0, "덤벼라. \n한 때 전설이라고 불렸던 몸이다.");
            else if (NPCCount[5] == 32)
            {
                Follower.Follow = NPC[1].transform;
                NPCFalse();
                isNotClick = true;
                NPCCount[5] += 1;
                SceneManager.LoadScene("SkyCatle2");
            }

            else if (NPCCount[5] == 100)
                NPCTalking(0, "...");
            else if (NPCCount[5] == 101)
                NPCTalking(0, "여기까진가...");
            else if (NPCCount[5] == 102)
                NPCTalking(0, "내 예상대로 너가 오기 전에도 \n푸른 돌을 찾은 인간들이\n 하늘섬으로 왔다.");
            else if (NPCCount[5] == 103)
                NPCTalking(0, "만약 욕심을 버리고 하늘섬을 떠나서\n 돌아오지 않겠다는 사람이 있다면 \n나를 이어서 하늘섬을 지키게 \n할 생각이었다.");
            else if (NPCCount[5] == 104)
                NPCTalking(0, "하지만... 누구도 그러지 않았다. \n하나같이 하늘섬의 힘을 이용해서 본인의 목적을 이룰 생각밖에 없었지.");
            else if (NPCCount[5] == 105)
                NPCTalking(0, "나는 인간을 위해서 싸웠었고 \n인간을 사랑했지만.... \n점점 그들에 대한 실망이 커져갔다.");
            else if (NPCCount[5] == 106)
                NPCTalking(0, "용족 아이가 결계를 깨고 들어와서 \n음모를 꾸미는 것도 알고 \n같이 들어온 용과 해저괴물에게 \n인간들이 당하는 것도 보았다.");
            else if (NPCCount[5] == 107)
                NPCTalking(0, "만약 한명이라도 욕심을 버리고 하늘섬을 떠나겠다고 말했다면.... 인간을 다시 믿고 그들을 도와줬을 텐데\n 누구도 그러지 않았어...");
            else if (NPCCount[5] == 108)
                NPCTalking(0, "이런 이야기를 너무 많이 주절거렸나...\n이제는 나도 늙었나 보군...");
            else if (NPCCount[5] == 109)
            {
                Follower.Follow = NPC[1].transform;
                NPCFalse();
                isNotClick = true;
                NPC[0].GetComponent<Animator>().SetBool("D2", true);
                Invoke("SetisNotClick5", 3);
                NPCCount[5] += 1;
            }
            else if (NPCCount[5] == 111)
                NPCTalking(0, "한 가지 말 안한 게 있다.");
            else if (NPCCount[5] == 112)
                NPCTalking(0, "사실 편지 뒤쪽에 내용이 하나 더 있었다.");
            else if (NPCCount[5] == 113)
                NPCTalking(0, "그건 하늘섬을 완전히 파괴하는 주문이지.");
            else if (NPCCount[5] == 114)
                NPCTalking(0, "천상인들은 자신이 만든 하늘섬을 \n차마 파괴할 수가 없었다. \n 하늘섬은 그 자체로 인류의 유산이지.");
            else if (NPCCount[5] == 115)
                NPCTalking(0, "그들은 인간을 믿었기에 편지를 읽은 \n인간들은 스스로 하늘섬을 떠날 거라고 \n생각했던 거다.");
            else if (NPCCount[5] == 116)
                NPCTalking(0, "...");
            else if (NPCCount[5] == 117)
                NPCTalking(0, "나는 방금 그 멸망의 주문을 말했다.");
            else if (NPCCount[5] == 118)
                NPCTalking(0, "... 도망가는 게 좋을거다.");
            else if (NPCCount[5] == 119)
            {
                NPC[0].GetComponent<Animator>().SetBool("D3", true);
                NPCFalse();
                isNotClick = true;
                NPCCount[5] += 1;
                Invoke("Ending3", 3);
                gamemanager.Key[3] = 1;
                gamemanager.StartPoint = 2;
                gamemanager.Quest[8] = 0;
            }


            Follower.Follow = NPC[1].transform;
            if (Input.GetButtonDown("Fire1") && !isNotClick)
            {
                NPCCount[5] += 1;
                SoundManager.instance.Click();
            }
        }

        if (Fade >0 && Fade2 < 1)
        {
            Fade2 += 0.05f* Fade;
        }
        else if (Fade < 0 && Fade2 > 0)
        {
            Fade2 -= 0.05f * Fade*(-1);
        }
        Black.color = new Color(0, 0, 0, Fade2);
        
        if(GoldText != null)
            GoldText.text = PlayerGold + "   Gold";

        if (Input.GetButtonDown("Fire1") && isNar == 1)
        {
            //NPCChat.SetActive(true);
            Naration.SetActive(false);
            isNar = 0;
        }

        if(isDie == true && Input.GetButtonDown("Fire1"))
        {
            SoundManager.instance.Click();
            Fade = -1;
            Invoke("PlayerDie1", 0.5f);
        }
    }
    void GoToBlender()
    {
        SceneManager.LoadScene("LastTower");
    }
    void Ending1()
    {
        Fade = 0.5f;
        Invoke("GotoEnd",2);
    }
    void Ending2()
    {
        Fade = 0.5f;
        Invoke("GotoEnd2", 2);
    }
    void Ending3()
    {
        Fade = 0.5f;
        Invoke("GotoEnd3", 2);
    }
    void GotoEnd()
    {
        SceneManager.LoadScene("LastTowerE");
    }
    void GotoEnd2()
    {
        Player.transform.position = NPC[2].transform.position;
        Fade = -0.5f;
        NPCCount[4] = 0;
        //NPC[0].SetActive(false);
        NPC[3].SetActive(true);
        Player.SetActive(true);
        Follower.Follow = Player.transform;
        Player.GetComponent<Player>().SwordInven(9);
        Player.GetComponent<Player>().ArmorSprite.sprite = GameManager.Instance.ArmorImage[13];
    }
    void GotoEnd3()
    {
        player.alive = 1;
        SceneManager.LoadScene("Robrah_Village");
    }
    void DungeonStart()
    {
        Dungeon.GetComponent<DungeonManager>().DungeonEnter(5);
    }
    void SwordAttack()
    {
        InvenObject[0].GetComponent<Animator>().SetTrigger("isAttack");
    }
    public void NPCTalking(int num, string sentence, bool isTrans = true)
    {
        if(isTrans)
            Follower.Follow = NPC[num].transform;
        text.text = sentence;
        text.transform.position = NPC[num].GetComponent<NPC>().chatTr.transform.position;
        float x = text.preferredWidth;
        x = (x > 7) ? 7 : x + 0.3f; //크기설정
        quad.transform.localScale = new Vector2(x, text.preferredHeight + 0.3f);//크기설정
    }
    public void NPCFalse()
    {
        text.transform.position = new Vector2(1000, 1000);
    }

    public void PlayerDie1()
    {
        SceneManager.LoadScene(MapName);
        isDie = false;
    }
    public void GOTOMAP()
    {
        SceneManager.LoadScene("Map");
    }

    public void Naration1(string sentence,GameObject Chat)
    {
        NPCChat = gameObject;
        //gameObject.SetActive(false);
        NarationText.text = sentence;
        Naration.SetActive(true);
        isNar = 1;
    }

    public void Naration2(string sentence)
    {
        NarationText.text = sentence;
        Naration.SetActive(true);
        Invoke("EndNaration", 3);
    }

    public void Naration3(string sentence)
    {
        sentence1 = sentence;
    }

    public void EndNaration()
    {
        if(sentence1 == "")
        {
            if (isNar == 0)
                Naration.SetActive(false);
        }
        else
        {
            Naration2(sentence1);
            sentence1 = "";
        }

    }
    public void PlayerDie()
    {
        DieNaration.SetActive(true);
        isDie = true;
    }


    public void ItemMouse(int num)
    {
        SoundManager.instance.Click();
        Itemnum = num;
        if (num >= 300)
        {
            int nnum = num - 300;
            Itemtext.text = ItemText[nnum];
        }
        else if (300>num && num >= 200)
        {
            int nnum = num - 200;
            Itemtext.text = SkillText[nnum];
        }
        else if (200>num && num >= 100)
        {
            int nnum = num - 100;
            Itemtext.text = ArmorText[nnum];
        }
        else
            Itemtext.text = SwordText[num];
    }


    public void BuyItem()
    {
        GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (Itemnum >= 300)
        {
            int num = Itemnum - 300;
            if (PlayerGold >= ItemSale[num] && Itemhave[num] == 0)
            {
                PlayerGold -= ItemSale[num];
                Itemhave[num] = 1;
                gamemanager.Key[num] = 1;
                SoundManager.instance.Click2();
            }
        }
        else if (Itemnum >= 200 && Itemnum<300)
        {
            int num = Itemnum - 200;
            if (PlayerGold >= SkillSale[num] && Skillhave[num] == 0)
            {
                PlayerGold -= SkillSale[num];
                Skillhave[num] = 1;
                gamemanager.Skillhave[num] = 1;
                SoundManager.instance.Click2();
            }
        }
        else if (Itemnum >= 100 && Itemnum<200)
        {
            int num = Itemnum - 100;
            if (PlayerGold >= ArmorSale[num] && Armorhave[num] == 0)
            {
                PlayerGold -= ArmorSale[num];
                Armorhave[num] = 1;
                player.ArmorInven(num);
                player.Defense = ArmorDefense[num];
                Armornum = num;
                gamemanager.Armornum = Armornum;
                SoundManager.instance.Click2();
            }
        }
        else
        {
            if (PlayerGold >= SwordSale[Itemnum] && Swordhave[Itemnum] == 0)
            {
                PlayerGold -= SwordSale[Itemnum];
                Swordhave[Itemnum] = 1;
                player.SwordInven(Itemnum);
                player.Power = SwordPower[Itemnum];
                Swordnum = Itemnum;
                gamemanager.Swordnum = Swordnum;
                SoundManager.instance.Click2();
            }
        }
        gamemanager.Gold = PlayerGold;
            
    }

    public void Inven()
    {
        SoundManager.instance.Click();
        for(int i = 0; i<Swordhave.Length; i++)
        {
            if (Swordhave[i] == 0)
                SwordSprite[i].color = new Color(0, 0, 0);
            else
                SwordSprite[i].color = new Color(1, 1, 1);
        }

        for (int i = 0; i < Skillhave.Length; i++)
        {
            if (Skillhave[i] == 0)
                SkillSprite[i].color = new Color(0, 0, 0);
            else
                SkillSprite[i].color = new Color(1, 1, 1);
        }
        for (int i = 0; i < Armorhave.Length; i++)
        {
            if (Armorhave[i] == 0)
                ArmorSprite[i].color = new Color(0, 0, 0);
            else
                ArmorSprite[i].color = new Color(1, 1, 1);
        }
        for (int i = 0; i < Itemhave.Length; i++)
        {
            if (Itemhave[i] == 0)
                ItemSprite[i].color = new Color(0, 0, 0);
            else
                ItemSprite[i].color = new Color(1, 1, 1);
        }
    }

    public void CheckInven(int num)
    {
        SoundManager.instance.Click();
        Itemnum = num;

        if (num >= 300 && Itemhave[num - 300] == 1)
        {
            int nnum = num - 300;
            Inventext.text = ItemText[nnum];
        }
        else if (num >= 200 && num < 300 && Skillhave[num - 200] == 1)
        {
            int nnum = num - 200;
            Inventext.text = SkillText[nnum];
        }
        else if (num >= 100 && num < 200 && Armorhave[num - 100] == 1)
        {
            int nnum = num - 100;
            Inventext.text = ArmorText[nnum];
        }
        else if(num < 100 && Swordhave[num] == 1)
        {
            Itemnum = num;
            Inventext.text = SwordText[num];
        }
    }

    public void WearInven() //장착
    {
        SoundManager.instance.Click();
        GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (Itemnum >= 100)
        {
            int num = Itemnum - 100;
            if (Armorhave[num] == 1)
            {
                player.ArmorInven(num);
                player.Defense = ArmorDefense[num];
                Armornum = num;
                gamemanager.Armornum = Armornum;
            }
        }
        else
        {
            if ( Swordhave[Itemnum] == 1)
            {
                player.SwordInven(Itemnum);
                player.Power = SwordPower[Itemnum];
                Swordnum = Itemnum;
                gamemanager.Swordnum = Swordnum;
            }
        }
    }
    public void CMRA(int num)
    {
        CMconfiner.m_BoundingShape2D = confiner[num];
        if (Dungeon != null)
            Dungeon.GetComponent<DungeonManager>().DungeonEnter(num);
    }

    public void OpneInven(int num)
    {
        SoundManager.instance.Click();
        for (int i = 0; i< InvenObject.Length; i++)
        {
            if (i == num)
                InvenObject[i].SetActive(true);
            else
                InvenObject[i].SetActive(false);
        }
    }

    void PlayerMove()
    {
        Player.GetComponent<Animator>().SetBool("isWalk", true);
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(7, Player.GetComponent<Rigidbody2D>().velocity.y);
        Invoke("PlayerStop", 1f);
    }
    void PlayerStop()
    {
        Player.GetComponent<Animator>().SetBool("isWalk", false);
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Player.GetComponent<Rigidbody2D>().velocity.y);
    }
    void SetisNotClick3()
    {
        isNotClick = false;
        NPCCount[3] += 1;
        SoundManager.instance.Click();
    }
    void SetisNotClick4()
    {
        isNotClick = false;
        NPCCount[4] += 1;
        SoundManager.instance.Click();
    }
    void SetisNotClick5()
    {
        isNotClick = false;
        NPCCount[5] += 1;
        SoundManager.instance.Click();
    }
    public void IsidorGo()
    {
        NPC[5].SetActive(false);
        NPC[6].SetActive(true);
        NPC[6].GetComponent<Boss1>().Angry();
        NPC[7].SetActive(true);
        NPC[8].SetActive(true);
        Naration2("이시도르");
        audiosource[0].Stop();
        audiosource[1].Play();
    }
}
