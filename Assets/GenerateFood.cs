using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFood : MonoBehaviour
{
    public GameObject foodPrefab;
    public float xMin, xMax, yMin, yMax;
    public SnakeMovement snakeMovement; // Reference to the SnakeMovement script

    private GameObject currentFood;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate one food GameObject at the start
        GenerateInitialFood();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current food GameObject is null (destroyed), and generate a new one
        if (currentFood == null)
        {
            GenerateInitialFood();
        }
    }

    void GenerateInitialFood()
    {
        // Generate random positions for food within the specified range
        Vector3 foodPosition;

        do
        {
            int randomX = Mathf.FloorToInt(Random.Range(xMin, xMax));
            int randomY = Mathf.FloorToInt(Random.Range(yMin, yMax));

            foodPosition = new Vector3(randomX, 0.0f, randomY); // Adjust the Y position as needed
        } while (IsFoodOverlappingWithSnake(foodPosition));

        // Instantiate the food GameObject at the randomly generated position
        currentFood = Instantiate(foodPrefab, foodPosition, Quaternion.identity);
    }

    bool IsFoodOverlappingWithSnake(Vector3 foodPosition)
    {
        if (snakeMovement == null)
        {
            return false; // No reference to SnakeMovement script, no overlap check
        }

        // Check if the food position is within a certain radius of the snake's body segments
        foreach (Transform bodySegment in snakeMovement.bodySegments)
        {
            float distance = Vector3.Distance(bodySegment.position, foodPosition);
            if (distance < 1.0f) // Adjust the radius as needed
            {
                return true; // Food is too close to a body segment
            }
        }

        return false; // Food is not overlapping with the snake
    }
}
