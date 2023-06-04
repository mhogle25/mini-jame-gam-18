using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private float duration = 0.2f;

    private PaintColor color = PaintColor.Magenta;
    private string tagTargeting = string.Empty;

    private void Awake()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(this.duration);
        Destroy(this.gameObject);
    }

    public void Setup(PaintColor color, string tagTargeting)
    {
        this.color = color;
        this.tagTargeting = tagTargeting;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(this.tagTargeting))
        {
            EntityController entity = collision.gameObject.GetComponent<EntityController>();
            entity.Damage(GameManager.Instance.GetColorInfo(this.color).Damage);
        }
    }
}
