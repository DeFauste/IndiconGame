using Assets.LoopBuild.Scripts.Manager;
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Assets.LoopBuild.Scripts.Saver
{
    public class SaverJson : ISavedata
    {
        public T Load<T>(string path)
        {
           try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string json = reader.ReadToEndAsync().Result;
                    var t = JsonUtility.FromJson<T>(json);
                    return t;
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
            return default(T);
        }

        public async void Save<T>(T data, string path)
        {
            try 
            {
                var json = JsonUtility.ToJson(data);
                Debug.Log(json);
                File.WriteAllText(path, json,Encoding.Default);

            } catch (Exception ex) 
            {
                Debug.Log(ex);
            }
        }
    }
}
