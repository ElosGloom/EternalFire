using System;
using System.Collections.Generic;

namespace Game.Scripts.States
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IState> _states = new();
        private IState _currentState;

        public void AddState<T>(T state) where T : IState
        {
            _states.Add(typeof(T), state);
        }

        public void SetState<T>() where T : IState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];
            _currentState.Enter();
        }

        public void Update()
        {
            _currentState.Update();
        }
    }
}