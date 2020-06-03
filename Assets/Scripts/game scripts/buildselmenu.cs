using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildselmenu : MonoBehaviour
{
  private const float TIMER_DELAY = 2.0f;

  public GameObject itself;
  public GameObject tower;
  public GameObject grid;
  public GameObject option1;
  public GameObject option2;
  public GameObject option3;
  public Vector2 selectedSpace;
  public float timer = TIMER_DELAY;
  private int selectedOption = 0;
  
  void SetActivationOfOptions() {
    GameObject[] options = new GameObject[] {option1, option2, option3}; 
    
    for (int i = 0; i < options.Length; i++)
    {
      if (i == selectedOption) {
        options[i].SetActive(true); 
      }
      else {
        options[i].SetActive(false);
      }
    }
  }

  // Start is called before the first frame update
  void Start()
  {
      SetActivationOfOptions();
  }

  void BuildTower(int selectedTower)
  {
    //grid.GetComponent<MapControl>().towerbuild = true;
    //tower.GetComponent<TowerAI>().splash = false;    
    //tower.GetComponent<TowerAI>().line = false;
    //tower.GetComponent<TowerAI>().homing = false;

    if (selectedTower == 0)
      tower.GetComponent<TowerAI>().type = TowerAIType.Splash;
    if (selectedTower == 1)
      tower.GetComponent<TowerAI>().type = TowerAIType.Homing;
    if (selectedTower == 2)
      tower.GetComponent<TowerAI>().type = TowerAIType.Line;

    selectedSpace = grid.GetComponent<MapControl>().selectedSpace;
    grid.GetComponent<MapControl>().BuildTower(selectedSpace, tower);
    grid.GetComponent<MapControl>().ResetSelector();
  }

    // Update is called once per frame
    void Update()
    {
      if (itself.activeSelf == true)
      {
        timer = timer - Time.deltaTime;
        if (timer <= 0f) {
          selectedOption = (selectedOption + 1) % 3;
          SetActivationOfOptions();
          timer = TIMER_DELAY;
        }
        if(Input.GetKeyDown("right"))
        {
          BuildTower(selectedOption);
          itself.SetActive(false);
		    }
        //grid.GetComponent<MapControl>().towerbuild = false; 
     }
  }
}
