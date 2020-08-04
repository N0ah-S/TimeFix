using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEditor;

[Serializable]
public class Item {
    [SerializeField]
    public string name;
    [TextArea(3, 10)]
    public string description;
    [SerializeField]
    public Sprite sprite;

    public Item(string name,string description, Sprite sprite) {
        this.name = name;
        this.description = description;
        this.sprite = sprite;
    }
}
