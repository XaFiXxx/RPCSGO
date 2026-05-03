using System.Collections.Generic;
using UnityEngine;

public class PlayerKeys : MonoBehaviour
{
    public List<string> keys = new List<string>();

    public bool HasKey(string key)
    {
        return keys.Contains(key);
    }

    public void AddKey(string key)
    {
        keys.Add(key);
        Debug.Log("Clé obtenue : " + key);
    }
}