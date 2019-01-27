using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Track animation component
    // Track animationClips for fade in/ out
    // Function that can receive animation events
    // Function to play fade in/out animations


    [SerializeField] private Animation _mainMenuAnimator;
    [SerializeField] private AnimationClip _fadeInAnimation;
    [SerializeField] private AnimationClip _fadeOutAnimation;

    public Events.EventFadeComplete OnMainMenuFadeComplete;


    private void Start()
    {
        SimulatorManager.Instance.OnSimStateChanged.AddListener(HandleGameStateChanged);
    }

    void HandleGameStateChanged(SimulatorManager.SimulatorState currentState, SimulatorManager.SimulatorState previousState)
    {
        if(previousState == SimulatorManager.SimulatorState.BOOTING && currentState == SimulatorManager.SimulatorState.RUNNING)
        {
            FadeOut();
        }

        if(previousState != SimulatorManager.SimulatorState.BOOTING && currentState == SimulatorManager.SimulatorState.BOOTING)
        {
            FadeIn();
        }

    }

    public void OnFadeOutComplete()
    {
        Debug.Log("Fadeout complete");
    }

    public void OnFadeInComplete()
    {
        OnMainMenuFadeComplete.Invoke(false);
        UIManager.Instance.SetDummyCameraActive(true);
        Debug.Log("Fadein complete");
    }

    public void FadeIn()
    {
        UIManager.Instance.SetDummyCameraActive(true);

        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeInAnimation;
        _mainMenuAnimator.Play();
    }

    public void FadeOut()
    {
        UIManager.Instance.SetDummyCameraActive(false);

        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeOutAnimation;
        _mainMenuAnimator.Play();
    }
    
}
