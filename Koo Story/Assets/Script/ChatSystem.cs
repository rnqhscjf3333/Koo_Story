using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatSystem : MonoBehaviour
{
    public Queue<string> sentences;
    public string currentSnetence;
    public TextMeshPro text;
    public GameObject quad;
    public int NPCB;
    public string NULL;

    public void Ondialogue(string[] lines, Transform point)
    {
        SoundManager.instance.NPC();
        transform.position = point.position;
        sentences = new Queue<string>();
        sentences.Clear(); //초기화
        foreach (var line in lines) //큐에넣음
        {
            sentences.Enqueue(line); //끝 부분에 추가
        }
        DialogueFlow(point);
    }

    void DialogueFlow(Transform point)
    {
        if (sentences.Count > 0)
        {
            currentSnetence = sentences.Dequeue(); //제거하고 반환
            if(currentSnetence.Substring(0,1) == " ")
            {
                GameObject.Find("Manager").GetComponent<Manager>().Naration1(currentSnetence,gameObject);
                SoundManager.instance.Click2();
            }
            else
            {
                SoundManager.instance.NPC();
                text.text = currentSnetence;
            }
            float x = text.preferredWidth;
            x = (x > 7) ? 7 : x + 0.3f; //크기설정
            quad.transform.localScale = new Vector2(x, text.preferredHeight + 0.3f);//크기설정

            transform.position = new Vector2(point.position.x, point.position.y);
            NPCB = 1;
        }
        else
        {
            Destroy(gameObject);
            NPCB = 0;
            Player player = GameObject.Find("Player").GetComponent<Player>();
            player.NPCA = 0;
            player.move = 1;
            player.jump = 1;
            player.alive = 1;
            player.gameObject.layer = 8;
            player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            player.SwordSprite.color = new Color(1, 1, 1, 1);
            player.ArmSprite.color = new Color(1, 1, 1, 1);
            player.ArmorSprite.color = new Color(1, 1, 1, 1);

            if (GameObject.Find("Manager") != null)
            {
                if (GameObject.Find("Manager").GetComponent<Manager>().PointName == "드엔 마을" && GameObject.Find("QuestManager").GetComponent<QuestManager>().Quest[9] ==1 && GameObject.Find("Manager").GetComponent<Manager>().CMconfiner.m_BoundingShape2D == GameObject.Find("Manager").GetComponent<Manager>().confiner[2])
                {
                    GameObject.Find("Manager").GetComponent<Manager>().IsidorGo();
                }
                if (GameObject.Find("Manager").GetComponent<Manager>().PointName == "바위산 정상")
                {
                    GameObject.Find("DungeonManager").GetComponent<DungeonManager>().TalkEnd(0);
                }
                if (GameObject.Find("Manager").GetComponent<Manager>().PointName == "바다")
                {
                    GameObject.Find("DungeonManager").GetComponent<DungeonManager>().TalkEnd(1);
                }
                if (GameObject.Find("Manager").GetComponent<Manager>().PointName == "망자의 바다")
                {
                    GameObject.Find("DungeonManager").GetComponent<DungeonManager>().TalkEnd(2);
                }
                if (GameObject.Find("Manager").GetComponent<Manager>().PointName == "산 중심부2")
                {
                    GameObject.Find("DungeonManager").GetComponent<DungeonManager>().TalkEnd(3);
                }
                if (GameObject.Find("Manager").GetComponent<Manager>().PointName == "고대의 사원")
                {
                    GameObject.Find("DungeonManager").GetComponent<DungeonManager>().TalkEnd(4);
                }
                if (GameObject.Find("Manager").GetComponent<Manager>().PointName == "저주받은 성 중층")
                {
                    GameObject.Find("DungeonManager").GetComponent<DungeonManager>().TalkEnd(5);
                }
                if (GameObject.Find("Manager").GetComponent<Manager>().PointName == "저주받은 성 상층")
                {
                    GameObject.Find("DungeonManager").GetComponent<DungeonManager>().TalkEnd(6);
                }
                if (GameObject.Find("Manager").GetComponent<Manager>().PointName == "저주받은 성 최상층")
                {
                    GameObject.Find("DungeonManager").GetComponent<DungeonManager>().TalkEnd(7);
                }
                if (GameObject.Find("Manager").GetComponent<Manager>().PointName == "저주받은 성 엔딩1")
                {
                    GameObject.Find("DungeonManager").GetComponent<DungeonManager>().TalkEnd(8);
                }
            }
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && NPCB == 1)
        {
            DialogueFlow(transform);
        }
    }
}
