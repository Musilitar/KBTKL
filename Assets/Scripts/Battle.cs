using UnityEngine;

public class Battle : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartBattle(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }
}
