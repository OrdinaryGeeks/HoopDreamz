using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLivePopupText : MonoBehaviour
{
    // Start is called before the first frame update

    float timeToLive;
    void Start()
    {

        timeToLive = .6f;
        
    }

    // Update is called once per frame
    void Update()
    {

        timeToLive -= Time.deltaTime;
        if(timeToLive<=0)
            Destroy(transform.gameObject);
    }
}
