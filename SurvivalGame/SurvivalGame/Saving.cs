namespace SurvivalGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    public static class Saving
    {
        public static void Save()
        {
            SaveCurrentGame();
        }

        private static void SaveCurrentGame()
        {
            XmlTextWriter writer = new XmlTextWriter("game.sgs", System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("SaveGame");
            writer.WriteStartElement("Map");
            SaveMap(writer);
            writer.WriteEndElement();
            writer.WriteStartElement("Character");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        private static void SaveMap(XmlTextWriter writer)
        {
            MapMaker myMap = Program.game.play.mapReader.MyMap;

            for (int i = 0; i < myMap.MapHeight; i++)
            {
                writer.WriteStartElement("Row" + (i + 1).ToString());
                for (int j = 0; j < myMap.MapWidth; j++)
                {
                    writer.WriteStartElement("Column" + (j + 1).ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }
    }
}
