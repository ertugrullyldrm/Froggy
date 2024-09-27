using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICell
{
    FrogColor Cell_Color { get; set; }
    CellType Cell_type { get; set; }


    int x { get; set; }
    int y { get; set; }
    int z { get; set; }
}
