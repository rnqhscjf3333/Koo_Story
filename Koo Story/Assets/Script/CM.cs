using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM : MonoBehaviour
{
    public int CMNum;


    public void CMR()
    {
        GameObject.Find("Manager").GetComponent<Manager>().CMRA(CMNum);
    }
}
