﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public Vector3 vec_top;
    public Vector3 vec_bottom;
    public float Speed ;
   
    Vector3 startingPosition;


    // Use this for initialization
    void Start () {
        Speed = Speed / 100;
        startingPosition = transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isPlayerInRange())
        {
            Vector3 latePosition = getPlayerPosition();
            StartCoroutine(setPosition(latePosition));
        }
            else
        {
            transform.position= startingPosition;
        }
	}

    IEnumerator setPosition(Vector3 playerPos)
    {
        yield return new WaitForSeconds(0f);
        Vector3 pos = transform.position;
        Vector3 newPos = pos;
        float directionX;
        float directionY;
        float distanceX=pos.x- getPlayerPosition().x;
        float distanceY= pos.y - getPlayerPosition().y;

        if (pos.x <= playerPos.x)
            directionX = 1;
        else
            directionX = -1;

        if (pos.y <= playerPos.y)
            directionY = 1;
        else
            directionY = -1;

        if(playerPos.x != pos.x)
        {
            
            newPos.x += Speed * directionX;
        }

        if (playerPos.y != transform.position.y)
        {
            if (Mathf.Abs(distanceY) < Mathf.Abs(distanceX))
            {
                newPos.y += Speed * directionY * Mathf.Abs((distanceY) / distanceX);
            }
            else { newPos.y += Speed * directionY ; }
        }

        transform.position = newPos;
    }

    Vector3 getPlayerPosition()
    {
        return PlayerController.current.transform.position;
    }

    bool isPlayerInRange()
    {
        Vector3 playerPosition = PlayerController.current.transform.position;
        return playerPosition.x >= vec_bottom.x && playerPosition.x <= vec_top.x && playerPosition.y >= vec_bottom.y && playerPosition.y <= vec_top.y;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
       
        transform.position = startingPosition;
        PlayerController.current.Death();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        OnTriggerEnter2D(collider);
    }
}
