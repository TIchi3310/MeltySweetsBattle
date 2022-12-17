using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 move;
    public float Speed;
    public float HP;
    public float areaDamage;
    public bool isSafe;
    [SerializeField]
    [Tooltip("�v���C���[�̃v���n�u��ݒ�")]
    private GameObject playerPrefab;


    void Start()
    {
        isSafe = true;
    }
    public void OnMove(InputAction.CallbackContext context) // �ړ�
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context) // �U��
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Fire");
        }
    }

    public void OnTeleport() // �e���|�[�g
    {
        transform.position = new Vector2(Random.Range(-1, 1),Random.Range(-1, 1));
    }

    void Update()
    {
        GameObject playerObj = GameObject.Find(playerPrefab.name);

        //�@�ړ��̑���
        transform.Translate(move * Speed * Time.deltaTime);

        // �e�̊O�ɏo�����̔���
        if (!isSafe)
        {
            HP -= areaDamage * Time.deltaTime;
        }

        if (HP <= 0)
        {
            Destroy(gameObject);
            gameObject.transform.parent = null;
            Invoke("hukkatu", 3f);
        }
        if (playerObj == null)
        {
            // playerPrefab����V����GameObject���쐬
            GameObject newPlayerObj = Instantiate(playerPrefab);

            // �V�����쐬����GameObject�̖��O���Đݒ�(�����"PlayerSphere"�ƂȂ�)
            newPlayerObj.name = playerPrefab.name;
            // �������Ŗ��O���Đݒ肵�Ȃ��ꍇ�A�����Ō��܂閼�O�́A"PlayerSphere(Clone)"�ƂȂ邽��
            //   13�s�ڂŒT���Ă���"PlayerSphere"���i���Ɍ�����Ȃ����ƂɂȂ�Aplayer�������ɐ��Y�����
            //   �ǂ��������Ƃ��́A22�s�ڂ��R�����g�A�E�g���ăQ�[�������s����΂킩��܂��B
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GetComponent<AudioSource>().Play();
    }
    // ���S�n�т̊O�ɏo����
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Shadow")
        {
            isSafe = false;
        }
        else
        {
            gameObject.transform.localScale = new Vector2(1f, 1f);
        }

    }

    // ���S�n�тɓ�������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Shadow")
        {
            isSafe = true;

        }
        else if (collision.gameObject.name != gameObject.name)
        {
            gameObject.transform.localScale = new Vector2(1.2f, 1.2f);
        }
    }

}