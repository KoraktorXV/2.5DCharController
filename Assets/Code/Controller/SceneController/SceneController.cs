using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private LevelController levelController;
    private PlayerController playerController;

    [SerializeField]
    private GameObject playerPrefab;
    private GameObject playerInstance;

    [SerializeField]
    private Transform playerSpawnPos;

    [SerializeField]
    private SoundEventSystem soundEvents;

    private InputController inputController;
    private CameraController camController;

    private void Awake()
    {
        inputController = new InputController();
    }

    private void Start()
    {
        camController = Camera.main.GetComponent<CameraController>();
        IniGame();
    }

    private void Update()
    {
        inputController.UpdateInput();

        if (playerController != null)
        {
            playerController.UpdateMovementInfo(inputController.GetMovementInfo());
        }
    }

    private void FixedUpdate()
    {
        if (playerController != null)
        {
            playerController.UpdatePlayerController();
        }        
    }

    private void IniGame()
    {
        if (playerPrefab != null)
        {
            playerInstance = Instantiate(playerPrefab, playerSpawnPos.position, Quaternion.identity);
            playerController = playerInstance.GetComponent<PlayerController>();
        }

        if (playerInstance != null)
        {
            camController.SetTargetToFollow(playerInstance);
        }
    }
}
