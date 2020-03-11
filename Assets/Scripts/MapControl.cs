using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public GameObject Selector;
    private List<GameObject> Rows;
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

    // Start is called before the first frame update
    void Start()
    {
        //Set selector size based on whether one of the four zones has been selected
        if (selectedZone == 0)
        {
            Selector.transform.localScale = new Vector3(5, 1, 5);
        }
        else
        {
            Selector.transform.localScale = new Vector3(1, 1, 1);
        }

        timer = selectionTime;
    }


    // Update is called once per frame
    void Update()
    {
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
            Selector.transform.localPosition = new Vector3(squareSize * -currentRow + baseOffsetX,
                                                           Selector.transform.localPosition.y,
                                                           squareSize * -currentColumn + baseOffsetZ);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (selectedZone == 0)
            {
                selectedZone = currentColumn + currentRow * 2 + 1;
                baseOffsetX = baseOffset - currentRow * squareSize;
                baseOffsetZ = baseOffset - currentColumn * squareSize;
                currentRow = 0;
                currentColumn = -1;
                Selector.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (selectedColumn < 0)
            {
                selectedColumn = currentColumn + (selectedZone - 1) % 2 * 5;
                currentRow -= 1;
            }
            else if (selectedRow < 0)
            {
                selectedRow = currentRow + (selectedZone - 1) / 2 * 5;
                selectedSpace = new Vector2(selectedColumn, selectedRow);
                openBuildMenu();
            }
            timer = 0.0f;
        }

    }
    void openBuildMenu()
    {
        Debug.Log("Buildin\' a buildin\'");
    }
}
