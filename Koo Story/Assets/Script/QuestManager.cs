
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuestManager : MonoBehaviour
{

    public int[] Quest;//대화 0:메인 1:보물 2:차우 3:늑대아이
    public string[] sentences; //대화문
    public GameObject GameManager;
    public Manager manager;
    GameManager gamemanager;

    public Sprite[] QuestIcon; //0 : !, 1 : ?
    public GameObject[] QuestAlarm; //0:마야 1:촌장 2:보물 3:상점아들 4:낚시꾼 5:스태틱의 일지 6:대장장이 7:편지 8:드락사르 9:이시도르

    public bool isDungeon; //던전인지


    void Awake()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Quest = gamemanager.Quest; //매니저에서 가져옴

    }


    // 됬 쫓 안됨
    void Update()
    {
        if(isDungeon && manager != null && manager.GetComponent<Manager>().PointName == "마을 뒷마당")
        {

            if (Quest[0] == 0) //마야
            {
                QuestAlarm[0].SetActive(true);
                QuestAlarm[0].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else
                QuestAlarm[0].SetActive(false);
            if (Quest[0] == 1) //마야
            {
                QuestAlarm[1].SetActive(true);
                QuestAlarm[1].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[1].SetActive(false);
        }

        
        if (!isDungeon && manager != null && manager.GetComponent<Manager>().PointName == "드엔 마을")
        {
            if (gamemanager.Quest[9] == 2) //이시도르
            {
                QuestAlarm[6].SetActive(false);
            }
            if (Quest[0] == 20) //마야
            {
                QuestAlarm[5].SetActive(false);
            }

            if (Quest[0] == 0 || Quest[0] == 12) //마야
            {
                QuestAlarm[0].SetActive(true);
                QuestAlarm[0].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }//마야
            else if (Quest[0] == 4 || Quest[0] == 2 || Quest[0] == 6)
            {
                QuestAlarm[0].SetActive(true);
                QuestAlarm[0].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[0].SetActive(false);

            if (Quest[0] == 1 || (Quest[3] == 0 && Quest[0] >= 7) || Quest[0] == 13) //촌장
            {
                QuestAlarm[1].SetActive(true);
                QuestAlarm[1].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }//촌장
            else if (Quest[3] == 2)
            {
                QuestAlarm[1].SetActive(true);
                QuestAlarm[1].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[1].SetActive(false);

            if (Quest[1] == 0) //보물
            {
                QuestAlarm[2].SetActive(true);
                QuestAlarm[2].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }//보물
            else if (Quest[1] == 1 && gamemanager.Chest[5] == true && gamemanager.Chest[1] == true && gamemanager.Chest[2] == true && gamemanager.Chest[3] == true && gamemanager.Chest[4] == true)
            {
                QuestAlarm[2].SetActive(true);
                QuestAlarm[2].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[2].SetActive(false);

            if (Quest[0] >= 5 && Quest[2] == 0) //상점아들
            {
                QuestAlarm[3].SetActive(true);
                QuestAlarm[3].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else if (Quest[2] == 2)
            {
                QuestAlarm[3].SetActive(true);
                QuestAlarm[3].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[3].SetActive(false);

            if (Quest[0] == 7) //강태공
            {
                QuestAlarm[4].SetActive(true);
                QuestAlarm[4].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else if (Quest[0] == 9 || Quest[0] == 14)
            {
                QuestAlarm[4].SetActive(true);
                QuestAlarm[4].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[4].SetActive(false);
        }
        if (!isDungeon && manager != null && manager.GetComponent<Manager>().PointName == "로브라 마을")
        {
            if (Quest[1] == 2) //보물
            {
                QuestAlarm[15].SetActive(true);
                QuestAlarm[15].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else if (Quest[1] == 3 && gamemanager.Chest[6] == true && gamemanager.Chest[7] == true && gamemanager.Chest[8] == true && gamemanager.Chest[9] == true && gamemanager.Chest[10] == true)
            {
                QuestAlarm[15].SetActive(true);
                QuestAlarm[15].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[15].SetActive(false);

            if (Quest[0] == 15 || Quest[0] == 19 || Quest[0] == 24 || Quest[0] == 26) //시몬
            {
                QuestAlarm[10].SetActive(true);
                QuestAlarm[10].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else if (Quest[0] == 22)
            {
                QuestAlarm[10].SetActive(true);
                QuestAlarm[10].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[10].SetActive(false);

            if (Quest[0] == 17 || Quest[0] == 31 || Quest[0] == 36) //김박사
            {
                QuestAlarm[11].SetActive(true);
                QuestAlarm[11].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else if (Quest[0] == 18 || Quest[7] == 1 || Quest[7] == 3)
            {
                QuestAlarm[11].SetActive(true);
                QuestAlarm[11].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[11].SetActive(false);

            if (Quest[0] == 16 || Quest[0] ==27 || Quest[0] == 30 || Quest[0] == 37 || Quest[7] == 4) //예언가
            {
                QuestAlarm[12].SetActive(true);
                QuestAlarm[12].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else
                QuestAlarm[12].SetActive(false);

            if (Quest[0] >= 20 && Quest[4] ==0) //강태공2
            {
                QuestAlarm[13].SetActive(true);
                QuestAlarm[13].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else if (Quest[4] == 2)
            {
                QuestAlarm[13].SetActive(true);
                QuestAlarm[13].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[13].SetActive(false);

            if (Quest[0] >= 30) //시몬 묘
            {
                QuestAlarm[1].SetActive(false);
                QuestAlarm[0].GetComponent<SpriteRenderer>().sprite = QuestAlarm[0].GetComponent<NPC>().sprites[0];
                QuestAlarm[0].GetComponent<NPC>().sentences[0] = "시몬의 묘비다.";
                QuestAlarm[0].GetComponent<NPC>().sentences[1] = "마음이 먹먹해진다.";
            }
            if (gamemanager.Key[2] == 1) //드락사르
            {
                QuestAlarm[20].SetActive(false);
                QuestAlarm[21].SetActive(true);
            }

            if ((Quest[0] >= 24 && Quest[6] == 0)) //대장장이
            {
                QuestAlarm[14].SetActive(true);
                QuestAlarm[14].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else if  (Quest[6] == 2 || (gamemanager.Swordhave[8] == 1 && gamemanager.Key[3] == 1 && Quest[6] == 3))
            {
                QuestAlarm[14].SetActive(true);
                QuestAlarm[14].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[14].SetActive(false);

        }
        if (isDungeon && manager != null && manager.GetComponent<Manager>().PointName == "절벽")
        {

            if (Quest[0] == 20 || Quest[0] == 21 || Quest[0] == 29 || Quest[0] == 32 || Quest[0] == 35) //블랜더
            {
                QuestAlarm[0].SetActive(true);
                QuestAlarm[0].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else if (Quest[0] == 33 || Quest[0] == 38 || Quest[0] == 39)
            {
                QuestAlarm[0].SetActive(true);
                QuestAlarm[0].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[0].SetActive(false);

            if (Quest[0] >= 41) //블랜더
            {
                QuestAlarm[1].SetActive(false);
            }
        }
        if (isDungeon && manager != null && manager.GetComponent<Manager>().PointName == "산 중심부")
        {

            if (Quest[0] == 20 || Quest[0] == 21 || Quest[0] == 29) //블랜더
            {
                QuestAlarm[0].SetActive(true);
                QuestAlarm[0].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else
                QuestAlarm[0].SetActive(false);

            if (Quest[0] == 28) //죽은 시몬
            {
                QuestAlarm[1].SetActive(true);
                QuestAlarm[1].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else
                QuestAlarm[1].SetActive(false);
        }
        if (!isDungeon && manager != null && manager.GetComponent<Manager>().PointName == "트타스 마을")
        {

            if (Quest[0] == 40 || (Quest[0] >= 43 && Quest[7] == 0 || Quest[7] == 2)) //로버트
            {
                QuestAlarm[0].SetActive(true);
                QuestAlarm[0].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else if (Quest[0] == 41 || Quest[0] == 42 || Quest[7] == 5)
            {
                QuestAlarm[0].SetActive(true);
                QuestAlarm[0].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[0].SetActive(false);

            if (Quest[0] == 44) //블랜더
            {
                QuestAlarm[1].SetActive(true);
                QuestAlarm[1].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else if (Quest[0] == 46 || Quest[0] == 48 || Quest[0] == 51)
            {
                QuestAlarm[1].SetActive(true);
                QuestAlarm[1].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[1].SetActive(false);

            if (Quest[1] == 4) //보물3
            {
                QuestAlarm[2].SetActive(true);
                QuestAlarm[2].GetComponent<SpriteRenderer>().sprite = QuestIcon[0];
            }
            else if (Quest[1] == 5 && gamemanager.Chest[11] ==true)
            {
                QuestAlarm[2].SetActive(true);
                QuestAlarm[2].GetComponent<SpriteRenderer>().sprite = QuestIcon[1];
            }
            else
                QuestAlarm[2].SetActive(false);
        }
    }

    public string[] QuestCheck(int NPCnum, GameObject NPCManager)
    {
         if (NPCnum == -10) //책상들
        {
            if (manager.GetComponent<Manager>().PointName == "드엔 마을")
            {
                if (Quest[3] == 3)
                {
                    System.Array.Resize(ref sentences, 18);
                    sentences[0] = "낡은 책상에 촌장님의 일기장이 있다.";
                    sentences[1] = "첫 장을 펼쳤다.";
                    sentences[2] = "아마 내일쯤 사랑하는 아내의 아이가  \n태어날 것 같다.";
                    sentences[3] = "그 아이는 나를 이어서 드엔마을의       \n촌장이 될 것이다.";
                    sentences[4] = "다음 장을 펼쳤다.";
                    sentences[5] = "생각지도 못한 문제가 생겼다.";
                    sentences[6] = "아들이 태어났지만 외모가 마치 악마의 저주를 받은 듯이 뒤틀린 것이 아닌가.";
                    sentences[7] = "아내에게는 걱정하지 말라고 했지만    \n고민이 된다.";
                    sentences[8] = "다음 장을 펼쳤다.";
                    sentences[9] = "결정을 내렸다. 마을사람들에게는 아이가 태어나자마자 죽었다고 한 뒤";
                    sentences[10] = "바위산까지 가서 아들을 버리고 왔다.";
                    sentences[11] = "아내는 반대했지만... 어쩔 수 없었다.   \n차마 죽이지는 못했다.";
                    sentences[12] = "아들은 살아남지 못할 것이고 아무       \n문제도 없을 것이다...";
                    sentences[13] = "... 맨 뒤로 넘겨서 마지막 장을 펼쳤다.";
                    sentences[14] = "오늘도 평소처럼 아내가 마을 밖으로      \n산책을 나갔지만 평소보다 늦게 왔다.";
                    sentences[15] = "그리고 몸을 떨더니 저녁에 쓰러져서     \n다시 일어나지 못하고 있다.";
                    sentences[16] = "대체 마을 밖에서 무엇을 본 것일까...";
                    sentences[17] = " 컬렉션에 NPC에 대한 새로운 정보가 추가되었다.";
                    Quest[3] = 4;
                    return sentences;
                }
                else
                {
                    System.Array.Resize(ref sentences, 17);
                    sentences[0] = "낡은 책상에 촌장님의 일기장이 있다.";
                    sentences[1] = "첫 장을 펼쳤다.";
                    sentences[2] = "아마 내일쯤 사랑하는 아내의 아이가  \n태어날 것 같다.";
                    sentences[3] = "그 아이는 나를 이어서 드엔마을의       \n촌장이 될 것이다.";
                    sentences[4] = "다음 장을 펼쳤다.";
                    sentences[5] = "생각지도 못한 문제가 생겼다.";
                    sentences[6] = "아들이 태어났지만 외모가 마치 악마의 저주를 받은 듯이 뒤틀린 것이 아닌가.";
                    sentences[7] = "아내에게는 걱정하지 말라고 했지만    \n고민이 된다.";
                    sentences[8] = "다음 장을 펼쳤다.";
                    sentences[9] = "결정을 내렸다. 마을사람들에게는 아이가 태어나자마자 죽었다고 한 뒤";
                    sentences[10] = "바위산까지 가서 아들을 버리고 왔다.";
                    sentences[11] = "아내는 반대했지만... 어쩔 수 없었다.   \n차마 죽이지는 못했다.";
                    sentences[12] = "아들은 살아남지 못할 것이고 아무       \n문제도 없을 것이다...";
                    sentences[13] = "... 맨 뒤로 넘겨서 마지막 장을 펼쳤다.";
                    sentences[14] = "오늘도 평소처럼 아내가 마을 밖으로      \n산책을 나갔지만 평소보다 늦게 왔다.";
                    sentences[15] = "그리고 몸을 떨더니 저녁에 쓰러져서     \n다시 일어나지 못하고 있다.";
                    sentences[16] = "대체 마을 밖에서 무엇을 본 것일까...";
                    return sentences;
                }
            }
            else if (manager.GetComponent<Manager>().PointName == "절벽")
            {
                if (Quest[5] == 0)
                {
                    System.Array.Resize(ref sentences, 3);
                    sentences[0] = "낡은 책상에 누군가가 쓴 글이 있다.";
                    sentences[1] = "글의 끝에는 작게 스태틱 이라고 \n적혀있다.";
                    sentences[2] = " 컬렉션의 스태틱의 일지에 새로운 정보가 추가되었다.";
                    Quest[5] = 1;
                    return sentences;
                }
                else
                {
                    System.Array.Resize(ref sentences, 2);
                    sentences[0] = "낡은 책상에 누군가가 쓴 글이 있다.";
                    sentences[1] = "글의 끝에는 작게 스태틱 이라고 \n적혀있다.";
                    return sentences;
                }
            }
            else
                return null;
        }
        else if (NPCnum == 1) //마야
        {
            if (Quest[0] == 0) //메인퀘스트가 0일경우
            {
                System.Array.Resize(ref sentences, 4);
                sentences[0] = "안녕~ 좋은 아침이야";
                sentences[1] = "오늘이 너가 성인이 되는 날인거 알지?";
                sentences[2] = "옆에 계신 촌장님께 가서 말하렴";
                sentences[3] = "촌장님께서 마을 밖으로 나갈 수 있게\n 허락하실거야";
                Quest[0] = 1;
                return sentences;
            }
            else if (Quest[0] == 1) //메인퀘스트가 1일경우
            {
                System.Array.Resize(ref sentences, 4);
                sentences[0] = "응?";
                sentences[1] = "촌장님은 오른쪽으로 가면 계셔";
                sentences[2] = "아마 오늘도 아내의 묘 앞에 나와계실거야";
                sentences[3] = "촌장님께 오늘부터 성인이라고 \n말하면 될거야~";
                return sentences;
            }
            else if (Quest[0] == 2)
            {
                System.Array.Resize(ref sentences, 6);
                sentences[0] = "표정을 보니 허락을 받았구나~";
                sentences[1] = "마을밖으로 나가는건 처음이니까\n 조심해야되";
                sentences[2] = "마을 앞의 꽃밭부터 산책하면 좋을거야~";
                sentences[3] = "만약 장비를 바꾸고 싶으면 옆에 있는\n 우리 집에 들렀다 가면 되~";
                sentences[4] = "저장은 ESC버튼을 눌러서 일시정지\n 메뉴로 가면 할 수 있어.";
                sentences[5] = " 새로운 던전이 열렸다.[꽃밭]";
                Quest[0] = 3;
                return sentences;
            }
            else if (Quest[0] == 3)
            {
                System.Array.Resize(ref sentences, 2);
                sentences[0] = "오른쪽 끝으로 가면 마을 밖으로 \n나갈수 있어";
                sentences[1] = "다시 말하지만 조심해야되~";
                return sentences;
            }
            else if (Quest[0] == 4)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "오~ 벌써 다녀왔구나~ \n마을밖으로 나가니까 좋지?";
                sentences[1] = "골드가 어느정도 모였으면 \n상점아저씨한테 가보는게 좋을거야";
                sentences[2] = "자신이 있으면 다음 장소인 바위산으로 가도 좋아. 근데 촌장님 말로는 요즘\n 늑대가 부쩍 많아졌대.";
                sentences[3] = "갑자기 늑대들의 서식지가 넓어졌다는데. \n무슨 일이 있는걸까?";
                sentences[4] = " 새로운 던전이 열렸다.[바위산]";
                sentences[5] = "아 참! 얘기가 나와서 \n그런데 상점아저씨네 아들이 \n고민이 있는거 같아.";
                sentences[6] = "아마 꽃밭이랑 관련된 일인거 같은데 \n상점아저씨네 아들을 돕거나 다음장소인 바위산으로 가거나 너의 선택이야~";
                Quest[0] = 5;
                return sentences;
            }
            else if (Quest[0] == 5)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "다음 장소로 가고싶으면 바위산으로\n 가면 되고 마을사람을 돕고싶으면\n 상점주인 아들한테 가면 돼~";
                return sentences;
            }
            else if (Quest[0] == 6)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "와! 벌써 왔구나~";
                sentences[1] = "혹시 마을 밖으로 갔는데 생각보다\n 재밌지 않으면 실망할까 걱정했는데 \n잘 맞나 보네~";
                sentences[2] = "하지만 아마 더 이상은 밖으로 \n못 나갈 거야.";
                sentences[3] = "더 밖으로 나가면 항구가 있어. 과거에는 대륙으로 많이 오고갔는데 전쟁 이후로는 오고가는 사람이 없어서 폐쇄됐었어.";
                sentences[4] = "근데 소문으로는 산적들이 산에 살다가 늑대에 밀려서 항구까지 간 김에 아예\n 정착을 했다고 하더라고.";
                sentences[5] = "성격이 흉포해서 마을 사람들은 얼씬도 못하는데 정 항구를 가고 싶다면...\n 먼저 어부 아저씨를 찾아가는 게 \n좋을거야~";
                sentences[6] = "그리고 촌장님께서 너를 찾으시더라고~";
                Quest[0] = 7;
                return sentences;
            }
            else if (Quest[0] == 7)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "항구에 갈 때는 조심해야 되~";
                return sentences;
            }
            else if (Quest[0] == 12)
            {
                System.Array.Resize(ref sentences, 11);
                sentences[0] = "다이스. 이제는 너에게 말해야 될 거 \n같아.";
                sentences[1] = "사실 너는 이 드엔 마을 출생이 아니야.";
                sentences[2] = "갓난아기였던 너가 마을 앞에 놓여진\n 것을 내 어머니께서 발견하시고\n 키우신거지.";
                sentences[3] = "어머니께서는 너를 친자식처럼 생각했고 나 역시 너를 친동생으로 생각하고 있어.";
                sentences[4] = "아버지께서는 내가 태어나기 전에 전쟁을 치르기 위해서 대륙으로 가서 돌아오지 \n않으셨고 어머니께서는... 내가 어릴 때\n 갑작스러운 사고로 돌아가셔서 슬펐지만 다이스. 너가 있어서 힘을 낼 수 있었어..";
                sentences[5] = "그만큼 너는 내게 소중한 존재이고\n 내 옆에 있었으면 좋겠지만 개인적으로\n 사람들마다 가야 하는 길은 모두\n 다르다고 생각해.";
                sentences[6] = "나는 마을에 남아있을테니까...\n 너는 신경쓰지 말고 마음껏 여행했으면 좋겠어.";
                sentences[7] = "강태공 아저씨한테 가기 전에 촌장님께 \n들리렴. 분명 유익한 정보를 주실거야.";
                sentences[8] = "그리고 마지막으로... 몸조심해.";
                sentences[9] = "너는 소중한 내 동생이니까.";
                sentences[10] = " 컬렉션에 NPC에 대한 새로운 정보가 추가되었다.";
                Quest[0] = 13;
                return sentences;
            }
            else if (Quest[0] >= 13)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "몸조심해.";
                return sentences;
            }
            else
                return null;
        }
        if (NPCnum == 2) //촌장
        {
            if (Quest[0] == 1) //메인퀘스트가 1일경우
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "오 오랫만이구나";
                sentences[1] = "이 드엔마을에서 너를 처음 만났을때가 엊그제같은데 벌써 성인이 되다니 \n놀랍구나";
                sentences[2] = "좀 더 챙겨줬어야 되는데... ";
                sentences[3] = "아내가 갑자기 세상을 떠난뒤로 \n잘 챙겨주지 못한거 같아서 미안하구나";
                sentences[4] = "어쨋든 이제 성인이니 마을 밖으로 \n나가도 좋다!";
                sentences[5] = "마을밖은 위험하니 나갈일이 있으면 \n몸조심하거라";
                sentences[6] = "아 참! 마야가 너에게 할 말이 있다고 \n하니 돌아가 보렴";
                Quest[0] = 2;
                return sentences;
            }
            else if (Quest[0] == 13)
            {
                System.Array.Resize(ref sentences, 18);
                sentences[0] = "다이스. 마야에게 대강 이야기는 들었지?";
                sentences[1] = "대륙에 가면 중심부에 \n트타스 마을이 있단다.";
                sentences[2] = "그리고 그 마을에는 전쟁 전에 \n나와 친분을 쌓았던 \n김박사라는 사람이 있을게다.";
                sentences[3] = "아마 내 이름을 대면 살 곳을 제공해주고 그곳의 상황을 자세히 알려줄 거다.";
                sentences[4] = "그리고 그에게 이 편지를 \n전해주면 좋겠단다.";
                sentences[5] = " 촌장님의 편지를 받았다.";
                sentences[6] = "나에게는 전쟁때부터 \n하나의 의문점이 있단다.";
                sentences[7] = "마을에 전시된 영웅 드락사르의 동상을 보면 알겠지만";
                sentences[8] = "30년 전에 드락사르는 이 로랑 섬에서 \n용족을 몰아내고 푸리에 대륙으로\n 가서 대규모 반란을 일으켰단다.";
                sentences[9] = "나 역시 따라가고 싶었지만 나이도 \n어리고 무서워서 마을에 남았지.";
                sentences[10] = "그리고 드락사르는 대륙에서 용족과 \n전쟁을 벌여서 승리함으로서";
                sentences[11] = "전설의 세 용사 중 한명이 되었고 \n현재까지도 영웅으로서 많은 사람들에게 추앙을 받고 있지.";
                sentences[12] = "당시에 우리 마을 역시 영웅이 된 \n드락사르의 동상을 만들고 그의 빛나는 귀환을 기다렸단다.";
                sentences[13] = "하지만.... 그는 돌아오지 않았어.";
                sentences[14] = "우리 마을의 배는 모두 젊은 사람들이 \n전쟁을 위해 푸리에 대륙으로 타고가서 \n돌아오지 않았기 때문에 확인할 방도가 없었단다.";
                sentences[15] = "그래서 드락사르가 왜 돌아오지 않는지 확인해서 알려줬으면 좋겠구나.";
                sentences[16] = "강태공에게 가면 푸리에 대륙으로 갈 수 있을게다.";
                sentences[17] = " 컬렉션에 NPC에 대한 새로운 정보가 추가되었다.";
                Quest[0] = 14;
                return sentences;
            }
            else if (Quest[0] >= 7 && Quest[3] == 0)
            {
                System.Array.Resize(ref sentences, 11);
                sentences[0] = "오 마을 밖은 어떻니? \n생각만큼 재밌니?";
                sentences[1] = "네가 성인이 된 지는 얼마 안됐지만 \n마을 주민들과 다르게 외향적이어서 \n마을 밖을 잘 다니는구나.";
                sentences[2] = "최근에는 늑대들이 사는 바위산까지\n 갔다왔다는데 참 대견스럽구나.";
                sentences[3] = "... 미안하지만 부탁할 일이 있구나.";
                sentences[4] = "내 아내는 나는 물론 마을 주민들과도\n 다르게 마을 밖을 자주 드나들었단다.";
                sentences[5] = "그런데 어느 날 마을 밖을 나갔다 오더니 뭔가 이상하더구나.";
                sentences[6] = "몸을 떨더니 그대로 정신을 \n잃고 쓰러졌단다.";
                sentences[7] = "그리고 다시 정신을 차리지 못하고 \n세상을 떠나고 말았지...";
                sentences[8] = "나는 아내의 죽음이 마을 바깥에서의 \n무언가와 관계가 있을 것이라고 \n생각한단다.";
                sentences[9] = "그러니 혹시 마을 밖에서 아내와 관련된 것을 본다면 다른 사람들 말고 바로 \n나에게 와서 말해주렴.";
                sentences[10] = " 새로운 던전이 열렸다.[바위산 정상]";
                Quest[3] = 1;
                return sentences;
            }
            else if (Quest[3] == 1)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "바위산 근처에서 아내와 관련된 것을\n발견하면 바로 알려다오.";
                return sentences;
            }
            else if (Quest[3] == 2)
            {
                System.Array.Resize(ref sentences, 9);
                sentences[0] = "응? 바위산 정상에서 늑대 탈을 뒤집어쓴 누군가를 봤다고?";
                sentences[1] = "내 얘기를 했다고...?";
                sentences[2] = "...! 설마!";
                sentences[3] = "아! 잠깐 생각했는데.";
                sentences[4] = "그 사람이... 누군지는 모르겠구나. \n 나랑은 관련이 없어!";
                sentences[5] = " 당황한 촌장님 옷에서 열쇠 하나가 떨어졌다.";
                sentences[6] = " 새로운 아이템을 획득했다.[촌장의 열쇠]";
                sentences[7] = "아내의 죽음에 대한 진실은 모르겠지만 여기서 끝내는게 좋겠구나. \n어쩌면 처음부터 내 착각이었을수도..";
                sentences[8] = " 컬렉션에 NPC에 대한 새로운 정보가 추가되었다.";
                Quest[3] = 3;
                GameObject.Find("GameManager").GetComponent<GameManager>().Key[0] = 1;
                return sentences;
            }
            else if (Quest[3] >= 3 && Quest[0] <= 12)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "설마... 그 아이가... \n아닐거야...";
                return sentences;
            }
            else if (Quest[0] >= 14)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "드락사르에 대해서 뭔가 알게되면 \n꼭 알려다오.";
                return sentences;
            }
            else
                return null;
        }
        else if (NPCnum == 3) //촌장집문
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().Key[0] == 1) //열쇠를 가지고 있을 경우
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "문이 열렸다.";
                NPCManager.GetComponent<SpriteRenderer>().sprite = NPCManager.GetComponent<NPC>().sprites[0];
                NPCManager.layer = 12;
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 4)) //보물
        {
            if (Quest[1] == 0)
            {
                System.Array.Resize(ref sentences, 8);
                sentences[0] = "흠... 지도를 보면 마지막 보물상잔데 \n이것도 아니야...";
                sentences[1] = "응? 안녕! 나는 보물 사냥꾼 \n벤 게이츠야.";
                sentences[2] = "보물상자에 들어있는 걸 가져가도 \n되냐고? 물론이지!";
                sentences[3] = "아마도 드엔마을에 있는 모든 \n보물상자를 찾은 거 같은데 내가 원하는 보물은 안나왔단 말이야...";
                sentences[4] = "내가 찾던 보물이 없으면 보물상자는 \n그대로 놓으니까 너가 가져가도 좋아! \n만약 보물상자를 열고 내용물을 \n안 가져가면 다른사람이 가져갈 테니까 주의해!";
                sentences[5] = "내가 드엔마을에서 찾은 보물은\n 총 5개니까 잘 찾아보라구!";
                sentences[6] = "만약 5개의 보물상자를 모두 찾는다면\n 너에게 선물을 줄게!";
                sentences[7] = " 컬렉션에 NPC에 대한 새로운 정보가 추가되었다.";
                Quest[1] = 1;
                return sentences;
            }
            if (Quest[1] == 1)
            {
                gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
                if (gamemanager.Chest[5] == true && gamemanager.Chest[1] == true && gamemanager.Chest[2] == true && gamemanager.Chest[3] == true && gamemanager.Chest[4] == true)
                {
                    System.Array.Resize(ref sentences, 5);
                    sentences[0] = "오! 보물상자 다섯개를 찾은거야??";
                    sentences[1] = "대단한데! 어쩌면 보물사냥꾼의 \n재능이 있을지도?";
                    sentences[2] = "너를 인정한다는 의미로 골드를 좀 \n나눠줄게.";
                    sentences[3] = " 골드를 획득했다.[300골드]";
                    sentences[4] = "어쩌면... 나중에 너의 도움이 필요할지도 모르겠어.";
                    GameObject.Find("Player").GetComponent<Player>().PlayerGold += 300;
                    Quest[1] = 2;
                }
                else
                {
                    System.Array.Resize(ref sentences, 1);
                    sentences[0] = "아직 보물상자 다섯개를 다 못찾았어.";
                }
                return sentences;
            }
            if (Quest[1] == 2)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "대단해!";
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 5)) //상점아들
        {
            if (Quest[2] == 0 && Quest[0] >= 5)
            {
                System.Array.Resize(ref sentences, 11);
                sentences[0] = "...";
                sentences[1] = "안녕. ";
                sentences[2] = "성인이 되서 마을 밖까지 나갔다며.";
                sentences[3] = "사실 나에게는 남들에게 말 못할\n 비밀이 있어.";
                sentences[4] = "내가 직접 나가서 해결하면 좋겠지만... 그럴 수 없으니 너에게 부탁 좀 할게.";
                sentences[5] = "나는 어릴때부터 마을에 또래 친구들이 없어서 혼자 지냈어.";
                sentences[6] = "그러다가 5살 쯤에 우연히 집에 멧돼지가 온 걸 발견했지.";
                sentences[7] = "멧돼지는 수시로 우리 집에 왔기 때문에 나는 아버지 몰래 멧돼지에게 차우 라는 이름을 지어주고 친구가 됐어.";
                sentences[8] = "그렇게 6달 정도 지났을 때 문제가 \n터졌어. 내가 없을 때 아버지께서 차우를 보고 독화살을 쏴 버린거야. ";
                sentences[9] = "그 뒤로 차우가 우리 집에 오지 않아서 \n너무 걱정돼. 혹시 꽃밭 깊숙한 곳까지 가서 차우가 있나 찾아줄 수 있어??";
                sentences[10] = " 새로운 던전이 열렸다.[꽃밭 깊숙한 곳]";
                Quest[2] = 1;
                return sentences;
            }
            else if (Quest[2] == 1)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "부탁이야. 꽃밭 깊숙한 곳으로 가서 \n차우를 찾아줘.";
                return sentences;
            }
            else if (Quest[2] == 2)
            {
                System.Array.Resize(ref sentences, 8);
                sentences[0] = "...";
                sentences[1] = "그랬구나... 독화살 때문에 점점 미쳐가고 있었나 보네... 분명 고통에서 해방시켜준 너에게 감사할거야.";
                sentences[2] = "나 때문에... 어쨋든 고마워... \n이건 내가 모은 용돈인데 줄게.";
                sentences[3] = " 100골드를 획득했다.";
                sentences[4] = "나? 나는 괜찮아. \n다시 못 만나서 아쉽긴 한데 이정도로 \n눈물을 보일 정도로 약하진 않다고.";
                sentences[5] = "...";
                sentences[6] = "...훌쩍";
                sentences[7] = " 컬렉션에 NPC에 대한 새로운 정보가 추가되었다.";
                Quest[2] = 3;
                GameObject.Find("Player").GetComponent<Player>().PlayerGold += 100;
                return sentences;
            }
            else if (Quest[2] == 3)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "...고마워";
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 6)) //늑대아이
        {
            if (Quest[3] == 1)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "...";
                sentences[1] = "너구나 요즘 내 늑대들을  죽인다는\n 인간이.";
                sentences[2] = "꽤나 강한 거 같지만  여기까지다.";
                sentences[3] = "나는 너를 죽이고 늑대들과 드엔마을을 침공해서 모두 죽일 것이다.";
                sentences[4] = "특히 드엔마을의 촌장.. \n촌장만큼은 만드시 죽일 거야.";
                sentences[5] = "이유? 이유는 알아서 뭐할려고.";
                sentences[6] = "어차피 죽을 녀석이.";
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 7)) //늑대아이2
        {
            if (Quest[3] == 1)
            {
                System.Array.Resize(ref sentences, 13);
                sentences[0] = "...";
                sentences[1] = "강하구나.";
                sentences[2] = "나의 복수는 실패다.";
                sentences[3] = "...";
                sentences[4] = "왜 복수하려는거냐고?";
                sentences[5] = "...";
                sentences[6] = "나는 태어나자마자 \n누군가에 의해서 이 바위산에 버려졌다.";
                sentences[7] = "죽는게 당연했지만 다행히 \n늑대들이 나를 키워줬다.";
                sentences[8] = "늑대들 사이에서의 삶은 당연히 쉽지 \n않았다. 하지만 나는 힘을 키워서 \n늑대들을 이기고 마침내 늑대들의 왕의 \n자리에 올랐다.";
                sentences[9] = "그리던 어느 날 우연히 어머니를 만나 \n마침내 진실을 알았다.";
                sentences[10] = "그리고 내게 비극을 준 장본인에게 \n복수하리라고 다짐했다.";
                sentences[11] = "... 더이상 말할 힘이 없군... \n나머지는 장본인인 촌장에게 들어라.";
                sentences[12] = " 새로운 무기를 획득했다.[늑대아이의 창]";
                GameObject.Find("GameManager").GetComponent<GameManager>().Swordhave[3] = 1;
                GameObject.Find("GameManager").GetComponent<GameManager>().Swordnum = 3;
                GameObject.Find("Player").GetComponent<Player>().swordnum = 3;
                GameObject.Find("Player").GetComponent<Player>().SwordInven(3);
                GameObject.Find("Player").GetComponent<Player>().Power = manager.SwordPower[3];

                Quest[3] = 2;
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 8)) //낚시꾼
        {
            if (Quest[0] == 7)
            {
                System.Array.Resize(ref sentences, 11);
                sentences[0] = "아~ 낚시하고 싶다~~";
                sentences[1] = "안녕! 벌써 이 작은 마을에 \n너의 소문이 자자하다고!";
                sentences[2] = "바위산까지 정복했다며!\n 그렇다면 다음이 어딘지는 알고 있겠지?";
                sentences[3] = "바로 더러운 해적놈들이 차지한 항구지!";
                sentences[4] = "내가 비록 여차여차해서 이 좁은 마을에 갇히게 됐지만...";
                sentences[5] = "과거에는 넓은 바다에서 마음껏 낚시를 하고 다녔단 말이지...";
                sentences[6] = "그래서 말인데... 나와 거래하지 않겠어?";
                sentences[7] = "간단한 거야! 너가 항구의 해적들을\n 몰아내고 쓸만한 배를 구해다주면";
                sentences[8] = "내가 그 배에 너를 태워서 대륙으로 \n데려다주지!";
                sentences[9] = "자! 거래 성립이니까 빨리 항구로 \n가즈아!";
                sentences[10] = " 새로운 던전이 열렸다.[항구]";
                Quest[0] = 8;
                return sentences;
            }
            if (Quest[0] == 8)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "쉴 시간없어! 빨리 항구로!!";
                return sentences;
            }
            if (Quest[0] == 9)
            {
                System.Array.Resize(ref sentences, 8);
                sentences[0] = "좋아! 배가 준비됐다아!";
                sentences[1] = "근데 사실... 작은 문제가 있는데...";
                sentences[2] = "해적들한테 물어봤는데 말이야... \n걔네들도 배는 있지만 항해를 \n못 나갔다고 하네.";
                sentences[3] = "왜냐하면 우리 섬과 대륙 사이에 거대한 참치가 있는데 보이는 배는 모두\n 침몰시킨다고...";
                sentences[4] = "그래서 대륙으로 가기 위해서 직접 배를 \n타고 참치를 잡아야 될 것 같아.";
                sentences[5] = "준비가 되면 같이 바다로 가자고! \n이번에는 나도 같이 싸우겠어!";
                sentences[6] = "진정한 낚시꾼이라면 거대한 참치정도는 잡아야지!";
                sentences[7] = " 새로운 던전이 열렸다.[바다]";
                Quest[0] = 10;
                return sentences;
            }
            if (Quest[0] == 10)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "참치를 잡으러 가자!";
                return sentences;
            }
            if (Quest[0] == 14)
            {
                System.Array.Resize(ref sentences, 10);
                sentences[0] = "좋아! 드디어 모든 문제가 해결됐구만.";
                sentences[1] = "이제 푸리에 대륙으로 갈 수 있을거야. \n그동안 고마웠네!";
                sentences[2] = "진짜 성공할 줄은 몰랐는데... \n내가 왜 이렇게 대륙으로 가고 싶은지\n 궁금하나?";
                sentences[3] = "사실 나는 대륙출신이야. ";
                sentences[4] = "우리집안은 대대로 어부잡이였는데\n 나는 그게 싫어서 집을 나왔거든.";
                sentences[5] = "근데 어부의 피는 못 속이겠는지 예전처럼 물고기를 잡고 싶어졌단 말이야.";
                sentences[6] = "근데 지금까지는 보다시피 섬 밖을 \n나갈 수가 없었기 때문에 지금이라도 \n 대륙에 있는 내 가족들에게 가서\n 어린마음에 집을 나간 것을\n 사과하고 싶은거야..";
                sentences[7] = "여하튼 나는 대륙에 가면 가장 먼저 \n가족들을 찾아볼 테니까 시간나면\n 밥이라도 먹으러 오라고!";
                sentences[8] = " 새로운 마을이 열렸다.[로브라 마을]";
                sentences[9] = " 컬렉션에 NPC에 대한 새로운 정보가 추가되었다.";
                Quest[0] = 15;
                return sentences;
            }
            if (Quest[0] > 11)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "고맙네!";
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 9)) //던전에서 낚시꾼
        {
            if (Quest[0] == 10 && manager.GetComponent<Manager>().PointName == "바다")
            {
                System.Array.Resize(ref sentences, 6);
                sentences[0] = "후... 배를 타고 나와보는건 \n오랫만이라서 좀 떨리는구만...";
                sentences[1] = "응? 왜 돛이 없냐고?";
                sentences[2] = "그딴 건 장식인데 높은 사람들이 몰라서 그렇지.";
                sentences[3] = "이제 출항할텐데 아래에 있는 게 \n배의 체력이야.";
                sentences[4] = "참치는 배를 공격할텐데 배의 체력이 \n바닥나기 전에 참치를 죽이면 되는거지! 쉽지? 나는 물의 흐름을 보고 참치가 \n어디로 공격할지 알려줄게!";
                sentences[5] = "그러면 출항한다! 분명 바로 공격이 \n올 테니까 조심하라고!";
                return sentences;
            }
            else if (Quest[4] == 1 && manager.GetComponent<Manager>().PointName == "망자의 바다")
            {
                System.Array.Resize(ref sentences,4);
                sentences[0] = "망자의 협곡으로 접근하면 바다에 안개가 둘러싸서 주변을 볼 수가 없다고 하네.";
                sentences[1] = "유령선이 출몰하면 유령선을 직접 타격할\n 수는 없고 유령선 선장을 직접 타격해야 한다니까 명심해!";
                sentences[2] = "로브라 마을에서 새로 무기를 장만했지? 드엔마을의 낡은 무기들로는 \n이길 수 없는 상대라고!";
                sentences[3] = "준비 됐으면... 가자!";
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == -2)) //듀토리얼 마야
        {
            if (Quest[0] == 0)
            {
                System.Array.Resize(ref sentences, 5);
                sentences[0] = "안녕 다이스~";
                sentences[1] = "내일이면 드디어 너가 성인이 되는\n 날이야~";
                sentences[2] = "성인이 되면 촌장님께서 마을로 나갈 수 있게 허락하실거구";
                sentences[3] = "오늘은 내일 마을 밖으로 나갈 것에\n 대비해서 미리 연습해 놓으려고 해.";
                sentences[4] = "앞쪽에 미리 몬스터를 몇마리 준비해 \n놨으니까 해치우면서 통과해봐!";
                Quest[0] = 1;
                return sentences;
            }
            if (Quest[0] == 1)
            {
                System.Array.Resize(ref sentences, 2);
                sentences[0] = "공격은 z버튼을 누르면 할 수 있어.";
                sentences[1] = "공격을 적중시키는 것만큼 \n상대의 공격을 피하는 게 중요해!";
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == -3)) //듀토리얼 마야2
        {
            if (Quest[0] == 1)
            {
                System.Array.Resize(ref sentences, 10);
                sentences[0] = "잘했어!";
                sentences[1] = "몬스터들을 처치하면 골드를 얻을 수 \n있으니까 잊지 말고 챙기도록 해.";
                sentences[2] = "골드로는 마을의 상점아저씨한테 \n아이템을 구입할 수 있지.";
                sentences[3] = "무기를 구입해서 공격력을 높이거나 \n방어구를 구입해서 방어력을 \n높일 수 있어.";
                sentences[4] = "방어력에 따라서 퍼센트단위로 \n적의 공격을 막을 수 있지.";
                sentences[5] = "너의 방어력이 20이면 50의 \n데미지를 40으로 줄이는 식이야.\n 알겠지?";
                sentences[6] = "왼쪽 위는 체력바와 기력바인데 적에게 타격당하면 체력이 줄어들고 \n스킬을 사용하면 기력이 줄어들어.";
                sentences[7] = "아직 사용할 수 있는 스킬이 없겠지만 \n골드로 상점아저씨한테 구입할 수 \n있을거야. 기본공격을 적에게 적중시키면 소모한 기력을 회복할 수 있어.";
                sentences[8] = "내가 한 말이 햇갈린다고 해도 상관없어. 일시정지 메뉴에서 도움말을 선택하면\n 지금까지 한 말들이 정리되어 있으니까. 햇갈리면 ESC를 누르면 돼. ";
                sentences[9] = "마지막으로 이 누님께서 직접 \n연습상대가 돼줄게. ";
                Quest[0] = 2;
                return sentences;
            }
            if (Quest[0] == 2)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "앞에서 미리 준비할게";
                return sentences;
            }
            else
                return null;
        }
        else if (NPCnum == 10) //바위굴 문
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().Key[1] == 1) //열쇠를 가지고 있을 경우
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "노란색 돌을 갖다대자 문이 열렸다.";
                NPCManager.GetComponent<Animator>().SetTrigger("Dooropen");
                NPCManager.layer = 12;
                return sentences;
            }
            else
                return null;
        }
        else if (NPCnum == 21) //성문
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().Key[4] == 1) //열쇠를 가지고 있을 경우
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "부적을 갖다대자 결계가 사라지고 문이 열렸다.";
                NPCManager.GetComponent<Animator>().SetTrigger("Dooropen");
                NPCManager.layer = 0;
                return sentences;
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 44;
                return null;
            }
        }
        else if (NPCnum == 11) //시몬
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 15)
            {
                System.Array.Resize(ref sentences, 5);
                sentences[0] = "안녕! 너를 기다리고 있었어.";
                sentences[1] = "어떻게 알고 기다렸냐고? \n왜냐하면 스승님께서 오늘이\n 운명의 날이라고 말하셨거든!";
                sentences[2] = "너는 분명 전설의 용사 \n드락사르의 딸일 거야! 맞지?";
                sentences[3] = "어라? 아니라고? 이상하네...";
                sentences[4] = "스승님의 예언이 또 틀리셨나 보네...\n여하튼 마을로 들어가면 \n스승님께서 기다리고 계실거야.";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 16;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 16)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "마을에 계신 예언가님께 가면 돼";
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 19)
            {
                System.Array.Resize(ref sentences, 6);
                sentences[0] = "김박사님한테 일이 대충 어떻게 돌아가는지 들었지?";
                sentences[1] = "그래서 아까 낚시꾼 아저씨는 가족을 찾으러 왔다는데 너는 왜 온 거야?";
                sentences[2] = "... 모험을 하러 왔다고?";
                sentences[3] = "때가 좀 안 좋긴 하네... 지금 마을 밖으로 나가면 용한테 죽여달라고 소리치는거나 마찬가지야.";
                sentences[4] = "정 못 믿겠으면 마을바깥에 산에있는 \n절벽까지만 갔다와봐도 용을 \n만날 수 있을거야.";
                sentences[5] = " 새로운 던전이 열렸다.[절벽]";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 20;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 20)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "절벽으로 가면 용의 무서움을 알게 될 \n거야.";
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 22)
            {
                System.Array.Resize(ref sentences, 16);
                sentences[0] = "이제는 용의 무서움을 잘 알겠지? \n마을 밖으로 나가는 건 자살행위라고!";
                sentences[1] = "...응? 용에게 공격을 당했는데 \n누군가에 의해서 구해졌다고???";
                sentences[2] = "너도 전설의 용사 중 한명인 \n스태틱 님을 만난거구나!!!";
                sentences[3] = "사실 예전에 형이랑 마을 밖에 몰래 \n나갔을 때 용에게 공격을 받았는데 \n누군가가 용의 공격으로부터 \n우릴 지켜줬어!";
                sentences[4] = "너무 빨리 사라져서 제대로 보지는 \n못했는데 스승님께 물어보니까 \n스태틱 님이래!";
                sentences[5] = "이유는 모르겠지만 마을까지 \n용이 못 오는 이유도 스태틱 님께서\n 지키고 계신 게 틀림없어!!!";
                sentences[6] = "엥? 아니라고? 음.. 다른 사람이 \n구해줬나 보구나... 좀 뻘쭘하네.";
                sentences[7] = "어쨋든 용의 무서움을 알았으니 \n이제는 마을 안에만 있을거지?";
                sentences[8] = "아니라고? 음...";
                sentences[9] = "어쩔 수 없네. 스승님은 물론 \n다른 사람들한테도 절대로 말하면 안돼!";
                sentences[10] = "사실은 말이야. 내가 마을 사람들 몰래 \n산에 굴을 파고 있어.";
                sentences[11] = "산 위로는 용이 날아다니니까 굴을 파서 통과해버리는 거지!";
                sentences[12] = "근데 말이야. 산 속에도 용만큼은 \n아니지만 무시무시한 몬스터들이 \n가득 있더라고!";
                sentences[13] = "그래서 굴을 더 이상 파지 못하는 상태야. 너가 몬스터들을 처리하면 내가 굴을 계속 파서 산을 통과하게 해 줄게 알겠지?";
                sentences[14] = "왜 이렇게 도와주냐고? 물...론 나도 \n모험을 좋아하기 때문이지!\n 가자! 모험을 떠나자!";
                sentences[15] = " 새로운 던전이 열렸다.[동굴]";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 23;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 23)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "동굴로 가서 몬스터를 처치하면 \n굴을 더 파줄게.";
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 24)
            {
                System.Array.Resize(ref sentences, 3);
                sentences[0] = "좋아! 덕분에 굴을 \n계속 팔 수 있었는데...";
                sentences[1] = "더 커다란 모험을 하고 싶은데 몬스터들 때문에 작업이 다시 막혔단 말이야... \n몬스터들을 처치해줘!";
                sentences[2] = " 새로운 던전이 열렸다.[슬라임의굴]";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 25;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 25)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "다음은 슬라임의 굴이야!";
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 26)
            {
                System.Array.Resize(ref sentences, 6);
                sentences[0] = "좋았어! 이제 곧 중심부야!";
                sentences[1] = "방금 굴을 팠는데 중심부가 \n이미 다 파져 있더라고??";
                sentences[2] = "굉장히 거대해서 사람이 판 것 같지는 \n않은데... 어쨋든 우리한테는 좋은 일이지! ";
                sentences[3] = "몬스터들도 없더라고! 이제 그대로 밖으로 나가면 될 거 같아! 그동안 수고했어!!";
                sentences[4] = "드디어 새로운 모험을 하러 떠나는 거야!";
                sentences[5] = "엥? 저기서 스승님께서 우리를 \n처다보시는데?";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 27;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 27)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "가자!";
                return sentences;
            }
            else
                return null;
        }
        else if (NPCnum == 12) // 예언가
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 16)
            {
                System.Array.Resize(ref sentences, 6);
                sentences[0] = "왔구나. 드락사르의 딸이여.";
                sentences[1] = "엥?? 아니라고?? 이런... 또 틀렸구나...";
                sentences[2] = "걱정하지 마. 이제는 예언이 하도\n 틀리니까 익숙해져서 직업을 바꾸는 것도 생각중이니깐.";
                sentences[3] = "트타스 마을의 김박사를 찾는다고? \n내 옆에 있는 사람이 김박사야.";
                sentences[4] = "왜 트타스 마을에서 로브라 마을로 \n왔는지는 직접 물어보면 돼.";
                sentences[5] = "내 예언에 따르면 우리는 다시 만날 날이 있을거야. 물론 또 틀릴수도 있지만.";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 17;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 17)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "내 왼쪽에 있는 사람이 김박사란다..";
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 27)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "... 시몬이랑 굴을 파고 있구나.";
                sentences[1] = "어차피 듣지 않을 거 같아서 시몬에게는 말하지 않았지만 너에게는 말해야겠구나.";
                sentences[2] = "간단해. 더 이상 산 속으로 들어가지 마렴.";
                sentences[3] = "안에 무언가가 있어. \n무서운 일이 생길 거야.";
                sentences[4] = "... 처음에 드락사르의 딸이라고 해서 내 예언을 믿지 못하는것 같은데";
                sentences[5] = "구체적인 단어는 틀리지만 전체적인 \n내 예언은 정확해. 가지 마렴.";
                sentences[6] = " 새로운 던전이 열렸다.[동굴 중심부]";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 28;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 28)
            {
                System.Array.Resize(ref sentences, 3);
                sentences[0] = "가지 마렴.";
                sentences[1] = "물론 너는 갈 거야. 그것도 \n나는 알고 있어...";
                sentences[2] = "그래서 더 마음이 아프단다.";
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 30)
            {
                System.Array.Resize(ref sentences,2);
                sentences[0] = "...";
                sentences[1] = "김박사에게 가 보렴...";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 31;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 37)
            {
                System.Array.Resize(ref sentences, 4);
                sentences[0] = "...";
                sentences[1] = "... 이상한 일이 벌어졌네.";
                sentences[2] = "너는 분명 시몬처럼 드래곤에게 죽을 \n운명이었는데... 너의 운명이 뒤집혔어...";
                sentences[3] = "어떻게 된 거지?? \n운명을 바꿀 만한 힘을 가진 \n존재가 있는건가?";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 38;
                return sentences;
            }
            else if (Quest[7] == 4)
            {
                System.Array.Resize(ref sentences, 5);
                sentences[0] = "...";
                sentences[1] = "오랫만이구나.";
                sentences[2] = "김박사에 대해서는 \n나쁘게 생각하지 말아줘.";
                sentences[3] = "그가 내 예언을 듣고 \n사람들을 이주시키지 않았더라면 \n끔찍한 일이 벌어졌을 거야.";
                sentences[4] = "로버트에게도 그렇게 \n말해 줬으면 좋겠어.";
                Quest[7] = 5;
                return sentences;
            }
            else
                return null;
        }
        else if (NPCnum == 13) // 김박사
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 17)
            {
                System.Array.Resize(ref sentences, 12);
                sentences[0] = "안녕 내가 김박사란다.";
                sentences[1] = "너도 항구에 있는 배에서 온 사람이구나. 먼저 온 사람은 가족의 행방을\n 묻고 있더라고.";
                sentences[2] = "왜 트타스 마을이 아닌 로브라 마을에 \n있냐고? 아. 로랑 섬에서는 전쟁 후의 \n 일은 아무것도 모르겠구나.";
                sentences[3] = "전쟁 이후 전설의 세 용사 중 드락사르와 스태틱이 사라지고 남은 스테락이\n 테일러 나라의 왕이 되었지.";
                sentences[4] = "처음에는 그럭저럭 괜찮았지만 그로부터 10년이 지난 뒤부터 불행이 시작됐어.";
                sentences[5] = "성에 저주가 내리고 저주가 점점 퍼지면서 성 바로 앞에 있던 트타스 마을에서는\n 나를 비롯한 마을 주민들이 고향을 \n떠날 수 밖에 없었지.";
                sentences[6] = "저주가 내린 지역은 황폐화되고 전염병이 돌고 시체들이 일어나고... 말 그대로 \n지옥과도 같은 장소가 되버렸어.";
                sentences[7] = "하지만 저주만이 문제가 아니었어.";
                sentences[8] = "피난하는 와중에 갑자기 거대한 용이 \n나타나서 보이는 모든 것을 \n불태워버리는 거야.";
                sentences[9] = "용은 전쟁 이후부터 이 나라에 들어올 수 없는 마법이 걸려 있는 것으로 알고 \n있었는데... 백성들을 도와줄 왕은 \n보이지도 않고... 문제가 참 많았단다. ";
                sentences[10] = "그렇게 우리는 계속 도망쳐서 결국 항구에 있는 작은 마을이었던 로브라 마을까지 오게 된 거지.";
                sentences[11] = "설명이 길어졌구나. 로랑 섬에도 알려야 했었지만 지금 마을은 작은데 사람은 \n많으면서 문제가 너무 많이 생겨서 다른 곳에는 전혀 신경쓰지 못하고 있단다. ";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 18;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 18)
            {
                System.Array.Resize(ref sentences, 12);
                sentences[0] = "오 촌장스탄의 편지구나.";
                sentences[1] = "트타스 마을에 있을 때 편지를 자주 주고받았지. 그때는 편지를 보내면 배를 타고 드엔마을까지 도착했단다.";
                sentences[2] = "그래서 내 여동생은 잘 있고?";
                sentences[3] = "아 어려서 모를 수도 있겠구나. \n촌장스탄의 아내가 내 여동생이란다.";
                sentences[4] = "우연히 내게 온 촌장스탄의 편지를 \n훔쳐보고 사랑에 빠져서 결국\n 드엔마을까지 가서 결혼했지.";
                sentences[5] = "응? 세상을 떠났다고? \n흠... 그거 참 안됐구나...";
                sentences[6] = "바빠서 동생의 죽을 때조차 함께 \n있어주지 못하다니 참 못된 오빠구나.";
                sentences[7] = "네가 이곳에 온 목적이 있겠지만 지금은 내가 신경을 써 줄 수가 없단다.";
                sentences[8] = "살 장소도 부족하고 음식도 부족하고... \n신경을 써야 할 게 너무 많구나.";
                sentences[9] = "그나마 용이 우리 마을까지는 오지 않아서 다행이라고 해야할까... ";
                sentences[10] = "혹시 따로 궁금한 점이 있으면 시몬한테 \n가 보렴. 오늘도 해야 할 일이 많단다.";
                sentences[11] = " 컬렉션에 NPC에 대한 새로운 정보가 추가되었다.";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 19;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 19)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "나는 바쁘니 시몬에게 가보렴.";
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 31)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "무슨 말을 해야 할지 모르겠다.";
                sentences[1] = "그렇게 마을 밖으로 나가지 말라고 \n했는데... ";
                sentences[2] = "요즘같은 시기에 모험을 할줄은 몰랐다...";
                sentences[3] = "게다가 시몬까지 끌어들이다니... \n실망이 크다.";
                sentences[4] = "애초에 시몬을 소개시켜준 내 잘못인가... 앞으로는 마을 밖으로 절대 나가지 말고 차라리... 로랑 섬으로 다시 돌아갔으면 \n좋겠구나.";
                sentences[5] = "미안하지만 너를 보면 \n시몬이 떠오르는구나...";
                sentences[6] = " 절벽에 있는 블랜더에게 가 보는 것이 좋겠다.";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 32;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 36)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "다이스! 온 마을에 용의 \n비명소리가 들렸단다!";
                sentences[1] = "너가 처치했다고? 정말 고맙다!";
                sentences[2] = "저번에는 정말 미안했단다,,, \n하지만 그 용을 처치하다니...";
                sentences[3] = "혹시 원하는 게 있으면 뭐든지 \n말해도 좋단다. ";
                sentences[4] = "드디어 마을을 제대로 \n넓힐 수 있게 되다니...!";
                sentences[5] = "이건 내 성의니까 받아두렴.";
                sentences[6] = " 골드를 획득했다.[1000골드]";
                GameObject.Find("Player").GetComponent<Player>().PlayerGold += 1000;
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 37;
                return sentences;
            }
            else if (Quest[7] == 1)
            {
                System.Array.Resize(ref sentences, 17);
                sentences[0] = "오 영웅님께서 오셨네.";
                sentences[1] = "다이스! \n너에게 좋은 소식이 있단다.";
                sentences[2] = "마을마다 세워진 영웅들의 동상 봤지?\n곧 너의 동상도 만들 예정이란다.";
                sentences[3] = "사양할 필요 없어. \n너라면 충분히 자격이 있으니까.";
                sentences[4] = " 로버트의 편지를 건네줬다.";
                sentences[5] = "이건 내게 온 편지니?";
                sentences[6] = "잠깐 이 글씨체는?";
                sentences[7] = " 김박사는 편지를 보지도 않고 찢었다.";
                sentences[8] = "트타스 마을까지 갔나 보구나.";
                sentences[9] = "거기엔 내 과거가 있지. \n이제는 잊고싶을만큼 부끄러운 과거...";
                sentences[10] = "현실에는 관심이 없었고 학문에만 \n열중한 채로 1차 대전쟁 당시의 유물들을 연구하고 있었지.";
                sentences[11] = "저주가 마을까지 닥치고 나서야 \n내가 해야 할 일을 깨달았단다.";
                sentences[12] = "마을 사람들을 설득해서 모두 \n이 로브라 마을로 왔지.";
                sentences[13] = "로버트... 그 녀석은 현실을 깨닫지 못하고 혼자 트타스 마을에 남았단다.";
                sentences[14] = "불쌍하지... 그게 그 녀석의 한계지...";
                sentences[15] = "미안하지만 나는 지금 생활이 좋단다. \n그 녀석에게 그대로 전해주렴. ";
                sentences[16] = "직접 가서 말하는 게 예의겠지만...\n 나는 바빠서 말이야. 게다가 그 녀석을 \n보면 내 한심했던 모습이 생각날 거 같네.";
                Quest[7] = 2;
                return sentences;
            }
            else if (Quest[7] == 3)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "이건...?";
                sentences[1] = "로버트 녀석이 쓸데없는 짓을...";
                sentences[2] = "너는 쓸데없는 짓을 하는 게 \n장점이자 단점이구나";
                sentences[3] = "더 이상 할 얘기는 없다.";
                sentences[4] = "혹시 원하는게 이거라면 로버트에게 줘라";
                sentences[5] = " 10000골드를 얻었다.";
                sentences[6] = " 예언자가 우리를 보고 있다. 가서 말을 걸어보자.";
                GameObject.Find("Player").GetComponent<Player>().PlayerGold += 10000;
                Quest[7] = 4;
                return sentences;
            }
            else
                return null;
        }
        else if (NPCnum == 14) // 블랜더
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 20)
            {
                System.Array.Resize(ref sentences, 4);
                sentences[0] = "...";
                sentences[1] = "구해줘서 고맙다고? 흥.";
                sentences[2] = "원래 구하려고 한 건 아니니까 \n감사할 필요는 없어.";
                sentences[3] = "다음은 없으니까 소중한 목숨을 \n잘 지키라고.";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 21;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 21)
            {
                System.Array.Resize(ref sentences, 4);
                sentences[0] = "내가 누구냐고?";
                sentences[1] = "그걸 말할 순 없지만 블랜더라고 부르면 돼. 나는 오랫동안 너를 기다려왔어.\n 다이스.";
                sentences[2] = "아직 용에 비하면 많이 약하구나.";
                sentences[3] = "괜히 용이랑 싸울 생각 하지말고 \n마을에 가서 머리 좀 식히는 게 좋을거야.";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 22;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 29)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "죽었네.";
                sentences[1] = "나 참... 뭐하나 했더니 \n산에 굴을 파고 있었던 거야??";
                sentences[2] = "산의 중심부에는 \n저 용이 살고 있단 말이야!";
                sentences[3] = "...물론 걱정되서 한 말은 아니야.";
                sentences[4] = "그건 그렇고 또 구해줘 버렸네;";
                sentences[5] = "더 이상 구해줬다간 내가 위험할 거 \n같으니까 진짜로 다음은 없어.\n 다시 안 만났으면 좋겠네.";
                sentences[6] = "더 이상 쓸데없는 짓 하지 말고 마을로 \n돌아가.";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 30;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 32)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "이번에는 뭐야";
                sentences[1] = "...? 마을에서 미움받고 있다고?";
                sentences[2] = "그래서 나한테 징징거리려고 온 거야?";
                sentences[3] = "너도 참...";
                sentences[4] = "도움이 필요하다고?";
                sentences[5] = "...";
                sentences[6] = "마음 또 약해지게 하네...";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 33;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 33)
            {
                System.Array.Resize(ref sentences, 8);
                sentences[0] = "용을 잡자고?";
                sentences[1] = "...";
                sentences[2] = "아까 탈탈 털렸으면서...";
                sentences[3] = "...";
                sentences[4] = "진심인가 보네.";
                sentences[5] = "...";
                sentences[6] = "그런 눈빛으로 보면 어쩔 수 없잖아... 가자.";
                sentences[7] = "내가 밖에 있는 용을 유인해 줄 테니까\n 너가 처치해야 돼. 내 역할은 딱 \n거기까지야.";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 34;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 34)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "용을 잡으러 가자.";
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 35)
            {
                System.Array.Resize(ref sentences, 6);
                sentences[0] = "...";
                sentences[1] = "정말로 저 귀찮은 용을 죽였네.";
                sentences[2] = "마지막에 용이 자살공격을 해서 놀라긴 했는데...";
                sentences[3] = "어쨋든 고마워, \n저 용은 사실 나랑도 관계가 있긴 하지.";
                sentences[4] = "마을로 돌아가서 용의 죽음을 알려주는 게 좋을거야.";
                sentences[5] = "그러고 나서... 한 번 더 내게 와줘";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 36;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 36)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "마을에 들렀다가 와줘.";
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 38)
            {
                System.Array.Resize(ref sentences, 14);
                sentences[0] = "안녕 다이스";
                sentences[1] = "후후 표정이 복잡하네. \n너의 생각을 맞춰볼까?";
                sentences[2] = "분명 너를 욕하던 사람들의 태도가 \n뒤집혀서 당황스럽겠지.";
                sentences[3] = "후후.. 마을 사람들에겐 이제 시몬이라는 아이의 죽음보다는 너라는 존재가 \n더 중요해 진 거지.";
                sentences[4] = "가족이 있던 것도 아니고 김박사같은 \n사람은 애초에 시몬보다는 마을의 재건이 더 중요할걸? 그저 보는 눈이 있으니까 \n시몬의 죽음을 슬퍼하는 척 했던 거지.";
                sentences[5] = "엥? 나에 대해서 궁금하다고?";
                sentences[6] = "음... 아직은 때가 아닌 거 같네.";
                sentences[7] = "하지만 너가 원하지 않더라고 조만간 \n모든 것을 말해야 하는 순간이 올 거야.";
                sentences[8] = "여하튼! 지금까지 내가 너를 도와줬잖아?";
                sentences[9] = "이제는 너가 나를 도와줘야겠어.";
                sentences[10] = "너는 시몬이라는 아이의 복수를 하기 위해 드래곤을 죽인 거잖아?";
                sentences[11] = "사실 내 어머니는... 전설의 세 용사 중 \n한명이자 현 테일러 나라의 국왕인 \n스태락에게 살해당했어...";
                sentences[12] = "이번에는 너가 내 복수를 도와줬으면 \n좋겠어.";
                sentences[13] = "물론 드래곤과는 비교가 안 될 정도로 \n어려울거야. 거절한다 해도 이해할게.";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 39;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 39)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "고마워";
                sentences[1] = "스태락은 저주받은 성에 있어.";
                sentences[2] = "과거에는 축복받은 성이라고 불렸지만 \n스태락이 왕이 되고 저주가 퍼지고 \n난 뒤부터 저주의 근원지가 된 곳이지.";
                sentences[3] = "저주를 받으면 어떻게 되는지 궁금하면\n 트타스 마을을 가 보는 게 좋아.";
                sentences[4] = "저주가 퍼지기 전만 해도 테일러 \n나라에서 가장 번성한 마을이었지";
                sentences[5] = "나는 먼저 가 있을게. 천천히 와";
                sentences[6] = " 새로운 마을이 열렸다. [트타스 마을]";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 40;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 44)
            {
                System.Array.Resize(ref sentences, 5);
                sentences[0] = "성의 결계를 통과하지 못했다고?";
                sentences[1] = "흠... 너라면 통과할 수 있을줄 알았는데 몸만으로는 안되는건가?";
                sentences[2] = "어쩔 수 없네. 이걸 받아.";
                sentences[3] = " 블랜더에게 특이한 문양이 새겨진 부적을 받았다.";
                sentences[4] = "이제 문제없을 거야. \n가서 한번 시험해 보고 돌아와.";
                GameObject.Find("GameManager").GetComponent<GameManager>().Key[4] = 1;
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 45;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 45)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "이제 성의 결게를 통과할 수 있을거야.";
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 46)
            {
                System.Array.Resize(ref sentences, 6);
                sentences[0] = "정말로 통과할 수 있었다고?";
                sentences[1] = "과연... ";
                sentences[2] = "안의 병사들 전부 저주받아서 \n꼭두각시처럼 조종받고 있는 중이야.";
                sentences[3] = "뭐 간단하게 그들을 편히 쉬게 해준다고 하면 마음이 편해지지.";
                sentences[4] = "이제 성 안으로 들어갈 수 있을거야.";
                sentences[5] = " 새로운 던전이 열렸다. [저주받은 성 하층]";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 47;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 48)
            {
                System.Array.Resize(ref sentences, 8);
                sentences[0] = "저주들의 집합체라...";
                sentences[1] = "이제 곧 그 원인을 직접 볼 수 있을거야.";
                sentences[2] = "이런 종류의 저주는 흑마법의 부작용으로 인해서 생기는 거야.";
                sentences[3] = "그리고 현재 이 나라에 이 정도의 마법을 부릴 수 있는 마법사는 한명밖에 없어..";
                sentences[4] = "너도 알고 있듯이 전쟁 당시에 인간쪽에 세 명의 용사와 두 명의 조력자가 있었지.";
                sentences[5] = "그리고 조력자 중 한명이 시르케라는 \n마법사였어.";
                sentences[6] = "이제 만날 수 있을거야.";
                sentences[7] = " 새로운 던전이 열렸다. [저주받은 성 하층]";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 49;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 51)
            {
                System.Array.Resize(ref sentences, 5);
                sentences[0] = "이제 마지막이야.";
                sentences[1] = "나와 모든 사람들의 비극의 원인인 스태락을 처치할 때야.";
                sentences[2] = "이 순간을 오랫동안 기다려왔어.";
                sentences[3] = "가자! 전설을 끝내러!";
                sentences[4] = " 새로운 던전이 열렸다. [저주받은 성 상층]";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] = 52;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] == 52)
            {
                System.Array.Resize(ref sentences,1);
                sentences[0] = "마지막이야.";
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 15)) //보물2
        {
            if (Quest[1] < 2)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "흠...";
                return sentences;
            }
            else if (Quest[1] == 2)
            {
                System.Array.Resize(ref sentences, 4);
                sentences[0] = "안녕! 여기서도 만나는구나!\n 너는 분명 드엔마을에서 보물상자를 \n모두 찾아서 나를 놀라게 했었지!";
                sentences[1] = "여기서도 모든 보물상자를 찾을 수 \n있을까? 로브라 마을의 보물상자는 모두 5개야.";
                sentences[2] = "근데 몇개의 보물상자는 보물을 주기 \n싫어하기도 하니까 조심해.";
                sentences[3] = "만약 이번 미션까지 성공하면 \n너를 나와 동급으로 인정해 줄게!";
                Quest[1] = 3;
                return sentences;
            }
            if (Quest[1] == 3)
            {
                gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
                if (gamemanager.Chest[6] == true && gamemanager.Chest[7] == true && gamemanager.Chest[8] == true && gamemanager.Chest[9] == true && gamemanager.Chest[10] == true)
                {
                    System.Array.Resize(ref sentences, 5);
                    sentences[0] = "오! 또 보물상자 다섯개를 찾은거야??";
                    sentences[1] = "진짜로 대단한데?";
                    sentences[2] = "너를 내 동급으로 인정한다는 \n의미로 골드를 좀 나눠줄게.";
                    sentences[3] = " 골드를 획득했다.[1000골드]";
                    sentences[4] = "이제 진짜로 너가 필요해.";
                    GameObject.Find("Player").GetComponent<Player>().PlayerGold += 1000;
                    Quest[1] = 4;
                }
                else
                {
                    System.Array.Resize(ref sentences, 1);
                    sentences[0] = "아직 보물상자 다섯개를 다 못찾았어.";
                }
                return sentences;
            }
            else
                return null;
        }
        else if (NPCnum == 16) // 낚시꾼2
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[0] >= 20 && GameObject.Find("GameManager").GetComponent<GameManager>().Quest[4] == 0)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "안녕...";
                sentences[1] = "가족들은 만났냐고? 어머니와 동생은 만났지만 아버지는 만나지 못했네.";
                sentences[2] = "사실은 만날 수가 없지. \n왜냐하면 이 마을에 없으시니까.";
                sentences[3] = "내가 집을 나가고... 아버지께서 \n계속 나를 찾아다녔다고 하네.";
                sentences[4] = "바다를 뒤지다가 망자의 협곡으로 간다는 말을 남기고 돌아오지 않았다고...";
                sentences[5] = "어머니와 동생은 괜찮다고 하지만 내 \n책임이기도 하니까 꼭 아버지를 \n찾고 싶은데 부탁할 수 있을까?";
                sentences[6] = "망자의 협곡에서는 죽은 사람들을 \n만날 수 있다고 해서 과거부터 죽은 이를 그리워하는 많은 사람들이 갔지만 \n돌아오는 사람은 없었어. 아버지는 내가 죽었다고 생각하고 만나기 위해 가셨던거 같아.";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[4] = 1;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[4] == 1)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "망자의 협곡으로 가자.";
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[4] == 2)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "... 왜 망자의 협곡까지 가지 않고 \n돌아가자고 했냐고?";
                sentences[1] = "자네와 싸운 선장의 얼굴 기억하나?";
                sentences[2] = "난 그 사람을 알아. 과거에 이 마을에서 동료들을 데리고 해적왕이 되겠다고 \n큰소리치던 사람이지.";
                sentences[3] = "주변 사람들 얘기에 따르면 적에게 \n동료들을 모두 잃고 절망에 휩싸인 채로 \n바다로 가서 돌아오지 않았다더군.";
                sentences[4] = "그 사람을 보면서 깨달았네. 망자의 \n협곡에 내 아버지가 살아있는지는 \n모르겠지만 죽은 사람보다는 현재 \n살아있는 사람이 중요하다는 것을 \n말이야.";
                sentences[5] = "내가 아버지를 찾는다는 생각으로 \n무리하게 망자의 협곡으로 갔으면 자네와 나 모두 그 선장처럼 유령이 돼서 죽지도 못하는 신세가 되었을 거야.";
                sentences[6] = "미안하네. 다음부터 이렇게 무리한 부탁은 하지 않을 거야. 나는 소중한 내 어머니와 동생을 지키겠네.";
                GameObject.Find("GameManager").GetComponent<GameManager>().Quest[4] = 3;
                return sentences;
            }
            else if (GameObject.Find("GameManager").GetComponent<GameManager>().Quest[4] == 3)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "진정으로 소중한 것을 알려줘서\n 정말 고맙네!!!";
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 17)) //시몬2
        {
            if (Quest[0]  == 28)
            {
                System.Array.Resize(ref sentences, 15);
                sentences[0] = "... 앞이 보이지 않네...";
                sentences[1] = "미안. 나는 여기까진거 같아. ";
                sentences[2] = "... 살아서 너랑 모험을 계속 하자고?";
                sentences[3] = "...";
                sentences[4] = "사실 나 모험 안 좋아해.";
                sentences[5] = "왜냐하면 그 잘난 모험을 하다가 \n형을 잃었거든.";
                sentences[6] = "형은 모험을 참 좋아했어. \n그 날도 모험을 하자며 내 손을 이끌고 \n마을 밖으로 나갔지.";
                sentences[7] = "... 용이 우리를 습격했고 형이 죽는걸 \n눈앞에서 지켜봐야 했어. 모든 걸 포기한 순간 어느 검사가 나를 구해줬지.";
                sentences[8] = "나는 살아남았고... 남들 앞에서는 \n내 형처럼 밝은 척 했지만 내 마음속의 \n공허함은 가시지 않았어.";
                sentences[9] = "다른 사람들은 내가 형의 죽음을 \n극복했다고 생각했지만 예언자 님만이\n 내 마음속의 죄책감을 꿰뚫어 보셨지.";
                sentences[10] = "...";
                sentences[11] = "결국은 죽은 형이 했을 모험을 대신이라도 하면서 죄책감을 덜려고 했던 것 뿐이야. 거기에 너를 이용한 거고...";
                sentences[12] = "이렇게 허무할 줄은 몰랐지만 \n형이라면 이렇게 했을 거라고 생각했어...";
                sentences[13] = "역시...";
                sentences[14] = "모험은 질색이라니까";
                Quest[0] = 29;
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 18)) //블랜더2
        {
            if (Quest[0] == 34)
            {
                System.Array.Resize(ref sentences, 4);
                sentences[0] = "여기는 엄청 뜨겁네.";
                sentences[1] = "말한대로 내가 밖에 날아다니는 용을 \n여기로 유인할 테니까";
                sentences[2] = "너가 어떤 방법을 써서든 처치하면 돼.";
                sentences[3] = "내 도움은 더 기대하지 말고.";
                return sentences;
            }
            else
                return null;
        }
        else if (NPCnum == 19) //대장장이
        {
            if (Quest[0] >= 24 && Quest[6] == 0)
            {
                System.Array.Resize(ref sentences, 9);
                sentences[0] = "자네! 잠깐 멈춰보게나";
                sentences[1] = "시몬과 얘기하는 걸 들었는데... \n산에 동굴을 판다면서?";
                sentences[2] = "오오! 그렇다면 이 늙은이 좀 도와주지 \n않겠나?";
                sentences[3] = "오오! 그렇다면 이내가 왕년에는 \n잘 나가는 대장장이였는데 말이야...";
                sentences[4] = "전쟁이 끝난 뒤로는 사람들이 무기에 \n관심이 없어서 농기구나 도끼같은 거에 망치질하면서 인생을 낭비하고 있다네...";
                sentences[5] = "그래도... 죽기 전에는 인생작품 하나를 \n만들고 싶다는 꿈이 있다네. ";
                sentences[6] = "그래서 그런데... 굴을 파다 보면 빛이 \n나는 곳이 있을거야. \n그곳은 과거 광산이 있던 곳인데";
                sentences[7] = "이 지도를 줄 테니까 이 늙은이의 한을 \n풀어줄 기가 막힌 광석 좀 구해다 주지\n 않겠나?";
                sentences[8] = " 새로운 던전이 열렸다.[광산]";
                Quest[6] = 1;
                return sentences;
            }
            else if (Quest[6] == 1)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "광산에서 기가 막힌 광석 좀 구해다\n 주게나.";
                return sentences;
            }
            else if (Quest[6] == 2)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "오오! 고맙네 어디!";
                sentences[1] = ".";
                sentences[2] = "..";
                sentences[3] = "...";
                sentences[4] = "...?";
                sentences[5] = "이런... 망치질 하니까 부숴져 버렸잖아...";
                sentences[6] = "나중에라도 혹!시! 내 혼을 쏟을만한 \n작품이 나오면 가져다 주게나";
                Quest[6] = 3;
                return sentences;
            }
            else if (Quest[6] == 3)
            {
                if (gamemanager.Swordhave[8] == 1 && gamemanager.Key[3] == 1)
                {
                    System.Array.Resize(ref sentences, 8);
                    sentences[0] = "잠깐! 자네가 가지고 있는 그거,,,";
                    sentences[1] = "힘을 잃은 마왕의 검과 드락사르의 \n검조각... 이거 가능하겠어! 이리 주게나!!";
                    sentences[2] = ".";
                    sentences[3] = "..";
                    sentences[4] = "...";
                    sentences[5] = "...!";
                    sentences[6] = "성공이다! 이제 죽어도 여한이 없네...";
                    sentences[7] = " 새로운 무기를 얻었다. [정화된 마왕의 검]";
                    GameObject.Find("GameManager").GetComponent<GameManager>().Swordhave[9] = 1;
                    GameObject.Find("GameManager").GetComponent<GameManager>().Swordnum = 9;
                    GameObject.Find("Player").GetComponent<Player>().swordnum = 9;
                    GameObject.Find("Player").GetComponent<Player>().SwordInven(9);
                    GameObject.Find("Player").GetComponent<Player>().Power = manager.SwordPower[9];
                    Quest[6] = 4;
                    return sentences;
                }
                else
                {
                    System.Array.Resize(ref sentences, 1);
                    sentences[0] = "내 혼을 쏟을만한 작품이 아직 없네. \n빨리 좀 가져다주게나.";
                    return sentences;
                }
                
            }
            else if (Quest[6] == 4)
            {
                System.Array.Resize(ref sentences, 3);
                sentences[0] = "죽어도 여한이 없어...";
                return sentences;
            }
            else
                return null;
        }
        else if (NPCnum == 20) //로버트
        {
            if (Quest[0] == 40)
            {
                System.Array.Resize(ref sentences, 8);
                sentences[0] = "오.. 이 마을로 오는 사람은 \n굉장히 오랫만에 보는군.";
                sentences[1] = "과거에는 보물사냥꾼들이나 모험가들이 오더니... 이제는 아예 오는 사람이 \n없다네.";
                sentences[2] = "이곳은 과거에는 나름 번성한 \n마을이었지만 저주가 퍼지고 사람들이\n 다 도망쳤어.";
                sentences[3] = "김박사... 그녀석이 선동해서 사람들을\n 모두 데리고 가 버렸어!";
                sentences[4] = "저주가 조금 퍼진다고 고향을 쉽게 \n버리다니 쯧쯧쯧";
                sentences[5] = "도망가는 길에 대부분이 용한테 \n당했다던데... 고향을 버리더니 \n벌 받은 게지.";
                sentences[6] = "우린 이렇게 저주를 피해서 땅에 숨어\n 있지만 나름 살 만 하다네. 오히려 \n저주에 대한 잘못된 소문들이 퍼지면서 도움을 줄 사람들이 안 와서 문제가 되고 있는 거지.";
                sentences[7] = "그래서 자네는 왜 왔는가? \n돈? 아니면 모험?";
                Quest[0] = 41;
                return sentences;
            }
            else if (Quest[0] == 41)
            {
                System.Array.Resize(ref sentences, 6);
                sentences[0] = "저주받은 성? 설마 왕이 있는\n 그 성을 말하는 건가?";
                sentences[1] = "후후후... 저주의 근원지로 가서 왕을 \n죽이겠다 이건가?";
                sentences[2] = "뭐 어리니까 그런 소리가 나올 수도 \n있겠군... 왜냐하면 전쟁 당시에 \n그를 본 사람들은 절대 그런 말을\n 못할테니까 말이야...";
                sentences[3] = "당시 반란군을 이끌던 세명의 용사와 \n두명의 조력자,,, 그들의 무력은 \n절대적이였지.";
                sentences[4] = "하지만 뭐... 선택은 너의 자유니까. \n성은 이 바로 앞에 있어. \n하지만 결계가 쳐져 있어서 \n아무도 들어가지 못하지.";
                sentences[5] = "또 궁금한 게 있나?";
                Quest[0] = 42;
                return sentences;
            }
            else if (Quest[0] == 42)
            {
                System.Array.Resize(ref sentences, 8);
                sentences[0] = "나? 나는 김박사와 같이 전쟁 \n당시의 유물을 연구하는 사람이네.";
                sentences[1] = "용족과의 전쟁? 아... 모를 수도 있겠군. \n지금까지 두 번의 대전쟁이 있었지.";
                sentences[2] = "흔히 용족과의 전쟁으로 알고 있는 건 \n두 번째 대전쟁이야. \n첫 번째 대전쟁은 100여년 전에 용족과 인간이 함께 마족에 맞선 전쟁이야.";
                sentences[3] = "마을마다 비석이 있을거야. \n내용은 지워졌을 텐데 그건 전쟁 후에 \n인간을 지배하던 용족이 과거에 인간과 힘을 합쳤다는 사실을 숨기기 위해서 \n일부러 내용을 지운거지";
                sentences[4] = "사실은 전쟁에서 힘을 합쳐서 마족을 \n이기고 서로 친선의 의미로 마을마다 \n세운 비석이야. 내용이 궁금하면 올라가서 보면 돼.";
                sentences[5] = "김박사와 전쟁 당시의 유물들을 해석해서 내용을 복원시켜 놨지.";
                sentences[6] = "질문은 끝난 거 같군. 여하튼 성으로 가고 싶으면 가면 돼. 하지만 결계때문에 \n들어가지는 못한다고.";
                sentences[7] = " 새로운 던전이 열렸다.[저주받은 성문]";
                Quest[0] = 43;
                return sentences;
            }
            else if (Quest[0] >= 43 && Quest[7] ==0)
            {
                System.Array.Resize(ref sentences, 4);
                sentences[0] = "뭐 성에 가던말던 마음대로 하고 \n김박사 알지? 이 편지 좀 전해줘.";
                sentences[1] = "사람들을 데리고 마을을 떠날 때 \n김박사의 눈빛은 내가 알던 사람이 \n아니였어.";
                sentences[2] = "나랑 유물들을 연구하고... 밤새는줄 \n모르게 토론을 펼쳤던 학자의 모습이\n 사라지고 지도자가 되겠다는 야심이 \n불타고 있었지.";
                sentences[3] = "그래도 친구로서 마음속 불꽃이 사라지지 않았는지 마지막으로 확인해 보고 싶군";
                Quest[7] = 1;
                return sentences;
            }
            else if (Quest[7] == 2)
            {
                System.Array.Resize(ref sentences, 8);
                sentences[0] = "...";
                sentences[1] = "그렇군...";
                sentences[2] = "로브라 마을까지 갔다왔는데 \n아무 성과가 없으니 미안하네...";
                sentences[3] = "그래... 혹시 그거라면...";
                sentences[4] = "부탁 하나만 더 들어줄 수 있는가?";
                sentences[5] = "이 마족의 유물을 보여주면 \n김박사의 마음을 돌릴 수 있을지도 몰라!";
                sentences[6] = "한 번만 더 김박사에게 \n가줄 수 있겠는가?";
                sentences[7] = " 새로운 무기를 얻었다.[노예의 한이 서린 곡괭이]";
                GameObject.Find("GameManager").GetComponent<GameManager>().Swordhave[7] = 1;
                GameObject.Find("GameManager").GetComponent<GameManager>().Swordnum = 7;
                GameObject.Find("Player").GetComponent<Player>().swordnum = 7;
                GameObject.Find("Player").GetComponent<Player>().SwordInven(7);
                GameObject.Find("Player").GetComponent<Player>().Power = manager.SwordPower[7];
                Quest[7] = 3;
                return sentences;
            }
            else if (Quest[7] == 5)
            {
                System.Array.Resize(ref sentences, 4);
                sentences[0] = "후...";
                sentences[1] = "그래... 어쩌면 거기에 있는 사람들은 \n김박사가 필요할지도 모르지...";
                sentences[2] = "내 옆에 두고 싶은 건 개인적인 \n욕심일지도 몰라.";
                sentences[3] = "정말 고맙네... 돈이랑 유물은 가져도 \n상관없네. 나에겐 필요없으니...";
                Quest[7] = 6;
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 22)) //시르케
        {
            if (Quest[0] == 49)
            {
                System.Array.Resize(ref sentences, 12);
                sentences[0] = "...";
                sentences[1] = "여기까지 오다니... 정말 강하구나...";
                sentences[2] = "성 밖에 저주가 내렸다고...?\n그건 나때문이야. 정말 미안해.";
                sentences[3] = "저주는 흑마법의 부작용이야. \n하지만 흑마법을 사용할 수 밖에 없었어.";
                sentences[4] = "마왕의 검에 깃든 마왕의 사념이 날이 \n갈수록 강해지고 있어.";
                sentences[5] = "스태락 국왕님의 마음이 약해진 틈을\n 파고들어서 몸과 마음을 지배하려고\n 하고 있지.";
                sentences[6] = "평범한 마법으로는 둘을 떼어놓을 수가 없었기 때문에 어쩔 수 없이 흑마법을\n 사용해서 마왕의 힘을 약화시키고 있어.";
                sentences[7] = "물론 영원히 떼어놓을 수는 없어. \n내 몸도 저주의 영향으로 \n얼마 살지 못할거야...";
                sentences[8] = "내가 죽는 순간 마법의 손아귀로부터 \n벗어난 마왕의 검이 국왕님을 \n지배할 것이고";
                sentences[9] = "전쟁영웅이였던 국왕이 시민들을 \n학살하는 대참사가 벌어질 거야.";
                sentences[10] = "과거의 영웅들은 전부 사라졌고 나는 \n너같은 새로운 영웅이 나타나서 대참사를 막아주기를 기다리고 있었어.";
                sentences[11] = "나를 이긴다면 국왕님을 막을 수 있을 가능성이 있다는 거겠지.";
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 23)) //왕비
        {
            if (Quest[0] == 52)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "너가 다이스구나.";
                sentences[1] = "나는 테일러 나라의 왕비인 도로시라고 한단다.";
                sentences[2] = "마야라는 아이가 와서 알려줬기 때문에 어떻게 흘러가는지는 알고 있단다.";
                sentences[3] = "남편은 오랜 세월 동안 여러 비극이 \n겹쳐지면서 몸과 마음이 매우 약해진 \n상태란다.";
                sentences[4] = "하지만... 포기하지는 않았어... \n지금도 마왕의 검에게 계속 저항하고 \n있는 중이란다.";
                sentences[5] = "나는 남편을 죽이는 것 외에 다른 방법이 있다고 생각한단다.";
                sentences[6] = "우리가 큰 잘못을 하기는 했지만... \n우리 손으로 되돌릴 기회를 주지 않겠니?";
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 24)) //스태락1
        {
            if (Quest[0] == 53)
            {
                System.Array.Resize(ref sentences, 5);
                sentences[0] = "...";
                sentences[1] = "밖이 시끄럽던데... \n왕비가 죽었나?";
                sentences[2] = "...";
                sentences[3] = "너도 길게 얘기하고 \n싶은 생각은 없는거 같군.";
                sentences[4] = "덤벼라.";
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 25)) //마왕
        {
            if (Quest[0] == 53)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "크크크... 겨우 스태락의 몸을 \n차지했었는데 결국은 실패로군...";
                sentences[1] = "용사여 그대에게 묻겠다. 내가 악한가? 너희 눈에는 그렇게 보이겠지.";
                sentences[2] = "내가 지배하기 전의 인간은 천상인과 \n지하인으로 나뉘어서 끝나지 않을 전쟁을 치루는 미개한 종족이었다.";
                sentences[3] = "누군가가 지배하지 않으면 통제가 되지 않는... 아주 멍청한 종족이였지. 크크크";
                sentences[4] = "그때로부터 오랜 기간이 흘렀지만\n 너희는 지금도 똑같구나. 다른 종족의 \n꼭두각시 노릇이나 하고 있다니...";
                sentences[5] = "너희는 약하다. 멍청하기 때문이지. \n우둔한 인간이여. 너희의 멸망이 \n보이는구나...";
                sentences[6] = "마지막이니 모처럼 나도 너처럼 \n꼭두각시놀음을 해보도록 하지.";
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 30)) //이시도르
        {
            if (Quest[0] >= 50)
            {
                System.Array.Resize(ref sentences, 7);
                sentences[0] = "...";
                sentences[1] = "너... 설마...";
                sentences[2] = "그녀... 시르케를 죽였나....??";
                sentences[3] = "대답해라!!!";
                sentences[4] = "...";
                sentences[5] = "모든 게 기억났다...";
                sentences[6] = "나는... 너를 죽이겠다...";
                Quest[9] = 1;
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 31)) //보물3
        {
            if (Quest[1] < 4)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "흠...";
                return sentences;
            }
            else if (Quest[1] == 4)
            {
                System.Array.Resize(ref sentences, 4);
                sentences[0] = "또 만났네? 너가 필요하다고 했지!";
                sentences[1] = "사실은 트타스 마을에도 보물상자가 있다고 해...";
                sentences[2] = "그런데 아직까지도 찾지 못했어...";
                sentences[3] = "혹시 보물상자를 찾아줄 수 있니??";
                Quest[1] = 5;
                return sentences;
            }
            if (Quest[1] == 5)
            {
                gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
                if (gamemanager.Chest[11] == true)
                {
                    System.Array.Resize(ref sentences, 12);
                    sentences[0] = "오! 역시! 내 눈은 정확했어!";
                    sentences[1] = "자! 이건 내 성의니까 받아줬으면 좋겠어.";
                    sentences[2] = " 새로운 방어구를 획득했다.[트래져 헌터 최우수 회원복]";
                    sentences[3] = "트레져 헌터라면 이 정도의 방어구를 \n갖춰야 해.";
                    sentences[4] = "트레져 헌터 회원이 몇명이냐고? \n음... 이제... 2명?";
                    sentences[5] = "...";
                    sentences[6] = "솔직히 말할게... \n사실 나는 정식 트레져 헌터\n 회원은 아니야.";
                    sentences[7] = "트레져 헌터 회원은 반드시 찾은 \n보물상자의 내용물을 협회에 제출해야 \n한다는 규칙이 있더라고.";
                    sentences[8] = "하지만 나는 보물을 찾는 그 자체의\n 즐거움 때문에 보물을 찾았기 때문에 \n내용물을 가져가지 않아서 잘렸어.";
                    sentences[9] = "그래서 아예 뉴 트레져 헌터 \n협회를 만들었지.";
                    sentences[10] = "이번에는 내가 찾지 못했지만 \n다음 보물은 반드시 내가 찾고 말겠어!";
                    sentences[11] = "그러면 힘내자고!";
                    Quest[1] = 6;
                    GameObject.Find("GameManager").GetComponent<GameManager>().Armorhave[5] = 1;
                    GameObject.Find("GameManager").GetComponent<GameManager>().Armornum = 5;
                    GameObject.Find("Player").GetComponent<Player>().armornum = 5;
                    GameObject.Find("Player").GetComponent<Player>().ArmorInven(5);
                    GameObject.Find("Player").GetComponent<Player>().Defense = manager.ArmorDefense[5];
                }
                else
                {
                    System.Array.Resize(ref sentences, 1);
                    sentences[0] = "아직 보물상자를 다 못찾았어.";
                }
                return sentences;
            }
            if (Quest[1] == 6)
            {
                System.Array.Resize(ref sentences, 1);
                sentences[0] = "굿굿굿...!";
                return sentences;
            }
            else
                return null;
        }
        else if ((NPCnum == 100)) //천재
        {
            if (Quest[0] == 55)
            {
                System.Array.Resize(ref sentences, 16);
                sentences[0] = "...?";
                sentences[1] = "안녕하세요. \n저는 이 게임을 만든 구본관입니다. ";
                sentences[2] = "...";
                sentences[3] = "지금 대화문을 만들면서도 \n사실 이걸 보는 사람이 있을지\n 모르겠네요...";
                sentences[4] = "여기까지 왔다는 것은 세 번째 엔딩을 \n봤다는 건데 흠.,.그렇게까지 이 게임을 \n하는 사람이 있으려나?";
                sentences[5] = "사실 여기까지 오면 제가 보스로서 \n싸우는 그림을 생각해 보기도 했는데 ";
                sentences[6] = "간단하게 NPC로 나왔습니다.";
                sentences[7] = "여하튼 게임을 플레이해주셔서 \n감사합니다. 처음에는 열심히 만들었는데 갈수록 어렵더라고요.";
                sentences[8] = "...";
                sentences[9] = "진짜로 여기까지 올 사람이 있으려나...";
                sentences[10] = "없을 거 같아서 여기까지 하겠습니다.";
                sentences[11] = "만약 여기까지 온 사람이 있다면";
                sentences[12] = "...";
                sentences[13] = "설마 있겠어.";
                sentences[14] = "설마 있다면 이제 지도로 가셔서 \n저장하고 메뉴로 가시면 됩니다.";
                sentences[15] = "만약 아직 처치하지 못한 보스가 있다면 \n찾아보시는 것도 좋습니다.";

                Quest[0] = 56;
                return sentences;
            }
            else
                return null;
        }
        else
            return null;



    }


        
}
