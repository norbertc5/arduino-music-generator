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
        TileSpawner.canSpawn = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TileSpawner.canSpawn = true;
    }
}
