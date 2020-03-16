using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public GameObject selector;
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
    private int gridSize;
    private int squareSize;
    public int baseOffset = 90;
    public int baseOffsetX = 70;
    public int baseOffsetZ = 70;

    //Placeholder
    public GameObject tower;

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
            selector.transform.localScale = new Vector3(5, 1, 5);
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
        // Create player inputs
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Runs if no zone has been selected
            if (selectedZone == 0)
            {
                selectedZone = currentColumn + currentRow * 2 + 1;
                baseOffsetX = baseOffset - currentRow * squareSize;
                baseOffsetZ = baseOffset - currentColumn * squareSize;
                currentRow = 0;
                currentColumn = -1;
                selector.transform.localScale = new Vector3(1, 1, 1);
            }
            // If no column has been selected yet, select a column
            else if (selectedColumn < 0)
            {
                selectedColumn = currentColumn + (selectedZone - 1) % 2 * 5;
                currentRow -= 1;
            }
            // If no row has been selected yet, select a row
            else if (selectedRow < 0)
            {
                selectedRow = currentRow + (selectedZone - 1) / 2 * 5;
                selectedSpace = new Vector2(selectedColumn, selectedRow);
                BuildTower(selectedSpace, tower);
                ResetSelector();
            }
            // Immediately updates the selector
            timer = 0.0f;
        }

        //Only interact with the selector when the timer goes off
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {

            timer = selectionTime;
            // Make a counter increase the selector's column 
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
                squareSize = 50;

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
                squareSize = 10;
            }


            currentColumn %= gridSize;
            currentRow %= gridSize;

            //Move the selector based on current row and column position
            selector.transform.localPosition = new Vector3(squareSize * -currentRow + baseOffsetX,
                                                           selector.transform.localPosition.y,
                                                           squareSize * -currentColumn + baseOffsetZ);
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
        baseOffsetX = baseOffsetZ = baseOffset - 20;
        selector.transform.localScale = new Vector3(5, 5, 5);
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
                            newTower.transform.localPosition = new Vector3(0, 0, 0);
                            Debug.Log("Built " + newTower.name + " at Row" + position.y + ", Column" + position.x + "!");
                            return newTower;
                        }
                    }
                }
            }
        }
        return null;
    }
}
