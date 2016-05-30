using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using TopicPublisher;
using TopicSubscriber;
using System.Xml;

/// <summary>
/// Summary description for RebateProcessor
/// </summary>
public static class RebateProcessor
{
    private static IConnection connection;
    private static IConnectionFactory connectionFactory;
    private static ISession session;
    const string TOPIC_NAME_SENDING_ACTION = "sendingAction";
    private const string BROKER = "tcp://localhost:61616";
    private static Subscriber subscriber;
    const string CONSUMER_ID = "ass3.subscriber";

    public static void recieveFromDeliveryToCRM()
    {
        //Create a new conneciton object, specifying who the message broker is and supplying our identifier
        connectionFactory = new ConnectionFactory(BROKER, CONSUMER_ID);
        connection = connectionFactory.CreateConnection();
        connection.Start();

        //Create a new session on our newly setup connection
        session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
        subscriber = new Subscriber(session, TOPIC_NAME_SENDING_ACTION);
        subscriber.Start(CONSUMER_ID);
        subscriber.OnMessageReceived += Subscriber_OnMessageReceived;
    }

    private static void Subscriber_OnMessageReceived(string message)
    {
        processMessage(message);
    }

    private static void processMessage(string message)
    {
        string xmlMessage = message.Trim();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlMessage);
        XmlNodeList xnList = xmlDoc.SelectNodes("/action");

              
    }
}