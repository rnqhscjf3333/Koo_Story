using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Main : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] MainObject;
    public GameObject Sword;
    public GameObject Armor;
    public GameObject Boss;
    public GameObject[] back;
    public GameObject Light;
    public Manager manager;
    public ParticleSystem Dust;

    float LightColor;

    public Sprite[] SwordSprite;
    public Sprite[] AromrSprite;

    public GameObject[] GB;
    public GameObject[] NPCButton;
    public GameObject[] BossButton;
    public GameObject[] StaticButton;
    public bool isGo;
    public TextMeshProUGUI[] text;
    public TextMeshProUGUI[] text1;
    public TextMeshProUGUI[] text2;
    public string sentence;
    public Sprite sprite;

    public float position1;
    public float position2;
    public float position3;

    void Awake()
    {
        Invoke("PlayerStart", 2f);
        GameManager.Instance.Load();
    }

    // Update is called once per frame
    void Update()
    {
        Sword.GetComponent<SpriteRenderer>().sprite = SwordSprite[GameManager.Instance.Swordnum];
        if (GameManager.Instance.Quest[0] < 56)
        {
            Armor.GetComponent<SpriteRenderer>().sprite = AromrSprite[GameManager.Instance.Armornum];
        }
        else
        {
            Armor.GetComponent<SpriteRenderer>().sprite = AromrSprite[10];
            Boss.GetComponent<SpriteRenderer>().sprite = AromrSprite[11];
        }

        if (GameManager.Instance.Quest[0] < 56)
            Light.GetComponent<Light2D>().color = new Color(1, LightColor, LightColor);
        else
            Light.GetComponent<Light2D>().color = new Color(LightColor, 1, LightColor);

        LightColor = (Boss.transform.position.x) / 300;
        if(LightColor <= 0.017)
        {
            Player.GetComponent<Animator>().SetBool("isWalk", false);
            Dust.Stop();
        }
    }

    public void NewStart()
    {
        for (int i = 0; i < GameManager.Instance.Chest.Length; i++)
            GameManager.Instance.Chest[i] = false;
        for (int i = 0; i < GameManager.Instance.Key.Length; i++)
            GameManager.Instance.Key[i] = 0;
        for (int i = 0; i < GameManager.Instance.Quest.Length; i++)
            GameManager.Instance.Quest[i] = 0;
        for (int i = 0; i < GameManager.Instance.Swordhave.Length; i++)
            GameManager.Instance.Swordhave[i] = 0;
        for (int i = 0; i < GameManager.Instance.Armorhave.Length; i++)
            GameManager.Instance.Armorhave[i] = 0;
        for (int i = 0; i < GameManager.Instance.Skillhave.Length; i++)
            GameManager.Instance.Skillhave[i] = 0;
        GameManager.Instance.Swordnum = 0;
        GameManager.Instance.Armornum = 0;
        GameManager.Instance.Gold = 0;
        GameManager.Instance.StartPoint = 0;
        GameManager.Instance.Swordhave[0] = 1;
        GameManager.Instance.Armorhave[0] = 1;

        SoundManager.instance.Click();
        manager.Fade = 1;
        Invoke("NewStart1", 1f);
    }
    void NewStart1()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void PlayerStart()
    {
        Player.GetComponent<Animator>().SetBool("isWalk", true);
        Dust.Play();
    }

    public void LoadStart()
    {
        SoundManager.instance.Click();
        GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gamemanager.Load();
        manager.Fade = 1;
        if (gamemanager.Quest[0] >= 40)
            Invoke("GoToTTS", 2f);
        else if (gamemanager.Quest[0] >= 13)
            Invoke("GoToRobra", 2f);
        else
            Invoke("GoToDne", 2f);
    }

    void GoToTTS()
    {
        SceneManager.LoadScene("ttas");
    }
    void GoToRobra()
    {
        SceneManager.LoadScene("Robrah_Village");
    }

    void GoToDne()
    {
        SceneManager.LoadScene("Dne_Village");
    }

    public void Exit()
    {
        SoundManager.instance.Click();
        Application.Quit();
    }

    public void Collection1()
    {
        foreach (GameObject i in back)
            i.SetActive(true);
        foreach (GameObject i in MainObject)
            i.SetActive(true);
        Player.SetActive(true);
        SoundManager.instance.Click();
        GB[0].SetActive(false);
        Player.GetComponent<Animator>().SetBool("isWalk", true);
        Dust.Play();
    }

    public void Collection()
    {
        foreach (GameObject i in back)
            i.SetActive(false);
        foreach (GameObject i in MainObject)
            i.SetActive(false);
        Player.SetActive(false);
        SoundManager.instance.Click();
        GB[0].SetActive(true);


        //보스
        if (GameManager.Instance.Quest[0] >= 4)//꽃
        {
            BossButton[1].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[1].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[2] >= 3)//차우
        {
            BossButton[2].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[2].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[0] >= 6)//늑대
        {
            BossButton[3].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[3].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[3] >= 3)//늑대인간
        {
            BossButton[4].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[4].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[0] >= 9)//검은수염
        {
            BossButton[5].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[5].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[0] >= 11)//참치
        {
            BossButton[6].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[6].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Swordhave[8] ==1)//갑옷
        {
            BossButton[7].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[7].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[9] >= 1)//이시도르
        {
            BossButton[8].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[8].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[4] >= 2)//유령
        {
            BossButton[9].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[9].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[0] >= 24)//골렘
        {
            BossButton[10].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[10].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[0] >= 26)//슬라임
        {
            BossButton[11].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[11].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[0] >= 35)//용
        {
            BossButton[12].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[12].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Key[2] == 1)//고대병기
        {
            BossButton[13].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[13].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[6] >= 2)//광산
        {
            BossButton[14].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[14].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[0] >= 46)//솔져
        {
            BossButton[15].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[15].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[0] >= 48)//저주
        {
            BossButton[16].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[16].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[0] >= 51)//캐스커
        {
            BossButton[17].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[17].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[0] >= 53)//스태락
        {
            BossButton[18].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[18].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Quest[0] >= 56)//블랜더
        {
            BossButton[19].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[19].GetComponent<Button>().interactable = true;
        }
        if (GameManager.Instance.Key[3] >= 1)//드락사르
        {
            BossButton[20].GetComponent<Image>().color = new Color(1, 1, 1);
            BossButton[20].GetComponent<Button>().interactable = true;
        }

        if (GameManager.Instance.Quest[5] < 1)
        {
            text2[0].text = "??";
        }
    }

    public void NPC(int num)
    {
        SoundManager.instance.Click();
        if (num == 0)//마야
        {
            text[0].text = "마야";
            text[1].text = "다이스의 누나.";
            text[2].text = "??";
            text[3].text = "??";
            text[4].text = "??";
            text[5].text = "??";
            if(GameManager.Instance.Quest[0] > 12)
            {
                text[2].text = "아버지 ??는 마야가 태어니가 전에 전쟁을 치르기 위해 대륙으로 가서 돌아오지 않았다.";
                text[3].text = "어머니 세릴다는 집 앞에 놓여진 다이스를 거둬서 마야와 같이 키웠으며 갑작스러운 사고로 세상을 떠났다.";
                text[4].text = "??";
                text[5].text = "힘든 상황이였지만 다이스와 이겨냈으며 다이스가 아버지처럼 대륙으로 가서 돌아오지 않을까봐 두려웠지만 믿고 보내주기로 결정했다.";
            }
            if (GameManager.Instance.Quest[0] > 50)
            {
                text[2].text = "아버지 드락사르는 마야가 태어니가 전에 전쟁을 치르기 위해 대륙으로 가서 돌아오지 않았다.";
                text[3].text = "어머니 세릴다는 집 앞에 놓여진 다이스를 거둬서 마야와 같이 키웠으며 갑작스러운 사고로 세상을 떠났다.";
                text[4].text = "어머니의 죽음 이후에 트라우마로 검을 들지 못하게 되었다.";
                text[5].text = "힘든 상황이였지만 다이스와 이겨냈으며 다이스가 아버지처럼 대륙으로 가서 돌아오지 않을까봐 두려웠지만 믿고 보내주기로 결심했다.";
            }

        }
        if (num == 1)//촌장
        {
            text[0].text = "촌장 스탄";
            text[1].text = "드엔마을의 촌장.";
            text[2].text = "??";
            text[3].text = "??";
            text[4].text = "";
            text[5].text = "";
            if (GameManager.Instance.Quest[3] > 3)
            {
                text[4].text = "아내가 낳은 아기를 바위산에 버렸고 바위산에서 늑대아이를 만난 아내는 자신의 아이임을 알고 충격을 받아서 세상을 떠난다.";
            }
            if (GameManager.Instance.Quest[0] > 13)
                text[3].text = "드엔마을에서 태어나서 자랐으며 전쟁에 대한 두려움 때문에 드락사르와 함께 전쟁에 참가하지 못하고 마을에 남았다.";
            if (GameManager.Instance.Quest[0] >= 19)
                text[2].text = "전쟁 전 푸리에 대륙의 김박사와 편지를 주고받으며 친해졌으며 그 과정에서 알게 된 김박사의 여동생과 결혼했다.";
        }
        if (num == 2)//상점주인 아들
        {
            text[0].text = "없음";
            text[1].text = "상점주인의 아들.";
            text[2].text = "??";
            text[3].text = "??";
            text[4].text = "";
            text[5].text = "";
            if (GameManager.Instance.Quest[2] >= 3)
            {
                text[3].text = "어릴때 만난 멧돼지 차우와 친구가 됐다. 하지만 아버지가 차우에게 독화살을 쏴 버렸다.";
            }
            if (GameManager.Instance.Quest[0] >= 12)
            {
                text[2].text = "아버지에게 모든 것을 말했으며 다이스를 롤모델로 삼았다.";
            }
        }
        if (num == 3)//지도
        {
            text[0].text = "??";
            text[1].text = "??";
            text[2].text = "??";
            text[3].text = "";
            text[4].text = "";
            text[5].text = "";
            if (GameManager.Instance.Quest[1] >= 1)
            {
                text[0].text = "벤 게이츠";
                text[1].text = "보물 사냥꾼";
            }
            if (GameManager.Instance.Quest[1] >= 6)
            {
                text[2].text = "신 트레져 협회의 협회장";
            }
        }
        if (num == 4)//이시도르
        {
            text[0].text = "??";
            text[1].text = "??";
            text[2].text = "??";
            text[3].text = "??";
            text[4].text = "??";
            text[5].text = "??";
            if (GameManager.Instance.Quest[9] >= 1)
            {
                text[0].text = "이시도르";
                text[1].text = "전설의 조력자";
                text[2].text = "과거 스테락, 스테틱, 드락사르를 동경하여 친구인 시르케와 전쟁에 참가해서 승리로 이끌었다.";
                text[3].text = "우연히 찾아낸 하늘섬으로 갈 수 있는 돌을 세 용사에게 보고했지만 오히려 그 돌로 인해서 세 용사는 분열하게 된다.";
                text[4].text = "동경하던 용사들이 자신때문에 분열한 것을 보고 죄책감에 견딜 수 없어서 성을 떠났다.";
                text[5].text = "전 지역을 떠돌다가 발견한 광전사의 갑옷을 입고 나서부터 점점 기억을 잃으며 미쳐 가고 있다.";
            }
        }
        if (num == 5)//낚시꾼
        {
            text[0].text = "??";
            text[1].text = "??";
            text[2].text = "??";
            text[3].text = "??";
            text[4].text = "??";
            text[5].text = "";
            if (GameManager.Instance.Quest[0] >= 15)
            {
                text[0].text = "강태공";
                text[1].text = "낚시꾼";
                text[2].text = "푸리에 대륙의 어부 집안에서 태어났지만 집을 나와서 드엔마을에 왔다.";
                text[3].text = "낚시를 다시 하고 싶어서 가족들에게 가려고 하지만 전쟁 이후로 배가 없어서 대륙으로 갈 수가 없었다.";
                text[4].text = "??";
            }
            if (GameManager.Instance.Quest[0] >= 20)
            {
                text[4].text = "대륙으로 와서 동생이 자신 때문에 죽었다는 것을 깨닫는다. 망자의 바다에서 동생의 영혼을 보고 사과한다.";
            }
        }
        if(num == 6)//김박사
        {
            text[0].text = "??";
            text[1].text = "??";
            text[2].text = "??";
            text[3].text = "??";
            text[4].text = "??";
            text[5].text = "";
            if (GameManager.Instance.Quest[0] >= 19)
            {
                text[0].text = "김박사";
                text[1].text = "로브라 마을의 리더";
                text[2].text = "트타스 마을에서 태어나서 촌장스탄과 연락을 자주 했으며 여동생이 촌장스탄과 결혼했다. ";
                text[3].text = "전쟁 이후 이유모를 저주와 거대한 용을 피해서 트티스 마을 사람들을 이끌고 로브라 마을로 도망쳤다.";
                text[4].text = "로브라 마을을 위해서 노력하고 있는 중이다.";
            }
        }
        if (num == 7)//시몬
        {
            text[0].text = "??";
            text[1].text = "??";
            text[2].text = "??";
            text[3].text = "??";
            text[4].text = "";
            text[5].text = "";
            if (GameManager.Instance.Quest[0] >= 19)
            {
                text[0].text = "시몬";
                text[1].text = "굴착꾼";
            }
            if (GameManager.Instance.Quest[0] >= 30)
            {
                text[2].text = "로브라 마을에서 태어났으며 형을 거대한 용에게 잃었다.";
            }
            if (GameManager.Instance.Quest[0] >= 30)
            {
                text[3].text = "?? 거대한 용에게 살해당했다.";
            }
            if (GameManager.Instance.Quest[0] >= 54)
            {
                text[3].text = "블랜더의 명령에 의해서 하이 드래건에게 살해당했다.";
            }
        }
        if (num == 8)//예언가
        {
            text[0].text = "??";
            text[1].text = "??";
            text[2].text = "??";
            text[3].text = "??";
            text[4].text = "";
            text[5].text = "";
            if (GameManager.Instance.Quest[0] >= 19)
            {
                text[0].text = "시모나";
                text[1].text = "예언가";
                text[2].text = "예언은 전체적으로는 정확하지만 부분적으로는 부정확하다.";
                text[3].text = "구글에서 예언가 캐릭터 라고 검색하니까 이 캐릭터가 나왔습니다.";
            }
        }
        if (num == 9)//대장장이
        {
            text[0].text = "??";
            text[1].text = "??";
            text[2].text = "??";
            text[3].text = "??";
            text[4].text = "";
            text[5].text = "";
            if (GameManager.Instance.Quest[0] >= 19)
            {
                text[0].text = "토토사이";
                text[1].text = "대장장이";
                text[2].text = "중요한 역할을 위해서 넣은 약간 소모적인 캐릭터입니다.";
                text[3].text = "";
            }
        }
        if (num == 10)//로버트
        {
            text[0].text = "??";
            text[1].text = "??";
            text[2].text = "??";
            text[3].text = "??";
            text[4].text = "";
            text[5].text = "";
            if (GameManager.Instance.Quest[0] >= 41)
            {
                text[0].text = "로버트";
                text[1].text = "고고학자";
                text[2].text = "과거 김박사와 일했으며 김박사와 다르게 고향에 남았다. 저주를 피하기 위해서 지하에 숨어지낸다.";
                text[3].text = "이름을 왜 로버트라고 지었더라...";
            }
        }

        if (num == 100)//보스 마야
        {
            text1[0].text = "마야";
            text1[1].text = "체력 : 50";
            text1[2].text = "공격력 : 10";
            text1[3].text = "스킬 공격력 : 없음";
            text1[4].text = "골드 : 100";
            text1[5].text = "처음 만들때는 없었는데 뭔가 듀토리얼이 있어야 될 거 같아서 만만한 마야가 듀토리얼 보스로 임명되었습니다.";
        }
        if (num == 101)//거대한 꽃
        {
            text1[0].text = "거대한 꽃";
            text1[1].text = "체력 : 100";
            text1[2].text = "공격력 : 30";
            text1[3].text = "스킬공격력 : 10(촉수)";
            text1[4].text = "골드 : 100";
            text1[5].text = "첫 번째로 만든 보스. 원래 첫 보스인만큼 스킬은 안 만들려고 했는데 의식의 흐름대로 만들다 보니까 스킬이 생겼습니다.";
        }
        if (num == 102)//차우
        {
            text1[0].text = "차우";
            text1[1].text = "체력 : 150/1500";
            text1[2].text = "공격력 : 100";
            text1[3].text = "스킬 공격력 : 없음";
            text1[4].text = "골드 : ";
            text1[5].text = "처음으로 만든 퀘스트 보스. 플레이어의 방어구가 없으면 한방에 죽어서 플레이어가 방어구를 사도록 유도하려고 했습니다.";
        }
        if (num == 103)//늑대
        {
            text1[0].text = "거대한 늑대";
            text1[1].text = "체력 : 200, 100되면 늑대 3마리 소환함";
            text1[2].text = "공격력 : 40";
            text1[3].text = "스킬 공격력 : 30(점프)";
            text1[4].text = "골드 : 200";
            text1[5].text = "스킬이 원래 공중으로 날아가서 덮치는 식으로 만들려고 했는데 만들다보니까 그냥 점프공격으로 바꿨습니다.";
        }
        if (num == 104)//늑대아이
        {
            text1[0].text = "늑대 아이";
            text1[1].text = "체력 : 300*2";
            text1[2].text = "공격력 : 40";
            text1[3].text = "스킬 공격력 : 30(검기)";
            text1[4].text = "골드 : 500";
            text1[5].text = "지금까지의 보스와는 다르게 공격속도를 빠르게 만들고 각성패턴도 만들어 봤습니다.\n원래는 다른 보스부터 사용하려고 했는데 어쩌다 보니까 뼈대를 이용해서 만든 첫 번째 보스입니다.";
        }
        if (num == 105)//해적
        {
            text1[0].text = "검은 수염";
            text1[1].text = "체력 : 500, 250부터 대포 쏨";
            text1[2].text = "공격력 : 50";
            text1[3].text = "스킬 공격력 : 50(화약통, 대포)";
            text1[4].text = "골드 : 500";
            text1[5].text = "사실 원래 만들려고 계획하진 않았는데 항구에도 던전이 있어야 될 거 같아서 급하게 만들었습니다. \n원래는 이름과 생김새로 알 수 있겠지만 원피스 캐릭터를 만들려고 했는데 의식의 흐름으로 만들다 보니까 롤 캐릭터가 나와버렸습니다.";
        }
        if (num == 106)//참치
        {
            text1[0].text = "참치";
            text1[1].text = "체력 : 1000";
            text1[2].text = "공격력 : 60";
            text1[3].text = "스킬 공격력 : 60(물방울)";
            text1[4].text = "골드 : 500";
            text1[5].text = "중학생 때 누가 이 캐릭터를 참치라고 불러서 그대로 가져왔습니다.\n 원래 이 보스가 처음으로 뼈대를 이용해서 만드려고 했던 보스입니다.";
        }
        if (num == 107)//갑옷
        {
            text1[0].text = "마왕의 갑옷";
            text1[1].text = "체력 : 2000";
            text1[2].text = "공격력 : 100";
            text1[3].text = "스킬 공격력 : 없음";
            text1[4].text = "골드 : 0";
            text1[5].text = "1차 대전쟁 당시 마왕이 착용했던 갑옷. 마왕이 패하자 바위산에 숨어서 누군가가 마왕의 검을 가지고 올 날을 기다리고 있었다.\n" +
                "원래는 보통 보스들처럼 움직이게 만들었다가 딱히 개성이 없는 거 같아서 중앙에 고정시키고 양쪽에 잡몹들을 추가했습니다.";
        }
        if (num == 108)//이시도르
        {
            text1[0].text = "이시도르";
            text1[1].text = "체력 : 10000*2";
            text1[2].text = "공격력 : 500";
            text1[3].text = "스킬 공격력 : 500";
            text1[4].text = "골드 : 0";
            text1[5].text = "자신이 용사들을 분열시켰다는 죄책감에 성을 떠나면서 시르케에게 광전사의 갑주를 찾아서 강해진 후에 반드시 돌아오겠다고 약속했다.\n" +
                "하지만 광전사의 갑주를 입은 후에 갑옷에 씌어진 저주에 걸려서 미쳐버린 바람에 시르케와의 약속을 잊고 떠돌고 있다..\n" +
                "예전부터 설계한 캐릭터입니다. 생긴 걸 보면 아시다시피 원래 이름은 가츠였지만 시르케랑 엮이게 설정이 바뀌면서 이시도르로 바꿨습니다.";
        }
        if (num == 109)//루피
        {
            text1[0].text = "고인 메리호의 선장";
            text1[1].text = "체력 : 2000";
            text1[2].text = "공격력 : 100";
            text1[3].text = "스킬 공격력 : 없음";
            text1[4].text = "골드 : 1000";
            text1[5].text = "밀집모자를 쓰고 있습니다. 예전에 원피스 극장판의 등장인물 중에서 동료를 잃고 악당이 된 캐릭터를 보고 그대로 주인공에게 적용해 봤습니다.\n" +
                "배의 디자인은 캐리비안의 해적의 배와 원피스의 배가 둘 다 이름이 메리라 가져왔습니다.";
        }
        if (num == 110)//골렘
        {
            text1[0].text = "거대 골렘";
            text1[1].text = "체력 : 1000";
            text1[2].text = "공격력 : 150";
            text1[3].text = "스킬 공격력 : 150";
            text1[4].text = "골드 : 700";
            text1[5].text = "무려 원으로만 구성된 만든 보스입니다.\n" +
                "";
        }
        if (num == 111)//슬라임
        {
            text1[0].text = "용암 슬라임";
            text1[1].text = "체력 : 1500";
            text1[2].text = "공격력 : 100";
            text1[3].text = "스킬 공격력 : 150";
            text1[4].text = "골드 : 800";
            text1[5].text = "진짜로 의식의 흐름대로 만든 보스입니다.\n" +
                "처음에는 큰 슬라임으로 할려다가 골렘과 겹치는 거 같아서 작게 만들었고 스킬로 입을 벌린 걸 그리다가 안에 눈이 있으면 멋있을 거 같아서 눈을 그려 넣었습니다.";
        }
        if (num == 112)//용
        {
            text1[0].text = "용";
            text1[1].text = "체력 : 5000";
            text1[2].text = "공격력 : 100";
            text1[3].text = "스킬 공격력 : 200";
            text1[4].text = "골드 : 1000";
            text1[5].text = "참치가 1페이즈 보스였다면 용이 2페이즈 보스입니다.\n" +
                "급조한 다른 보스들과 다르게 처음부터 계획하고 있던 보슨데 스킬은... 역시 손이 가는대로 만들었습니다.";
        }
        if (num == 113)//고대병기
        {
            text1[0].text = "고대 병기";
            text1[1].text = "체력 : 10000";
            text1[2].text = "공격력 : 100";
            text1[3].text = "스킬 공격력 : 100";
            text1[4].text = "골드 : 1500";
            text1[5].text = "먼 과거에 파란 돌의 힘을 사용하는 천상인과 노란 돌의 힘을 사용하는 지하인이 서로 대립하고 있는 설정입니다.\n" +
                "고대 병기는 천상인이 지하인을 공격하기 위한 병기이고 노란 돌을 인식하면 지하인으로 간주해서 공격하는 겁니다.\n" +
                "과거에 천상인이 지하인을 공격하기 위해서 대량으로 고대 병기를 보냈고 그 중 한대가 작동을 멈춘 채로 있다가 노란 돌을 보인 다이스를 공격한다는 스토린데 역시 만들면서 급조한 설정들입니다...\n" +
                "모습은 예~전에 주인공이 작아지는 쯔꾸르 게임을 했는데 그 게임의 보스 중 하나가 기억에 남아서 기억을 더듬어서 그려 봤습니다.";
        }
        if (num == 114)//보석골렘
        {
            text1[0].text = "보석 골렘";
            text1[1].text = "체력 : 2000";
            text1[2].text = "공격력 : 150";
            text1[3].text = "스킬 공격력 : 150";
            text1[4].text = "골드 : 800";
            text1[5].text = "영롱한 빛을 뽐내는 보스입니다.\n" +
                "믿기지 않으시겠지만 던파의 하늘성에 있는 골렘을 보고 그렸습니다.";
        }
        if (num == 115)//솔져
        {
            text1[0].text = "군단장";
            text1[1].text = "체력 : 5000";
            text1[2].text = "공격력 : 200";
            text1[3].text = "스킬 공격력 : 100";
            text1[4].text = "골드 : 0";
            text1[5].text = "왕을 지키기로 맹세한 병사입니다.\n" +
                "저주에 걸렸지만 왕을 지켜야 한다는 생각만 남아서 성을 지키는 불쌍한 존재죠";
        }
        if (num == 116)//저주
        {
            text1[0].text = "저주의 집합체";
            text1[1].text = "체력 : 5000";
            text1[2].text = "공격력 : 200";
            text1[3].text = "스킬 공격력 : 100";
            text1[4].text = "골드 : 0";
            text1[5].text = "말 그대로 저주들이 모인 집합체입니다.\n" +
                "눈 하나씩 기본공격 포함해서 4개의 스킬을 가지게 만들려고 했는데 급하게 만들다 보니까 스킬이 3개만 있네요.";
        }
        if (num == 117)//마법사
        {
            text1[0].text = "시르케";
            text1[1].text = "체력 : 5000";
            text1[2].text = "공격력 : 0";
            text1[3].text = "스킬 공격력 : 100";
            text1[4].text = "골드 : 0";
            text1[5].text = "대마법사의 딸로서 숲속에서 숨어살다가 우연히 이시도르를 만나서 사랑에 빠졌다.\n" +
                "싸움을 싫어했지만 이시도르가 전쟁에 참전하자 그를 지키기 위해서 같이 참전한다.\n" +
                "둘은 전설의 용사들을 도와서 활약했고 전설의 조력자라는 별명을 얻는다.\n" +
                "전쟁 후에 모두 성을 떠났지만 유일하게 스태락 곁에 남아서 반드시 돌아오겠다던 이시도르를 기다리고 있다.";
        }
        if (num == 118)//마왕
        {
            text1[0].text = "스태락";
            text1[1].text = "체력 : 5000*2";
            text1[2].text = "공격력 : 100";
            text1[3].text = "스킬 공격력 : 100";
            text1[4].text = "골드 : 0";
            text1[5].text = "영원히 고통받는 스태락입니다.\n" +
                "세번째 페이즈로 성 옥상으로 올라가서 해방된 마왕과 싸우는 그림을 생각했는데 다음 보스를 빨리 만들고 싶어서 스킵했습니다.\n" +
                "모티브는 초등학생 때 투니버스에서 메르헤븐이라는 만화를 봤는데 최종보스가 너무 멋있던게 아직도 기억에 남아서 거기서 따왔습니다.";
        }
        if (num == 119)//블랜더
        {
            text1[0].text = "블랜더";
            text1[1].text = "체력 : 5000*4";
            text1[2].text = "공격력 : 300";
            text1[3].text = "스킬 공격력 : 300~500";
            text1[4].text = "골드 : 0";
            text1[5].text = "최종보스인 맥시아입니다.\n" +
                "제가 좋아하는 캐릭터의 표본입니다. 고난을 겪었지만 노오력을 해서 극복해내고 뛰어난 두뇌와 강력한 육체를 가지고 있습니다.";
        }
        if (num == 120)//드락
        {
            text1[0].text = "드락사르";
            text1[1].text = "체력 : 10000";
            text1[2].text = "공격력 : 300";
            text1[3].text = "스킬 공격력 : 300";
            text1[4].text = "골드 : 0";
            text1[5].text = "전설의 용사 드락사르입니다..\n" +
                "드락사르를 처음에는 멋있게 생각했는데 만들다 보니까 본의아니게 이상적인 인물은 아니게 바꿔버렸습니다.";
        }
    }
    public void CollectionSellect(int num) //NPC랑 boss 선택
    {
        if(num == 0)
        {
            GB[1].SetActive(true);
            GB[2].SetActive(false);
            GB[3].SetActive(false);

        }
        if (num == 1)
        {
            GB[1].SetActive(false);
            GB[2].SetActive(true);
            GB[3].SetActive(false);

        }
        if (num == 2)
        {
            GB[1].SetActive(false);
            GB[2].SetActive(false);
            GB[3].SetActive(true);
        }

    }

    public void VillageSellect(float num) //드래그
    {
        if(position1 != num)
        {
            foreach (GameObject i in NPCButton)
            {
                if(i!=null)
                    i.transform.position = i.transform.position + Vector3.left * (num - position1) * 20;
            }
            position1 = num;
        }
    }
    public void BossSellect(float num) //드래그
    {
        if (position2 != num)
        {
            foreach (GameObject i in BossButton)
            {
                if (i != null)
                    i.transform.position = i.transform.position + Vector3.left * (num - position2) * 60;
            }
            position2 = num;
        }
    }
    public void StaticSellect(float num) //드래그
    {
        if (position3 != num)
        {
            foreach (GameObject i in StaticButton)
            {
                if (i != null)
                    i.transform.position = i.transform.position + Vector3.up * (num - position3) * 5;
            }
            position3 = num;
        }
    }


}
