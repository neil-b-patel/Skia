using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlayerEvolution : MonoBehaviour
{   
    private PlayerManager player;
    public Sprite oneLeg;
    public Sprite twoLeg;
    private SpriteRenderer lightPlayer;

    // Start is called before the first frame update
    void Start()
    {   
        player = FindObjectOfType<PlayerManager>();
        lightPlayer = GameObject.Find("Light Player").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(player.GetNumFeet() == 1) {
            lightPlayer.sprite = oneLeg;
        }

        if(player.GetNumFeet() == 2) {
            lightPlayer.sprite = twoLeg;
        }
    }
}
