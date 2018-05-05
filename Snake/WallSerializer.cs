using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Snake
{
    class WallSerializer
    {
        static Wall defaultWall = new Wall
        (
            new List<MyPoint>()
            {
                new MyPoint(40, 60),
                new MyPoint(60, 60),
                new MyPoint(40, 80),
                new MyPoint(340, 340),
                new MyPoint(320, 340),
                new MyPoint(340, 320)
            }
        );

        public static Wall GetWall()
        {
            string path = Environment.CurrentDirectory + @"\Walls\wall.xml";

            if (!File.Exists(path))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Walls");               
                SetWall(defaultWall);
            }
            
            XmlSerializer formatter = new XmlSerializer(typeof(Wall));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                Wall wall = (Wall)formatter.Deserialize(fs);
                return wall;
            }
        }

        public static void SetWall(Wall wall)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Wall));
            string path = Environment.CurrentDirectory + @"\Walls\wall.xml";
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, wall);               
            }
        }
    }
}
