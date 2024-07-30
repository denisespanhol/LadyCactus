using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonsHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color _hoverColor;
    [SerializeField] private TextMeshProUGUI _textBttn;

    private Color _currentColor;
    private Image _bttn;

    private void Awake()
    {
        _bttn = GetComponent<Image>();
        _currentColor = _bttn.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _bttn.color = _hoverColor;
        _textBttn.color = _currentColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _bttn.color = _currentColor;
        _textBttn.color = _hoverColor;
    }
}
