//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.Gizmo
{
    public class MMomentAction : Message
    {
        public const string RosMessageName = "gizmo_msgs/MomentAction";

        public string @event;
        public string uuid;

        public MMomentAction()
        {
            this.@event = "";
            this.uuid = "";
        }

        public MMomentAction(string @event, string uuid)
        {
            this.@event = @event;
            this.uuid = uuid;
        }
        public override List<byte[]> SerializationStatements()
        {
            var listOfSerializations = new List<byte[]>();
            listOfSerializations.Add(SerializeString(this.@event));
            listOfSerializations.Add(SerializeString(this.uuid));

            return listOfSerializations;
        }

        public override int Deserialize(byte[] data, int offset)
        {
            var @eventStringBytesLength = DeserializeLength(data, offset);
            offset += 4;
            this.@event = DeserializeString(data, offset, @eventStringBytesLength);
            offset += @eventStringBytesLength;
            var uuidStringBytesLength = DeserializeLength(data, offset);
            offset += 4;
            this.uuid = DeserializeString(data, offset, uuidStringBytesLength);
            offset += uuidStringBytesLength;

            return offset;
        }

        public override string ToString()
        {
            return "MMomentAction: " +
            "\n@event: " + @event.ToString() +
            "\nuuid: " + uuid.ToString();
        }
    }
}
