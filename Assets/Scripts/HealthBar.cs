using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float timeToDrain = 0.25f;
    private Image image;

    private float target;

    private Coroutine drainHealthBarCoroutine;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;
        drainHealthBarCoroutine = StartCoroutine(DrainHealthBar());
    }

    private IEnumerator DrainHealthBar()
    {
        float fillAmount = image.fillAmount;

        float elapsedTime = 0f;
        while (elapsedTime < timeToDrain)
        {
            elapsedTime += Time.deltaTime;
            image.fillAmount = Mathf.Lerp(fillAmount, target, (elapsedTime / timeToDrain));
            yield return null;
        }
    }
}
