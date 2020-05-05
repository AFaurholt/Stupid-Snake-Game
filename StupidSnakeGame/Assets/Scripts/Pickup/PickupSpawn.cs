using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AF.StupidSnakeGame
{
    public class PickupSpawn : MonoBehaviour
    {

        List<MeshWithWorldPos> _meshWithWorldPos = new List<MeshWithWorldPos>();
        [SerializeField] GameObject _pickupPrefab = default;

        private void Awake()
        {
            foreach (Component child in GetComponentsInChildren(typeof(MeshFilter)))
            {
                Mesh mesh = ((MeshFilter)child).mesh;
                Vector3 worldPos = child.transform.position;
                Vector3 scale = child.transform.localScale;

                _meshWithWorldPos.Add(
                    new MeshWithWorldPos { 
                        mesh = mesh, 
                        worldPos = worldPos, 
                        scale = scale });

                Destroy(child.gameObject);
            }
        }

        public void SpawnPickup()
        {
            int index = Random.Range(0, _meshWithWorldPos.Count);
            var current = _meshWithWorldPos[index];

            Vector3 fromVect3 = new Vector3(
                current.worldPos.x + current.mesh.bounds.size.x * (current.scale.x / 2),
                current.worldPos.y + current.mesh.bounds.size.y * (current.scale.y / 2),
                current.worldPos.z + current.mesh.bounds.size.z * (current.scale.z / 2));
            Vector3 toVect3 = new Vector3(
                current.worldPos.x - (current.mesh.bounds.size.x * (current.scale.x / 2)),
                current.worldPos.y - (current.mesh.bounds.size.y * (current.scale.y / 2)),
                current.worldPos.z - (current.mesh.bounds.size.z * (current.scale.z / 2)));

            Vector3 spawnPos = new Vector3(
                Random.Range(fromVect3.x, toVect3.x),
                Random.Range(fromVect3.y, toVect3.y),
                Random.Range(fromVect3.z, toVect3.z));

            Instantiate(_pickupPrefab, spawnPos, Quaternion.identity);
        }


    }
    [System.Serializable]
    public struct MeshWithWorldPos
    {
        public Mesh mesh;
        public Vector3 worldPos;
        public Vector3 scale;
    }
}