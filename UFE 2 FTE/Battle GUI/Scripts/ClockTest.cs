using UnityEngine;
using UnityEngine.UI;
using UFE3D;
using FPLibrary;

public class ClockTest : MonoBehaviour
{
    [SerializeField]
    private Image[] numbers;
    private Vector2 onesDigitOrginalPosition;
    [SerializeField]
    private GameObject infinityGameObject;
    [SerializeField]
    private bool useInfinityGameObject;

    // in order, like 0, 1, 2, ..., 9
    [SerializeField]
    private Sprite[] numberSprites;

    private void Start()
    {
        onesDigitOrginalPosition = numbers[1].rectTransform.anchoredPosition;

        int time = Mathf.CeilToInt((float)UFE.timer);

        SetNumber(time);
    }

    private void Update()
    {
        int time = Mathf.CeilToInt((float)UFE.timer);
        
        SetNumber(time);
    }

    private void SetNumber(int number)
    {
        if (useInfinityGameObject == true
            && UFE.gameMode == GameMode.TrainingRoom)
        {
            infinityGameObject.SetActive(true);

            numbers[0].gameObject.SetActive(false);

            numbers[1].gameObject.SetActive(false);

            return;
        }

        infinityGameObject.SetActive(false);

        int tens = (number % 100) / 10;
        int ones = (number % 10);

        if (tens > 0)
        {
            numbers[0].gameObject.SetActive(true);
            numbers[0].sprite = numberSprites[tens];

            numbers[1].rectTransform.anchoredPosition = onesDigitOrginalPosition;
        }
        else
        {
            numbers[0].gameObject.SetActive(false);

            Vector2 pos = numbers[1].rectTransform.anchoredPosition;
            pos.x = 0;
            numbers[1].rectTransform.anchoredPosition = pos;
        }

        numbers[1].sprite = numberSprites[ones];
    }
}
