using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State currentState;
    protected Dictionary<string, State> states = new Dictionary<string, State>();

    public void ChangeState<T>() where T : State
    {
        string stateName = typeof(T).Name;
        if (!states.ContainsKey(stateName))
        {
            T state = gameObject.AddComponent<T>();
            states.Add(stateName, state);
        }

        if(states[stateName] != currentState)
        {
            if(currentState != null) currentState.Exit();
            currentState = states[stateName];
            currentState.Enter();
        }

    }

    public void Update()
    {
        if(currentState != null) currentState.Execute();
    }
}
