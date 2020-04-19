using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapControl : MonoBehaviour
{
    public GameObject selector;
    // Stores the initial position of the selector
    private Vector3 selectorStartPosition;
    private int selectedZone = 0;
    public float selectionTime = 6.0f;
    private float timer = 0.0f;
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

    //Placeholder to spawn in a tower
    public GameObject tower;

    // Determines time between spawning enemies
    public float spawnDelay = 5.0f;
    private float spawnTimer;
    // True after setup
    public bool waveActive = false;
    //Stores the enemy to spawn
    public GameObject enemy;
    // The amount of enemies to spawn
    public int enemyCap;
    // Enemies spawned in this wave
    private int enemyCount = 0;

    // Tower building menu
    public GameObject buildWindow;
    // Is the building menu open or not?
    private bool buildMenuOpen = false;

    // Start is called before the first frame update
    void Start()
    {
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
            selector.transform.localScale = new Vector3(1, 1, 1);
        }

        timer = selectionTime;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Setup phase
        if (!waveActive)
        {
            // Create player inputs
            if (Input.GetKeyDown("right"))
            {
                // Runs if the tower building menu is already open
                //if (buildMenuOpen == true)
                //{
                //    BuildTower(selectedSpace, tower);
                //    ResetSelector();
                //    buildWindow.SetActive (false);
                //    buildMenuOpen = false;
                //}
                // Runs if no zone has been selected, selects the current zone
                if (selectedZone == 0)
                {
                    selectedZone = currentColumn + currentRow * 2 + 1;
                    baseOffsetX =  4.5f - currentColumn * squareSize;
                    baseOffsetY =  4.5f  - currentRow * squareSize;
                    currentRow = 2;
                    currentColumn = -1;
                    selector.transform.localScale = new Vector3(1, 5, 1);
                }
                // If no column has been selected yet, select a column
                else if (selectedColumn < 0)
                {
                    selectedColumn = currentColumn + (selectedZone - 1) % 2 * 5;
                    currentRow = -1;
                    selector.transform.localScale = new Vector3(1, 1, 1);
                }
                // If no row has been selected yet, select a row
                else if (selectedRow < 0)
                {
                    selectedRow = currentRow + (selectedZone - 1) / 2 * 5;
                    selectedSpace = new Vector2(selectedColumn, selectedRow);

                    // Opens the tower building menu
                    BuildTower(selectedSpace, tower);
                    ResetSelector();
                    //if (buildMenuOpen == false)
                    //{
                    //buildWindow.SetActive (true);
                    //buildMenuOpen = true;
                    //}
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
            }

            if (Input.GetKeyDown("left"))
            {
                if (selectedZone == 0)
                {
                    Debug.Log("Started the wave");
                    Destroy(selector);
                    waveActive = true;
                    spawnTimer = spawnDelay;
                }
                else
                {
                    // Sets the selector back to the previous state depending on current state; Column if selecting rows, zone if selecting columns
                    if(selectedColumn >= 0)
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
            }
        }
        // Wave phase
        else
        {
            Debug.Log("Spawning enemies...");
            // Spawns in enemies on a timer
            spawnTimer += Time.deltaTime;
            if(spawnTimer > spawnDelay && enemyCount < enemyCap )
            {
                CreateEnemy(enemy);
                enemyCount += 1;
                spawnTimer = 0.0f;
            }
            else if (enemyCount >= enemyCap)
            {
                waveActive = false;
            }
        }
        

    }

    // Resets selector to initial state
    void ResetSelector()
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
    GameObject BuildTower(Vector2 position, GameObject tower)
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
                            var newTower = Instantiate(tower);
                            newTower.transform.parent = column;
                            newTower.transform.localPosition = new Vector3(0, 0, -1);
                            Debug.Log("Built " + newTower.name + " at Row" + position.y + ", Column" + position.x + "!");
                            return newTower;
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
}
