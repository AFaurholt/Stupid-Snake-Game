using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AF.StupidSnakeGame
{

    [RequireComponent(typeof(InputCommandController), typeof(MovementSystem))]
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] InputCommandController _inputCommandController = default;
        [SerializeField] MovementSystem _movementSystem = default;
        [SerializeField] TextMesh _scoreTextMesh = default;
        [SerializeField] PickupSpawn _pickupSpawn = default;
        [SerializeField] Rigidbody _rb = default;
        [HideInInspector] public int score = 0;
        [SerializeField] string _scoreText = "Score: ";

        void Awake()
        {
            _scoreTextMesh.text = $"{_scoreText}{score}";
            _movementSystem.SetTransform(transform);
            _movementSystem.SetRigidbody(_rb);
        }

        private void Start()
        {
            _pickupSpawn.SpawnPickup();
        }

        void FixedUpdate()
        {
            var activeCommands = _inputCommandController.GetActiveCommands();
            if (activeCommands.Count > 0)
            {
                _movementSystem.UpdateMoveCommand(activeCommands);
            }
        }
        //void OnCollisionEnter(Collision collision)
        //{
        //    //8 == Wall
        //    //9 == Pickup
        //    //switch (collision.gameObject.layer)
        //    //{
        //    //    case 8:
        //    //        {
        //    //            //TODO death screen
        //    //            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //    //            break;
        //    //        }
        //    //    case 9:
        //    //        {
        //    //            score++;
        //    //            _scoreTextMesh.text = $"{_scoreText}{score}";
        //    //            Destroy(collision.transform.root.gameObject);
        //    //            _pickupSpawn.SpawnPickup();
        //    //            break;
        //    //        }
        //    //    default:
        //    //        break;
        //    //}
        //}

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.layer)
            {
                case 8:
                    {
                        //TODO death screen
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        break;
                    }
                case 9:
                    {
                        score++;
                        _scoreTextMesh.text = $"{_scoreText}{score}";
                        Destroy(other.transform.root.gameObject);
                        _pickupSpawn.SpawnPickup();
                        break;
                    }
                default:
                    break;
            }
        }
    }

}