using UnityEngine;
using System.Collections;

public class RandomSprites : MonoBehaviour
{
    public Sprite[] sprites;

    // Use this for initialization
    void Awake()
    {
        SpriteRenderer sRend = GetComponent<SpriteRenderer>();
        int rand = Random.Range(0, sprites.Length);
        sRend.sprite = sprites[rand];
    }
}
