//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.Gizmo
{
    public class MWakeUpRequest : Message
    {
        public const string RosMessageName = "gizmo_msgs/WakeUp";


        public MWakeUpRequest()
        {
        }
        public override List<byte[]> SerializationStatements()
        {
            var listOfSerializations = new List<byte[]>();

            return listOfSerializations;
        }

        public override int Deserialize(byte[] data, int offset)
        {

            return offset;
        }

        public override string ToString()
        {
            return "MWakeUpRequest: ";
        }
    }
}
