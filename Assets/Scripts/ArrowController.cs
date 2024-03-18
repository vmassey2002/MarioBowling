using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class ArrowController : MonoBehaviour
{
    private Ball ballScript;

    private MqttClient client;
    public static string message = "";
    void Start()
    {
        // Find the GameObject with the Ball script attached
        GameObject ballGameObject = GameObject.FindGameObjectWithTag("Ball");

        // Get the Ball script component attached to the GameObject
        ballScript = ballGameObject.GetComponent<Ball>();

        // Initialize MQTT client
        client = new MqttClient("mqtt.eclipseprojects.io");
        client.MqttMsgPublishReceived += OnMessageReceived;

        // Connect to MQTT broker
        client.Connect("UnityClient");
        
        // Subscribe to MQTT topic
        client.Subscribe(new string[] { "ece180da/test/arrow_control" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    void OnMessageReceived(object sender, MqttMsgPublishEventArgs e)
    {
        // Handle received MQTT message
        message = Encoding.UTF8.GetString(e.Message);
        Debug.Log("Received MQTT message: " + message);

        // Parse message and adjust arrow movement accordingly
        // Add more conditions to handle other arrow movements as needed
    }

    void OnDestroy()
    {
        // Disconnect from MQTT broker when the script is destroyed
        if (client != null && client.IsConnected)
        {
            client.Disconnect();
        }
    }
    void Update()
    {
        if (message == "a")
        {
            transform.Rotate(Vector3.down, 5.0f);
            message = "";
        }
        if (message == "d")
        {
            transform.Rotate(Vector3.up, 5.0f);
            message = "";
        }
        if (message == "w")
        {
            float incrementAmount = 0.1f; // Adjust this value as needed
            Vector3 newSize = transform.localScale + new Vector3(0, 0, incrementAmount);
            transform.localScale = newSize;
            message = "";
        }

        if (message == "s")
        {
            ballScript.StartCoroutine(ballScript.Shoot());
            message = "";

        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down, Time.deltaTime * 30f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 30f);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.localScale.z < 2)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
                    transform.localScale.z + (1 * Time.deltaTime));
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 2);
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.localScale.z > 0.1f)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
                    transform.localScale.z - (1 * Time.deltaTime));
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.1f);
            }

        }
    }
}