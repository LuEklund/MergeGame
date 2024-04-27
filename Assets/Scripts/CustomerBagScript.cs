using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomerBagScript : MonoBehaviour
{
    private float timer = 0f;
    private GameObject RequestItem;
    private Progressionbar progressionbar;
    private float progression;
    private float Maxprogression;


    private void Awake()
    {
        progressionbar = GetComponentInChildren<Progressionbar>();
        progression = 20f;
        Maxprogression = 100f;

    }

    private void Start()
    {
        progressionbar.updateSlider(progression, Maxprogression);
    }

    void Update()
    {
        if (RequestItem)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                progression -= 10f;
                progressionbar.updateSlider(progression, Maxprogression);
                Debug.Log("CUSTOMER = MAD");

                ResetCustomer();

            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= 5f)
            {
                RequestItem = GameManager.Instance.GetRandomItem();
                Debug.Log("CUSTOMER = NEW");

                RequestItem.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1f);
            }
        }
        
    }

    private void ResetCustomer()
    {
        timer = 0f;
        Destroy(RequestItem);
        RequestItem = null;

    }

    
    public bool PlaceInBag(GameObject Coin)
    {
        //CoinScript coinScript = Coin.GetComponent<CoinScript>();
        if (Coin.GetComponent<CoinScript>().value == RequestItem.GetComponent<CoinScript>().value)
        {
            progression += 10f;
            progressionbar.updateSlider(progression, Maxprogression);
            Destroy(Coin);
            ResetCustomer();
            Debug.Log("CUSTOMER = HAPPY");
        }
        return false;
    }
    
}
