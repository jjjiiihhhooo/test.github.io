using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
public class ServerManager : MonoBehaviour
{
    public string serverIP = "59.28.216.152"; // 서버 IP 주소
    public int serverPort = 8080; // 서버 포트 번호
    private string savePath; // 저장할 경로
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
            Debug.Log("파일 다운로드 실패: " + downloadRequest.error);
            a.text = "연결 실패...";
        }
        else
        {
            byte[] fileData = downloadRequest.downloadHandler.data;
            File.WriteAllBytes(savePath, fileData);
            Debug.Log("파일 다운로드 완료: " + savePath);

            // 다운로드한 파일의 내용 출력
            string fileContent = File.ReadAllText(savePath);

            Debug.Log("파일 내용: " + fileContent);
            a.text = fileContent;

        }
    }
}