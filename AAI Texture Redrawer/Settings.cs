using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AAI_Texture_Redrawer
{
    class Settings
    {
        private Dictionary<string, string> paths;

        public Settings()
        {
            paths = new Dictionary<string, string>();
            Read();
        }

        public void Save(BinaryWriter bw)
        {
            bw.Write(paths.Count);
            foreach (string key in paths.Keys)
            {
                bw.Write(key);
                bw.Write(paths[key]);
            }
        }

        public void Save(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(fs))
                Save(bw);
        }

        public void Save()
        {
            Save("settings.bin");
        }

        public void Read(BinaryReader br)
        {
            int l = br.ReadInt32();
            for (int i = 0; i < l; i++)
            {
                string key = br.ReadString();
                string val = br.ReadString();
                paths.Add(key, val);
            }
        }

        public void Read(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
                Read(br);
        }

        public void Read()
        {
            if (File.Exists("settings.bin"))
                Read("settings.bin");
            else
            {
                SetDefault();
            }
        }

        private void SetDefault()
        {
            Clear();
            string[] names = MainManager.names;
            foreach (string name in names)
            {
                paths.Add(name, "");
            }
        }

        public string GetPath(string key)
        {
            return paths[key];
        }

        public void SetPath(string key, string val)
        {
            paths[key] = val;
        }

        public void Clear()
        {
            paths.Clear();
        }

        public string[] GetPaths()
        {
            List<string> res = new List<string>();
            foreach (string key in paths.Keys)
            {
                res.Add(paths[key]);
            }
            return res.ToArray();
        }
    }
}
