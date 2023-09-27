#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.Alarm;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.Recipe;
using FTOptix.EventLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
using Grapevine;
using System.Threading;
using System.Threading.Tasks;
#endregion

public class RestfulWebServerNetLogic : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        t1 = new Thread(() =>
        {
            using (server = RestServerBuilder.UseDefaults().Build())
            { 
                server.Start();
                while(true)
                {
                    Thread.Sleep(500);
                }
            }
        });
        t1.Start();
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        server.Stop();
        
    }

    Thread t1;
    IRestServer server;
}

[RestResource]
public class MyResource
{
    [RestRoute("Get", "/api/test")]
    public async Task Test(IHttpContext context)
    {
        await context.Response.SendResponseAsync("Client connects Successfully!");
    }
}