using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Vector3 _StartPosition;
    [SerializeField] private LayerMask _LayerTile;
    [SerializeField] private LayerMask _LayerBag;
    private RaycastHit2D _Hit2D;
    
    public int value = 1;
    private void OnMouseDown()
    {
        _StartPosition = transform.position;
        // Debug.Log(_StartPosition);
    }

    private void OnMouseDrag()
    {
        transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
            _StartPosition.z);

    }

    private void OnMouseUp()
    {
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 RayDirection = Camera.main.transform.forward;
        float RayLength = 10f;
        _Hit2D = Physics2D.Raycast(MousePosition, RayDirection, RayLength,_LayerTile);
        if (_Hit2D) //Hit placeable tile
        {
            if (!GameManager.Instance.PlaceCoin(this.gameObject, _Hit2D.collider.gameObject))
            {
                transform.position = _StartPosition;
            }
        }
        else
        {
            _Hit2D = Physics2D.Raycast(MousePosition, RayDirection, RayLength,_LayerBag);
            if (_Hit2D) //hit bag
            {

                CustomerBagScript bagScript = _Hit2D.collider.gameObject.GetComponent<CustomerBagScript>();
                if(bagScript)
                {
                    bagScript.PlaceInBag(this.gameObject);
                    transform.position = _StartPosition;
                }
            }
            else //hit nothing of importance
            {
                transform.position = _StartPosition;
            }
        }
    }
}
