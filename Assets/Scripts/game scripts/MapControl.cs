using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapControl : MonoBehaviour
{
    public GameObject buildselector;
    public bool towerbuild;
    public GameObject selector;
    // Stores the initial position of the selector
    private Vector3 selectorStartPosition;
    public int selectedZone = 0;
    public float selectionTime = 6.0f;
    public float timer = 0.0f;
    public Vector2 selectedSpace = new Vector2(-1, -1);

    //selectedRow and selectedColumn are absolute to the map
    private int selectedRow = -1;
    private int selectedColumn = -1;

    //currentRow and currentColumn are relative to the effective area for the selector
    private int currentRow = 0;
    private int currentColumn = 0;
    // Determines how many squares wide and high the grid area is
    private int gridSize;
    // Determines how big a square/the selector is to determine how far it should move
    private int squareSize;
    // Determines the starting position of the selector
    public float baseOffset = 2.5f;
    public float baseOffsetX = 2.5f;
    public float baseOffsetY = 2.5f;
    // scrap variable
    public float scrap = 25f;

    //Placeholder to spawn in a tower
    public GameObject tower;

    // Determines time between spawning enemies
    public float spawnDelay = 1.0f;
    private float spawnTimer;
    // True after setup
    public bool waveActive = false;
    //Stores the enemy to spawn
    public GameObject enemy;
    public GameObject enemy2;
    public float enemyspawned; 
    // The amount of enemies to spawn
    public int enemyCap;
    // Enemies spawned in this wave
    private int enemyCount = 0;

    // Tower building menu
    public GameObject buildWindow;
    // Is the building menu open or not?
    private bool showBuildMenu = false;

    // The camera for the scene, where it starts, and how far and quickly to move it
    public GameObject camera;
    private Vector3 initialCameraPosition;
    public int cameraPanDistance;
    public int cameraSpeed;

    // The left and right UI elements
    public GameObject UILeft;
    public GameObject UIRight;
    public GameObject UISelector;
    private Vector3 UILeftStart;
    private Vector3 UIRightStart;
    private bool moveLeft;
    private bool generatedUI = false;

    // List of all buildings to be used in UI menus
    public List<GameObject> buildingList;
    public int selectedOption;

    //other UI elements
    public GameObject backtomenu;

    // Start is called before the first frame update
    void Start()
    {
        buildselector.SetActive(false);
        towerbuild = false;
        backtomenu.SetActive(false);
        initialCameraPosition = camera.transform.position;
        UILeftStart = UILeft.transform.parent.position;
        UIRightStart = UIRight.transform.parent.position;

        if (selector == null)
        {
            Debug.LogError("No selector assigned!");
        }
        //Set selector size based on whether one of the four zones has been selected
        if (selectedZone == 0)
        {
            selector.transform.localScale = new Vector3(5, 5, 1);
        }
        else
        {
            selector.transform.localScale = new Vector3(5, 5, 1);
        }

        timer = selectionTime;
    }


    // Update is called once per frame
    void Update()
    {
        enemyspawned = UnityEngine.Random.Range(1, 3);
        Debug.Log(enemyspawned);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Setup phase
        if (!waveActive)
        {
            // Use these inputs if the player is not in the buildMenu
            if (!showBuildMenu)
            {
                if (Input.GetKeyDown("right"))
                {
                    // Runs if the tower building menu is already open
                    if (showBuildMenu == true)
                    {
                       Debug.Log("what");
                       BuildTower(selectedSpace, tower);
                       ResetSelector();
                       buildWindow.SetActive (true);
                       showBuildMenu = true;
                    }
                    // Runs if no zone has been selected, selects the current zone
                    if (selectedZone == 0)
                    {
                        selectedZone = currentColumn + currentRow * 2 + 1;
                        baseOffsetX = 4.5f - currentColumn * squareSize;
                        baseOffsetY = 4.5f - currentRow * squareSize;
                        currentRow = 2;
                        currentColumn = -1;
                        selector.transform.localScale = new Vector3(1, 5, 1);
                        FindObjectOfType<AudioManager>().Play("Select");
                    }
                    // If no column has been selected yet, select a column
                    else if (selectedColumn < 0)
                    {
                        selectedColumn = currentColumn + (selectedZone - 1) % 2 * 5;
                        currentRow = -1;
                        selector.transform.localScale = new Vector3(1, 1, 1);
                        FindObjectOfType<AudioManager>().Play("Select");
                    }
                    // If no row has been selected yet, select a row
                    else if (selectedRow < 0)
                    {
                        selectedRow = currentRow + (selectedZone - 1) / 2 * 5;
                        selectedSpace = new Vector2(selectedColumn, selectedRow);
                        FindObjectOfType<AudioManager>().Play("Select");

                        buildselector.SetActive(true);
                        
                        if(towerbuild == true)
                        {
                           Debug.Log("do something");
                           BuildTower(selectedSpace, tower);
                           ResetSelector();
						}
                    }
                    // Immediately updates the selector
                    timer = 0.0f;
                }

                //Only interact with the selector when the timer goes off
                timer -= Time.deltaTime;
                if (timer <= 0.0f)
                {

                    timer = selectionTime;
                    // Make a counter increase the selector's column, if column has been selected, increase row
                    if (selectedColumn < 0)
                    {
                        currentColumn += 1;
                    }
                    else if (selectedRow < 0)
                    {
                        currentRow += 1;
                    }


                    if (selectedZone == 0)
                    {
                        // Change effective area to move grid selector for large area
                        gridSize = 2;
                        squareSize = 5;

                        // Cycle through columns, then rows
                        if (currentRow > gridSize - 1)
                        {
                            currentColumn += 1;
                        }
                        if (currentColumn > gridSize - 1)
                        {
                            currentRow += 1;
                        }
                    }
                    else
                    {
                        // Change effective area to move grid selector for smaller area
                        gridSize = 5;
                        squareSize = 1;
                    }


                    currentColumn %= gridSize;
                    currentRow %= gridSize;

                    //Move the selector based on current row and column position
                    selector.transform.localPosition = new Vector3(squareSize * currentColumn - baseOffsetX,
                                                                   squareSize * -currentRow + baseOffsetY,
                                                                   selector.transform.localPosition.z);
        
                    FindObjectOfType<AudioManager>().Play("Scroll");
                    
                }

                if (Input.GetKeyDown("left"))
                {
                    if (selectedZone == 0)
                    {
                        //Debug.Log("Started the wave");
                        //Destroy(selector);
                        //spawnTimer = spawnDelay;
                        //instantiating the pause menu thing
                       backtomenu.SetActive(true);
                       Destroy(selector);
                       waveActive = true;
                    }
                    else
                    {
                        // Sets the selector back to the previous state depending on current state; Column if selecting rows, zone if selecting columns
                        if (selectedColumn >= 0)
                        {
                            selectedColumn = -1;
                            selector.transform.localScale = new Vector3(1, 5, 1);
                            currentRow = 2;
                            selector.transform.localPosition = new Vector3(squareSize * currentColumn - baseOffsetX,
                                                                   squareSize * -currentRow + baseOffsetY,
                                                                   selector.transform.localPosition.z);
                            timer = selectionTime;
                        }
                        else
                        {
                            selectedZone = 0;
                            ResetSelector();
                        }
                    }
                    FindObjectOfType<AudioManager>().Play("Cancel");
                }
            }

            // ### This section contains all code relating to the buildmenu ###
            if (showBuildMenu)
            {
                UISelector.SetActive(true);
                moveLeft = selectedZone % 2 != 0;
                BuildMenuOpen(moveLeft, buildingList);
                if (Input.GetKeyDown("left"))
                {
                    showBuildMenu = false;
                }
            }
            else if (camera.transform.position.x != initialCameraPosition.x)
            {
                if (UISelector.activeSelf)
                {
                    ResetSelector();
                }
                UISelector.SetActive(false);
                BuildMenuClose();

            }
            else if(generatedUI)
            {
                UISelector.transform.SetParent(buildOptions[0].transform);
                for (var i = 1; i < buildOptions.Count; i++)
                {
                    Destroy(buildOptions[i]);
                }
                generatedUI = false;
            }

        }
        // Wave phase
        else
        {
            Debug.Log("Spawning enemies...");
            // Spawns in enemies on a timer
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnDelay && enemyCount < enemyCap)
            {
                if(enemyspawned == 1)
                {
                    CreateEnemy(enemy);
                    enemyCount += 1;
                    spawnTimer = 0.0f;
                }
                if (enemyspawned == 2)
                {
                    CreateEnemy2(enemy2);
                    enemyCount += 1;
                    spawnTimer = 0.0f;
                }

            }
            else if (enemyCount >= enemyCap)
            {
                waveActive = false;
            }
        }


    }

    // Resets selector to initial state
    public void ResetSelector()
    {
        selectedSpace = new Vector2(-1, -1);
        selectedRow = -1;
        selectedColumn = -1;
        selectedZone = 0;
        currentRow = 0;
        currentColumn = 0;
        baseOffsetX = baseOffsetY = baseOffset;
        selector.transform.localScale = new Vector3(5, 5, 1);
        selector.transform.localPosition = new Vector3(squareSize * currentColumn - baseOffsetX,
                                                        squareSize * -currentRow + baseOffsetY,
                                                        selector.transform.localPosition.z);
        timer = selectionTime;
    }

    // Builds a tower at the selected space on the grid
     public GameObject BuildTower(Vector2 position, GameObject tower)
    {
        Debug.Log("Trying to build a tower...");
        // Cycles through all roles and finds one to match selected space
        foreach (Transform row in gameObject.transform)
        {
            if (row.name == "Row" + position.y)
            {
                // Cycles through all columns and finds one to match selected space
                foreach (Transform column in row)
                {
                    if (column.name == "Col" + position.x)
                    {
                        // If there's already a tower, give error 
                        if (column.childCount > 0)
                        {
                            Debug.LogError("There's already a building there!");
                        }
                        // Construct the actual tower
                        else
                        {
                            if (scrap >= 5)
                            {
                                scrap = scrap - 5;
                                var newTower = Instantiate(tower);
                                newTower.transform.parent = column;
                                newTower.transform.localPosition = new Vector3(0, 0, -1);
                                Debug.Log("Built " + newTower.name + " at Row" + position.y + ", Column" + position.x + "!");
                                return newTower;
                            }
                            if (scrap == 0)
                            {
                                Debug.LogError("You're out of scrap!");
                            }
                        }
                    }
                }
            }
        }
        return null;
    }

    GameObject CreateEnemy(GameObject enemy)
    {
        GameObject result = Instantiate(enemy);
        return result;
    }
    GameObject CreateEnemy2(GameObject enemy2)
    {
        GameObject result2 = Instantiate(enemy2);
        return result2;
    }

    GameObject UIElement;
    int direction;
    List<GameObject> buildOptions = new List<GameObject>();
    void BuildMenuOpen(bool left, List<GameObject> options)
    {
        // Generates a list of UI elements
        if (!generatedUI)
        {
            if (left)
            {
                UIElement = UILeft;
            }
            else
            {
                UIElement = UIRight;
            }

            timer = selectionTime;

            buildOptions = new List<GameObject>();

            

            UIElement.transform.position = new Vector3(UIElement.transform.position.x, (float)25 * (options.Count - 1), UIElement.transform.position.z);
            for (var i = 0; i < options.Count; i++)
            {
                if (i == 0)
                {
                    buildOptions.Add(UIElement);
                }
                else
                {
                    buildOptions.Add(Instantiate(
                        UIElement,
                        new Vector3(
                            UIElement.transform.position.x,
                            (float)25 * (options.Count - 1) - (float)50 * i,
                            UIElement.transform.position.z
                        ),
                        UIElement.transform.rotation,
                        UIElement.transform.parent
                        ));
                }
            }
            if (buildOptions.Count == 0)
            {
                Debug.LogError("No building options!");
            }
            selectedOption = 0;
            UISelector.transform.SetParent(buildOptions[selectedOption].transform);
            UISelector.transform.localPosition = new Vector3(0, 0, -1);
            UISelector.transform.localScale = new Vector3(1, 1, 1);
            generatedUI = true;
        }
        // Moves the camera to the proper position
        if (camera.transform.position.x != initialCameraPosition.x + cameraPanDistance && camera.transform.position.x != initialCameraPosition.x - cameraPanDistance)
        {
            direction = (left ? 1 : 0) * 2 - 1;
            camera.transform.position = Vector3.MoveTowards(
                camera.transform.position,
                new Vector3(
                    initialCameraPosition.x - direction * cameraPanDistance,
                    initialCameraPosition.y,
                    initialCameraPosition.z),
                cameraSpeed * Time.deltaTime);
            if (left)
            {
                
                UILeft.transform.parent.position = Vector3.MoveTowards(
                UILeft.transform.parent.position,
                new Vector3(
                    UILeftStart.x + cameraPanDistance,
                    UILeftStart.y,
                    UILeftStart.z
                    ),
                cameraSpeed * Time.deltaTime);
            }
            else
            {
                
                UIRight.transform.parent.position = Vector3.MoveTowards(
                UIRight.transform.parent.position,
                new Vector3(
                    UIRightStart.x - cameraPanDistance,
                    UIRightStart.y,
                    UIRightStart.z
                    ),
                cameraSpeed * Time.deltaTime);
            }
        }
        // Scrolls through UI
        else
        {
            Debug.Log("Counting down the seconds");
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                selectedOption += 1;
                if (buildOptions.Count != 0)
                {
                    selectedOption = selectedOption % buildOptions.Count;
                }
                else
                {
                    Debug.LogError("No building options!");
                }
                UISelector.transform.SetParent(buildOptions[selectedOption].transform);
                UISelector.transform.localPosition = new Vector3(0, 0, -1);
                timer = selectionTime;
            }
            if (Input.GetKeyDown("right"))
            {
                BuildTower(selectedSpace, buildingList[selectedOption]);
            }
        }
    }


    void BuildMenuClose()
    {
        camera.transform.position = Vector3.MoveTowards(
            camera.transform.position,
            new Vector3(
                initialCameraPosition.x,
                initialCameraPosition.y,
                initialCameraPosition.z),
            cameraSpeed * Time.deltaTime);

        UILeft.transform.parent.position = Vector3.MoveTowards(
            UILeft.transform.parent.position,
            new Vector3(
                UILeftStart.x,
                UILeftStart.y,
                UILeftStart.z
                ),
            cameraSpeed * Time.deltaTime);

        UIRight.transform.parent.position = Vector3.MoveTowards(
            UIRight.transform.parent.position,
            new Vector3(
                UIRightStart.x,
                UIRightStart.y,
                UIRightStart.z
                ),
            cameraSpeed * Time.deltaTime);

        
    }
}
