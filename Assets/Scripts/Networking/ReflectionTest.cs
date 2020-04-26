using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using StupidNetworking;

public class ReflectionTest : MonoBehaviour
{
    public List<MethodInfo> methodInfos;
    public List<string> clientMethods;
    public List<string> serverMethods;

    void Start()
    {
        methodInfos = new List<MethodInfo>();
        clientMethods = new List<string>();
        serverMethods = new List<string>();

        methodInfos.AddRange(GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance));

        Debug.Log("My methdods are : ");
        foreach (MethodInfo i in methodInfos)
        {
            RPCAttribute[] attributes = (RPCAttribute[])i.GetCustomAttributes(typeof(RPCAttribute), true);
            foreach(RPCAttribute attribute in attributes)
            {
                if (attribute is ClientRPCAttribute)
                {
                    clientMethods.Add(i.Name);
                }
                else if (attribute is ServerRPCAttribute)
                {
                    serverMethods.Add(i.Name);
                }
            }
        }

        Debug.Log(clientMethods);
        Debug.Log(serverMethods);
    }

    [ClientRPC]
    public void ClientMethod()
    {
        Debug.Log("I am the client");
    }

    [ServerRPC]
    public void ServerMethod()
    {
        Debug.Log("I am the server");
    }

    public void NoneMethod()
    {
        Debug.Log("I am nobody");
    }
}
