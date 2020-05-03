using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AF.StupidSnakeGame
{
    [CreateAssetMenu(fileName = "Input Mapping", menuName = "Input System/Input Mapping", order = 1)]
    public class InputLayoutSO : ScriptableObject, IInputLayout
    {
        public List<KeyMapping> inputMappings = new List<KeyMapping>();
        public Dictionary<KeyCode, KeyCommand> GetEasyLookupMapping
        {
            get
            {
                Dictionary<KeyCode, KeyCommand> pairs = new Dictionary<KeyCode, KeyCommand>();
                foreach (var item in inputMappings)
                {
                    pairs.Add(item.keyCode, item.mapping);
                }

                return pairs;
            }
        }
    }
    [System.Serializable]
    public struct KeyMapping
    {
        public KeyCode keyCode;
        public KeyCommand mapping;
    }
    [System.Serializable]
    public enum KeyCommand
    {
        None,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight
    }
}
