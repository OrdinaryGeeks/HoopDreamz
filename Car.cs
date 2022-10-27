using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public float decisionTimer;
    public float driveTimer;

    public PlayerController playerController;

    public bool tut3Flag;
    public bool tut3Flagb;
    public float tut3FlagDelayTimer;
    Sprite car;
    SpriteRenderer renderer;
    bool flipX = false;
    public AudioClip tireSkrt;
    public AudioClip horn;
    public AudioClip alarm;
    AudioSource audioSource;
    public enum carState
    {
        Drive,
        Horn, 
        Alarm,
        Skrt,
        Timer,
        Tut3

    }


    public float alarmMin;

    float moveSpeed = .10f;

    public bool activeScript = false;
    public Vector3 origin;

    public enum Tut3FlagActions
    {
        Horn,
        HornFail,
        HornPass,
        Alarm,
        None,
        Pass
    }

   public Tut3FlagActions tut3Actions;

  public  carState cState;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        cState = carState.Tut3;
        audioSource = GetComponent<AudioSource>();
        tut3Flag = false;
        tut3Actions = Tut3FlagActions.None;

        origin = transform.position;
    }

    public void ActivateScript()
    {
        activeScript = true;
    }

    public void DeactivateScript()
    {
        activeScript = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeScript)
        {
            if (!tut3Flag)
            {
                if (cState == carState.Timer)
                {
                    decisionTimer -= Time.deltaTime;
                    if (decisionTimer <= 0)
                        newState();
                }
                if (cState == carState.Drive)
                {
                    UpdateDrive();

                }
                if (cState == carState.Horn)
                {
                    UpdateHorn();
                }
                if (cState == carState.Alarm)
                {
                    UpdateAlarm();
                }
                if (cState == carState.Skrt)
                {
                    UpdateSkrt();
                }
            }


            if (tut3Flag)
            {

                if (tut3Actions == Tut3FlagActions.Horn)
                    UpdateTut3Horn();
               // if (tut3Actions == Tut3FlagActions.Alarm)
                 //   UpdateTut3Alarm();
            }
        }
    }

    public void setTut3Horn()
    {

        tut3FlagDelayTimer = Random.Range(2, 4);
        tut3Actions = Tut3FlagActions.Horn;
    }

    public void setTut3None()
    {
        tut3Actions = Tut3FlagActions.None;
    }
    public void setTut3Alarm()
    {

        tut3FlagDelayTimer = Random.Range(2, 4);
        tut3Actions = Tut3FlagActions.Alarm;
    }
    public void tut3Advance()
    {


        if (tut3Actions == Tut3FlagActions.Horn)
            tut3Actions = Tut3FlagActions.Alarm;

        if (tut3Actions == Tut3FlagActions.Alarm)
            tut3Actions = Tut3FlagActions.Pass;

    }
    public void UpdateTut3Horn()
    {
        tut3FlagDelayTimer -= Time.deltaTime;

        if (tut3FlagDelayTimer < 0)
            tut3FlagDelayTimer = 0;
        if(tut3FlagDelayTimer <= 0)
        {
            if (!playerController.startSlider && !playerController.processRedCarAlarm & !playerController.processRedCarHorn)
            {
                audioSource.clip = horn;

                audioSource.Play();
                playerController.redCarHorn = true;

               // changeToTimerState();
            }

        }

    }
    //Makes you take a shoot right now during a shot for a good shot
    void UpdateTut3Alarm()
    {
            if (playerController.startSlider && playerController.shotSlider.value < 60 && playerController.shotSlider.value > alarmMin)
            {
                playerController.redCarAlarm = true;
                audioSource.clip = alarm;
                audioSource.Play();
               // changeToTimerState();
            }
    }

    public void resetOrigin()
    {

        transform.position = origin;
        tut3FlagDelayTimer = 0; 

    }
    public void newState()
    {
        int decision = Random.Range(0, 4);

        if (decision == 0)
        {
            cState = carState.Horn;
            alarmMin = Random.Range(0.10f, .30f);
        }

        if (decision == 1)
        {
            cState = carState.Drive;
            driveTimer = Random.Range(3, 8);
        }

        if (decision == 2)
        {
            cState = carState.Horn;
            alarmMin = Random.Range(0.10f, .30f);
        }
        if (decision == 3)
        {
            cState = carState.Drive;
            driveTimer = Random.Range(3, 8);

        }
     

    }

    //starts a shot and changes the good shot area
    void UpdateHorn()
    {
        if (!playerController.startSlider && !playerController.processRedCarAlarm &!playerController.processRedCarHorn &! playerController.badShot &! playerController.goodShot)
        {
            audioSource.clip = horn;


            audioSource.Play();
            playerController.redCarHorn = true;

            changeToTimerState();
        }
    }

    void changeToTimerState()
    {
        cState = carState.Timer;
        decisionTimer = Random.Range(3, 6);
    }

    //Makes you take a shoot right now during a shot for a good shot
    void UpdateAlarm()
    {
            if (playerController.startSlider && playerController.shotSlider.value < 60 && playerController.shotSlider.value > alarmMin)
        {
            playerController.redCarAlarm = true;
            audioSource.clip = alarm;
            audioSource.Play();
            changeToTimerState();
        }
    }

    void UpdateSkrt()
    {
        audioSource.clip = tireSkrt;
        audioSource.Play();
        changeToTimerState();
    }
    void UpdateDrive()
    {

        driveTimer -= Time.deltaTime;
        if (driveTimer <= 0)
            changeToTimerState();
        transform.position -= new Vector3(moveSpeed, 0.0f, 0.0f);

        if (Mathf.Abs(transform.position.x) > 240)
        {
            moveSpeed *= -1;

            flipX = !flipX;

            renderer.flipX = flipX;


        }
    }

   
}
