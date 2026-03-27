using UnityEngine;

public class CheckpointBlade : MonoBehaviour
{
    [Header("Detection Zone")]
    public Vector2 detectSize = new Vector2(2f, 3f);
    public Vector2 detectOffset = new Vector2(0f, 2f); 

    [Header("Slide Up")]
    public float slideSpeed = 20f;
    public float slideDistance = 4f;

    [Header("Return")]
    public float returnDelay = 1f;
    public float returnSpeed = 8f;

    private Vector3 originalPosition;
    private bool isActive = false;      
    private bool hasTriggered = false;
    private bool isMoving = false;

    public static CheckpointBlade Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        originalPosition = transform.position;
        isActive = false;
        hasTriggered = false;

        GetComponent<Collider2D>().enabled = false;
    }

    public void Activate()
    {
        isActive = true;
        Debug.Log("Blade armed!");
    }

    void Update()
    {
        if (!isActive || hasTriggered || isMoving) return;

        Vector2 center = new Vector2(
            transform.position.x + detectOffset.x,
            transform.position.y + detectOffset.y
        );

        Collider2D[] hits = Physics2D.OverlapBoxAll(center, detectSize, 0f);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hasTriggered = true;
                StartCoroutine(SlideUp());
                break;
            }
        }
    }

    System.Collections.IEnumerator SlideUp()
    {
        isMoving = true;
        GetComponent<Collider2D>().enabled = true; 

        Vector3 targetPos = originalPosition + new Vector3(0f, slideDistance, 0f);

        while (Vector3.Distance(transform.position, targetPos) > 0.02f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, targetPos, slideSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.position = targetPos;

        yield return new WaitForSeconds(returnDelay);

        while (Vector3.Distance(transform.position, originalPosition) > 0.02f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, originalPosition, returnSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.position = originalPosition;
        GetComponent<Collider2D>().enabled = false; 
        isMoving = false;
        hasTriggered = false; 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<DeathHandler>()?.Die();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<DeathHandler>()?.Die();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(
            new Vector3(
                transform.position.x + detectOffset.x,
                transform.position.y + detectOffset.y, 0),
            new Vector3(detectSize.x, detectSize.y, 0)
        );
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,
            transform.position + new Vector3(0, slideDistance, 0));
    }
}