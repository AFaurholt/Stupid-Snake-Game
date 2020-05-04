using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AF.StupidSnakeGame
{
    public class DieOnCollisionBehaviour : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Ded");
        }
    }
}