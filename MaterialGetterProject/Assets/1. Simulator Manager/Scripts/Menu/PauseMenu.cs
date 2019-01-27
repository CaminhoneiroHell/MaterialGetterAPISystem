using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button QuitButton;

    private void Start()
    {
        ResumeButton.onClick.AddListener(HandleResumeClicked);
        RestartButton.onClick.AddListener(HandleResumeClicked);
        QuitButton.onClick.AddListener(HandleResumeClicked);
    }

    void HandleResumeClicked()
    {
        SimulatorManager.Instance.TooglePause();
    }
    void HandleStartClicked()
    {
        SimulatorManager.Instance.RestartApplication();
    }
    void HandleQuitClicked()
    {
        SimulatorManager.Instance.QuitApplication();
    }

}
