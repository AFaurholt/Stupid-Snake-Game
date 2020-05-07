using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        List<GameObject> _tails;
        List<TailBehaviour> _tailBehaviours;
        [SerializeField] GameObject _tailPrefab;
        [SerializeField] float _tailOffset;

        void Awake()
        {
            _tails = new List<GameObject>();
            _tailBehaviours = new List<TailBehaviour>();
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
            HashSet<KeyCommand> activeCommands = _inputCommandController.GetActiveCommands();
            if (activeCommands.Count > 0)
            {
                _movementSystem.UpdateMoveCommand(activeCommands);
            }

            for (int i = 0; i < _tailBehaviours.Count; i++)
            {
                if (i > 0)
                {
                    _tailBehaviours[i].PushCommand(_tailBehaviours[i - 1].GetCurrentCommand());
                }
                else
                {
                    _tailBehaviours[i].PushCommand(_movementSystem.CurrentCommand);
                }
            }
        }

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
                        SpawnTail();
                        break;
                    }
                default:
                    break;
            }
        }

        private void SpawnTail()
        {
            if (_tails.Count > 0)
            {

                TailBehaviour tailBehaviour = _tailBehaviours.Last();
                SpawnTail(tailBehaviour.GetCurrentCommand(), tailBehaviour.transform);
            }
            else
            {
                SpawnTail(_movementSystem.CurrentCommand, transform);

            }

        }

        private void SpawnTail(KeyCommand currentDirection, Transform _transform)
        {
            Vector3 offset = Vector3.zero;
            switch (currentDirection)
            {
                case KeyCommand.None:
                    break;
                case KeyCommand.MoveUp:
                    offset = new Vector3(0, 1) * _tailOffset;

                    break;
                case KeyCommand.MoveDown:
                    offset = new Vector3(0, -1) * _tailOffset;

                    break;
                case KeyCommand.MoveLeft:
                    offset = new Vector3(-1, 0) * _tailOffset;

                    break;
                case KeyCommand.MoveRight:
                    offset = new Vector3(1, 0) * _tailOffset;

                    break;
                default:
                    break;
            }

            var tail = Instantiate(_tailPrefab, _transform.position - offset, Quaternion.identity);
            _tails.Add(tail);

            var tailBehav = (TailBehaviour)tail.GetComponent(typeof(TailBehaviour));
            tailBehav.SetUp(_movementSystem);
            _tailBehaviours.Add(tailBehav);
        }
    }
}