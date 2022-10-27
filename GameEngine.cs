using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class GameEngine : MonoBehaviour
{

    public GameObject gamePlayCanvasObject;
    public Canvas gamePlayCanvas;
    public Canvas monologueCanvas;
    public PlayerController playerController;
    public SpriteRenderer bigPlayer;
    public SpriteRenderer monologueBackdrop;

    public bool isChallenge = false;
    public bool isChallengeTut = false;
    public GameObject cheer;
    public GameObject token;
    public GameObject challengeToken;
    public BirdController birdController;

    public GameObject challengeToolTip;
    public TextMeshProUGUI challengeText;
    public float challengeTimer;

    public Car car;
    enum gameState
    {
        ChooseMode,
        Monologue,
        Pause,
        Action
    }

    public bool newTutorialEntered;
    enum challengeStateTut
    {

        GrabToken,
        GrabTokenSuccess,
        DunkChallengeStart,
        DunkChallengePass,
        DunkChallengeFail
    }

    challengeStateTut cTutState;
    enum challengeState
    {
        GrabToken,
        DunkChallenge,
        TwoPtChallenge,
        ThreePtChallenge,
        SuperDunkChallenge
    }

    challengeState cState;
    enum monologueScene
    {
        Tutorial1,
        Tutorial2,
        Tutorial3,
        Scene1
    }

    int tutorial1RightClicks;
    enum Tutorial2State
    {
        Missed,
        Two,
        Three
    }

    
    monologueScene mSceneState;
    public TMPro.TextMeshProUGUI textMeshPro;
    string choose1;
    string[] tutorial1;
    string[] tutorial2;
    string[] tutorial3;
    int tutorial1StringIndex;
    int tutorial2StringIndex;
    int tutorial3StringIndex;
    gameState gState;
    // Start is called before the first frame update
    void Start()
    {
        gState = gameState.ChooseMode;
        SetUpTutorial1Monologue();
        SetupTutorial2Monologue();
        SetupTutorial3Monologue();
        SetUpChoiceText();

        mSceneState = monologueScene.Tutorial1;

        tutorial1RightClicks = 0;

        textMeshPro.text = choose1;
        newTutorialEntered = false;
    }

    void SetUpChoiceText()
    {
        choose1 = 
            "Welcome To Hoop Dreamz.  To do the tutorial, Press R. To get to the action Press S";
        
    }

    void SetUpTutorial1Monologue()
    {

         tutorial1 = new string[4]{
            "Welcome to Hoop Dreamz.  This is the tutorial.\nPress the R button to progress through the Dialogues or when you see (R)",
            "Drag the icon on the bottom right to move the character (R)",
            "The game tries to keep you on the court (R)",
            "Click the S button to start a shot and release it to shoot (R)"   };


    }

    void SetupTutorial2Monologue()
    {
        tutorial2 = new string[8]
        {
            "When you see a @ in the chat there is an action to perform before continuing (R)",
            "Click S and release it before the bar turns green or wait for it to turn black, This means you missed @",
            "If you click and release S between 30% and 50% you get two points @",
            "If you click and release S between 50% and 75% you get three points @",
            "Dunks are just like shots except you hit the D button instead of the S (R)",
            "If you click and release D between 30% and 50% you Regular dunk and get two points @",
            "If you click and release D between 50% and 75% you SUPER dunk and get three points @",
            "The timer to shoot again is longer for missed shots than made shots (R)",
            
        };
    }


    void SetupTutorial3Monologue()
    {

        tutorial3 = new string[8]
        {   
            "The car is an opponent.  It uses sound to distract you (R)",
            "Make your shot after the car horn forces you to start by pressing (S) after the slider turns blue @",
            "The bird swoops down to zero out all points in the air. Dodge the bird by a distance of your height @",
            "Zeroed out your points even though you dodged the bird. 6 points will make you generate a token (Make 6 points)@",
            "The token will not go away until you reach it and attempt to do a challenge (Reach the token)@",
            "Win a challenge to bank the points and put them away safely by doing a regular Dunk@",
            "In the game, you may need to do a 2pt 3pt dunk or super dunk (R)",
            "Good job. Now try to bank 30 points (R)"
        };


    }
    void checkForRightClick()
    {


        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            playerController.ProcessRightClick(0);
        }

        if (Gamepad.current.buttonNorth.wasReleasedThisFrame)
        {
            playerController.ProcessRightClick(0);
        }

        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            playerController.ProcessRightClick(1);
        }
        if (Gamepad.current.buttonEast.wasReleasedThisFrame)
        {
            playerController.ProcessRightClick(1);
        }

    }

    void checkForRightClickTut3()
    {


        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            playerController.ProcessRightClickTut3(0);
        }

        if (Gamepad.current.buttonNorth.wasReleasedThisFrame)
        {
            playerController.ProcessRightClickTut3(0);
        }

        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            playerController.ProcessRightClickTut3(1);
        }
        if (Gamepad.current.buttonEast.wasReleasedThisFrame)
        {
            playerController.ProcessRightClickTut3(1);
        }

    }

    void checkLeftClick()
    {


        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        RaycastHit rcHit;
        Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
 


     


    }

    public void SetUpForActionState()
    {
        gState = gameState.Action;
        monologueCanvas.enabled = false;
        bigPlayer.enabled = false;
        mSceneState = monologueScene.Scene1;
        gState = gameState.Action;
        monologueBackdrop.sortingOrder = -70;
        playerController.tut3Flag = false;
        car.tut3Flag = false;
        birdController.SetSpriteRenderer7();
        car.cState = Car.carState.Timer;
        playerController.ActivateScript();
        car.ActivateScript();
        birdController.ActivateScript();
        playerController.air = 0;
        playerController.banked = 0;
        playerController.setScoreString();



    }

    public void SetUpForTutorial()
    {
        gState = gameState.Monologue;
        textMeshPro.text = tutorial1[tutorial1StringIndex];
        newTutorialEntered = false;

    }
    public void checkMovePlayer()
    {
        Vector2 leftStick = Gamepad.current.leftStick.ReadValue();
        if (Mathf.Abs(leftStick.x) > 0.0f || Mathf.Abs(leftStick.y) > 0.0f)
            playerController.TranslatePlayer(leftStick);


    }
    public void Tutorial1()
    {
       
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
           
            if(tutorial1StringIndex == 3)
            {
                SetupTutorial2State();
            }
            else
            {
                if(newTutorialEntered == false)
                {
                    newTutorialEntered = true;
                }
                else
                textMeshPro.text = tutorial1[++tutorial1StringIndex];
            }
        }

        if (tutorial1StringIndex == 1)
        {
            gamePlayCanvasObject.SetActive(true);
            playerController.ActivateScript();
        }
        if(tutorial1StringIndex == 3)
        {
            if(Gamepad.current.buttonNorth.wasPressedThisFrame)
            {
                mSceneState = monologueScene.Tutorial2;
            }
        }
        if(tutorial1StringIndex >= 1)
        {
            checkMovePlayer();

        }


    }
    public void SetupTutorial2State()
    {

        bigPlayer.flipX = true;
        mSceneState = monologueScene.Tutorial2;
        playerController.DeactivateScript();
        textMeshPro.text = tutorial2[tutorial2StringIndex];
        newTutorialEntered = false;

    }

    public void Tutorial2()
    {
        checkMovePlayer();

        if (tutorial2StringIndex == 0)
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                if (newTutorialEntered == false)
                {
                    newTutorialEntered = true;
                }
                else
                {
                    textMeshPro.text = tutorial2[++tutorial2StringIndex];
                    playerController.ActivateScript();
                }
            }
        }
        else if (tutorial2StringIndex == 1)
        {
            checkForRightClick();
            if (playerController.tut2Actions == PlayerController.Tut2FlagActions.Miss)

            {
                textMeshPro.text = tutorial2[++tutorial2StringIndex];
            }
        }
        else if (tutorial2StringIndex == 2)
        {
            checkForRightClick();
            if (playerController.tut2Actions == PlayerController.Tut2FlagActions.Two)

            {
                textMeshPro.text = tutorial2[++tutorial2StringIndex];
            }
        }
        else if (tutorial2StringIndex == 3)
        {
            checkForRightClick();
            if (playerController.tut2Actions == PlayerController.Tut2FlagActions.Three)

            {
                textMeshPro.text = tutorial2[++tutorial2StringIndex];
            }
        }
        else if (tutorial2StringIndex == 4)
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                textMeshPro.text = tutorial2[++tutorial2StringIndex];
            }

        }
        else if (tutorial2StringIndex == 5)
        {
            checkForRightClick();
            if (playerController.tut2Actions == PlayerController.Tut2FlagActions.Dunk)

            {
                textMeshPro.text = tutorial2[++tutorial2StringIndex];
            }
        }
        else if (tutorial2StringIndex == 6)
        {
            checkForRightClick();
            if (playerController.tut2Actions == PlayerController.Tut2FlagActions.SuperDunk)

            {
                textMeshPro.text = tutorial2[++tutorial2StringIndex];
            }
        }
        if (tutorial2StringIndex == 7)
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                SetUpTutorial3();
            }

        }

    }

    public void SetUpTutorial3()
    {
        mSceneState = monologueScene.Tutorial3;
        monologueBackdrop.sortingOrder = 4;
        playerController.DeactivateScript();
        playerController.tut3Flag = true;
        playerController.tut2Flag = false;
        textMeshPro.text = tutorial3[tutorial3StringIndex];
        bigPlayer.enabled = false;
        newTutorialEntered = false;


    }

    public void Tutorial3()
    {
        checkMovePlayer();

        if (tutorial3StringIndex == 0)
        {
            if (newTutorialEntered == false)
            {
                newTutorialEntered = true;
            }
            else if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                textMeshPro.text = tutorial3[++tutorial3StringIndex];
                playerController.ActivateScript();
                car.ActivateScript();
            }

        }

        else if (tutorial3StringIndex == 1)
        {

            checkForRightClickTut3();

            if (!car.tut3Flag)
            {
                car.tut3Flag = true;
                playerController.tut3Flag = true;
                car.setTut3Horn();
            }
            if (playerController.tut3Actions == PlayerController.Tut3FlagActions.Horn)
            {

                textMeshPro.text = tutorial3[++tutorial3StringIndex];
                car.DeactivateScript();
                playerController.tut3Flag = false;
                car.setTut3None();

            }

            if (playerController.tut3Actions == PlayerController.Tut3FlagActions.HornFail)
            {
                car.setTut3Horn();
                playerController.tut3Actions = PlayerController.Tut3FlagActions.None;
            }
        }
        else if (tutorial3StringIndex == 2)
        {
            checkForRightClickTut3();
            if (!birdController.tut3Flag)
            {
                birdController.ActivateScript();
                birdController.tut3Flag = true;
                birdController.setUpTut3Dive();
            }
            if (birdController.tut3Actions == BirdController.Tut3FlagActions.DivePass)
            {

                textMeshPro.text = tutorial3[++tutorial3StringIndex];
                birdController.DeactiveateScript();
                birdController.tut3Flag = false;
                birdController.setTut3None();
                playerController.air = 0;
                playerController.setScoreString();
            }
            else if (birdController.tut3Actions == BirdController.Tut3FlagActions.DiveFail)
            {
                birdController.setUpTut3Dive();

            }
        }
        else if (tutorial3StringIndex == 3)
        {
            checkForRightClickTut3();
            if (playerController.air > 5)
            {
                updateChallengeTut3();
                textMeshPro.text = tutorial3[++tutorial3StringIndex];

            }
         


        }
        else if (tutorial3StringIndex == 4)
        {
            checkForRightClickTut3();
            updateChallengeTut3();
            if (cTutState == challengeStateTut.GrabTokenSuccess)
            {
                textMeshPro.text = tutorial3[++tutorial3StringIndex];
            }
        }

        else if(tutorial3StringIndex == 5)
        {
            checkForRightClickTut3();
            updateChallengeTut3();
            if(cTutState == challengeStateTut.DunkChallengePass)
            {
                textMeshPro.text = tutorial3[++tutorial3StringIndex];

            }
            if(cTutState == challengeStateTut.DunkChallengeFail)
            {
                cTutState = challengeStateTut.GrabToken;
            }

        }
        else if(tutorial3StringIndex == 6)
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                textMeshPro.text = tutorial3[++tutorial3StringIndex];
            }
        }
        else if(tutorial3StringIndex == 7)
        {

            if(Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                SetUpForActionState();
            }
        }




    }
    // Update is called once per frame
    void Update()
    {

      

        if (gState == gameState.ChooseMode)
        {
            if (Gamepad.current.buttonNorth.wasPressedThisFrame)
            {
                SetUpForActionState();
            }

            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                SetUpForTutorial();
            }

        }



        //If game state is monologue
        if (gState == gameState.Monologue)
        {
            //if monologue state = tutorial 1
            if (mSceneState == monologueScene.Tutorial1)
            {
                Tutorial1();
            }
            else if (mSceneState == monologueScene.Tutorial2)
            {

                Tutorial2();
            }
            else if (mSceneState == monologueScene.Tutorial3)
            {
                Tutorial3();

            }
        }


        else if (gState == gameState.Action)
        {

            Action();

        }
    }
    public void Action()
    {

        Vector2 leftStick = Gamepad.current.leftStick.ReadValue();

        

        if (Mathf.Abs(leftStick.x) > 0.0f || Mathf.Abs(leftStick.y) > 0.0f)
            playerController.TranslatePlayer(leftStick);

        checkForRightClick();

        if (playerController.air > 5)
        {
            updateChallenge();
            
        }
        else
        {
            
            under5PointsEndChallenges();

        }

        if(playerController.banked >= 30)
        {

            birdController.DeactiveateScript();
            car.DeactivateScript();
            playerController.DeactivateScript();

            challengeToolTip.SetActive(true);
            challengeText.text= "Congratulations. You have won the game!";

          
                
        }



    }

    public void under5PointsEndChallenges()
    {
        cheer.SetActive(false);
        cState = challengeState.GrabToken;
        challengeToolTip.SetActive(false);
        if (challengeToken != null)
            challengeToken.SetActive(false);
    }
    public void updateChallengeTut3()
    {

      

        if (!isChallengeTut)
        {
            startChallengeTut();

        }
        else
        {
            checkChallengeTut3();


        }

    }
    public void updateChallenge()
    {

        if (!cheer.activeSelf)
        {
            cheer.SetActive(true);

            startChallenge();

        }
        else
        {
            checkChallenge();


        }

    }

    public void checkChallengeTut3()
    {
        if (cTutState == challengeStateTut.GrabToken)
        {
            if (Vector3.Distance(playerController.Position(), challengeToken.transform.position) < 5.0f)
            {
                cTutState = challengeStateTut.GrabTokenSuccess;
                playerController.spawnChallenge(5, 0);
                challengeTimer = 5;

                challengeToolTip.SetActive(false);
                challengeText.text = "DUNK CHALLENGE Bonus = Air * 4";
             

            }

        }
        else if (cTutState == challengeStateTut.GrabTokenSuccess)
        {
            processDunkChallengeTut();
        }
        else if(cTutState == challengeStateTut.DunkChallengeFail)
        {
            cTutState = challengeStateTut.GrabToken;
        }


    }

    public void processDunkChallengeTut()
    {
        challengeTimer -= Time.deltaTime;

        if (playerController.cState == PlayerController.ChallengeState.DunkPass)
        {
            playerController.banked += playerController.air * 4;

            playerController.setScoreString();
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);

            cTutState = challengeStateTut.DunkChallengePass;
            challengeToken.SetActive(false);
            isChallengeTut = false;
        }
        else if (playerController.cState == PlayerController.ChallengeState.DunkFail)
        {
            playerController.cState = PlayerController.ChallengeState.None;
            
            challengeToolTip.SetActive(false);
            cTutState = challengeStateTut.DunkChallengeFail;
        }
        else if (challengeTimer <= 0)
        {
            playerController.cState = PlayerController.ChallengeState.None;
          
            challengeToolTip.SetActive(false);
            cTutState = challengeStateTut.DunkChallengeFail;
        }


    }
    public void checkChallenge()
    {

        if (cState == challengeState.GrabToken)
        {
            if(challengeToken.activeSelf)
            if (Vector3.Distance(playerController.Position(), challengeToken.transform.position) < 5.0f)
            {
               
                int challengeType = Random.Range(0, 4);

                if (challengeType == 0)
                {
                    cState = challengeState.DunkChallenge;
                    playerController.spawnChallenge(5, 0);
                    challengeTimer = 5;

                    challengeToolTip.SetActive(true);
                    challengeText.text = "DUNK CHALLENGE Bonus = Air * 4";
                }
                if (challengeType == 1)
                {
                    cState = challengeState.TwoPtChallenge;
                    playerController.spawnChallenge(3,1);
                    challengeTimer = 3;

                    challengeToolTip.SetActive(true);
                    challengeText.text = "TWO CHALLENGE Bonus = Air * 2";
                }

                if (challengeType == 2)
                {
                    cState = challengeState.ThreePtChallenge;
                    playerController.spawnChallenge(3,2);
                    challengeTimer = 3;
                    challengeToolTip.SetActive(true);
                    challengeText.text = "THREE CHALLENGE Bonus = Air * 3";
                }
                if (challengeType == 3)
                {
                    cState = challengeState.SuperDunkChallenge;
                    playerController.spawnChallenge(5, 3);
                    challengeTimer = 5;
                    challengeToolTip.SetActive(true);
                    challengeText.text = "Super Dunk CHALLENGE Bonus = Air * 5";
                }
            }
        }

        if(cState == challengeState.DunkChallenge)
        {
            processDunkChallenge();
        }
        if (cState == challengeState.TwoPtChallenge)
        {
            processTwoPtChallenge();
        }
        if (cState == challengeState.ThreePtChallenge)
        {
            processThreePtChallenge();
        }
        if(cState == challengeState.SuperDunkChallenge)
        {
            processSuperDunkChallenge();
        }


    }

    public void processDunkChallengeTut3()
    {
        challengeTimer -= Time.deltaTime;

        if (playerController.cState == PlayerController.ChallengeState.DunkPass)
        {
            playerController.banked += playerController.air * 4;

            playerController.setScoreString();
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }
        else if (playerController.cState == PlayerController.ChallengeState.DunkFail)
        {
            playerController.cState = PlayerController.ChallengeState.None;
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }
        else if (challengeTimer <= 0)
        {
            playerController.cState = PlayerController.ChallengeState.None;
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }


    }

    public void processDunkChallenge()
    {
        challengeTimer -= Time.deltaTime;

        if(playerController.cState == PlayerController.ChallengeState.DunkPass)
        {
            playerController.banked += playerController.air * 4;

            playerController.setScoreString();
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }
        else if(playerController.cState == PlayerController.ChallengeState.DunkFail)
        {
            playerController.cState = PlayerController.ChallengeState.None;
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }
        else if(challengeTimer <= 0)
        {
            playerController.cState = PlayerController.ChallengeState.None;
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }


    }
    public void processSuperDunkChallenge()
    {
        challengeTimer -= Time.deltaTime;

        if (playerController.cState == PlayerController.ChallengeState.SuperDunkPass)
        {
            playerController.banked += playerController.air * 5;

            playerController.setScoreString();
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }
        else if (playerController.cState == PlayerController.ChallengeState.SuperDunkFail)
        {
            playerController.cState = PlayerController.ChallengeState.None;
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }
        else if (challengeTimer <= 0)
        {
            playerController.cState = PlayerController.ChallengeState.None;
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }


    }
    public void processTwoPtChallenge()
    {

        challengeTimer -= Time.deltaTime;
        if (playerController.cState == PlayerController.ChallengeState.TwoPass)
        {
            playerController.banked += playerController.air * 2;

            playerController.setScoreString();
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }
        else if (playerController.cState == PlayerController.ChallengeState.TwoFail)
        {
            playerController.cState = PlayerController.ChallengeState.None;
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }
        else if (challengeTimer <= 0)
        {
            playerController.cState = PlayerController.ChallengeState.None;
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }
    }

    public void processThreePtChallenge()
    {
        challengeTimer -= Time.deltaTime;
        if (playerController.cState == PlayerController.ChallengeState.ThreePass)
        {
            playerController.banked += playerController.air * 3;

            playerController.setScoreString();
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }
        else if (playerController.cState == PlayerController.ChallengeState.ThreeFail)
        {
            playerController.cState = PlayerController.ChallengeState.None;
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }
        else if (challengeTimer <= 0)
        {
            playerController.cState = PlayerController.ChallengeState.None;
            cState = challengeState.GrabToken;
            challengeToolTip.SetActive(false);
        }
    }

        public void startChallenge()
        {

        isChallenge = true;
            float tokenX = Random.Range(-160, 160);
            float tokenY = Random.Range(-50, -20);

            if(challengeToken == null)
            challengeToken = Instantiate(token, new Vector3(tokenX, tokenY, 0.0f), Quaternion.identity);
            else
        {
            challengeToken.SetActive(true);
            challengeToken.transform.position = new Vector3(tokenX, tokenY, 0.0f);
        }
        cState = challengeState.GrabToken;
        }

    public void startChallengeTut()
    {

        isChallengeTut = true;
        float tokenX = Random.Range(-160, 160);
        float tokenY = Random.Range(-50, -20);



        challengeToken = Instantiate(token, new Vector3(tokenX, tokenY, 0.0f), Quaternion.identity);
        cTutState = challengeStateTut.GrabToken;
    }

}
