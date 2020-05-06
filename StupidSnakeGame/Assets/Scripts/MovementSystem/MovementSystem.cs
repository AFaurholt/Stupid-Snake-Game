using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AF.StupidSnakeGame
{
    public class MovementSystem : MonoBehaviour
    {
        public float moveSpeedMs = 10f;
        public bool isCellBasedMovement = true;
        public float moveLength = 1f;
        float _currentMoveTime = 0f;
        Vector3 _lastVect3 = Vector3.zero;
        Vector3 _destination = Vector3.zero;
        Transform _tf = null;
        Rigidbody _rb = null;
        KeyCommand _currentKeyCommand = KeyCommand.None;
        KeyCommand _queuedCommand = KeyCommand.None;
        [SerializeField] DeltaToUse _deltaToUse = DeltaToUse.FixedDelta;

        public KeyCommand CurrentCommand
        {
            get
            {
                return _currentKeyCommand;
            }
        }
        public KeyCommand QueuedCommand
        {
            get
            {
                return _queuedCommand;
            }
        }
        void Move()
        {
            if (HasCommand())
            {
                var delta = _deltaToUse ==
                    DeltaToUse.FixedDelta ?
                    Time.fixedDeltaTime : Time.deltaTime;

                _currentMoveTime += delta;
                _rb.MovePosition(Vector3.Lerp(_lastVect3, _destination, _currentMoveTime / moveSpeedMs));
            }
        }
        void GetNextDestination()
        {
            /*If we can move, get the next position*/
            if (HasCommand())
            {
                _currentMoveTime = 0f;
                _lastVect3 = _tf.position;
                _destination = GetDestination();
            }
        }
        Vector3 GetDestination()
        {
            Vector3 direction = Vector3.zero;

            switch (_currentKeyCommand)
            {
                case KeyCommand.None:
                    break;
                case KeyCommand.MoveUp:
                    direction.y = 1;
                    break;
                case KeyCommand.MoveDown:
                    direction.y = -1;
                    break;
                case KeyCommand.MoveLeft:
                    direction.x = -1;
                    break;
                case KeyCommand.MoveRight:
                    direction.x = 1;
                    break;
                default:
                    break;
            }
            Vector3 destination = _lastVect3 + (direction * moveLength);
            return destination;
        }
        KeyCommand GetCurrentKeyCommand(HashSet<KeyCommand> keyCommands)
        {
            /*If same button is held, we'll prefer that*/
            if (keyCommands.Contains(_currentKeyCommand))
            {
                return _currentKeyCommand;
            }
            else
            {
                return keyCommands.First();
            }

        }
        bool CanMove()
        {
            return _currentMoveTime >= moveSpeedMs;
        }
        bool HasCommand()
        {
            return _currentKeyCommand != KeyCommand.None;
        }
        void UpdateCommandQueue()
        {
            /*If we can move we want to update the current command*/
            /*If we have a different command queued*/
            if (_currentKeyCommand != _queuedCommand && _queuedCommand != KeyCommand.None)
            {
                /*Make sure we prefer the queued input*/
                _currentKeyCommand = _queuedCommand;
            }

        }
        public void SetTransform(Transform tf)
        {
            _tf = tf;
        }
        public void SetRigidbody(Rigidbody rb)
        {
            _rb = rb;
        }
        public void UpdateMoveCommand(KeyCommand keyCommand)
        {
            /*Queue the command*/
            _queuedCommand = keyCommand;
        }
        public void UpdateMoveCommand(HashSet<KeyCommand> keyCommands)
        {
            UpdateMoveCommand(GetCurrentKeyCommand(keyCommands));
        }
        void FixedUpdate()
        {
            if (CanMove() || !HasCommand())
            {
                UpdateCommandQueue();
                GetNextDestination();
            }
            Move();

        }

    }

    [System.Serializable]
    enum DeltaToUse
    {
        FixedDelta,
        UpdateDelta
    }
}
