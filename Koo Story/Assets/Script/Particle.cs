using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public int damage;
    void Update()
    {
    }

    void OnParticleCollision(GameObject gameobject)

    {
        if (gameobject.layer == 8) //ÇÃ·¹ÀÌ¾îÇÑÅ× ¸Â­ŸÀ»¶§
        {
            Player player = gameobject.GetComponent<Player>();
            player.onDamaged(transform.position);
            player.PlayerHp -= damage * 0.01f * (100 - player.Defense);
        }
        Debug.Log("ÆÄÆ¼Å¬ Ãæµ¹");

    }
}
