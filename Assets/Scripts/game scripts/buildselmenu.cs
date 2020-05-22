using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildselmenu : MonoBehaviour
{
  public GameObject itself;
  public GameObject tower;
  public GameObject grid;
  public GameObject option1;
  public GameObject option2;
  public GameObject option3;
  public Vector2 selectedSpace;
  public int timer1 = 120;
  public int timer2 = 0;
  public int timer3 = 0;
  
    // Start is called before the first frame update
    void Start()
    {
        option1.SetActive(true);
        option2.SetActive(false);
        option3.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
    if(itself.activeSelf == true)
     {
        if(option1.activeSelf == true)
        {
          timer1 = timer1 - 1;
          if(timer1 == 0)
          {
            option2.SetActive(true);
            option1.SetActive(false);
            timer2 = 120;
		  }
		}
        if(option2.activeSelf == true)
        {
          timer2 = timer2 - 1;
          if(timer2 == 0)
          {
            option3.SetActive(true);
            option2.SetActive(false);
            timer3 = 120;
		  }
		}
        if(option3.activeSelf == true)
        {
          timer3 = timer3 - 1;
          if(timer3 == 0)
          {
            option1.SetActive(true);
            option3.SetActive(false);
            timer1 = 120;
		  }
		}
        if(Input.GetKeyDown("right") && option1.activeSelf == true && option2.activeSelf == false && option3.activeSelf == false)
        {
          Debug.Log("happening");
          grid.GetComponent<MapControl>().towerbuild = true;  
          selectedSpace = grid.GetComponent<MapControl>().selectedSpace;
          grid.GetComponent<MapControl>().BuildTower(selectedSpace, tower);
          grid.GetComponent<MapControl>().ResetSelector();
          tower.GetComponent<TowerAI>().splash = true;
          tower.GetComponent<TowerAI>().line = false;
          tower.GetComponent<TowerAI>().homing = false;
          itself.SetActive(false);
		}
        grid.GetComponent<MapControl>().towerbuild = false; 
        if(Input.GetKeyDown("right") && option1.activeSelf == false && option2.activeSelf == true && option3.activeSelf == false)
        {
          Debug.Log("happening");
          grid.GetComponent<MapControl>().towerbuild = true;  
          selectedSpace = grid.GetComponent<MapControl>().selectedSpace;
          grid.GetComponent<MapControl>().BuildTower(selectedSpace, tower);
          grid.GetComponent<MapControl>().ResetSelector();
          tower.GetComponent<TowerAI>().splash = false;
          tower.GetComponent<TowerAI>().line = false;
          tower.GetComponent<TowerAI>().homing = true;
          itself.SetActive(false);
		}
        grid.GetComponent<MapControl>().towerbuild = false; 
        if(Input.GetKeyDown("right") && option1.activeSelf == false && option2.activeSelf == false && option3.activeSelf == true)
        {
          Debug.Log("happening");
          grid.GetComponent<MapControl>().towerbuild = true;  
          selectedSpace = grid.GetComponent<MapControl>().selectedSpace;
          grid.GetComponent<MapControl>().BuildTower(selectedSpace, tower);
          grid.GetComponent<MapControl>().ResetSelector();
          tower.GetComponent<TowerAI>().splash = false;
          tower.GetComponent<TowerAI>().line = true;
          tower.GetComponent<TowerAI>().homing = false;
          itself.SetActive(false);
		}
        grid.GetComponent<MapControl>().towerbuild = false; 
     }
  }
}
