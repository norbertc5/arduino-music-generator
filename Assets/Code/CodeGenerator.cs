using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor.Experimental.GraphView;

public class CodeGenerator : MonoBehaviour
{
    List<Tile> orderedTiles = new List<Tile>();
    float posx;

    public void Generate()
    {
        string path = Path.Combine(Application.dataPath, "Resources", "code.txt");  // path to file with generated code

        Tile[] unorderedTiles = FindObjectsOfType<Tile>();
        orderedTiles = unorderedTiles.OrderBy(t => t.transform.position.x).ToList();
        posx = orderedTiles[0].transform.position.x;

        File.WriteAllText(path, "");  // clear file

        // appends music data into code.txt flie

        // frequencies
        for (int i = 0; i < orderedTiles.Count; i++)
        {
            string note = orderedTiles[i].frequency.ToString().Replace(',', '.');
            if(IsFreeSpace(i, out float x))
                File.AppendAllText(path, "0, ");
            File.AppendAllText(path, $"{note}, ");
        }
        File.AppendAllText(path, "\n");

        // durations
        for (int i = 0; i < orderedTiles.Count; i++)
        {
            int duration = orderedTiles[i].duration;

            if (IsFreeSpace(i, out float diff))
                File.AppendAllText(path, $"{((diff/-100)-1)*(600/(Manager.tempo / 100))}, ");

            File.AppendAllText(path, $"{duration*(600 / (Manager.tempo / 100))}, ");
        }

        Process.Start("notepad.exe", path);
    }

    bool IsFreeSpace(int i, out float diff)
    {
        diff = posx - orderedTiles[i].transform.position.x + 100 * orderedTiles[i].duration;

        // it's using to compare positions of neares tiles and to define whether we come across free space
        Tile checking = orderedTiles[i];
        posx = checking.transform.position.x;
        print(diff);

        if (diff <= -100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
