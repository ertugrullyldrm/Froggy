using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFrog
{
    FrogColor Frog_Color { get; set; }
    public int frogToungeSpeed { get; set; }
    public GameObject tonguePrefab { get; set; }
    public string eatableBarryTag { get; set; }
    public LookType Look_type { get; set; }
}
