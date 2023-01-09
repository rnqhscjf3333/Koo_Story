using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject Inven;
    public bool isInven;
    public GameObject Player; //플레이어위치
    public string[] sentences;//대화
    public string[] Newsentences;
    public Sprite[] sprites;
    public Transform chatTr; //NPC 위치
    public GameObject chatBoxPrefab;
    public int NPCNum; //-1:인벤 1:마야, 2:촌장 3:촌장집문
    public GameObject QuestManager;
    public GameObject GameManager;
    public GameObject NPCManager;

    public GameObject Naration;
    public Text NarationText;


    void Awake()
    {
        NPCManager = gameObject;
    }

    void Update()
    {

        if (isInven == true && (Input.GetButtonUp("Horizontal")|| Input.GetButtonDown("Jump")))
        {
            Player player = Player.GetComponent<Player>();
            player.NPCA = 0;
            player.move = 1;
            player.jump = 1;
            isInven = false;
            Inven.SetActive(false);
        }
    }

    public void TalkNPC()
    {


        if (NPCNum == 0) //퀘스트없는 인간
        {
            GameObject go = Instantiate(chatBoxPrefab); //복제
            go.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
        }
        else if (NPCNum == -1) //상점/인벤
        {
            Manager manager = GameManager.GetComponent<Manager>();
            Inven.SetActive(true);
            manager.Inven();
            isInven = true;
        }
        else //퀘있는인간
        {
            QuestManager questmanager = QuestManager.GetComponent<QuestManager>();
            Newsentences = questmanager.QuestCheck(NPCNum, NPCManager);
            GameObject go = Instantiate(chatBoxPrefab); //복제

            if (Newsentences == null)
                go.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
            else
                go.GetComponent<ChatSystem>().Ondialogue(Newsentences, chatTr);
        }


    }

    public void TalkingNPC(string sentence, int i)
    {
        NarationText.text = sentence;
        Naration.SetActive(true);
        Invoke("EndNaration", i);
    }

    public void EndNaration()
    {
            Naration.SetActive(false);

    }
}
