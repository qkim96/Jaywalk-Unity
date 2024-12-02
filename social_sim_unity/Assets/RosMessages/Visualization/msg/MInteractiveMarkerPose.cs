//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;
using RosMessageTypes.Std;

namespace RosMessageTypes.Visualization
{
    public class MInteractiveMarkerPose : Message
    {
        public const string RosMessageName = "visualization_msgs/InteractiveMarkerPose";

        //  Time/frame info.
        public MHeader header;
        //  Initial pose. Also, defines the pivot point for rotations.
        public Geometry.MPose pose;
        //  Identifying string. Must be globally unique in
        //  the topic that this message is sent through.
        public string name;

        public MInteractiveMarkerPose()
        {
            this.header = new MHeader();
            this.pose = new Geometry.MPose();
            this.name = "";
        }

        public MInteractiveMarkerPose(MHeader header, Geometry.MPose pose, string name)
        {
            this.header = header;
            this.pose = pose;
            this.name = name;
        }
        public override List<byte[]> SerializationStatements()
        {
            var listOfSerializations = new List<byte[]>();
            listOfSerializations.AddRange(header.SerializationStatements());
            listOfSerializations.AddRange(pose.SerializationStatements());
            listOfSerializations.Add(SerializeString(this.name));

            return listOfSerializations;
        }

        public override int Deserialize(byte[] data, int offset)
        {
            offset = this.header.Deserialize(data, offset);
            offset = this.pose.Deserialize(data, offset);
            var nameStringBytesLength = DeserializeLength(data, offset);
            offset += 4;
            this.name = DeserializeString(data, offset, nameStringBytesLength);
            offset += nameStringBytesLength;

            return offset;
        }

        public override string ToString()
        {
            return "MInteractiveMarkerPose: " +
            "\nheader: " + header.ToString() +
            "\npose: " + pose.ToString() +
            "\nname: " + name.ToString();
        }
    }
}
