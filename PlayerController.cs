using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip twoAudio;
    public AudioClip threeAudio;
    public AudioClip challengeStart;
    public AudioClip challengeFail;
    public AudioClip shotFail;
    AudioSource audioSource;
    public Vector3 moveToDestination;
    Animator anim;
    public Slider shotSlider;
    public bool startSlider;
    public Image sliderFill;
    public float badShotTimer;
    public bool badShot;

    public float goodShotTimer;
    public bool goodShot;
    public int banked;
    public int air;

    public bool isChallenge;

    public bool tut2Flag;

    public float redCarAlarmMark;
    public float redCarAlarmDepth;

    public bool redCarHorn;
    public bool greenCarHorn;
    public bool processRedCarHorn;

    public bool outOfbounds;
    //public bool inTheStreet;

    public bool redCarAlarm;
    public bool processRedCarAlarm;

    public float redCarMark;
    public float redCarDepth;
    public bool activeScript = false;
    public string scoreString;
    public GameObject two;
    public GameObject three;
    public GameObject challenge;
    public TextMeshProUGUI scoreTMeshPro;


    public enum ChallengeState
    {   None,
        TwoStart,
        TwoPass,
        TwoFail,
        ThreeStart,
        ThreePass,
        ThreeFail,
        DunkStart,
        DunkPass,
        DunkFail,
        SuperDunkStart,
        SuperDunkPass,
        SuperDunkFail
        
    }
    public enum Tut2FlagActions
    { Open,
        Miss,
        Two,
        Three,
        Dunk,
        SuperDunk
    }

    public bool tut3Flag;
    public enum Tut3FlagActions
    {
        None,
        Horn,
        HornFail,
        Alarm,
        AlarmFail,
        AlarmCheck
    }
    public Tut2FlagActions tut2Actions;

    public Tut3FlagActions tut3Actions;

    public ChallengeState cState;
    void Start()
    {
        cState = ChallengeState.None;
        tut3Actions = Tut3FlagActions.None;
        tut3Flag = false;
        tut2Actions = Tut2FlagActions.Open;
        processRedCarHorn = false;
        redCarHorn = false;
        greenCarHorn = false;
        startSlider = false;
        anim = GetComponent<Animator>();
        banked = 0;
        air = 0;
        shotSlider.maxValue = 2;
        scoreString = "";
        activeScript = false;
        audioSource = GetComponent<AudioSource>();
        tut2Flag = false;
    }

    public void ActivateScript()
    {
        activeScript = true;
    }

    public void DeactivateScript()
    {
        activeScript = false;
    }

    public void processBadShot()
    {
        if (badShotTimer > 0)
            badShotTimer -= Time.deltaTime;
        else
        {
            badShot = false;
            sliderFill.color = Color.red;
            shotSlider.value = 0;
            processRedCarHorn = false;
        }

    }

    public void processGoodShot()
    {
        if (goodShotTimer > 0)
            goodShotTimer -= Time.deltaTime;
        else
        {
            goodShot = false;
            sliderFill.color = Color.red;
            shotSlider.value = 0;
            processRedCarHorn = false;
        }

    }

    public void SetupRedCarHorn()
    {

        redCarHorn = false;
        redCarMark = Random.Range(.3f, .80f);
        redCarDepth = Random.Range(.55f, 1.0f);

        processRedCarHorn = true;
    }
    void UpdateTut3()
    {

        if (badShot)
        {
            processBadShot();
     
        }
        else if (goodShot)
        {
            processGoodShot();
        }

        /*
        if (redCarAlarm)
        {

            redCarAlarm = false;
            redCarAlarmMark = shotSlider.value;
            redCarAlarmDepth = Random.Range(.50f, .90f);

            processRedCarAlarm = true;

        }*/
        if (redCarHorn)
        {

            SetupRedCarHorn();
        }
        /*
        if (processRedCarAlarm)
        {

            shotSlider.value += Time.deltaTime;


            if (shotSlider.value > redCarAlarmMark & !badShot && shotSlider.value < (redCarAlarmMark + redCarAlarmDepth))
                sliderFill.color = Color.gray;
            if (shotSlider.value >= (redCarAlarmMark + redCarAlarmDepth) & !badShot)
            {
                sliderFill.color = Color.black;
                setBadShotTut3();
                startSlider = false;
            }



        }*/
        else if (processRedCarHorn)
        {
            processRedCarHornFN();
        }

    }

    public void processRedCarHornFN()
    {

        shotSlider.value += Time.deltaTime;

        if (shotSlider.value > redCarMark & !badShot && shotSlider.value < (redCarMark + redCarDepth))
            sliderFill.color = Color.blue;

        if (shotSlider.value >= (redCarMark + redCarDepth) & !badShot)
        {
            sliderFill.color = Color.black;
            setBadShotTut3();
            startSlider = false;
        }


    }
    // Update is called once per frame
    void Update()
    {
        if (activeScript)
        {
            if (!tut3Flag)
            {
                if (badShot)
                {
                    processBadShot();
                }
                else if (goodShot)
                {
                    processGoodShot();
                }

                /*
                else if (redCarAlarm)
                {

                    redCarAlarm = false;
                    redCarAlarmMark = shotSlider.value;
                    redCarAlarmDepth = Random.Range(.10f, .30f);

                    processRedCarAlarm = true;

                }*/
                else if (redCarHorn)
                {
                    SetupRedCarHorn();
                }
                /*
                else if (processRedCarAlarm)
                {

                    shotSlider.value += Time.deltaTime;


                    if (shotSlider.value > redCarAlarmMark && !badShot && shotSlider.value < (redCarAlarmMark + redCarAlarmDepth))
                        sliderFill.color = Color.gray;
                    else if (shotSlider.value >= (redCarAlarmMark + redCarAlarmDepth) & !badShot)
                    {
                        sliderFill.color = Color.black;
                        setBadShot();
                        startSlider = false;
                    }



                }*/
                else if (processRedCarHorn)
                {
                    processRedCarHornFN();

                }
                else if (startSlider)
                {
                    processSlider();
                   

                }
            }
            else
                UpdateTut3();


        }

    }
    public void processSlider()
    {
        shotSlider.value += Time.deltaTime;


        {
            if (shotSlider.value > .50 && shotSlider.value < 1.5f && !badShot)
                sliderFill.color = Color.green;

            else if (shotSlider.value >= 1.5f & !badShot)
            {
                sliderFill.color = Color.black;
                setBadShot();
                startSlider = false;
            }

        }

    }

    public void setGoodShot()
    {
        goodShotTimer = 1.0f;
        goodShot = true;
        sliderFill.color = Color.yellow;
        startSlider = false;
        endProcessesOnAShot();
    }

    public void endProcessesOnAShot()
    {

        processRedCarAlarm = false;
        processRedCarHorn = false;
    }
    public void setBadShot()
    {
        startSlider = false;
        badShotTimer = 2.0f;
        badShot = true;
        endProcessesOnAShot();
        tut2Actions = Tut2FlagActions.Miss;
        //Need to branch opff a tut3 version of update and put this somewhere else
        //audioSource.clip = shotFail;
        //audioSource.Play();

    }
    public void setBadShotTut3()
    {
        startSlider = false;
        badShotTimer = 2.0f;
        badShot = true;
        endProcessesOnAShot();
        if (tut3Actions == Tut3FlagActions.None)
            tut3Actions = Tut3FlagActions.HornFail;

        if (tut3Actions == Tut3FlagActions.None)
            tut3Actions = Tut3FlagActions.AlarmFail;

      //  audioSource.clip = shotFail;
       // audioSource.Play();
    }
    public void MovePlayer(Vector3 mousePosition)
    {

        transform.position = GetMouseInWorld(mousePosition);

    }

    public void TranslatePlayer(Vector2 thumbStickInput)
    {
        Vector3 oldPosition = transform.position;
        transform.position += new Vector3(thumbStickInput.x, thumbStickInput.y, 0.0f);
        if (transform.position.y <= -84 || transform.position.y >= -15)
        {
            outOfbounds = true;
            transform.position = new Vector3(transform.position.x, oldPosition.y, 0.0f);
        }
        if(transform.position.x <= -178 || transform.position.x >= 178)
           {
            outOfbounds = true;
            transform.position = new Vector3(oldPosition.x, transform.position.y, 0.0f);

        }

    }

    public void processRedCarHornRCTut3()
    {
       

        if (shotSlider.value > redCarMark && shotSlider.value <= redCarMark + redCarDepth)
            GetTwoPointerTut3();


        else if (shotSlider.value > redCarMark + redCarDepth && shotSlider.value <= redCarMark + redCarDepth + redCarDepth)
            GetThreePointerTut3();
        else
        {
            sliderFill.color = Color.black;
            setBadShotTut3();
            processRedCarHorn = false;
        }
    }
    public Vector3 Position()
    {
        return transform.position;
    }
    public void ProcessRightClickTut3(int type)
    {
        if (!startSlider &!processRedCarHorn)
        {
            if (!badShot & !goodShot)
                startSlider = true;




        }
        else if (startSlider & !processRedCarHorn)
        {

            if (shotSlider.value > 1.0f && shotSlider.value <= 1.5f)
            {
                checkMaxScore(type);
            }

            else if (shotSlider.value > .5f && shotSlider.value <= 1.0f)
            {
                checkScore(type);
            }
            else
            {
                EndSliderShot();
            }

            SetAnimForAttempt(type);



        }


        else if (processRedCarHorn)
        {
            processRedCarHornRCTut3();
           
        }
        /*
        else if(processRedCarAlarm)
        {


            if (shotSlider.value > redCarAlarmMark && shotSlider.value <= redCarAlarmMark + redCarAlarmDepth)
                GetTwoPointerTut3();


            else if (shotSlider.value > redCarAlarmMark + redCarAlarmDepth && shotSlider.value <= redCarAlarmMark + redCarAlarmDepth + redCarAlarmDepth)
                GetThreePointerTut3();
            else
            {
                sliderFill.color = Color.black;
                setBadShotTut3();
                processRedCarAlarm = false;
            }


        }*/

    }

    public void ProcessRightClick(int type)
    {



        if (!startSlider &! processRedCarHorn)
        {
            if (!badShot & !goodShot)
                startSlider = true;




        }

        else if (startSlider & !processRedCarHorn)
        {

            if (shotSlider.value > 1.0f && shotSlider.value <= 1.5f)
            {
                checkMaxScore(type);
            }

            else if (shotSlider.value > .5f && shotSlider.value <= 1.0f)
            {
                checkScore(type);
            }
            else
            {
                EndSliderShot();
            }

            SetAnimForAttempt(type);

  

            }
            else if (processRedCarHorn)
            {

                if (shotSlider.value > redCarMark && shotSlider.value <= redCarMark + redCarDepth)
                {
                    GetTwoPointerCar();
                    processRedCarHorn = false;
                }

                else if (shotSlider.value > redCarMark + redCarDepth && shotSlider.value <= redCarMark + redCarDepth + redCarDepth)
                {
                    processRedCarHorn = false;
                    GetThreePointerCar();
                }
                else
                {
                EndSliderShot();
                }
            }

        }

    public void SetAnimForAttempt(int type)
    {
        if (type == 0)
            anim.SetTrigger("isShootTrigger");
        else if (type == 1)
            anim.SetTrigger("isDunkTrigger");


    }
    public void DeactivateSlider()
    {

        startSlider = false;
    }
    public void checkScore(int type)
    {
            if (type == 0)
                GetTwoPointer();
            if (type == 1)
                GetDunk();
        DeactivateSlider();
   }
    public void checkMaxScore(int type)
    {
        if (type == 0)
                GetThreePointer();
            if (type == 1)
                GetSuperDunk();
        DeactivateSlider();
    
    }
    public void EndSliderShot()
    {
        sliderFill.color = Color.black;
        setBadShot();
        startSlider = false;
        processRedCarHorn = false;

    }
    public void spawnChallenge(float TTL, int type)
    {

        

        GameObject currentChallenge = Instantiate(challenge, transform.position + new Vector3(0.0f, 25.0f, 0.0f), transform.rotation);

        currentChallenge.GetComponent<TimeToLiveChallengePopupText>().timeToLive = TTL;

        if (type == 0)
            cState = ChallengeState.DunkStart;
        if (type == 1)
            cState = ChallengeState.TwoStart;
        if (type == 2)
            cState = ChallengeState.ThreeStart;
        if (type == 4)
            cState = ChallengeState.SuperDunkStart;
    }

    public void checkChallenge(int type)
    {

        if (cState != ChallengeState.None)
        {
            if (cState == ChallengeState.TwoStart)
            {
                if (type == 1)
                {
                    cState = ChallengeState.TwoPass;
                }
                else
                {
                    cState = ChallengeState.TwoFail;
                }
            }
            if (cState == ChallengeState.DunkStart)
            {

                if (type == 0)
                {
                    cState = ChallengeState.DunkPass;
                }
                else
                {
                    cState = ChallengeState.DunkFail;
                }


            }
            if (cState == ChallengeState.ThreeStart)
            {

                if (type == 2)
                {
                    cState = ChallengeState.ThreePass;
                }
                else
                {
                    cState = ChallengeState.ThreeFail;
                }

            }
            if (cState == ChallengeState.SuperDunkStart)
            {

                if (type == 3)
                {
                    cState = ChallengeState.SuperDunkPass;
                }
                else
                {
                    cState = ChallengeState.SuperDunkFail;
                }


            }

        }
    }
    public void GetThreePointer()
    {

        Instantiate(three, transform.position + new Vector3(0.0f, 25.0f, 0.0f), transform.rotation);
        air += 3;
        audioSource.clip = threeAudio;
        audioSource.Play();
        setScoreString();
        setGoodShot();
        tut2Actions = Tut2FlagActions.Three;
        checkChallenge(2);
    }
    public void GetThreePointerCar()
    {

        Instantiate(three, transform.position + new Vector3(0.0f, 25.0f, 0.0f), transform.rotation);
        air += 6;
        audioSource.clip = threeAudio;
        audioSource.Play();
        setScoreString();
        setGoodShot();
        tut2Actions = Tut2FlagActions.Three;
        
      
    }
    public void GetSuperDunk()
    {

        Instantiate(three, transform.position + new Vector3(0.0f, 25.0f, 0.0f), transform.rotation);
        air += 3;
        audioSource.clip = threeAudio;
        audioSource.Play();
        setScoreString();
        setGoodShot();
        checkChallenge(3);
        tut2Actions = Tut2FlagActions.SuperDunk;
    }

    public void GetTwoPointer()
    {
        Instantiate(two, transform.position + new Vector3(0.0f, 25.0f, 0.0f), transform.rotation);
        air += 2;
        audioSource.clip = twoAudio;
        audioSource.Play();
        setScoreString();
        tut2Actions = Tut2FlagActions.Two;

        checkChallenge(1);
    }
    public void GetTwoPointerCar()
    {
        Instantiate(two, transform.position + new Vector3(0.0f, 25.0f, 0.0f), transform.rotation);
        air += 2;
        audioSource.clip = twoAudio;
        audioSource.Play();
        setScoreString();
        tut2Actions = Tut2FlagActions.Two;

       
    }

    public void GetDunk()
    {

        Instantiate(two, transform.position + new Vector3(0.0f, 25.0f, 0.0f), transform.rotation);
        air += 2;
        audioSource.clip = twoAudio;
        audioSource.Play();
        setScoreString();
        tut2Actions = Tut2FlagActions.Dunk;

        checkChallenge(0);


    }


    public void setScoreString()
    {
        scoreString = "In the Air: " + air.ToString() + " \nBanked: " + banked.ToString();
        scoreTMeshPro.text = scoreString;
        setGoodShot();
    }

    public void GetThreePointerTut3()
    {

        Instantiate(three, transform.position + new Vector3(0.0f, 25.0f, 0.0f), transform.rotation);
        air += 3;
        audioSource.clip = threeAudio;
        audioSource.Play();
        setScoreString();
        setGoodShot();

        if(tut3Actions == Tut3FlagActions.None)
        tut3Actions = Tut3FlagActions.Horn;

        else if (tut3Actions == Tut3FlagActions.AlarmCheck)
            tut3Actions = Tut3FlagActions.Alarm;

        

    }

    public void GetTwoPointerTut3()
    {
        Instantiate(two, transform.position + new Vector3(0.0f, 25.0f, 0.0f), transform.rotation);
        air += 2;
        audioSource.clip = twoAudio;
        audioSource.Play();
        setScoreString();
        tut2Actions = Tut2FlagActions.Two;
        if (tut3Actions == Tut3FlagActions.None)
            tut3Actions = Tut3FlagActions.Horn;

        else if (tut3Actions == Tut3FlagActions.AlarmCheck)
            tut3Actions = Tut3FlagActions.Alarm;

    }


    public Vector3 ZeroOutZ(Vector3 vector)
    {

        return new Vector3(vector.x, vector.y, 0.0f);
    }
    public Vector3 GetMouseInWorld(Vector3 mousePosition )
    {
        return ZeroOutZ(Camera.main.ScreenToWorldPoint(mousePosition));


    }
}
