using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PikachuItem
{
    public Sprite icon;
    private int m_id;
    public int Id {get => m_id; set => m_id = value;}
}
