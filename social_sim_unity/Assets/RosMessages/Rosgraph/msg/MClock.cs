//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;
using RosMessageTypes.Std;

namespace RosMessageTypes.Rosgraph
{
    public class MClock : Message
    {
        public const string RosMessageName = "rosgraph_msgs/Clock";

        //  roslib/Clock is used for publishing simulated time in ROS. 
        //  This message simply communicates the current time.
        //  For more information, see http://www.ros.org/wiki/Clock
        public MTime clock;

        public MClock()
        {
            this.clock = new MTime();
        }

        public MClock(MTime clock)
        {
            this.clock = clock;
        }
        public override List<byte[]> SerializationStatements()
        {
            var listOfSerializations = new List<byte[]>();
            listOfSerializations.AddRange(clock.SerializationStatements());

            return listOfSerializations;
        }

        public override int Deserialize(byte[] data, int offset)
        {
            offset = this.clock.Deserialize(data, offset);

            return offset;
        }

        public override string ToString()
        {
            return "MClock: " +
            "\nclock: " + clock.ToString();
        }
    }
}
