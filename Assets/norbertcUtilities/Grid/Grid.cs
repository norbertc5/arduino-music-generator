using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace norbertcUtilities.Grid
{
    public class GridImage : Grid
    {
        protected override void Awake()
        {
            base.Awake();

            Image img = cellPrefab.GetComponentInChildren<Image>();
            Vector2 position = -(new Vector3(width * img.rectTransform.rect.width, height * img.rectTransform.rect.height)
                - cellPrefab.transform.localScale) / 2;

            position.x += cellPrefab.transform.localScale.x;

            position.y += img.rectTransform.rect.height;
            position.x = -(width * img.rectTransform.rect.width - img.rectTransform.rect.width) / 2;
        }
    }
}
