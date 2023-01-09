using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public Manager manager;
    public AudioClip[] clip;
    GameManager gamemanager;
    public GameObject[] Village;
    public GameObject[] Dungeon;
    public Sprite[] DungeonSprite; // 0:Èò»ö 1:°ËÀº»ö 2:»¡°£»ö
    public int MapNum; //0:µå¿£¸¶À» 1:²É¹ç 2:²É¹ç ±í¼÷ 3:È²¾ß
    public string[] Map; //¼³¸í
    public Text MapText;

    public string MapName;
    public int startpoint;

    public Transform[] PlayerTrans1;

    void Awake()
    {
        GameManager gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gamemanager.Quest[0] >= 3)
        {
            Dungeon[1].SetActive(true);
            if (gamemanager.Quest[0] >= 4)
                Dungeon[1].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[1].SetActive(false);

        if (gamemanager.Quest[2] >= 1)
        {
            Dungeon[2].SetActive(true);
            if (gamemanager.Quest[2] >= 2)
                Dungeon[2].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[2].SetActive(false);

        if (gamemanager.Quest[0] >= 5)
        {
            Dungeon[3].SetActive(true);
            if (gamemanager.Quest[0] >= 6)
                Dungeon[3].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[3].SetActive(false);

        if (gamemanager.Quest[3] >= 1)
        {
            Dungeon[4].SetActive(true);
            if (gamemanager.Quest[3] >= 2)
                Dungeon[4].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[4].SetActive(false);

        if (gamemanager.Quest[0] >= 8)//Ç×±¸
        {
            Dungeon[5].SetActive(true);
            if (gamemanager.Quest[0] >= 9)
                Dungeon[5].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[5].SetActive(false);

        if (gamemanager.Quest[0] >= 10)//¹Ù´Ù
        {
            Dungeon[6].SetActive(true);
            if (gamemanager.Quest[0] >= 11)
                Dungeon[6].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[6].SetActive(false);

        if (gamemanager.Quest[0] >= 20)//Àýº®
        {
            Dungeon[7].SetActive(true);
            if (gamemanager.Quest[0] >= 21)
                Dungeon[7].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[7].SetActive(false);

        if (gamemanager.Quest[0] >= 15)//·Îºê¶ó ¸¶À»
        {
            Village[1].SetActive(true);
        }
        else
            Village[1].SetActive(false);



        if (gamemanager.Quest[4] >= 1)//¸ÁÀÚÀÇ ¹Ù´Ù
        {
            Dungeon[8].SetActive(true);
            if(gamemanager.Quest[4] >= 2)
                Dungeon[8].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[8].SetActive(false);



        if (gamemanager.Quest[0] >= 23)//µ¿±¼
        {
            Dungeon[9].SetActive(true);
            if(gamemanager.Quest[0] >= 24)
                Dungeon[9].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[9].SetActive(false);

        if (gamemanager.Quest[0] >= 25)//½½¶óÀÓÀÇ ±¼
        {
            Dungeon[10].SetActive(true);
            if (gamemanager.Quest[0] >= 26)
                Dungeon[10].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[10].SetActive(false);

        if (gamemanager.Quest[0] == 28)//Áß½ÉºÎ
        {
            Dungeon[11].SetActive(true);
        }
        else
            Dungeon[11].SetActive(false);

        if (gamemanager.Quest[0] >= 34)//Áß½ÉºÎ2
        {
            Dungeon[12].SetActive(true);
            if (gamemanager.Quest[0] >= 35)
                Dungeon[12].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[12].SetActive(false);

        if (gamemanager.Quest[0] >= 40)//Æ®Å¸½º ¸¶À»
        {
            Village[2].SetActive(true);
        }
        else
            Village[2].SetActive(false);

        if (gamemanager.Quest[6] >= 1)//±¤»ê
        {
            Dungeon[13].SetActive(true);
            if (gamemanager.Quest[6] >= 2)
                Dungeon[13].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[13].SetActive(false);

        if (gamemanager.Quest[0] >= 43)//¼º¹®
        {
            Dungeon[14].SetActive(true);
            if (gamemanager.Quest[0] >= 46)
                Dungeon[14].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[14].SetActive(false);

        if (gamemanager.Quest[0] >= 47)//ÇÏÃþ
        {
            Dungeon[15].SetActive(true);
            if (gamemanager.Quest[0] >= 48)
                Dungeon[15].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[15].SetActive(false);

        if (gamemanager.Quest[0] >= 49)//ÁßÃþ
        {
            Dungeon[16].SetActive(true);
            if (gamemanager.Quest[0] >= 50)
                Dungeon[16].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[16].SetActive(false);

        if (gamemanager.Quest[0] >= 52)//»óÃþ
        {
            Dungeon[17].SetActive(true);
            if (gamemanager.Quest[0] >= 54)
                Dungeon[17].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[17].SetActive(false);

        if (gamemanager.Quest[0] >= 54)//ÃÖÈÄÀÇ Å¾
        {
            Dungeon[18].SetActive(true);
            if (gamemanager.Quest[0] >= 55)
                Dungeon[18].GetComponent<Image>().sprite = DungeonSprite[1];
        }
        else
            Dungeon[18].SetActive(false);

    }

    void Update()
    {
        
    }


    public void CheckMap(int Num)
    {
        SoundManager.instance.SFXPlay("MapGo", clip[1]);
        MapNum = Num;
        MapText.text = Map[Num];
    }

    public void GoMap()
    {
        SoundManager.instance.SFXPlay("MapGo", clip[0]);
        manager.Fade = 1;

        if (MapNum == 0)
            gotomap(1, "Dne_Village");
        if (MapNum == 1)
            gotomap(0, "Flower");
        if (MapNum == 2)
            gotomap(0, "Flower2");
        if (MapNum == 3)
            gotomap(0, "Wilderness");
        if (MapNum == 4)
            gotomap(0, "Wilderness2");
        if (MapNum == 5)
            gotomap(0, "Harbor");
        if (MapNum == 6)
            gotomap(0, "Sea");
        if (MapNum == 7 && GameManager.Instance.Quest[0]==15)
            gotomap(0, "Robrah_Village");
        else if (MapNum == 7)
            gotomap(1, "Robrah_Village");
        if (MapNum == 8)
            gotomap(0, "Cliff");
        if(MapNum == 9)
            gotomap(0, "DeadSea");
        if (MapNum == 10)
            gotomap(0, "Cave1");
        if (MapNum == 11)
            gotomap(0, "SlimeCave");
        if (MapNum == 12)
            gotomap(0, "Dragon");
        if (MapNum == 13)
            gotomap(0, "Dragon2");
        if (MapNum == 14)
            gotomap(0, "ttas");
        if (MapNum == 15)
            gotomap(0, "Mineral");
        if (MapNum == 16)
            gotomap(0, "CatleDoor");
        if (MapNum == 17)
            gotomap(0, "Catle1");
        if (MapNum == 18)
            gotomap(0, "Catle2");
        if (MapNum == 19)
            gotomap(0, "Catle2.5");
        if (MapNum == 20)
        {
            GameManager.Instance.BlenderDieCount = 5;
            gotomap(0, "LastTower");
        }
    }
    void gotomap(int num, string sentence)
    {
        MapName = sentence;
        startpoint = num;
        Invoke("gogo", 1f);
    }

    void gogo()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().StartPoint = startpoint;
        SceneManager.LoadScene(MapName);
    }
}
