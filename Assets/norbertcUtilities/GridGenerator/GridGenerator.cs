using UnityEngine;
using UnityEngine.UI;

namespace norbertcUtilities.GridGenerator
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        public int width;
        public int height;
        [SerializeField] private Color color1;
        [SerializeField] private Color color2;

        enum BoardColorMode  { chessBoard, stripes };
        [SerializeField] BoardColorMode boardColorMode;

        protected virtual void Awake()
        {
            // generates grid

            // set position of the board in middle
            Vector2 position = -(new Vector3(width * cellPrefab.transform.localScale.x, height * cellPrefab.transform.localScale.y)
                - cellPrefab.transform.localScale) / 2;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    GameObject newCell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                    newCell.name = $"Cell ({x}, {y})";
                    position.x += cellPrefab.transform.localScale.x;

                    if(IsCellUI(newCell, out Image img))
                    {
                        newCell.GetComponent<RectTransform>().anchoredPosition = position;
                    }

                    // set colors like in chessboard
                    if(boardColorMode == BoardColorMode.chessBoard)
                    {
                        if ((y + x) % 2 == 0)
                            SetCellColor(newCell, img, color1);
                        else
                            SetCellColor(newCell, img, color2);
                    }
                    else if(boardColorMode == BoardColorMode.stripes)  // set color like stripes
                    {
                        if (y % 2 == 0)
                            SetCellColor(newCell, img, color1);
                        else
                            SetCellColor(newCell, img, color2);
                    }
                }
                position.y += cellPrefab.transform.localScale.y;
                position.x = -(width * cellPrefab.transform.localScale.x - cellPrefab.transform.localScale.x) / 2;
            }
        }

        bool IsCellUI(GameObject cell, out Image img)
        {
            img = cell.GetComponentInChildren<Image>();
            if(img)
                return true;
            else 
                return false;

        }

        void SetCellColor(GameObject cell, Image img, Color color)
        {
            if (img)
                img.color = color;
            else
                cell.GetComponentInChildren<SpriteRenderer>().color = color;
        }
    }
}
