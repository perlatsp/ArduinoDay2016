using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour { 
    public Arduino arduino;
    public Text KastoriaRobotics;
    public bool ledisON;
    public GameObject Cube;
	void Start ()
    {

        rg = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    Rigidbody rg;
	void Update ()
    {
      int  readValue = arduino.GetComponent<Arduino>().ReadInt;
       // float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("Vertical");
         
        if (readValue ==300)
        { 
            rg.velocity = (Vector2.up * 10);
            Debug.Log("Jumped");
        }
        else if (readValue <= 160)
        {
            transform.Translate(-Vector2.right * 3 * Time.deltaTime);
        }
        else if (readValue >=200)
        {
            transform.Translate (Vector2.right * 3 * Time.deltaTime);
        }
        
        else
        {
            rg.velocity = (Vector2.zero);
        }
        if (readValue == 400)
        {
            SceneManager.LoadScene("END");
        }
        Debug.Log(readValue);
        Cube.transform.localEulerAngles = new Vector3(0,0,readValue-360);
        KastoriaRobotics.fontSize = readValue / 10;

    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "ON" && !ledisON)
        {
            arduino.SendToArduino(1);
            ledisON = true;
        }
        else if (col.gameObject.name =="OFF" && ledisON)
        {
            arduino.SendToArduino(0);
            ledisON = false;
        }
    }


    public void LedON()
    {
        ledisON = true;

        arduino.serial.Write("1");
       arduino.serial.DiscardOutBuffer();
    }

    public void LedOFF()
    {
        ledisON = false;

        arduino.serial.Write("0");
        arduino.serial.DiscardOutBuffer();
    }
}
