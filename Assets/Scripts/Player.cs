using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player : MonoBehaviour
{
    public float size;
    private Animator animator;
    public Inventory inventory;
    private bool managing, interacting, battling;
    public Text interact;
    public Battle battle;

	// Use this for initialization
	void Start() 
    {
        size = 0.05f;
        animator = GetComponent<Animator>();
        managing = false;
        interacting = false;
        battling = false;
        interact.gameObject.SetActive(false);
        SetIdle();
	}
	
	// Update is called once per frame
	void Update() 
    {
        CheckForMovement();
        CheckForManagement();
        CheckForInteraction();
	}

    public void SetIdle()
    {
        animator.SetBool("pushingUp", false);
        animator.SetBool("pushingRight", false);
        animator.SetBool("pushingDown", false);
        animator.SetBool("pushingLeft", false);
    }

    public bool IsMoving()
    {
        if 
        (
            (    
                Input.GetKey(Controls.Instance.up) ||
                Input.GetKey(Controls.Instance.right) ||
                Input.GetKey(Controls.Instance.down) ||
                Input.GetKey(Controls.Instance.left)
            )
            && !managing
            && !battling
        )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckCollision(Vector3 destination)
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position + destination);
        if (hit.transform != null)
        {
            HandleCollider(hit.collider.gameObject);
            return true;
        }
        else
        {
            if (interact.gameObject.activeSelf)
            {
                interact.gameObject.SetActive(false);
            }
            return false;
        }
    }

    public void HandleCollider(GameObject collider)
    {
        if (collider.tag == "Item")
        {
            Item generatedItem = Item.GenerateItem();
            inventory.Items.Add(generatedItem);
            inventory.UpdateInventory(generatedItem);
            Destroy(collider);
        }
        if (collider.tag == "NPC")
        {
            interact.rectTransform.position = new Vector3(transform.position.x + 2, transform.position.y - 1, 0);
            interact.gameObject.SetActive(true);
        }
        if (collider.tag == "Enemy")
        {
            battling = true;
            battle.StartBattle(transform.position);
        }
    }

    public Vector3 GetVectorForDirection(KeyCode direction)
    {
        Vector3 vector;
        if (direction == Controls.Instance.up)
        {
            vector = new Vector3(0, size);
        }
        else if (direction == Controls.Instance.right)
        {
            vector = new Vector3(size, 0);
        }
        else if (direction == Controls.Instance.down)
        {
            vector = new Vector3(0, -size);
        }
        else if (direction == Controls.Instance.left)
        {
            vector = new Vector3(-size, 0);
        }
        else
        {
            vector = transform.position;
        }
        return vector;
    }

    public void Move(KeyCode direction)
    {
        Vector3 destination = GetVectorForDirection(direction);
        if (!CheckCollision(destination))
        {
            transform.Translate(destination);
        }
    }

    public void CheckForMovement()
    {
        if (IsMoving())
        {
            if (Input.GetKey(Controls.Instance.up))
            {
                animator.SetBool("pushingUp", true);
                Move(Controls.Instance.up);
            }
            if (Input.GetKey(Controls.Instance.right))
            {
                animator.SetBool("pushingRight", true);
                Move(Controls.Instance.right);
            }
            if (Input.GetKey(Controls.Instance.down))
            {
                animator.SetBool("pushingDown", true);
                Move(Controls.Instance.down);
            }
            if (Input.GetKey(Controls.Instance.left))
            {
                animator.SetBool("pushingLeft", true);
                Move(Controls.Instance.left);
            }
        }
        else
        {
            SetIdle();
        }
    }

    public void CheckForManagement()
    {
        if (Input.GetKeyDown(Controls.Instance.inventory))
        {
            managing = !managing;
            inventory.ToggleAt(transform.position);
        }
        if (Input.GetKeyDown(Controls.Instance.save))
        {
            Persistence.Save();
        }
        if (Input.GetKeyDown(Controls.Instance.load))
        {
            Persistence.Load();
        }
    }

    public void CheckForInteraction()
    {
        if (Input.GetKeyDown(Controls.Instance.interact))
        {
            interacting = !interacting;
        }
    }
}
