using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField ipInputField;
    [SerializeField] TMP_InputField portInputField;
    [SerializeField] TextMeshProUGUI serverDisplayTMPro;
    [SerializeField] FixedJoystick fixedJoyStick;

    Client client;

    string ip;
    int port;

    bool shouldStop = false;

    private void Awake()
    {
        client = GetComponent<Client>();
    }

    public void StartConnection()//192.168.0.143
    {
        ip = ipInputField.text.Trim();
        port = int.Parse(portInputField.text);
        serverDisplayTMPro.text = ipInputField.text + ":" + portInputField.text;
        StartCoroutine(HandleJoyStick());
    }
    private IEnumerator HandleJoyStick()
    {
        while (true)
        {
            if (!(fixedJoyStick.Vertical == 0 && fixedJoyStick.Horizontal == 0))
            {
                shouldStop = true;
                //print((fixedJoyStick.Horizontal.ToString() + ":" + fixedJoyStick.Vertical.ToString()));
                client.SendString(ip, port, (fixedJoyStick.Horizontal.ToString() + ":" + fixedJoyStick.Vertical.ToString()));
            }
            else
            {
                if (shouldStop)
                {
                    shouldStop = false;
                    client.SendString(ip, port, ("0:0"));
                }
            }
            yield return new WaitForSeconds(.2f);
        }
    }
}
