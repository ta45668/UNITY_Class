using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSector : MonoBehaviour
{
    // 空白區域色值
    [SerializeField] Color emptyColor = new Color(0, 0, 0, 0);
    // 圓環區域色值
    [SerializeField] Color circleColor = new Color(1, 0, 0, 0.5f);

    // 圓環內徑/外徑
    [SerializeField] int minRadius = 80;
    [SerializeField] int maxRadius = 100;
    // 扇形角度
    [SerializeField] float circleAngle = 90;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (spriteRenderer == null)
        {
            gameObject.AddComponent<SpriteRenderer>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    private void Update()
    {
        Draw();
    }

    public void Draw()
    {
        spriteRenderer.sprite = CreateSprite(minRadius, maxRadius, circleAngle / 2, circleColor);
    }

    /// <summary>
    /// 繪製扇形圓環，生成 Sprite
    /// </summary>
    /// <param name="minRadius">圓環內徑，值為0即是實心圓</param>
    /// <param name="maxRadius">圓環外徑</param>
    /// <param name="halfAngle">1/2扇形弧度，值>=180度即是整圓</param>
    /// <param name="circleColor"></param>
    /// <returns></returns>
    Sprite CreateSprite(int minRadius, int maxRadius, float halfAngle, Color circleColor)
    {
        // 圖片尺寸
        int spriteSize = maxRadius * 2;
        // 創建 Texture2D
        Texture2D texture2D = new Texture2D(spriteSize, spriteSize);
        // 圖片中心像素點座標
        Vector2 centerPixel = new Vector2(spriteSize / 2, spriteSize / 2);
        //
        Vector2 tempPixel;
        float tempAngle, tempDisSqr;
        if (halfAngle > 0 && halfAngle < 360)
        {
            // 遍歷像素點，繪製扇形圓環
            for (int x = 0; x < spriteSize; x++)
            {
                for (int y = 0; y < spriteSize; y++)
                {
                    // 以中心為起點，獲取像素點向量
                    tempPixel.x = x - centerPixel.x;
                    tempPixel.y = y - centerPixel.y;
                    // 是否在半徑範圍內
                    tempDisSqr = tempPixel.sqrMagnitude;
                    if (tempDisSqr >= minRadius * minRadius && tempDisSqr <= maxRadius * maxRadius)
                    {
                        // 是否在角度範圍內
                        tempAngle = Vector2.Angle(Vector2.up, tempPixel);
                        if (tempAngle < halfAngle || tempAngle > 360 - halfAngle)
                        {
                            texture2D.SetPixel(x, y, circleColor);
                            continue;
                        }
                    }
                    // 設置為透明
                    texture2D.SetPixel(x, y, emptyColor);
                }
            }
        }
        else
        {
            // 遍歷像素點，繪製圓環
            for (int x = 0; x < spriteSize; x++)
            {
                for (int y = 0; y < spriteSize; y++)
                {
                    // 以中心為起點，獲取像素點向量
                    tempPixel.x = x - centerPixel.x;
                    tempPixel.y = y - centerPixel.y;
                    // 是否在半徑範圍內
                    tempDisSqr = tempPixel.sqrMagnitude;
                    if (tempDisSqr >= minRadius * minRadius && tempDisSqr <= maxRadius * maxRadius)
                    {
                        // 設置像素色值
                        texture2D.SetPixel(x, y, circleColor);
                        continue;
                    }
                    // 設置為透明
                    texture2D.SetPixel(x, y, emptyColor);
                }
            }
        }
        texture2D.Apply();

        Sprite sprite;
        sprite = Sprite.Create(texture2D, new Rect(0, 0, spriteSize, spriteSize), new Vector2(0.5f, 0.5f));

        return sprite;
    }
}