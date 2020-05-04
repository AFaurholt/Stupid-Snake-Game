using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AF.StupidSnakeGame
{

    [RequireComponent(typeof(InputCommandController), typeof(MovementSystem))]
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] InputCommandController _inputCommandController = default;
        [SerializeField] MovementSystem _movementSystem = default;
        [SerializeField] Rigidbody _rb = default;

        void Awake()
        {
            _movementSystem.SetTransform(transform);
            _movementSystem.SetRigidbody(_rb);
        }
        void FixedUpdate()
        {
            var activeCommands = _inputCommandController.GetActiveCommands();
            if (activeCommands.Count > 0)
            {
                _movementSystem.UpdateMoveCommand(activeCommands);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            //8 == Wall
            //9 == Pickup
            switch (collision.gameObject.layer)
            {
                case 8:
                    {
                        Debug.Log("Hit a wall");
                        break;
                    }
                case 9:
                    {
                        Debug.Log("Hit a pickup");
                        break;
                    }
                default:
                    break;
            }
        }
    }

}