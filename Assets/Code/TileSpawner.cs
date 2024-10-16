using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] Canvas canvas;
    [SerializeField] Transform ghostTile;

    GameObject newTile;
    Vector2 mousePos;

    int length;
    int Length { get { return length; } set { length = value;  ghostTile.transform.localScale = new Vector2(value, 0.3f); } }

    public static bool canSpawn = true;

    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, canvas.worldCamera, out mousePos);

        // rounding x and y to make ghost attaches to grid
        int rx = RoundTo(mousePos.x, 100);
        int ry = RoundTo(mousePos.y, 30);

        mousePos = new Vector2(rx, ry);

        // spawn new tiles
        if(Input.GetMouseButtonDown(0))
        {
            if (!canSpawn)
                return;

            newTile = Instantiate(tilePrefab, canvas.transform);

            newTile.transform.localPosition = mousePos;
            newTile.transform.localScale = new Vector3(Length, 0.3f);
            newTile.transform.SetSiblingIndex(2);
        }
        else if(Input.GetMouseButtonDown(1))  // remove tiles
        {
            if (canSpawn) return;
            RaycastHit2D hit = Physics2D.Raycast(ghostTile.position, Vector2.down);
            Destroy(hit.transform.gameObject);
        }

        #region Change shape of tile
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        if(scroll > 0)
            Length += 1;
        else if(scroll < 0)
            Length -= 1;

        if(Length <= 0 || Length > 4)
            Length = Mathf.Clamp(Length, 1, 4);
        #endregion

        ghostTile.transform.localPosition = mousePos;
    }

    int RoundTo(float valToRound, int roundTo = 10)
    {
        return ((int)Math.Round(valToRound / roundTo)) * roundTo;
    }
}
