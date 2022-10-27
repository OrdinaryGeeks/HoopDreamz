using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{

    public enum birdState
    {
        Roam,
        Dive,
        Settle, 
        Timer

    }

    public enum diveState
    {
        Diving,
        Ascending,
        Timer
    }
    public enum Tut3FlagActions
    {
        Dive,
        DivePass,
        DiveFail,
        Timer,
        None

    }
    public Tut3FlagActions tut3Actions;
    public bool tut3Flag;
    bool hitPlayer;
     bool activeScript;
    SpriteRenderer spriteRenderer;
    public float roamTimer;
    public float decisionTimer;
    public float moveSpeed = .1f;
    public float diveSpeed = 1.0f;
    bool flipX = false;
    public birdState bState;
    SpriteRenderer renderer;
    public GameObject player;
    public Vector3 diveDestination;
    public Vector3 diveDirection;
    public Vector3 diveStart;
    diveState dState;
    float tut3DelayTimer;
    // Start is called before the first frame update
    void Start()
    {
        tut3Actions = Tut3FlagActions.None;
        activeScript = false;
        hitPlayer = false;
        bState = birdState.Roam;
        decisionTimer = 6;
        renderer = GetComponent<SpriteRenderer>();
    }

    public void SetSpriteRenderer7()
    {


        renderer.sortingOrder = 7;
    }

    public void setUpTut3Dive()
    {
        tut3DelayTimer = Random.Range(2, 4);
       
       tut3Actions = Tut3FlagActions.Dive;
        hitPlayer = false;
        startTut3Dive();
        SetSpriteRenderer7();

    }

    public void startTut3Dive()
    {
        bState = birdState.Dive;
        diveDestination = player.transform.position;
        diveDirection = player.transform.position - transform.position;
        diveStart = transform.position;
        dState = diveState.Diving;
    }
    public void setTut3None()
    {
        tut3Actions = Tut3FlagActions.None;
    }
    public void Tutorial3()
    {
    
    if(activeScript)
        {
      
            if(tut3Actions == Tut3FlagActions.Timer)
            {
               

            }
            if(tut3Actions == Tut3FlagActions.Dive)
            {
                tut3DelayTimer -= Time.deltaTime;
                if (tut3DelayTimer <= 0)
                {
                   
                    UpdateDiveTut3();
                  
                }
            }





        }
    
    
    
    }


    // Update is called once per frame
    void Update()
    {
        if (activeScript)
        {
            if (tut3Flag)
            {
                Tutorial3();
            }
            else
            {
                if (bState == birdState.Timer)
                {


                    if (decisionTimer <= 0)
                    {
                        newBirdState();
                    }




                    decisionTimer -= Time.deltaTime;
                }
                if (bState == birdState.Roam)
                    UpdateRoam();
                if (bState == birdState.Settle)
                    UpdateSettle();
                if (bState == birdState.Dive)
                {
                    hitPlayer = false;
                    UpdateDive();
                }
            }
        }
        
    }

    public void ActivateScript()
    {
        activeScript = true;
    }
    public void DeactiveateScript()
    {
        activeScript = false;
    }
    public void newBirdState()
    {

        int decision = Random.Range(0, 3);

        if (decision == 0)
        {
            bState = birdState.Roam;
            roamTimer = Random.Range(3, 10);
        }
        if (decision == 1)
        {
            bState = birdState.Settle;

            decisionTimer = Random.Range(1, 7);
        }
        if (decision == 2)
        {
            bState = birdState.Dive;
            diveDestination = player.transform.position;
            diveDirection = player.transform.position - transform.position;
            diveStart = transform.position;
            dState = diveState.Diving;
        }
    }
    public void UpdateRoam()
    {

        transform.position -= new Vector3(moveSpeed, 0.0f, 0.0f);

        if (Mathf.Abs(transform.position.x) > 240)
        {
            moveSpeed *= -1;

            flipX = !flipX;

            renderer.flipX = flipX;



        }

        roamTimer -= Time.deltaTime;
        if(roamTimer <= 0)
        {
            bState = birdState.Timer;
            decisionTimer = Random.Range(1, 7);
        }

    }
    public void checkHitPlayer()
    {
        if(Vector3.Distance(transform.position, player.transform.position)< 30)
        {

            player.GetComponent<PlayerController>().air = 0;
            player.GetComponent<PlayerController>().setScoreString();
            hitPlayer = true;
        }


    }
    public bool atDestination(Vector3 vector)
    {


        if (Vector3.Distance(vector, transform.position) < 10)
        {
            return true;
        }
        else
            return false;
    }

    public void UpdateDiveTut3()
    {


        if (dState == diveState.Diving)
        {
            checkHitPlayer();
            if (atDestination(diveDestination))
            {
                dState = diveState.Ascending;

            }
            else
            {
                transform.position += diveDirection * diveSpeed * Time.deltaTime;
            }

        }


        if (dState == diveState.Ascending)
        {
            if (atDestination(diveStart))
            {

                if (hitPlayer)
                    tut3Actions = Tut3FlagActions.DiveFail;
                else
                    tut3Actions = Tut3FlagActions.DivePass;


            }
            else
            {
                transform.position -= diveDirection * diveSpeed * Time.deltaTime;
            }


        }



    }
    public void UpdateDive()
    {
        
        if (dState == diveState.Diving)
        {
            checkHitPlayer();
            if (atDestination(diveDestination))
            {
                dState = diveState.Ascending;
                
            }
            else
            {
                transform.position += diveDirection * diveSpeed * Time.deltaTime;
            }

        }
        

        if(dState == diveState.Ascending)
        {
            if (atDestination(diveStart))
            {

                bState = birdState.Timer;
                decisionTimer = Random.Range(1, 7);
             

            }
            else
            {
                transform.position -= diveDirection * diveSpeed * Time.deltaTime;
            }


        }


    }
    public void UpdateSettle()
    {

        bState = birdState.Timer;
        decisionTimer = Random.Range(1, 7);
        //Scream
    }
}
