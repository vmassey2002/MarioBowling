using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;

public class ArrowController : MonoBehaviour
{
    private MqttClient client;
    private string brokerIpAddress = "mqtt_broker_ip";
    private string topic = "game_controls";

    void Start()
    {
        // create client instance
        client = new MqttClient(brokerIpAddress);
chan
        // register to message received
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId);

        // subscribe to the topic
        client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        // handle received message
        string message = System.Text.Encoding.UTF8.GetString(e.Message);
        Debug.Log("Received message: " + message);

        // process the received message and perform actions accordingly
        if (message == "left_arrow_pressed")
        {
            float rotationAmount = Time.deltaTime * 30f;
            transform.Rotate(Vector3.down, rotationAmount);
            Debug.Log("Rotation amount: " + rotationAmount);
        }
        // Add more conditions for other messages as needed
    }

    // Update is called once per frame
    void Update()
    {
        // Your existing input handling code remains unchanged
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