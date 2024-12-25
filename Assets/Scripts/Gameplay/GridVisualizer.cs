﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;


public class GridVisualizer : MonoBehaviour
{
    [SerializeField] private float cellSizeX = 3.5f;
    [SerializeField] private float cellSizeY = 2f;
    [SerializeField] private Sprite[] groundSprites;
    [SerializeField] private GridManager gridManager;

    private Vector2Int drawGridSize;

    private const string emptyGroundSpriteName = "ground_empty";

    private const string leftCornerRoadSpriteName = "ground_left_corner_2";
    private const string rightCornerRoadSpriteName = "ground_right_corner_2";
    private const string topCornerRoadSpriteName = "ground_top_corner_2";
    private const string bottomCornerRoadSpriteName = "ground_bottom_corner_2";

    private const string leftCrossRoadSpriteName = "ground_left_cross";
    private const string rightCrossRoadSpriteName = "ground_right_cross";
    private const string topCrossRoadSpriteName = "ground_top_cross";
    private const string bottomCrossRoadSpriteName = "ground_bottom_cross";
    private const string fullCrossRoadSpriteName = "ground_cross";

    private const string straightXRoadSpriteName = "ground_straight_x";
    private const string straightYRoadSpriteName = "ground_straight_y";

    private Dictionary<string, Sprite> spriteDict;

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {
        while (gridManager == null)
        {
            yield return null;
        }

        InitializeSpriteDict();
        drawGridSize = CalculateGridSize();
        DrawGround();

    }

    private void InitializeSpriteDict()
    {
        spriteDict = new Dictionary<string, Sprite>();
        foreach (Sprite sprite in groundSprites)
        {
            spriteDict[sprite.name] = sprite;
        }
    }

    private void DrawGround()
    {
        DrawOutline();
        DrawOuterRoads();
        DrawInnerRoads();
    }

    private void DrawOutline()
    {
        var groundSprite = spriteDict[emptyGroundSpriteName];
        DrawGroundTile(new Vector2Int(0, 0), groundSprite, 0);
        DrawGroundTile(new Vector2Int(0, drawGridSize.y - 1), groundSprite, -drawGridSize.y + 1);
        DrawGroundTile(new Vector2Int(drawGridSize.x - 1, 0), groundSprite, drawGridSize.x - 1);
        DrawGroundTile(new Vector2Int(drawGridSize.x - 1, drawGridSize.y - 1), groundSprite, drawGridSize.x - drawGridSize.y);

        for(int i = 1; i < drawGridSize.y - 1; i++)
        {
            DrawGroundTile(new Vector2Int(0, i), groundSprite, -i);
            DrawGroundTile(new Vector2Int(drawGridSize.x - 1, i), groundSprite, drawGridSize.x - i - 1);
        }

        for (int i = 1; i < drawGridSize.x - 1; i++)
        {
            DrawGroundTile(new Vector2Int(i, 0), groundSprite, i);
            DrawGroundTile(new Vector2Int(i, drawGridSize.y - 1), groundSprite, -drawGridSize.y + i + 1);
        }
    }
    
