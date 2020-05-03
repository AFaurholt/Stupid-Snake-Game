using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using AF.StupidSnakeGame;

namespace Tests
{
    public class InputCommandShould
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ConvertControlInputToInputCommand()
        {
            var go = new GameObject("SystemUnderTest");
            var sut = go.AddComponent<InputCommandController>();

            IInputLayout inputLayout = new MockInputLayout();

            sut.inputLayout = inputLayout;
            var eventUp = Event.KeyboardEvent("w");
            eventUp.Use();
            
            Debug.Log($"Before frame: {Event.GetEventCount()}");
            yield return new WaitForEndOfFrame();
           // Debug.Log($"After frame: {Event.GetEventCount()}");

            var actual = sut.GetActiveCommands();
            Assert.That(actual, Is.EquivalentTo(new HashSet<KeyCommand>() { KeyCommand.MoveUp }));
        }

        class MockInputLayout : IInputLayout
        {
            Dictionary<KeyCode, KeyCommand> _keyValuePairs = new Dictionary<KeyCode, KeyCommand>() {
                {KeyCode.UpArrow, KeyCommand.MoveUp },
                {KeyCode.DownArrow, KeyCommand.MoveDown },
                {KeyCode.LeftArrow, KeyCommand.MoveLeft },
                {KeyCode.RightArrow, KeyCommand.MoveRight },
                {KeyCode.A, KeyCommand.MoveLeft },
                {KeyCode.S, KeyCommand.MoveDown },
                {KeyCode.D, KeyCommand.MoveRight },
                {KeyCode.W, KeyCommand.MoveUp }
            };

            public Dictionary<KeyCode, KeyCommand> GetEasyLookupMapping
            {
                get
                {
                    return _keyValuePairs;
                }
            }
        }
    }
}
