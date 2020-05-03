using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AF.StupidSnakeGame
{
    public class InputCommandController : MonoBehaviour, IInputCommandController
    {
        Dictionary<KeyCode, bool> _currentInputStatus;
        [SerializeField] InputLayoutSO _inputLayoutSO = default;
        public IInputLayout inputLayout = default;

        public Dictionary<KeyCode, bool> CurrentInputStatus
        {
            get
            {
                return _currentInputStatus;
            }
        }
        public HashSet<KeyCommand> GetActiveCommands()
        {
            HashSet<KeyCommand> keyCommands = new HashSet<KeyCommand>();
            foreach (KeyCode activeInput in _currentInputStatus.Where(x => x.Value).Select(x => x.Key))
            {
                keyCommands.Add(inputLayout.GetEasyLookupMapping[activeInput]);
            }

            return keyCommands;
        }

        void Awake()
        {
            _currentInputStatus = new Dictionary<KeyCode, bool>();
            inputLayout = inputLayout ?? _inputLayoutSO;
        }

        // Update is called once per frame
        void Update()
        {
            foreach (KeyCode item in (KeyCode[])Enum.GetValues(typeof(KeyCode)))
            {
                _currentInputStatus[item] = Input.GetKey(item);
            }

            foreach (var item in GetActiveCommands())
            {
                Debug.Log(item.ToString());
            }
        }
    }
}