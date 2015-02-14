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
            SaveCharacter(writer);
            writer.WriteEndElement();
            writer.WriteStartElement("Enemy");
            SaveEnemy(writer);
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
                    writer.WriteStartElement("Walkable");
                    writer.WriteString(myMap.Rows[i].Columns[j].Walkable.ToString());
                    writer.WriteEndElement();
                    writer.WriteStartElement("SlopeMap");
                    writer.WriteString(myMap.Rows[i].Columns[j].SlopeMap.ToString());
                    writer.WriteEndElement();
                    writer.WriteStartElement("BaseTiles");
                    writer.WriteStartElement("Count");
                    writer.WriteString(myMap.Rows[i].Columns[j].BaseTiles.Count.ToString());
                    writer.WriteEndElement();
                    for (int k = 0; k < myMap.Rows[i].Columns[j].BaseTiles.Count; k++)
                    {
                        writer.WriteStartElement("BaseTileID");
                        writer.WriteString(myMap.Rows[i].Columns[j].BaseTiles[k].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("HeightTile");
                    writer.WriteStartElement("Count");
                    writer.WriteString(myMap.Rows[i].Columns[j].HeightTiles.Count.ToString());
                    writer.WriteEndElement();
                    for (int k = 0; k < myMap.Rows[i].Columns[j].HeightTiles.Count; k++)
                    {
                        writer.WriteStartElement("HeightTileID");
                        writer.WriteString(myMap.Rows[i].Columns[j].HeightTiles[k].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteStartElement("TopperTiles");
                    writer.WriteStartElement("Count");
                    writer.WriteString(myMap.Rows[i].Columns[j].TopperTiles.Count.ToString());
                    writer.WriteEndElement();
                    for (int k = 0; k < myMap.Rows[i].Columns[j].TopperTiles.Count; k++)
                    {
                        writer.WriteStartElement("TopperTileID");
                        writer.WriteString(myMap.Rows[i].Columns[j].TopperTiles[k].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

        private static void SaveCharacter(XmlTextWriter writer)
        {
            Character character = Program.game.play.character;
        }

        private static void SaveEnemy(XmlTextWriter writer)
        {

        }
    }
}
