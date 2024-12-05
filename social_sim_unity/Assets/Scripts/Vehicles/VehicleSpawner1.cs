using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

namespace Vehicles
{
    public class VehicleSpawner1 : MonoBehaviour
    {
        [SerializeField] private string topicName;            // Name of the topic to publish
        [SerializeField] private float poissonRate = 0.03f;   // Poisson Distribution expected rate per second
        [SerializeField] private int xDirection = -1;         // Vehicle direction in x axis (+1 or -1)
        [SerializeField] private float moveSpeed = 15.0f;     // Vehicle speed
        [SerializeField] private float maxDistance = 184.0f;  // Maximum distance that a vehicle travels
        [SerializeField] private Transform myRobot;           // Robot's pose in the scene

        private Transform target;
        private Transform[] children;
        private List<int> activeChildIdx;
        private Vector3 originalPosition;
        private bool collided;
        private int collisionCount;

        private static ROSConnection ros;
        private float publishMessageFrequency = 0.5f;
        private float timeElapsed;
        private MInt32 message;

        void Start()
        {
            ros = ROSConnection.instance;

            InitializeVariables();
            InitializeMessage();
            StartCoroutine(PoissonSpawn(poissonRate));
        }

        void FixedUpdate()
        {
            UpdateMessage();
        }

        private void InitializeVariables()
        {
            collisionCount = 0;
            collided = false;

            int childCount = transform.childCount;
            children = new Transform[childCount];
            activeChildIdx = new List<int>();

            for (int i = 0; i < childCount; i++)
            {
                children[i] = transform.GetChild(i);
                children[i].gameObject.SetActive(false);
                activeChildIdx.Add(i);
            }
        }

        private void InitializeMessage()
        {
            message = new MInt32();
            message.data = 0;
        }

        private void UpdateMessage()
        {
            timeElapsed += Time.fixedDeltaTime;
            if (timeElapsed <= publishMessageFrequency) return;

            message.data = collisionCount;
            ros.Send(topicName, message);
            timeElapsed = 0;
        }

        // Spawn a vehicle based on Poisson Distribution
        private IEnumerator PoissonSpawn(float lambda)
        {
            while (true)
            {
                // Wait random interval based on Poisson Distribution to spawn a vehicle
                float interval = (float)PoissonInterval(lambda);

                Debug.Log("[1] Wait Time: " + interval);
                yield return new WaitForSeconds(interval);

                // Activate a vehicle
                StartVehicle();
            }
        }

        // Generate a random interval between events based on Poisson Distribution
        static double PoissonInterval(float lambda)
        {
            double randomVal = UnityEngine.Random.value;
            return -Math.Log(1 - randomVal) / lambda;
        }

        // Choose a vehicle, activate, move, and reset
        private void StartVehicle()
        {
            int inactiveCount = 0;
            foreach (Transform child in children)
            {
                if (!child.gameObject.activeSelf) inactiveCount++;
            }

            // Return if there are no vehicles at all or no inactive vehicles to activate
            if (children.Length == 0 || inactiveCount == 0) return;

            // Select a random vehicle that is not already on the road (inactive GameObject)
            int randomIdx;
            while (true)
            {
                randomIdx = UnityEngine.Random.Range(0, children.Length);
                if (!children[randomIdx].gameObject.activeSelf) break;
            }

            Transform selectedChild = children[randomIdx];

            // Store original position of the vehicle
            originalPosition = selectedChild.position;

            // Activate and move the vehicle
            selectedChild.gameObject.SetActive(true);
            StartCoroutine(MoveVehicle(randomIdx, selectedChild, originalPosition));
        }

        // Move the selected vehicle to target position
        private IEnumerator MoveVehicle(int idx, Transform child, Vector3 originalPos)
        {
            Vector3 targetPosition = new Vector3(
                child.position.x + xDirection * maxDistance,
                child.position.y,
                child.position.z
            );

            // Gradually move the vehicle to the target position
            while (Vector3.Distance(child.position, targetPosition) > 0.01f)
            {
                child.position = Vector3.MoveTowards(child.position, targetPosition, moveSpeed * Time.deltaTime);
                collided = CheckCollision(idx, child);

                if (collided)
                {
                    collisionCount++;
                    collided = false;
                    break;
                }

                yield return null;  // allow Unity to process other tasks out of the loop
            }

            // Reset the vehicle
            ResetVehicle(idx, child, originalPos);
        }

        // Reset the selected vehicle to original position and deactivate it
        private void ResetVehicle(int idx, Transform child, Vector3 originalPos)
        {
            child.gameObject.SetActive(false);
            child.position = originalPos;
            activeChildIdx.Add(idx);
        }

        // Check for collision
        private bool CheckCollision(int idx, Transform child)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            Vector3 size = renderer.bounds.size;
            float carLength = size.x;
            float carWidth = size.z;
            float robotRadius = 0.16f;

            float distanceX = Mathf.Abs(child.position.x - myRobot.position.x);
            float distanceZ = Mathf.Abs(child.position.z - myRobot.position.z);

            if (distanceX < (robotRadius + carLength / 2 + 0.25f) && distanceZ < (robotRadius + carWidth / 2 + 0.05f))
            {
                if (activeChildIdx.Remove(idx)) return true;
                else return false;
            }
            else return false;
        }
    }
}