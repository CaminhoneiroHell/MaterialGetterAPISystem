using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns.Creational.Singleton;

public class UIManager : Singleton<UIManager>
{ 
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PauseMenu _pauseMenu;

    [SerializeField] private Camera _dummyCamera;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    private void Start()
    {
        _mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
        SimulatorManager.Instance.OnSimStateChanged.AddListener(HandleGameStateChanged);
    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        OnMainMenuFadeComplete.Invoke(fadeOut);
    }

    void HandleGameStateChanged(SimulatorManager.SimulatorState currentState, SimulatorManager.SimulatorState previousState)
    {
        _pauseMenu.gameObject.SetActive(currentState == SimulatorManager.SimulatorState.PAUSED);
    }

    private void Update()
    {
        if(SimulatorManager.Instance.CurrentGameState != SimulatorManager.SimulatorState.BOOTING)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //_mainMenu.FadeOut();
            SimulatorManager.Instance.StartApplication(); //Remove from here
        }
    }

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.gameObject.SetActive(active);
    }
}
