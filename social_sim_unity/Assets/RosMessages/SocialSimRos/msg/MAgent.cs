//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;
using RosMessageTypes.Std;

namespace RosMessageTypes.SocialSimRos
{
    public class MAgent : Message
    {
        public const string RosMessageName = "social_sim_ros/Agent";

        //  Message defining an entry of a agent
        public MHeader header;
        //  Age of the track
        public ulong track_id;
        //  Unique ID for each agent
        //  Type of agent
        public string type;
        //  Pose of the track
        public Geometry.MPose pose;
        //  Velocity of the track
        public Geometry.MTwist twist;

        public MAgent()
        {
            this.header = new MHeader();
            this.track_id = 0;
            this.type = "";
            this.pose = new Geometry.MPose();
            this.twist = new Geometry.MTwist();
        }

        public MAgent(MHeader header, ulong track_id, string type, Geometry.MPose pose, Geometry.MTwist twist)
        {
            this.header = header;
            this.track_id = track_id;
            this.type = type;
            this.pose = pose;
            this.twist = twist;
        }
        public override List<byte[]> SerializationStatements()
        {
            var listOfSerializations = new List<byte[]>();
            listOfSerializations.AddRange(header.SerializationStatements());
            listOfSerializations.Add(BitConverter.GetBytes(this.track_id));
            listOfSerializations.Add(SerializeString(this.type));
            listOfSerializations.AddRange(pose.SerializationStatements());
            listOfSerializations.AddRange(twist.SerializationStatements());

            return listOfSerializations;
        }

        public override int Deserialize(byte[] data, int offset)
        {
            offset = this.header.Deserialize(data, offset);
            this.track_id = BitConverter.ToUInt64(data, offset);
            offset += 8;
            var typeStringBytesLength = DeserializeLength(data, offset);
            offset += 4;
            this.type = DeserializeString(data, offset, typeStringBytesLength);
            offset += typeStringBytesLength;
            offset = this.pose.Deserialize(data, offset);
            offset = this.twist.Deserialize(data, offset);

            return offset;
        }

        public override string ToString()
        {
            return "MAgent: " +
            "\nheader: " + header.ToString() +
            "\ntrack_id: " + track_id.ToString() +
            "\ntype: " + type.ToString() +
            "\npose: " + pose.ToString() +
            "\ntwist: " + twist.ToString();
        }
    }
}
