using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class AddressablesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameEngine;

    [SerializeField]
    private GameObject birdController;

    [SerializeField]
    private GameObject car;

    [SerializeField]
    private AssetReference Player;

    private GameObject playerObject;

    [SerializeField]
    private AudioClip twoPts;
    [SerializeField]
    private AudioClip threePts;
    [SerializeField]
    private AudioClip challengeStart;
    [SerializeField]
    private AudioClip challengeFail;

    [SerializeField]
    private AudioClip shotFail;
    [SerializeField]
    private Slider Slider;
    [SerializeField]
    private Image SliderFill;
    [SerializeField]
    private TMPro.TextMeshProUGUI tmText;

    public bool setPlayerController;
    void Start()
    {
        setPlayerController = false;
        Addressables.InitializeAsync().Completed += AddressablesManager_Completed;
        
    }

    private void AddressablesManager_Completed(AsyncOperationHandle<IResourceLocator>obj)
    {

        Player.InstantiateAsync().Completed += (go) =>
        {
            playerObject = go.Result;
            playerObject.GetComponent<PlayerController>().shotSlider = Slider;
            playerObject.GetComponent<PlayerController>().sliderFill = SliderFill;
            playerObject.GetComponent<PlayerController>().scoreTMeshPro = tmText;

            if (gameEngine.GetComponent<GameEngine>() != null)
                gameEngine.GetComponent<GameEngine>().playerController = playerObject.GetComponent<PlayerController>();
            birdController.GetComponent<BirdController>().player = playerObject;
            car.GetComponent<Car>().playerController = playerObject.GetComponent<PlayerController>();

            playerObject.transform.position = new Vector3 (0.0f, -20.0f, 0.0f);

            playerObject.GetComponent<PlayerController>().twoAudio = twoPts;
            playerObject.GetComponent<PlayerController>().threeAudio = threePts;
            playerObject.GetComponent<PlayerController>().shotFail = shotFail;
            playerObject.GetComponent<PlayerController>().challengeStart = challengeStart;

        };

    }
    // Update is called once per frame
    void Update()
    {
        if(gameEngine.GetComponent<GameEngine>()!=null &! setPlayerController && playerObject != null)
        {
            gameEngine.GetComponent<GameEngine>().playerController = playerObject.GetComponent<PlayerController>();
            setPlayerController = true;
        }
    }
    private void OnDestroy()
    {
        Player.ReleaseInstance(playerObject);
    }
}
