using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AF.StupidSnakeGame
{
    public interface IInputLayout
    {
        Dictionary<KeyCode, KeyCommand> GetEasyLookupMapping { get; }
    }
}
