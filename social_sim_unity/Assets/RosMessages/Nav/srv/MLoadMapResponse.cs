//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.Nav
{
    public class MLoadMapResponse : Message
    {
        public const string RosMessageName = "nav_msgs/LoadMap";

        //  Result code defintions
        public const byte RESULT_SUCCESS = 0;
        public const byte RESULT_MAP_DOES_NOT_EXIST = 1;
        public const byte RESULT_INVALID_MAP_DATA = 2;
        public const byte RESULT_INVALID_MAP_METADATA = 3;
        public const byte RESULT_UNDEFINED_FAILURE = 255;
        //  Returned map is only valid if result equals RESULT_SUCCESS
        public MOccupancyGrid map;
        public byte result;

        public MLoadMapResponse()
        {
            this.map = new MOccupancyGrid();
            this.result = 0;
        }

        public MLoadMapResponse(MOccupancyGrid map, byte result)
        {
            this.map = map;
            this.result = result;
        }
        public override List<byte[]> SerializationStatements()
        {
            var listOfSerializations = new List<byte[]>();
            listOfSerializations.AddRange(map.SerializationStatements());
            listOfSerializations.Add(new[] { this.result });

            return listOfSerializations;
        }

        public override int Deserialize(byte[] data, int offset)
        {
            offset = this.map.Deserialize(data, offset);
            this.result = data[offset]; ;
            offset += 1;

            return offset;
        }

        public override string ToString()
        {
            return "MLoadMapResponse: " +
            "\nmap: " + map.ToString() +
            "\nresult: " + result.ToString();
        }
    }
}
