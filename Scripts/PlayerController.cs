using Inventory;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    CharacterController p_chcon;
    public float p_movSpeed;
    public float p_walkSpeed = 5.0f;
    public float p_runSpeed = 8.0f;
    public float p_gravity = 10.0f;
    private Vector3 relative_pos2 = new Vector3(0, 1.5f, 0.25f);
    private Vector3 relative_pos1 = new Vector3(0, 1.7f, 0.2f);
    public bool show_TransNPC;
    public bool show_cursor;

    public float p_jumpSpeed = 4.0f;
    private CamerControl camerControl;
    private Vector3 p_movDirection = Vector3.zero;
    private Camera cam;
    private Transform camTrans;
    [FormerlySerializedAs("_bagManager")] public  BagManager bagManager;
    public bool _show;
    private Vector3 p_rot;
    private Animator animator;

    private RaycastHit hit;

    private SceneSetting sceneSetting;
    private float load_time = 1.0f;
    private float load_timer;
    // Start is called before the first frame update
    void Start()
    {
        sceneSetting = GameObject.FindGameObjectWithTag("SceneInfo").GetComponent<SceneSetting>();

        animator = GetComponent<Animator>();
        p_rot = transform.eulerAngles;

        Cursor.lockState = CursorLockMode.Locked;
        show_cursor = false;

        cam = Camera.main;
        camerControl = GetComponentInChildren<CamerControl>();
        p_chcon = GetComponent<CharacterController>();
        camTrans = cam.transform;
        // Cursor.lockState = CursorLockMode.Locked;
        bagManager = BagManager.Instance;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (show_cursor)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (!GameObject.FindGameObjectWithTag("Menu"))
            Control();
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        EnableFunc();
    }

    void Control()
    {
        if (!show_cursor)
        {
            p_rot.y += Input.GetAxis("Mouse X");
            transform.localRotation = Quaternion.Euler(p_rot.x, p_rot.y, p_rot.z);
        }
        Vector3 translation = Vector3.zero;
        p_movSpeed = p_walkSpeed;
        animator.SetInteger("State", 0);
        if (Input.GetAxis("Vertical") != 0)
        {
            animator.SetInteger("State", 1);
            translation += camTrans.forward * Input.GetAxis("Vertical");
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                camerControl.transform.localPosition = relative_pos2;
                animator.SetInteger("State", 2);
                p_movSpeed = p_runSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                camerControl.transform.localPosition = relative_pos1;
                animator.SetInteger("State", 1);
            }
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            translation += camTrans.right * Input.GetAxis("Horizontal");
            animator.SetInteger("State", 1);
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                camerControl.transform.localPosition = relative_pos2;
                animator.SetInteger("State", 2);
                p_movSpeed = p_runSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                camerControl.transform.localPosition = relative_pos1;
                animator.SetInteger("State", 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (p_chcon.isGrounded)
                p_movDirection.y = p_jumpSpeed;
        }
        p_movDirection.y -= p_gravity * Time.deltaTime;
        p_chcon.Move(translation.normalized * (Time.deltaTime * p_movSpeed));
        p_chcon.Move(p_movDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.B))   //显示隐藏背包
        {
            _show = !_show;
            bagManager.ShowBag(_show);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            show_cursor = !show_cursor;
        }

        show_TransNPC = false;
        if (Input.GetKey(KeyCode.R))
        {
            show_TransNPC = true;
        }
 
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            if (hit.collider.CompareTag("Object"))
            {
                if (Input.GetKey(KeyCode.F))
                {
                    if (Vector3.Distance(transform.position, hit.transform.position) < 2.0f)
                    {
                        bagManager.PutInBag(hit.collider.name);
                    }
                }
                
            }
            if (Input.GetMouseButton(0)  && hit.collider.gameObject.name == bagManager.target)
            { 
                if (hit.collider.gameObject.name == "Water")
                {
                    Destroy(hit.collider.gameObject);
                    bagManager.TakeOutBag(bagManager.currentSelection);
                }
            }

        }

        if (sceneSetting.load_saved && !sceneSetting.player_load)
        {
            if (load_timer < load_time)
                load_timer += Time.deltaTime;
            else
            {
                SetTrans(sceneSetting.playerSave);
                sceneSetting.player_load = true;
            }
        }

    }

    void EnableFunc()
    {
        if (camerControl.isTrans)
        {
            if(GetComponent<InitPlayerControl>())
                Destroy(gameObject);
            tag = "TransNPC";
            enabled = false;         
        }
    }
    private void SetTrans(PlayerSave playerSave)
    {
        Vector3 pos = new Vector3(playerSave.pos_x, playerSave.pos_y, playerSave.pos_z);
        Vector3 rot = new Vector3(playerSave.rot_x, playerSave.rot_y, playerSave.rot_z);
        Vector3 scl = new Vector3(playerSave.scl_x, playerSave.scl_y, playerSave.scl_z);
        var transform1 = transform;
        transform1.position = pos;
        transform1.eulerAngles = rot;
        transform1.localScale = scl;
        p_rot = rot;
    }

}