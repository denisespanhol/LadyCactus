using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class ButtonsHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color _hoverColor;
    [SerializeField] private TextMeshProUGUI _textBttn;

    private Color _currentColor;
    private Vector3 _currentScale;
    private Image _bttn;

    private void Awake()
    {
        _bttn = GetComponent<Image>();
        _currentColor = _bttn.color;
        _currentScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _bttn.color = _hoverColor;
        _textBttn.color = _currentColor;
        transform.DOScale(1.2f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.KillAll();
        _bttn.color = _currentColor;
        _textBttn.color = _hoverColor;
        transform.DOScale(_currentScale, 0.1f);
    }
}
