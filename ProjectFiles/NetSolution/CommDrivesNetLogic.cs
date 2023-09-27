#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.EventLogger;
using FTOptix.Recipe;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.Alarm;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.CommunicationDriver;
using FTOptix.NetLogic;
using FTOptix.Core;
using FTOptix.WebUI;
using System.Threading;
using System.Collections.Generic;
using FTOptix.CODESYS;
using System.IO;
using System.Globalization;
using System.Threading.Tasks;
using FTOptix.DataLogger;
#endregion

public class CommDrivesNetLogic : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        variableSynchronizer = new RemoteVariableSynchronizer();
        childs = Owner.Children;
        foreach (var child in childs)
        {
            if (child.GetType() == typeof(FTOptix.RAEtherNetIP.Driver)) {
                 foreach (var ch in child.Children) {
                    if  (ch.GetType() == typeof(FTOptix.RAEtherNetIP.Station)) {
                        tags = ch.GetObject("Tags");
                        foreach(var tag in tags.Children)
                        {
                            foreach(var item in tag.Children) 
                            {
                                test1 = tag.GetVariable(item.BrowseName);
                                test1.VariableChange += Tag_VariableChange;
                                variableSynchronizer.Add(test1);
                            }
                        }
                    }
                }
           }
       }
    }
    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        test1.VariableChange -= Tag_VariableChange;
    }

    private void Tag_VariableChange(object sender, VariableChangeEventArgs e) 
    {
        if(e.Variable.BrowseName == "dinttag1"){
            Log.Info($"The value of tagName {e.Variable.BrowseName} is {e.NewValue}");
        }
    }

    private ChildNodeCollection childs;
    private IUAObject tags;
    private RemoteVariableSynchronizer variableSynchronizer;
    IUAVariable test1;

    [ExportMethod]
    public void Refresh(string url)
    {
        Thread t = new Thread(() =>
        {
            childs = Owner.Children;
            foreach (var child in childs)
            {
                if (child.GetType() == typeof(FTOptix.RAEtherNetIP.Driver)) {
                    foreach (var ch in child.Children) {
                        if  (ch.GetType() == typeof(FTOptix.RAEtherNetIP.Station)) {
                            tags = ch.GetObject("Tags");
                            foreach(var tag in tags.Children)
                            {
                                foreach(var item in tag.Children)
                                {
                                    if (item.BrowseName == "dinttag1") {
                                        var test2 = tag.GetVariable(item.BrowseName);
                                        test2.RemoteWrite(0);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        });
        t.Start();
    }
}