    private void DrawOuterRoads()
    {
        var leftCornerRoadSprite = spriteDict[leftCornerRoadSpriteName];
        var rightCornerRoadSprite = spriteDict[rightCornerRoadSpriteName];
        var topCornerRoadSprite = spriteDict[topCornerRoadSpriteName];
        var bottomCornerRoadSprite = spriteDict[bottomCornerRoadSpriteName];

        var leftCrossRoadSprite = spriteDict[leftCrossRoadSpriteName];
        var rightCrossRoadSprite = spriteDict[rightCrossRoadSpriteName];
        var topCrossRoadSprite = spriteDict[topCrossRoadSpriteName];
        var bottomCrossRoadSprite = spriteDict[bottomCrossRoadSpriteName];

        var straightXRoadSprite = spriteDict[straightXRoadSpriteName];
        var straightYRoadSprite = spriteDict[straightYRoadSpriteName];

        DrawGroundTile(new Vector2Int(1, 1), leftCornerRoadSprite, 0);
        DrawGroundTile(new Vector2Int(1, drawGridSize.y - 2), topCornerRoadSprite, -drawGridSize.y + 3);
        DrawGroundTile(new Vector2Int(drawGridSize.x - 2, 1), bottomCornerRoadSprite, drawGridSize.x - 3);
        DrawGroundTile(new Vector2Int(drawGridSize.x - 2, drawGridSize.y - 2), rightCornerRoadSprite, drawGridSize.x - drawGridSize.y);

        for (int i = 2; i < drawGridSize.y - 2; i++)
        {
            if (i % 2 == 0)
            {
                DrawGroundTile(new Vector2Int(1, i), straightYRoadSprite, -i + 1);
                DrawGroundTile(new Vector2Int(drawGridSize.x - 2, i), straightYRoadSprite, drawGridSize.x - i - 2);
            }
            else
            {
                DrawGroundTile(new Vector2Int(1, i), leftCrossRoadSprite, -i + 1);
                DrawGroundTile(new Vector2Int(drawGridSize.x - 2, i), rightCrossRoadSprite, drawGridSize.x - i - 2);
            }
        }

        for (int i = 2; i < drawGridSize.x - 2; i++)
        {
            if (i % 2 == 0)
            {
                DrawGroundTile(new Vector2Int(i, 1), straightXRoadSprite, i - 1);
                DrawGroundTile(new Vector2Int(i, drawGridSize.y - 2), straightXRoadSprite, -drawGridSize.y + i + 2);
            }
            else
            {
                DrawGroundTile(new Vector2Int(i, 1), bottomCrossRoadSprite, i - 1);
                DrawGroundTile(new Vector2Int(i, drawGridSize.y - 2), topCrossRoadSprite, -drawGridSize.y + i + 2);
            }
        }
    }

    private void DrawInnerRoads()
    {
        var straightXRoadSprite = spriteDict[straightXRoadSpriteName];
        var straightYRoadSprite = spriteDict[straightYRoadSpriteName];
        var crossRoadSprite = spriteDict[fullCrossRoadSpriteName];
        var emptyGroundSprite = spriteDict[emptyGroundSpriteName];

        for (int i = 2; i < drawGridSize.x - 2; i++)
        {
            for (int j = 2; j < drawGridSize.y - 2; j++)
            {
                if(i % 2 == 0)
                {
                    if(j % 2 == 0)
                    {
                        DrawGroundTile(new Vector2Int(i, j), emptyGroundSprite, i - j);
                    }
                    else
                    {
                        DrawGroundTile(new Vector2Int(i, j), straightXRoadSprite, i - j);
                    }
                }
                else
                {
                    if(j % 2 == 0)
                    {
                        DrawGroundTile(new Vector2Int(i, j), straightYRoadSprite, i - j);
                    }
                    else
                    {
                        DrawGroundTile(new Vector2Int(i, j), crossRoadSprite, i - j);
                    }
                }
            }
        }
    }

    private void DrawGroundTile(Vector2Int isometricalPosition, Sprite sprite, int order)
    {
        var spritePosition = new Vector2(
            (isometricalPosition.x + isometricalPosition.y) * cellSizeX,
            (isometricalPosition.y - isometricalPosition.x) * cellSizeY);

        GameObject tileObject = new GameObject($"Ground_Tile_{isometricalPosition.x}_{isometricalPosition.y}");
        tileObject.transform.SetParent(transform);
        tileObject.transform.localPosition = spritePosition;

        SpriteRenderer spriteRenderer = tileObject.AddComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = order;
    }

    private Vector2Int CalculateGridSize()
    {
        var x = gridManager.SizeX + gridManager.SizeX + 1 + 2;
        var y = gridManager.SizeY + gridManager.SizeY + 1 + 2;

        return new Vector2Int(x, y);
    }
}