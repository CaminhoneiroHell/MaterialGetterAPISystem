using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Patterns.Creational.Singleton;

public class SimulatorManager : Singleton<SimulatorManager>
{
    // What state the simulator is currently in
    // Load and unload states
    // Keep track of the sim state
    // generate other persistent systems
    //
    // Pause menu support
    // Add method to enter/exit pause
    // Trigger method via esc 
    // Trigger method via pause menu
    // Pause simulation when in pause state
    // Modify cursor to use pointer when in pause state

    public enum SimulatorState
    {
        BOOTING,
        RUNNING,
        PAUSED
    }

    public GameObject[] SystemPrefabsCreator;
    protected List<GameObject> _instancedSystemPrefabHandler;
    
    public Events.EventSimState OnSimStateChanged;
    List<AsyncOperation> _loadOperations;
    private string _currentLvlName = string.Empty;
    
    SimulatorState _currentSimulatorState = SimulatorState.BOOTING;

    public SimulatorState CurrentGameState
    {
        get { return _currentSimulatorState; }
        private set { _currentSimulatorState = value; }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        _loadOperations = new List<AsyncOperation>();

        InstantiateSystemPrefabs();

        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);

            if(_loadOperations.Count == 0) // Ensure all loads are complete
            {
                UpdateState(SimulatorState.RUNNING);
            }
        }

        //dispatch message
        //transition between scenes
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload complete");
    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        if(!fadeOut)
            UnloadLevel(_currentLvlName);
    }
    
    void UpdateState(SimulatorState state)
    {
        SimulatorState previousGameState = _currentSimulatorState;
        _currentSimulatorState = state;

        switch (_currentSimulatorState)
        {
            case SimulatorState.BOOTING:
                Time.timeScale = 1.0f;
                break;
            case SimulatorState.RUNNING:
                Time.timeScale = 1.0f;
                break;
            case SimulatorState.PAUSED:
                Time.timeScale = 0.0f;
                break;

            default:
                break;
        }

        OnSimStateChanged.Invoke(_currentSimulatorState, previousGameState); // Dispatching Messages

    }

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for(int i = 0; i < SystemPrefabsCreator.Length; ++i)
        {
            prefabInstance = Instantiate(SystemPrefabsCreator[i]);
            _instancedSystemPrefabHandler.Add(prefabInstance);
        }
    } 

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.Log("[GameManager] Unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);

        _currentLvlName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.Log("[GameManager] Unable to unload level " + levelName);
            return;
        }

        ao.completed += OnUnloadOperationComplete;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for(int i = 0; i < _instancedSystemPrefabHandler.Count; ++i)
        {
            Destroy(_instancedSystemPrefabHandler[i]);
        }
        _instancedSystemPrefabHandler.Clear();
    }

    public void StartApplication()
    {
        LoadLevel("Main");
    }
    
    private void Update()
    {
        if (_currentSimulatorState == SimulatorState.BOOTING)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) //Use dependency injection over here
        {
            TooglePause();
        }
    }

    public void TooglePause()
    {
        // condition ? true : false
        UpdateState(_currentSimulatorState == SimulatorState.RUNNING ? SimulatorState.PAUSED : SimulatorState.RUNNING);
    }

    public void RestartApplication()
    {
        UpdateState(SimulatorState.BOOTING);
    }

    public void QuitApplication()
    {

        //features for quitting
        Application.Quit();
    }

}
