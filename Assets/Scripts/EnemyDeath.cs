using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private bool useHealthBar;
    [SerializeField] private Image healthBarFiller;
    [SerializeField] private Image healthBarBorder;

    public float fadeSpeed;

    private Animator animator;
    private Transform trans;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual IEnumerator Death()
    {
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<EnemyController>());
        Destroy(GetComponent<PolygonCollider2D>());
        trans.position = new Vector3(trans.position.x, trans.position.y - 0.098f, trans.position.z);

        animator.SetTrigger("Death");

        yield return new WaitForSeconds(2f);

        while(spriteRenderer.color.a - fadeSpeed >= 0)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - fadeSpeed);
            if (useHealthBar)
            {
                healthBarBorder.color = new Color(healthBarBorder.color.r, healthBarBorder.color.g, healthBarBorder.color.b, healthBarBorder.color.a - fadeSpeed);
                healthBarFiller.color = new Color(healthBarFiller.color.r, healthBarFiller.color.g, healthBarFiller.color.b, healthBarFiller.color.a - fadeSpeed);
            }
            yield return new WaitForFixedUpdate();
        }

        Destroy(this.gameObject);
    }

    public Animator GetAnimator()
    {
        return animator;
    }
}
