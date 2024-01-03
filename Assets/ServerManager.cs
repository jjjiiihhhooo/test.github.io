using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
public class ServerManager : MonoBehaviour
{
    public string serverIP = "59.28.216.152"; // ���� IP �ּ�
    public int serverPort = 8080; // ���� ��Ʈ ��ȣ
    private string savePath; // ������ ���
    public TextMeshProUGUI a;

    private void Start()
    {
        savePath = Application.persistentDataPath + "/downloaded_text1.txt";
        DownloadFile();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("dd");
            DownloadFile();
        }

    }

    public void DownloadFile()
    {
        StartCoroutine(Download());
    }

    IEnumerator Download()
    {
        string url = $"http://{serverIP}:{serverPort}";

        UnityWebRequest downloadRequest = UnityWebRequest.Get(url);
        yield return downloadRequest.SendWebRequest();

        if (downloadRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("���� �ٿ�ε� ����: " + downloadRequest.error);
            a.text = "���� ����...";
        }
        else
        {
            byte[] fileData = downloadRequest.downloadHandler.data;
            File.WriteAllBytes(savePath, fileData);
            Debug.Log("���� �ٿ�ε� �Ϸ�: " + savePath);

            // �ٿ�ε��� ������ ���� ���
            string fileContent = File.ReadAllText(savePath);

            Debug.Log("���� ����: " + fileContent);
            a.text = fileContent;

        }
    }
}