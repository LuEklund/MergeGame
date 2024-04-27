using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTile : MonoBehaviour
{

    [SerializeField] private GameObject _CoolDownMask;
    
    //Timer
    private float timer = 0f;
    
    //current coin
    public GameObject Coin;

    // Update is called once per frame
    void Update()
    {
        if (Coin == null)
        {
            timer += Time.deltaTime;
            _CoolDownMask.transform.position = new Vector3(-6f, timer, 0f);
        }
        if (timer >= 1f)
        {
            Coin = GameManager.Instance.GetRandomItem();
            Coin.transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
            timer = 0f;
        }
    }


}
