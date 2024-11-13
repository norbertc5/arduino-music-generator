using norbertcUtilities.GridGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarLines : MonoBehaviour
{
    [SerializeField] GameObject barLine;
    [SerializeField] float barLinesYpos = 100;
    [SerializeField] GridGenerator gridGenerator;
    [SerializeField] int xposOffset = -1200;

    [SerializeField] GameObject octaveLine;
    [SerializeField] int yposOffset = 0;

    void Start()
    {
        // spawn bar lines
        int xpos = xposOffset;
        for (int i = 0; i < (gridGenerator.width / 4)-1; i++)
        {
            var newBarLine = Instantiate(barLine, transform);
            newBarLine.transform.localPosition = new Vector3(xpos, barLinesYpos);
            xpos += 400;
        }

        int ypos = yposOffset;
        for (int i = 0; i < (gridGenerator.height / 13); i++)
        {
            var newOctaveLine = Instantiate(octaveLine, transform);
            newOctaveLine.transform.localPosition = new Vector3(0, ypos);
            ypos += 30 * (13 - 1);
        }
    }
}
