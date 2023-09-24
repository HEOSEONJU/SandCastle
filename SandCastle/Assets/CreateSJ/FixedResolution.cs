
using UnityEngine;

public class FixedResolution : MonoBehaviour
{
    [SerializeField]
    int setWidth = 1440; // ����� ���� �ʺ�
    [SerializeField]
    int setHeight = 2960; // ����� ���� ����
    [SerializeField]
    Camera cam;
    private void Start()
        {
            SetResolution(); // �ʱ⿡ ���� �ػ� ����
        }

        /* �ػ� �����ϴ� �Լ� */
        public void SetResolution()
        {
        

        // ī�޶� ������Ʈ�� Viewport Rect
        Rect rt = cam.rect;

        // ���� ���� ��� 9:16, �ݴ�� �ϰ� ������ 16:9�� �Է�.
        float scale_height = ((float)Screen.width / Screen.height) / ((float)9 / 16); // (���� / ����)
        float scale_width = 1f / scale_height;

        if (scale_height < 1)
        {
            rt.height = scale_height;
            rt.y = (1f - scale_height) / 2f;
        }
        else
        {
            rt.width = scale_width;
            rt.x = (1f - scale_width) / 2f;
        }

        cam.rect = rt;

        return;
        int deviceWidth = Screen.width; // ��� �ʺ� ����
            int deviceHeight = Screen.height; // ��� ���� ����

            Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution �Լ� ����� ����ϱ�

            if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
            {
                float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
                Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
            }
            else // ������ �ػ� �� �� ū ���
            {
                float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
                Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
            }
        }
}
