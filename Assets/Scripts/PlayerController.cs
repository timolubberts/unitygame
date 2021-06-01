using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public int hunger = 3;
    public int maxHunger = 10;

    public void Feed(int amount)
    {
        Instance.hunger += amount;
    }
    public bool movementDisabled = false;
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
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        playerRb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Open inventory
        {
            Game.Instance.uim.SwitchInventory();
        }
        if (movementDisabled)   //  ^
        {                       //  | 
            return;             //  |
        }                       //  v

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Game.Instance.crafting.Craft(ItemDatabase.Instance.GetRecipe("Fruits"));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Game.Instance.PlaceItem();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Interact();
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            InventoryManager.Instance.ChangeSelected(Input.GetAxis("Mouse ScrollWheel"));
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (InventoryManager.Instance.inventory[InventoryManager.Instance.selected] != null)
            {
                InventoryManager.Instance.inventory[InventoryManager.Instance.selected].slotItem.Use();
            }
            
        }
    }


    private void Move(float movementX, float movementY)
    {
        if (movementDisabled)
        {
            return;
        }
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
    }

    private void Interact()
    {
        if (movementDisabled)
        {
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit)
        {   
            GameObject obj = hit.transform.gameObject;
            float dist = Vector2.Distance(transform.position, obj.transform.position);
            //Debug.Log(obj.name);
            switch (obj.tag)
            {
                
                case "Item":
                    if (dist <= pickUpRadius)
                    {
                        Game.Instance.PickUp(obj);
                        Item i = obj.GetComponent<ObjectItem>().item;
                    }
                    break;
                case "Door":
                    if(dist <= pickUpRadius)
                    {
                        Game.Instance.ChangeScene(obj.name, obj);
                    }
                    break;
            }

        }
    }


}
