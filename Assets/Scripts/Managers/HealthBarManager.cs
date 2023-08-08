using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private GameObject canvas;

    private List<GameObject> icons = new List<GameObject>();

    public void UpdateBar(int health, int maxHealth)
    {
        foreach(GameObject icon in icons)
        {
            Destroy(icon);
        }

        for (int i = 0; i < maxHealth + 1; i++)
        {
            GameObject newIcon = Instantiate(healthPrefab);
            newIcon.transform.SetParent(canvas.transform, false);

            if (i < health)
            {
                newIcon.GetComponent<Image>().sprite = sprites[0];
            }
            else if (i == maxHealth)
            {
                newIcon.GetComponent<Image>().sprite = sprites[3];
            }
            else
            {
                newIcon.GetComponent<Image>().sprite = sprites[2];
            }

            newIcon.GetComponent<RectTransform>().localPosition = new Vector3(-762 + (i * 70), 458, 0);
            icons.Add(newIcon);
        }
    }
}
