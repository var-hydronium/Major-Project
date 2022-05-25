using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Button Sprites")]
    [SerializeField] bool self = true;
    [SerializeField] Image _img;
    [SerializeField] Sprite _default, _pressed;
    [SerializeField] GameObject _highLight;


    private void Start()
    {
        if (self)
        {
            _img = gameObject.GetComponent<Image>();
            _default = _img.sprite;
        }
            
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_pressed != null)
        _img.sprite = _pressed;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_highLight != null)
            _highLight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_highLight != null)
        _highLight.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_default != null)
            _img.sprite = _default;
    }



}
