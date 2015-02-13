namespace SurvivalGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public static class Saving
    {
        public static void Save()
        {
            SaveCurrentGame();
        }

        private static void SaveCurrentGame()
        {
            MapMaker myMap = new MapMaker();
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(myMap));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, overview);
            file.Close();
        }
    }
}
