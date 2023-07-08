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

    public Vector2 inputVector;    // �߰�
    public bool isInput;    // �߰�
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
        ControlJoystickLever(eventData);  // �߰�
        isInput = true;    // �߰�
    }

    // ������Ʈ�� Ŭ���ؼ� �巡�� �ϴ� ���߿� ������ �̺�Ʈ
    // ������ Ŭ���� ������ ���·� ���콺�� ���߸� �̺�Ʈ�� ������ ����    
    public void OnDrag(PointerEventData eventData)
    {
        // var inputDir = eventData.position - rectTransform.anchoredPosition;
        // var clampedDir = inputDir.magnitude < leverRange ? inputDir 
        //     : inputDir.normalized * leverRange;
        // lever.anchoredPosition = clampedDir;
        
        ControlJoystickLever(eventData);    // �߰�
        
    }

    // �߰�
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
        isInput = false;    // �߰�
    }
}