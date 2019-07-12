using UnityEngine;
using UnityEngine.UI;

public class FurniturePreview : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject selectedGO;

    [HideInInspector]
    public int furnitureIndex;

    public delegate void OnSelectedDel(int furnitureIndex);
    public event OnSelectedDel onSelectedEvent;

    public void SetData(Sprite newSprite, int newIndex)
    {
        image.sprite = newSprite;
        furnitureIndex = newIndex;
    }

    public void Deselect()
    {
        selectedGO.SetActive(false);
    }

    public void Select()
    {
        selectedGO.SetActive(true);
    }

    public void OnClick()
    {
        Select();

        if(onSelectedEvent != null)
            onSelectedEvent(furnitureIndex);
    }
}
