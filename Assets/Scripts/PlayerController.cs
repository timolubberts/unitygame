using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform pointer;

    public float speed;
    public float pickUpRadius;
    public float sprintMultiplier;
    private bool isSprinting = false;

    private Rigidbody2D playerRb2d;

    private SpriteRenderer sr;
    private Game game;
    public Sprite[] sprites;

    void Awake()
    {
        playerRb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }

    void FixedUpdate()
    {
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            game.PlaceItem(transform.position);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Interact();
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            game.im.ChangeSelected(Input.GetAxis("Mouse ScrollWheel"));
        }
    }


    private void Move(float movementX, float movementY)
    {
        float multiplier = 1f;
        if (isSprinting)
        {
            multiplier = sprintMultiplier;
        }

        Vector3 movement = new Vector3(movementX, movementY, movementY);
        movement = movement.normalized * (speed * multiplier) * Time.deltaTime;

        playerRb2d.MovePosition(transform.position + movement);
        if(movementX > 0) //Right
        {
            sr.sprite = sprites[0];
            sr.flipX = true;
        } else if(movementX < 0)//Left
        {
            sr.sprite = sprites[0];
            sr.flipX = false;
        } else if (movementY > 0)//Up
        {
            sr.sprite = sprites[1];
            sr.flipY = false;
        } else if (movementY < 0)//Down
        {
            sr.sprite = sprites[1];
            sr.flipY = true;
        }
        pointer.position = playerRb2d.position;
    }

    private void Interact()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit)
        {   
            GameObject obj = hit.transform.gameObject;
            float dist = Vector2.Distance(transform.position, obj.transform.position);

            switch (obj.tag)
            {
                case "Item":
                    if (dist <= pickUpRadius)
                    {
                        game.PickUp(obj);
                    }
                    break;
                case "Door":
                    if(dist <= pickUpRadius)
                    {
                        game.ChangeScene(obj.name, obj);
                    }
                    break;
            }

        }
    }


}
