﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollspeed;
    public float tileSizeZ; 

    private Vector3 startPosition;
    private int flagValue;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float newPosition = Mathf.Repeat(Time.time * scrollspeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
    }

    private void speed()
        {
            scrollspeed = scrollspeed - 3f;
        }

    public void speedUp ()
        {
            speed();
        }   

}
