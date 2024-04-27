using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    //Coin sprites
    public Sprite[] sprites;
    public GameObject CoinClassInstance;

    

    //Tile that sapwns the coins
    private GameObject _spawnTileObject;
    private SpawnTile _spawnTileScript;
    
    //Customer
    // private GameObject _CustomerBagObject;
    // private CustomerBagScript _CustomerBagScript;

    
    //list of all tiles
    public List<GameObject> _listOfTiles = new List<GameObject>();
    


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _listOfTiles.AddRange(GameObject.FindGameObjectsWithTag("Tile"));
        _spawnTileObject = GameObject.Find("Spawn Tile");
        _spawnTileScript = _spawnTileObject.GetComponent<SpawnTile>();
        // _CustomerBagObject = GameObject.Find("CustomerBag");
        // _CustomerBagScript = _CustomerBagObject.GetComponent<CustomerBagScript>();
    }

   

    public bool PlaceCoin(GameObject Coin, GameObject _Tile)
    {
        TileScript tileScript = _Tile.GetComponent<TileScript>();
        GameObject OccupyingCoin = tileScript.GetOccupyingCoin();
        int CoinValue = Coin.GetComponent<CoinScript>().value;
        if (OccupyingCoin)
        {
            CoinValue++;
            if (OccupyingCoin == Coin
                || Coin.GetComponent<CoinScript>().value != OccupyingCoin.GetComponent<CoinScript>().value
                || CoinValue == sprites.Length)
            {
                return false;
            }
            if (CoinValue < sprites.Length)
            {
                Coin.GetComponent<SpriteRenderer>().sprite = sprites[CoinValue];
            }
            
            Debug.Log("new Value = " + CoinValue);
            Coin.GetComponent<CoinScript>().value = CoinValue;
            //remove the occupying coin
            Destroy(tileScript.GetOccupyingCoin());
        }
        //reset spawn timer if coin is from spawner
        if (Coin == _spawnTileScript.Coin)
        {
            _spawnTileScript.Coin = null;
        }
        //remove coin from previous tile
        foreach (var tile in _listOfTiles)
        {
            TileScript _tileScript = tile.GetComponent<TileScript>();
            if (_tileScript != null && _tileScript.GetOccupyingCoin() == Coin)
            {
                _tileScript.SetOccupyingCoin(null);
                break;
            }
        }
        
        //move the coin to the tile and update the tile coin
        Coin.transform.position = new Vector3(_Tile.transform.position.x, _Tile.transform.position.y, -0.1f);
        tileScript.SetOccupyingCoin(Coin);
        return true;
    }
    
    public Sprite[] GetSprites()
    {
        return sprites;
    }



    public GameObject GetRandomItem()
    {
        GameObject Coin = null;
        if (sprites.Length > 0)
        {
            int RandomIndex = UnityEngine.Random.Range(0, GameManager.Instance.GetSprites().Length);
            Coin = Instantiate(CoinClassInstance,
                transform.position,
                new Quaternion());
            Coin.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.GetSprites()[RandomIndex];
            Coin.GetComponent<CoinScript>().value = RandomIndex;
        }
        return Coin;
    }
}


