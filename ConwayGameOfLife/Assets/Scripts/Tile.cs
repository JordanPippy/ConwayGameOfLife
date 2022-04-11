using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isAlive;

    public Color alive, dead;
    private SpriteRenderer spriteRenderer;
    private GameManager gm;

    void Start()
    {
        alive = Color.white;
        dead = Color.black;
        isAlive = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        UpdateState();
    }

    public void Die()
    {
        if (!isAlive)
            return;
        isAlive = false;
        UpdateState();
    }

    public void Respawn()
    {
        if (isAlive)
            return;
        isAlive = true;
        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnMouseOver()
    {
        if (GameManager.started)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            isAlive = !isAlive;
            UpdateState();
        }
    }

    void UpdateState()
    {
        spriteRenderer.color = isAlive ? alive : dead;
    }
}
