using norbertcUtilities.GridGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarLines : MonoBehaviour
{
    [SerializeField] GameObject barLine;
    [SerializeField] float ypos = 100;
    [SerializeField] GridGenerator gridGenerator;
    [SerializeField] int xposOffset = -1200;

    void Start()
    {
        // spawn bar lines
        int xpos = xposOffset;
        for (int i = 0; i < (gridGenerator.width / 4)-1; i++)
        {
            var newBarLine = Instantiate(barLine, transform);
            newBarLine.transform.localPosition = new Vector3(xpos, ypos);
            xpos += 400;
        }
    }
}
