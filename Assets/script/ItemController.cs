using UnityEngine;

public class ItemSwitcher : MonoBehaviour
{
    public GameObject[] items;
    private int currentIndex = -1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ToggleItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ToggleItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ToggleItem(2);
    }

    void ToggleItem(int index)
    {
        if (index < 0 || index >= items.Length)
            return;

        if (currentIndex == index)
        {
            // Jika tombol yang sama ditekan, matikan semua
            DeactivateAllItems();
            currentIndex = -1;
        }
        else
        {
            ActivateItem(index);
        }
    }

    void ActivateItem(int index)
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(i == index);
        }

        currentIndex = index;
    }

    void DeactivateAllItems()
    {
        foreach (GameObject item in items)
        {
            item.SetActive(false);
        }
    }
}
