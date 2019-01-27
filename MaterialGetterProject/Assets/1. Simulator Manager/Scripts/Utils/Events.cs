using UnityEngine.Events;
public class Events
{
    [System.Serializable]
    public class EventFadeComplete: UnityEvent<bool> { }
    [System.Serializable] public class EventSimState : UnityEvent<SimulatorManager.SimulatorState, SimulatorManager.SimulatorState> { }
}
