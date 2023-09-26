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
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
using FTOptix.Core;
using FTOptix.CommunicationDriver;
using FTOptix.Modbus;
using FTOptix.RAEtherNetIP;
#endregion

public class RuntimeNetLogic1 : BaseNetLogic
{
    private IUAVariable motorSpeed;
    private RemoteVariableSynchronizer variableSynchronizer;
    public override void Start()
    {
       
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void Method1()
    {
       DataGrid taggrid = Owner.Owner.Get<IUANode>("UI").Get<IUANode>("Pages").Get<IUANode>("Page5").Get<DataGrid>("TagList");
       var model = taggrid.GetVariable("Model");
       //IUANode CtrlTags=Owner.Owner.Get<IUANode>("CommDrivers").Get<IUANode>("RAEtherNet_IPDriver1").Get<IUANode>("RAEtherNet_IPStation1").Get<IUANode>("Tags").Get<IUANode>("Controller Tags");
       
       if(model.GetVariable("DynamicLink")==null)
       {
            var dynamicLink = InformationModel.MakeVariable<DynamicLink>("DynamicLink", FTOptix.Core.DataTypes.NodePath);
            dynamicLink.Value = "/Objects/myQuickStart/CommDrivers/RAEtherNet_IPDriver1/RAEtherNet_IPStation1/Tags/Controller Tags";
            dynamicLink.Mode = DynamicLinkMode.ReadWrite;
            model.Refs.AddReference(FTOptix.CoreBase.ReferenceTypes.HasDynamicLink, dynamicLink);
       }
    //    taggrid.Refresh();
    //    IUANode page5=Owner.Owner.Get<IUANode>("UI").Get<IUANode>("Pages").Get<IUANode>("Page5");
       
       
       //IUAVariable CtrlTagVar=(IUAVariable)CtrlTags;
       //model.SetDynamicLink(CtrlTags);
       //taggrid.Refresh();
       //IUAVariable taglistvar=taglist.GetVariable("")
        // Insert code to be executed by the method
       ChildNodeCollection childs = Owner.Children;
       foreach (var child in childs)
       {
            if (child.GetType() == typeof(FTOptix.RAEtherNetIP.Driver)) 
            {
                foreach (var ch in child.Children) 
                {
                    if(ch.GetType() == typeof(FTOptix.RAEtherNetIP.Station))
                    {

                    }
                }
            }
       }
    }
 
    [ExportMethod]
    public void Method2()
    {
        // Insert code to be executed by the method
        DataGrid tagvaluegrid = Owner.Owner.Get<IUANode>("UI").Get<IUANode>("Pages").Get<IUANode>("Page5").Get<DataGrid>("TagValueGrid");
        var model = tagvaluegrid.GetVariable("Model");
       //IUANode CtrlTags=Owner.Owner.Get<IUANode>("CommDrivers").Get<IUANode>("RAEtherNet_IPDriver1").Get<IUANode>("RAEtherNet_IPStation1").Get<IUANode>("Tags").Get<IUANode>("Controller Tags");
       
        if(model.GetVariable("DynamicLink")==null)
        {
                var dynamicLink = InformationModel.MakeVariable<DynamicLink>("DynamicLink", FTOptix.Core.DataTypes.NodePath);
                dynamicLink.Value = "/Objects/myQuickStart/CommDrivers/RAEtherNet_IPDriver1/RAEtherNet_IPStation1/Tags/Controller Tags";
                dynamicLink.Mode = DynamicLinkMode.ReadWrite;
                model.Refs.AddReference(FTOptix.CoreBase.ReferenceTypes.HasDynamicLink, dynamicLink);
        }
        else
        {
            IUANode node = model.Get<IUANode>("DynamicLink");
            model.Refs.RemoveReference(FTOptix.CoreBase.ReferenceTypes.HasDynamicLink, node.NodeId);
        }
    }
}
