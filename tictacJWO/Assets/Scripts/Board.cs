using UnityEngine;
using UnityEngine.Events;

public class Board : MonoBehaviour
{
    [Header("Input Settings : ")]
    [SerializeField] private LayerMask boxesLayerMask;
    [SerializeField] private float touchRadius;

    [Header("Sprites : ")]
    [SerializeField] private Sprite spriteX;
    [SerializeField] private Sprite spriteO;

    [Header("Colors : ")]
    [SerializeField] private Color colorX;
    [SerializeField] private Color colorO;

    public UnityAction<XnO, Color> OnWinAction;

    public XnO[] xnos;

    private Camera cam;

    private XnO currentXnO;

    private bool canPlay;

    private LineRenderer lineRenderer;

    private int xnosCount = 0;

    private void Start()
    {
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        currentXnO = XnO.X;

        xnos = new XnO[9];

        canPlay = true;
    }

    private void Update()
    {
        if (canPlay && Input.GetMouseButtonUp(0))
        {
            Vector2 touchPosition = cam.ScreenToWorldPoint(Input.mousePosition);

            Collider2D hit = Physics2D.OverlapCircle(touchPosition, touchRadius, boxesLayerMask);

            //if box is in contac = true
            if (hit)
                HitBox(hit.GetComponent<Box>());
        }
    }

    private void HitBox(Box box)
    {
        if (!box.isXnO)
        {
            xnos[box.index] = currentXnO;

            box.SetAsXnO(GetSprite(), currentXnO);
            xnosCount++;

            //checking for winners
            bool won = CheckIfWin();
            if (won)
            {
                if (OnWinAction != null)
                    OnWinAction.Invoke(currentXnO, GetColor());

                canPlay = false;
                return;
            }

            if (xnosCount == 9)
            {
                if (OnWinAction != null)
                    OnWinAction.Invoke(XnO.None, Color.black);

                canPlay = false;
                return;
            }

            SwitchPlayer();
        }
    }

    private bool CheckIfWin()
    {
        return
        AreBoxesMatched(0, 1, 2) || AreBoxesMatched(3, 4, 5) || 
        AreBoxesMatched(6, 7, 8) || AreBoxesMatched(0, 3, 6) || 
        AreBoxesMatched(1, 4, 7) || AreBoxesMatched(2, 5, 8) ||
        AreBoxesMatched(0, 4, 8) || AreBoxesMatched(2, 4, 6);
        //all possibilities of a tic tac toe win

    }

    private bool AreBoxesMatched(int i, int j, int k)
    {
        XnO m = currentXnO;
        bool matched = (xnos[i] == m && xnos[j] == m && xnos[k] == m);

        if (matched)
            DrawLine(i, k);

        return matched;
    }

    private void DrawLine(int i, int k)
    {
        lineRenderer.SetPosition(0, transform.GetChild(i).position);
        lineRenderer.SetPosition(1, transform.GetChild(k).position);
        Color color = GetColor();
        color.a = .3f;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        lineRenderer.enabled = true;
    }

    private void SwitchPlayer()
    {
        currentXnO = (currentXnO == XnO.X) ? XnO.O : XnO.X;
    }

    private Color GetColor()
    {
        return (currentXnO == XnO.X) ? colorX : colorO;
    }

    private Sprite GetSprite()
    {
        return (currentXnO == XnO.X) ? spriteX : spriteO;
    }
}
//@preeth.freqjwo 