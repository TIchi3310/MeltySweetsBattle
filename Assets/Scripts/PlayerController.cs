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
    [Tooltip("プレイヤーのプレハブを設定")]
    private GameObject playerPrefab;


    void Start()
    {
        isSafe = true;
    }
    public void OnMove(InputAction.CallbackContext context) // 移動
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context) // 攻撃
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Fire");
        }
    }

    public void OnTeleport() // テレポート
    {
        transform.position = new Vector2(Random.Range(-1, 1),Random.Range(-1, 1));
    }

    void Update()
    {
        GameObject playerObj = GameObject.Find(playerPrefab.name);

        //　移動の速さ
        transform.Translate(move * Speed * Time.deltaTime);

        // 影の外に出た時の判定
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
            // playerPrefabから新しくGameObjectを作成
            GameObject newPlayerObj = Instantiate(playerPrefab);

            // 新しく作成したGameObjectの名前を再設定(今回は"PlayerSphere"となる)
            newPlayerObj.name = playerPrefab.name;
            // ※ここで名前を再設定しない場合、自動で決まる名前は、"PlayerSphere(Clone)"となるため
            //   13行目で探している"PlayerSphere"が永遠に見つからないことになり、playerが無限に生産される
            //   どういうことかは、22行目をコメントアウトしてゲームを実行すればわかります。
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GetComponent<AudioSource>().Play();
    }
    // 安全地帯の外に出たら
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

    // 安全地帯に入ったら
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