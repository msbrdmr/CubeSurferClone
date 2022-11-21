using System.Collections.Generic;
using UnityEngine;
public class StackController : MonoBehaviour
{
    [SerializeField] private GameObject GameOver;
    public List<GameObject> cubelist = new List<GameObject>();
    private GameObject lastCube;
    private RaycastHit hit;
    private bool isOver;
    public AudioSource collectSound;


    void Start()
    {
        UpdateLastCube();
    }

    void Update()
    {
        setRayCast();
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.forward * 1.1f, Color.red);
    }

    public void PushStack(GameObject gameobj)
    {

        transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        gameobj.transform.position = new Vector3(lastCube.transform.position.x, lastCube.transform.position.y - 2f, lastCube.transform.position.z);
        gameobj.transform.SetParent(transform);
        gameobj.AddComponent<Rigidbody>();
        gameobj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        gameobj.GetComponent<BoxCollider>().material.bounciness = 0f;
        cubelist.Add(gameobj);
        UpdateLastCube();
    }

    public void PopStack(GameObject gameobj)
    {
        // Debug.Log("popped from stack");
        gameobj.transform.parent = null;
        cubelist.Remove(gameobj);
        UpdateLastCube();
    }

    public void VoidStack(GameObject gameobj)
    {
        gameobj.transform.parent = null;
        gameobj.GetComponent<BoxCollider>().isTrigger = true;
        cubelist.Remove(gameobj);
        Destroy(gameobj, 2f);
        UpdateLastCube();
    }

    private void UpdateLastCube()
    {
        if (cubelist.Count == 0)
            lastCube = null;
        else
        {
            lastCube = cubelist[cubelist.Count - 1];

            if (cubelist.Count == 2)
            {
                cubelist[1].GetComponent<CubeController>().Last = true;
            }
            else if (cubelist.Count > 2)
            {
                cubelist[cubelist.Count - 1].GetComponent<CubeController>().Last = true;
                cubelist[cubelist.Count - 2].GetComponent<CubeController>().Last = false;
            }
        }
    }

    private void setRayCast()
    {
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.forward, out hit, 1.1f)
        )
        {
            if (hit.transform.tag == "obstacle")
            {
                if (!isOver)
                {
                    GameOver.GetComponent<Animator>().SetTrigger("isgameOver");
                    Time.timeScale = 0f;
                    isOver = true;
                }
            }

            
        }

    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.transform.tag == "cheese")
        {   
            collectSound.Play();
            other.transform.gameObject.GetComponent<Cheese>().collectCheese();
            Destroy(other.transform.gameObject);
        }
    }
}
