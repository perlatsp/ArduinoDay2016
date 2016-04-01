using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO.Ports;
using System;

public class Arduino : MonoBehaviour
{
    public bool ConnectOnStart = true;
    public string selectedPort = "";
    public Text text;
    public GameObject ConnectionPanel; 
    public SerialPort serial = new SerialPort();
    string[] ports = SerialPort.GetPortNames();
    public Dropdown portsDrop;

    private string ReadStr;
    public int ReadInt;
    void Start()
    {
        foreach (string pt in ports)
        {
            portsDrop.options.Add(new Dropdown.OptionData { text = pt });
        }

        if (ConnectOnStart)
        {
            Connect();
        }
        Time.timeScale = 0;

    }


    public void Connect()
    {
        try
        {
            if (!serial.IsOpen)
            {
                if (selectedPort == "") GetPortName();
                serial.PortName = selectedPort;
                serial.BaudRate = 9600;
                serial.Open();
                ConnectionPanel.SetActive(false);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    [ExecuteInEditMode]
    public void GetPortName()
    {
        selectedPort = portsDrop.captionText.text;
    }

    public void SendToArduino( int msg)
    {
        if (serial.IsOpen)
        {
            serial.Write(msg.ToString());
            serial.DiscardOutBuffer();
        }
    }

    void Update()
    {
        if (serial.IsOpen)
        {
            ReadStr = serial.ReadLine();
            ReadInt = int.Parse(ReadStr);
            HidePanel();
            Time.timeScale = 1;
        }
    }

    public void HidePanel()
    {
        if (ConnectionPanel.active)
        {
            ConnectionPanel.SetActive(false);
        } 
    }


}


