using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splatter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite = null;
    private PaintColor color;

    public void SetColor(PaintColor color)
    {
        this.sprite.color = GameManager.Instance.GetColorInfo(color).Hue;
        this.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Scraper"))
        {
            ScraperController scraper = collision.gameObject.GetComponent<ScraperController>();
            scraper.AnimScrape();
            GameManager.Instance.Splatterer.RemoveSplatter(this.color, this);
            Destroy(this.gameObject);
        }
    }
}