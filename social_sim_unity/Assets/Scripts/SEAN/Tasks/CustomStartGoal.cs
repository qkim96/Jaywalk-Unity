// Copyright (c) 2021, Members of Yale Interactive Machines Group, Yale University,
// Nathan Tsoi
// All rights reserved.
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using UnityEngine;

namespace SEAN.Tasks
{
    public class CustomStartGoal : Base
    {
        public Vector3 startPosition;
        public Vector3 goalPosition;
        public Vector3 startRotation;
        public Vector3 goalRotation;
        public Vector3 rotationVector;

        protected override bool NewTask()
        {
            robotGoal.SetActive(true);

            startPosition.x = 28.7f;
            startPosition.y = 0.75f;
            startPosition.z = -103.6f; //-112.0f;
            goalPosition.x = 28.7f;
            goalPosition.y = 0.75f;
            goalPosition.z = -80.0f;
            rotationVector = new Vector3(0, 0, 0);

            robotStart.transform.position = startPosition;
            robotStart.transform.rotation = Quaternion.Euler(rotationVector);
            robotGoal.transform.position = goalPosition;
            robotGoal.transform.rotation = Quaternion.Euler(rotationVector);
            
            SetTargetFlags(robotGoal);

            return true;
        }
    }
}