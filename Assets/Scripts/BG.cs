using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BG : MonoBehaviour
{
    private Image image = default;
    private Image _image => image ??= GetComponent<Image>();
    [SerializeField]
    private Sprite[] _sprites = default;

    private void OnEnable()
    {
        Set(Product.GetCurrentId());
        Product.OnSetCurrentId.AddListener(Set);
    }

    private void OnDisable()
    {
        Product.OnSetCurrentId.RemoveListener(Set);
    }


    private void Set(int id)
    {
        _image.sprite = _sprites[id];
    }
}
