using UnityEngine;


public enum FruitType { Apple, Banana, Cherry, Kiwi, Melon, Orange, Pineapple, Strawberry}
public class fruit : MonoBehaviour
{
    [SerializeField]private FruitType type;
    [SerializeField] private GameObject pickUpEffect;
    private GameManager gameManager;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {

        gameManager = GameManager.Instance;
        SetRandomLookIfNeeded();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player pyr = collision.GetComponentInChildren<player>();
        if (pyr != null)
        {
            gameManager.AddFruit();
            Destroy(gameObject);

            GameObject newFx = Instantiate(pickUpEffect,transform.position,Quaternion.identity);
            //Destroy(newFx, .5f);
        }
    }
    private void SetRandomLookIfNeeded()
    {
        if (gameManager.FruitsHaveRandomLook() == false)
        {
            UpdatetFruitVisuals();
            return;
        }

        int randomIndex = Random.Range(0, 8);
        anim.SetFloat("fruitIndex", randomIndex);
    }
    private void UpdatetFruitVisuals() => anim.SetFloat("fruitIndex", (int)type);
}
