using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject OccupyingCoin;

    public void SetOccupyingCoin(GameObject _OccupyingCoin)
    {
        OccupyingCoin = _OccupyingCoin;
    }
    
    public GameObject GetOccupyingCoin() 
    {
        return (OccupyingCoin);
    }
}
