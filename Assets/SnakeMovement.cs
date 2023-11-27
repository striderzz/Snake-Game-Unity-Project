using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public float moveInterval;
    public int StartSize = 10;
    private Vector3 direction;
    private int xpos, ypos;

    public GameObject bodyPrefab;
    public GameObject headPrefab;// Assign your body segment prefab in the Inspector

    public List<Transform> bodySegments = new List<Transform>();
    public bool snakeGenerated = false;
    public bool GameOver = false;

    private void Start()
    {
        StartCoroutine(moveCoroutine());
        direction = Vector3.right;

        for(int i=0;i<StartSize;i++)
        {
            GrowSnake();
        }
        Invoke("SetSnakeGenerated", 5);
    }
    private void SetSnakeGenerated()
    {
        snakeGenerated = true;
    }

    private void Update()
    {
        HandleInput();
    }

    private void movement()
    {
        if (!GameOver)
        {
            // Move the head
            Vector3 newPosition = transform.position + direction;
            transform.position = newPosition;

            // Move the body segments
            for (int i = bodySegments.Count - 1; i > 0; i--)
            {
                bodySegments[i].position = bodySegments[i - 1].position;
            }
            if (bodySegments.Count > 0)
            {
                bodySegments[0].position = newPosition;
            }
        }
        
    }

    private void HandleInput()
    {
        // Reset the direction

        if (Input.GetKey(KeyCode.W) && direction != -Vector3.forward)
        {
            direction = Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S) && direction != Vector3.forward)
        {
            direction = -Vector3.forward;
        }

        if (Input.GetKey(KeyCode.A) && direction != Vector3.right)
        {
            direction = Vector3.left;
        }

        if (Input.GetKey(KeyCode.D) && direction != Vector3.left)
        {
            direction = Vector3.right;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Call the GrowSnake function when the space key is pressed
            GrowSnake();
        }
    }

    public void GrowSnake()
    {
        GameObject newSegment;
        if(bodySegments.Count==0)
        {
            newSegment = Instantiate(headPrefab, transform.position, Quaternion.identity);
          
        }
        else
        {
            newSegment = Instantiate(bodyPrefab, bodySegments[bodySegments.Count - 1].position, Quaternion.identity);
            
        }
        Transform segmentTransform = newSegment.transform;
        // Create a new body segment
        

        // Add the new segment to the list
        bodySegments.Add(segmentTransform);
    }
    public void CheckSelfCol()
    {
        if (bodySegments.Count < 2)
        {
            // No self-collision is possible with less than 2 body segments
            return;
        }

        Vector3 headPosition = bodySegments[0].position; // Assuming the head is at index 0

        // Check if the head's position overlaps with any body segment's position
        for (int i = 1; i < bodySegments.Count; i++)
        {
            Vector3 bodySegmentPosition = bodySegments[i].position;

            // Check if the head's position is the same as a body segment's position
            if (headPosition == bodySegmentPosition)
            {
                // The head has overlapped with a body segment; handle the collision here
                Debug.Log("Self Collision Detected");
                // You can add more actions, such as ending the game or taking damage
                // Destroy the snake or perform other relevant actions as needed
                // Example: Destroy(this.gameObject);
                GameOverFunc();

                break; // Exit the loop after detecting the collision
                
            }
        }
    }
    public void GameOverFunc()
    {
        GameOver = true;
        for(int i=0;i<bodySegments.Count;i++)
        {
            bodySegments[i].gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
            if(i==0)
            {
                bodySegments[0].GetComponent<BoxCollider>().isTrigger = false;
                bodySegments[0].GetComponent<Rigidbody>().isKinematic = false;
                bodySegments[0].GetComponent<Rigidbody>().useGravity = true;
            }
            else
            {
                bodySegments[i].gameObject.AddComponent<Rigidbody>();
            }
            bodySegments[i].transform.localScale = new Vector3(1f, 1f, 1f)*0.75f;
            bodySegments[i].GetComponent<Rigidbody>().AddForce(200 * Vector3.up);
        }
       
       
    }

    private IEnumerator moveCoroutine()
    {
          while (true)
          {
                yield return new WaitForSeconds(moveInterval);
                movement();
                if (snakeGenerated == true)
                {
                    CheckSelfCol();
                }

          }
        
       
    }
}
