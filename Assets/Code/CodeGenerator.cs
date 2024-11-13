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
    float comparingTilePosX;

    public void Generate()
    {
        string path = Path.Combine(Application.dataPath, "Resources", "code.txt");  // path to file with generated code

        Tile[] unorderedTiles = FindObjectsOfType<Tile>();
        orderedTiles = unorderedTiles.OrderBy(t => t.transform.position.x).ToList();
        int numberOfNotesAndPauses = 0;

        ResetComparingTile();

        File.WriteAllText(path, "");  // clear file

        using (StreamWriter writer = new StreamWriter(path, append: true))
        {
            // appends music data into code.txt flie

            // frequencies
            writer.WriteLine("Frequencies:");
            for (int i = 0; i < orderedTiles.Count; i++)
            {
                string note = orderedTiles[i].frequency.ToString().Replace(',', '.');
                writer.Write(note);  // write note
                numberOfNotesAndPauses++;

                if (i < orderedTiles.Count - 1)
                    writer.Write(", "); // add comma until the end

                if (IsFreeSpace(i, out int x))
                {
                    writer.Write("0, ");  // add 0 when pause
                    numberOfNotesAndPauses++;
                }
            }
            writer.WriteLine("\n");
            ResetComparingTile();

            // durations
            writer.WriteLine("Durations:");
            for (int i = 0; i < orderedTiles.Count; i++)
            {
                int duration = orderedTiles[i].duration;
                writer.Write($"{duration * 600}");

                if (IsFreeSpace(i, out int diff))
                {
                    if(diff < 0)
                        writer.Write($", {(diff / -100) * 600}");
                }

                if (i < orderedTiles.Count - 1)
                    writer.Write(", "); // add comma until the end
            }

            writer.WriteLine($"\n\nNumber: {numberOfNotesAndPauses}\n");

            writer.Flush();
        }

        Process.Start("notepad.exe", path);
    }

    bool IsFreeSpace(int i, out int diff)
    {
        Tile checking = orderedTiles[i];

        int tileLen = 100 * checking.duration;
        int distToNext = Mathf.Abs((int)comparingTilePosX - (int)checking.transform.position.x);
        diff = tileLen - distToNext;

        // it's using to compare positions of neares tiles and to define whether we come across free space
        try { comparingTilePosX = orderedTiles[i + 2].transform.position.x; } catch { }

        if (diff < 0)
            return true;
        else
            return false;
    }

    void ResetComparingTile()
    {
        // set comparing tile to next one
        if (orderedTiles.Count > 1)
            comparingTilePosX = orderedTiles[1].transform.position.x;
        else
            comparingTilePosX = orderedTiles[0].transform.position.x;
    }
}
