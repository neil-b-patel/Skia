using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlayerEvolution : MonoBehaviour
{   
    private PlayerManager player;
    private SpriteRenderer spriteRenderer;
    private SphereCollider sphereCollider;
    public Sprite oneLeg;
    public Sprite twoLeg;

    void Start()
    {   
        player = FindObjectOfType<PlayerManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    public void EvolvePlayer()
    {
        if (player.GetNumFeet() == 1)
        {
            spriteRenderer.sprite = oneLeg;
        }

        if (player.GetNumFeet() == 2)
        {
            spriteRenderer.sprite = twoLeg;
        }

        if (player.GetNumFeet() > 1)
        {
            sphereCollider.radius = 9.75f;
            sphereCollider.center = new Vector3(0f, 0f, 0f);
        }
    }
}
