using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using norbertcUtilities.Extensions;

public class Tile : MonoBehaviour
{
    public double frequency;
    public int duration;
    public int l;
    public int pos;

    private void Start()
    {
        int level = ncUtilitiesExtensions.RoundTo((int)transform.localPosition.y, 30) / Manager.CELL_Y_SIZE + 9;
        l = level;
        pos = (int)transform.localPosition.y;
        //level = (level == 0) ? level = 1 : level;
        frequency = 261.63 * Mathf.Pow(1.059f, level);
        duration = (int)transform.localScale.x;
    }

    public void MakeDark()
    {
        Image img = GetComponentInChildren<Image>();
        img.color -= new Color32(100, 100, 100, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ghost"))
        {
            TileSpawner.canSpawn = false;
            TileSpawner.tileUnderGhost = gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayNoteTrigger"))
        {
            Manager.freq = frequency;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Ghost"))
        {
            TileSpawner.canSpawn = true;
            if(TileSpawner.tileUnderGhost == gameObject)
                TileSpawner.tileUnderGhost = null;
        }
        if (collision.CompareTag("PlayNoteTrigger"))
        {
            Manager.freq = 0;
        }
    }
}
