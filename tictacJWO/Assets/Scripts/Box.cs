using UnityEngine;

public class Box : MonoBehaviour
{
    public int index;
    public XnO xno;
    public bool isXnO;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        index = transform.GetSiblingIndex();
        xno = XnO.None;
        isXnO = false;
    }

    public void SetAsXnO(Sprite sprite, XnO xno)
    {
        isXnO = true;
        this.xno = xno;

        spriteRenderer.sprite = sprite;

        GetComponent<CircleCollider2D>().enabled = false;
    }
}
//@preeth.freqjwo