using UnityEngine;
using System;

[Serializable] 
public class BlockData
{
    public string Id
    {
        get
        {
            return id;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }

    [SerializeField] 
    private string id;

    [SerializeField] 
    private Sprite sprite;
}
