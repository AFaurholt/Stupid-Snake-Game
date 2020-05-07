using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AF.StupidSnakeGame
{
    public class TailBehaviour : MonoBehaviour
    {

        [SerializeField] MovementSystem _movementSystem;
        [SerializeField] Rigidbody _rb;

        private void Awake()
        {
            _movementSystem.SetTransform(transform);
            _movementSystem.SetRigidbody(_rb);
        }

        public void SetUp(MovementSystem movementSystem)
        {
            _movementSystem.SyncWith(movementSystem);
        }

        public void PushCommand(KeyCommand keyCommand)
        {
            _movementSystem.UpdateMoveCommand(keyCommand);
        }

        public KeyCommand GetQueuedCommand()
        {
            return _movementSystem.QueuedCommand;
        }

        public KeyCommand GetCurrentCommand()
        {
            return _movementSystem.CurrentCommand;
        }
    }
}