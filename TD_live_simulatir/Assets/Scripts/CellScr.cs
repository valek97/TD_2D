using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellScr : MonoBehaviour
{
    public int state, id;

    public Color normCol, pathCol;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetState(int i)
    {
        state = i;
        if (i == 0)
            GetComponent<Image>().color = normCol;
        if (i==1)
            GetComponent<Image>().color = pathCol;

    }
}
