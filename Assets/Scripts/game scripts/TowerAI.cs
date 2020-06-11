using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TowerAIType
{
    Splash,
    Line,
    Homing
}


public class TowerAI : MonoBehaviour
{

    // Assigns a bullet to be shot by this turret, which determines how its attacks function
    public GameObject bulletsplash;
    public GameObject bulletline;
    public GameObject bullethoming;
    public GameObject grid;
    public float scrapcost;
    public int towerhealth = 10;

    //stuff
    public TowerAIType type = TowerAIType.Homing;

    //sprites
    public Sprite spiral;
    public Sprite spiraldmg1;
    public Sprite spiraldmg2;
    public Sprite fire;
    public Sprite firedmg1;
    public Sprite firedmg2;
    public Sprite ice;
    public Sprite icedmg1;
    public Sprite icedmg2;
    private SpriteRenderer rend;
    public Collider2D collideboye;

  
    

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("GridContainer");
        collideboye = GetComponentInChildren<CircleCollider2D>();
        rend = gameObject.GetComponent<SpriteRenderer>();
        FindObjectOfType<AudioManager>().Play("Construction");
    }

    // Update is called once per frame
    void Update()
    {
        if(towerhealth <= 0)
        {
            grid.GetComponent<MapControl>().scrap += 10;
            Destroy(gameObject);
        }

        switch (type)
        {
            case TowerAIType.Splash:
                rend.sprite = fire;
                if(towerhealth <= 10 && towerhealth >= 7)
                {
                    rend.sprite = fire;
                }
                if (towerhealth >= 3 && towerhealth < 7)
                {
                    rend.sprite = firedmg1;
                }
                if (towerhealth < 3 && towerhealth > 0)
                {
                    rend.sprite = firedmg2;
                }
                break;
            case TowerAIType.Line:
                rend.sprite = ice;
                if (towerhealth <= 10 && towerhealth >= 7)
                {
                    rend.sprite = ice;
                }
                if (towerhealth >= 3 && towerhealth < 7)
                {
                    rend.sprite = icedmg1;
                }
                if (towerhealth < 3 && towerhealth > 0)
                {
                    rend.sprite = icedmg2;
                }
                break;
            case TowerAIType.Homing:
                rend.sprite = spiral;
                if (towerhealth <= 10 && towerhealth >= 7)
                {
                    rend.sprite = spiral;
                }
                if (towerhealth >= 3 && towerhealth < 7)
                {
                    rend.sprite = spiraldmg1;
                }
                if (towerhealth < 3 && towerhealth > 0)
                {
                    rend.sprite = spiraldmg2;
                }

                break;
        }

    }
}