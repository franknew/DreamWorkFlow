using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DreamWorkflow.Engine.Model;
using System.Collections.Generic;

namespace DreamWorkflow.Engine.UnitTest
{
    [TestClass]
    public class WorkflowDefinitionModelTesting
    {
        private WorkflowDefinitionModel model = null;

        [TestInitialize]
        public void Init()
        {
            CleanUp();
            model = new WorkflowDefinitionModel
            {
                Value = new WorkflowDefinition
                {
                    ID = "unittest1",
                    Name = "unit test 1",
                    Enabled = 1,
                    Creator = "frank",
                },

            };
            ActivityDefinitionModel activity1 = new ActivityDefinitionModel
            {
                Value = new ActivityDefinition
                {
                    ID = "unittestactivity1",
                    Name = "activity1",
                    Page = "page1",
                    Title = "title1",
                    Creator = "frank",
                    WorkflowDefinitionID = "unittest1",
                    Type = 1,
                    Enabled = 1,
                }
            };
            ActivityDefinitionModel activity2 = new ActivityDefinitionModel
            {
                Value = new ActivityDefinition
                {
                    ID = "unittestactivity2",
                    Name = "activity2",
                    Page = "page2",
                    Title = "title2",
                    Creator = "frank",
                    WorkflowDefinitionID = "unittest1",
                    Enabled = 1,
                    Type = 1,
                }
            };

            ActivityDefinitionModel activity3 = new ActivityDefinitionModel
            {
                Value = new ActivityDefinition
                {
                    ID = "unittestactivity3",
                    Name = "activity3",
                    Page = "page3",
                    Title = "title3",
                    Creator = "frank",
                    WorkflowDefinitionID = "unittest1",
                    Type = 1,
                    Enabled = 1,
                }
            };
            LinkDefinitionModel link1 = new LinkDefinitionModel
            {
                Value = new LinkDefinition
                {
                    ID = "unittestlink1",
                    Name = "link1",
                    FromActivityDefinitionID = "unittestactivity1",
                    ToActivityDefinitionID = "unittestactivity2",
                    WorkflowDefinitionID = "unittest1",
                },
                FromActivityDefinition = activity1,
                ToActivityDefinition = activity2,
            };
            LinkDefinitionModel link2 = new LinkDefinitionModel
            {
                Value = new LinkDefinition
                {
                    ID = "unittestlink2",
                    Name = "link2",
                    FromActivityDefinitionID = "unittestactivity2",
                    ToActivityDefinitionID = "unittestactivity3",
                    WorkflowDefinitionID = "unittest1",
                },
                FromActivityDefinition = activity2,
                ToActivityDefinition = activity3,
            };
            model.Root = activity1;
            model.Save();
        }

        [TestCleanup]
        public void CleanUp()
        {
            model = WorkflowDefinitionModel.Load("unittest1");
            if (model != null)
            {
                model.Remove();
            }
        }

        [TestMethod]
        public void WorkflowDefinitionModelSaveTest()
        {
            model = WorkflowDefinitionModel.Load("unittest1");
            model.Value.Remark = "changed";
            model.Root.Value.Remark = "changed";
            model.Save();
            model = WorkflowDefinitionModel.Load("unittest1");

            Assert.AreEqual("changed", model.Value.Remark);
            Assert.AreEqual("changed", model.Root.Value.Remark);
        }

        [TestMethod]
        public void WorkflowDefinitionStartNewTest()
        {
            model = WorkflowDefinitionModel.Load("unittest1");

            WorkflowModel workflow = model.StartNew("frank", "1", new GetUser());

            Assert.AreEqual(3, workflow.Root.Value.Status);
            Assert.AreEqual(2, workflow.Root.Children[0].Value.Status);
            Assert.AreEqual("title3", workflow.Root.Children[0].Children[0].Value.Title);
            workflow.Remove();
        }
    }

    public class GetUser: IWorkflowAuthority
    {

        public List<string> GetUserIDList(System.Collections.Generic.List<ActivityAuth> auth)
        {
            return new List<string> { "1", "2" };
        }
    }
}
