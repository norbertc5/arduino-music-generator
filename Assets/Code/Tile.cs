using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using norbertcUtilities.ActionOnTime;

public class Tile : MonoBehaviour
{
    [SerializeField] float speed = 1;
    static bool stop;

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

            int level = (int)transform.localPosition.y / Manager.CELL_Y_SIZE + 9;

            Manager.freq = 261 * Mathf.Pow(1.059f, level);
            print(Manager.freq);
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
