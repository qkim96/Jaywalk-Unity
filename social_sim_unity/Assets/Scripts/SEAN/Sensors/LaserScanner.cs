// Copyright (c) 2021, Members of Yale Interactive Machines Group, Yale University,
// Nathan Tsoi
// All rights reserved.
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using UnityEngine;

namespace SEAN.Sensors
{
    public abstract class LaserScanner : MonoBehaviour
    {
        public abstract float[] Scan();
        public abstract float ScanPeriod();
        public abstract RosMessageTypes.Sensor.MLaserScan InitializeMessage(string FrameID);
    }
}
