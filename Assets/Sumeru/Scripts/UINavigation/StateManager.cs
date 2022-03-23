using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    #region singleton

    private static StateManager _instance;

    public static StateManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    private MainState _currentState;
    [SerializeField] List<MainState> _mainStates;
    [SerializeField] int _currentIndex = 0;


    public State currentState;

    public MainState CurrentState
    {
        get => _currentState;

        set
        {
            if (_currentState != null)
            {
                _currentState.Ended();
            }
            _currentState = value;
            _currentState.ResetEverything();
        }
    }


    private void Start()
    {
        InitializeStates();
    }

    private void Update()
    {
        
    }

    void InitializeStates()
    {
        SwitchStates(State.FlashScreen);
    }

    public void SwitchStates(State state)
    {
        if((int)state < _mainStates.Count)
        {
            CurrentState = _mainStates[(int)state];
            currentState = state;
        }

    }

    public void SwitchStates(int state)
    {
        if(state < _mainStates.Count)
        {
            CurrentState = _mainStates[state];
            currentState = (State)state;
        }

    }
}

public enum State
{
    FlashScreen,
    MenuScreen,
    ARScreen
}
