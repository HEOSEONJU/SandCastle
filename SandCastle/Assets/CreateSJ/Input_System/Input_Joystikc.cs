using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Input_Joystikc : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;
    [SerializeField, Range(10f, 150f)]
    private float leverRange;

    public Vector2 inputVector;    // 추가
    public bool isInput;    // 추가
    [SerializeField]
    Vector2 origin;


    [SerializeField]
    float minXRange=250;
    [SerializeField]
    float maxXRange = 830;
    [SerializeField]
    float minYRange = 250;
    [SerializeField]
    float maxYRange = 1520;
    private void Awake()
    {
        inputVector = Vector2.zero;
        isInput = false;
        rectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        origin = rectTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // var inputDir = eventData.position - rectTransform.anchoredPosition;
        // var clampedDir = inputDir.magnitude < leverRange ? inputDir 
        //     : inputDir.normalized * leverRange;
        // lever.anchoredPosition = clampedDir;

        
        Vector2 posi = Input.mousePosition;
        posi.x=Mathf.Clamp(posi.x,minXRange, maxXRange);
        posi.y = Mathf.Clamp(posi.y, minYRange, maxYRange);
        rectTransform.anchoredPosition = posi;
        ControlJoystickLever(eventData);  // 추가
        isInput = true;    // 추가
    }

    // 오브젝트를 클릭해서 드래그 하는 도중에 들어오는 이벤트
    // 하지만 클릭을 유지한 상태로 마우스를 멈추면 이벤트가 들어오지 않음    
    public void OnDrag(PointerEventData eventData)
    {
        // var inputDir = eventData.position - rectTransform.anchoredPosition;
        // var clampedDir = inputDir.magnitude < leverRange ? inputDir 
        //     : inputDir.normalized * leverRange;
        // lever.anchoredPosition = clampedDir;
        
        ControlJoystickLever(eventData);    // 추가
        
    }

    // 추가
    public void ControlJoystickLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir
            : inputDir.normalized * leverRange;
        lever.anchoredPosition = clampedDir;
        inputVector = clampedDir / leverRange;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.position = origin;
        inputVector = Vector2.zero;
        lever.anchoredPosition = Vector2.zero;
        isInput = false;    // 추가
    }
}