using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMeter : MonoBehaviour
{
    public MeterType AssociatedMeter;
    public int NumContainers;
    public GameObject ContainerBaseObject;
    public Sprite EmptySprite, FillSprite;
    public float ContainerDistance;
    public Color EmptyTint;

    float containerMaxValue => 100f / NumContainers;

    List<Image> emptyContainers, fillContainers;

    void Start ()
    {
        var image = GetComponent<Image>();
        if (image != null)
        {
            Destroy(image);
        }
        
        initializeContainers();
    }

    void Update ()
    {
        setContainerFills();
    }

    void initializeContainers ()
    {
        emptyContainers = new List<Image>();
        fillContainers = new List<Image>();

        Vector2 pos = transform.position;

        for (int i = 0; i < NumContainers; i++)
        {
            Image empty = Instantiate(ContainerBaseObject).GetComponent<Image>();
            Image fill = Instantiate(ContainerBaseObject).GetComponent<Image>();

            empty.name = $"empty{i}";
            fill.name = $"fill{i}";

            emptyContainers.Add(empty);
            fillContainers.Add(fill);

            empty.sprite = EmptySprite;
            fill.sprite = FillSprite;

            empty.color = EmptyTint;

            fill.type = Image.Type.Filled;
            fill.fillMethod = Image.FillMethod.Vertical;
            fill.fillOrigin = 0; // bottom

            empty.transform.position = pos;
            fill.transform.position = pos;

            pos += Vector2.right * ContainerDistance;
        }

        foreach (var empty in emptyContainers)
        {
            ((RectTransform) empty.transform).SetParent(transform, true);
        }

        foreach (var fill in fillContainers)
        {
            ((RectTransform) fill.transform).SetParent(transform, true);
        }
    }

    void setContainerFills ()
    {
        float value = DragonStats.Instance.GetMeterByType(AssociatedMeter).Value;

        for (int i = 0; i < NumContainers; i++)
        {
            Image container = fillContainers[i];

            if (value <= containerMaxValue)
            {
                container.fillAmount = value / containerMaxValue;
                break;
            }
            else
            {
                container.fillAmount = 1;
                value -= containerMaxValue;
            }
        }
    }
}
