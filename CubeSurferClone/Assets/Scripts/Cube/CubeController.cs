using UnityEngine;

public class CubeController : MonoBehaviour
{

    [Header("Layer Mask")]
    [SerializeField] private LayerMask voidLM;
    [SerializeField] private LayerMask finishLM;
    [SerializeField] private GameObject stackControllerobj;
    [Header("Animation")]
    [SerializeField] private GameObject mouse;
    private Vector3 direction = Vector3.back;
    private RaycastHit hit;
    [Header("Bool")]
    public bool Last;
    private bool isPushed = false;
    [Header("Audio")]
    public AudioSource collectSound;


    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(new Vector3(transform.position.x - 0.9f, transform.position.y, transform.position.z), direction * 1.1f, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x + 0.9f, transform.position.y, transform.position.z), direction * 1.1f, Color.red);
        if (Last)
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f), Vector3.down * 1.1f, Color.red);
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f), Vector3.down * 1.1f, Color.red);
            Debug.DrawRay(new Vector3(transform.position.x - 0.9f, transform.position.y, transform.position.z), Vector3.down * 1.1f, Color.red);
            Debug.DrawRay(new Vector3(transform.position.x + 0.9f, transform.position.y, transform.position.z), Vector3.down * 1.1f, Color.red);
        }
        setRayCast();
        setBottomRayCast();
    }

    private void setRayCast()
    {
        if (Physics.Raycast(new Vector3(transform.position.x - 0.9f, transform.position.y, transform.position.z), direction, out hit, 1f) ||
        Physics.Raycast(new Vector3(transform.position.x + 0.9f, transform.position.y, transform.position.z), direction, out hit, 1f)
        )
        {
            // Debug.Log("raycash hitted!");
            if (!isPushed)
            {
                isPushed = true;
                setDir();
                stackControllerobj.GetComponent<StackController>().PushStack(gameObject);
            }

            if (hit.transform.tag == "obstacle")
            {
                stackControllerobj.GetComponent<StackController>().PopStack(gameObject);
            }
            if(hit.transform.tag =="finishStairs"){
                stackControllerobj.GetComponent<MovementController>().VerticalMovementSpeed = 0;
            }
        }
    }

    public void setBottomRayCast()
    {
        if (Last)
        {
            //void check raycasts
            if (
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f), Vector3.down, out hit, 1.1f, voidLM) &&
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f), Vector3.down, out hit, 1.1f, voidLM) &&
            Physics.Raycast(new Vector3(transform.position.x - 0.9f, transform.position.y, transform.position.z + 0.2f), Vector3.down, out hit, 1.1f, voidLM) &&
            Physics.Raycast(new Vector3(transform.position.x + 0.9f, transform.position.y, transform.position.z + 0.2f), Vector3.down, out hit, 1.1f, voidLM))
            {
                stackControllerobj.GetComponent<StackController>().VoidStack(gameObject);
            }

            //finish line raycast
            else if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f), Vector3.down, out hit, 1.1f, finishLM) &&
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f), Vector3.down, out hit, 1.1f, finishLM)
            )
            {
                Debug.Log("finish");
                mouse.GetComponent<Animator>().SetBool("isFinished", true);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Last)
        {
            if (other.transform.tag == "cheese")
            {
                collectSound.Play();
                other.transform.gameObject.GetComponent<Cheese>().collectCheese();
                Destroy(other.transform.gameObject);
            }
        }
    }

    public void setDir()
    {
        direction = Vector3.forward;
    }
}