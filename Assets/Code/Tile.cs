using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    [SerializeField] float speed = 1;

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
        if(collision.CompareTag("PlayNoteTrigger"))
        {
            print("play note");
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
    }
}
