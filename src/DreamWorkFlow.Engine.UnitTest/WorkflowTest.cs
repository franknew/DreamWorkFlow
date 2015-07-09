using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Model;
using DreamWorkflow.Engine.Form;

namespace DreamWorkflow.Engine.UnitTest
{
    [TestClass]
    public class EngineTest
    {
        private WorkflowDao dao = new WorkflowDao();

        [TestMethod]
        public void TestAdd()
        {
            Workflow wf = new Workflow
            {
                Creator = "test add",
                Name = "testing add",
                Status = 0,
                CreateTime = DateTime.Now,
                WorkflowDefinitionID = "1",
            };
            dao.Add(wf);
            WorkflowQueryForm form = new WorkflowQueryForm
            {
                ID = wf.ID,
            };
            var list = dao.Query(form);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("testing add", list[0].Name);
            Assert.AreEqual("1", list[0].WorkflowDefinitionID);
            Assert.AreEqual("test add", list[0].Creator);
            Assert.AreEqual(0, list[0].Status);
            dao.Delete(form);
        }

        [TestMethod]
        public void TestQuery()
        {
            WorkflowQueryForm form = new WorkflowQueryForm
            {
                ID = "11",
            };
            var list = dao.Query(form);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("testing", list[0].Name);
            Assert.AreEqual("1", list[0].WorkflowDefinitionID);
            Assert.AreEqual("test", list[0].Creator);
        }

        [TestMethod]
        public void TestUpdate()
        {
            WorkflowQueryForm form = new WorkflowQueryForm
            {
                ID = "11",
            };
            var list = dao.Query(form);
            WorkflowUpdateForm updateform = new WorkflowUpdateForm
            {
                Workflow = new Workflow
                {
                    Status = 1
                },
                WorkflowQueryForm = new WorkflowQueryForm
                {
                    ID = list[0].ID,
                },
            };
             
            dao.Update(updateform);
            list = dao.Query(form);
            Assert.AreEqual(1, list[0].Status);
        }

        [TestMethod]
        public void TestDelete()
        {
            Workflow wf = new Workflow
            {
                Creator = "test for delete",
                Name = "testing for delete",
                Status = 0,
                CreateTime = DateTime.Now,
                WorkflowDefinitionID = "1",
            };
            dao.Add(wf);
            WorkflowQueryForm form = new WorkflowQueryForm
            {
                ID = wf.ID,
            };
            var list = dao.Query(form);
            Assert.AreEqual(1, list.Count);
            dao.Delete(form);
            list = dao.Query(form);
            Assert.AreEqual(0, list.Count);
        }

        [TestInitialize]
        public void Init()
        {
            Workflow wf = new Workflow
            {
                ID = "11",
                Creator = "test",
                Name = "testing",
                Status = 0,
                CreateTime = DateTime.Now,
                WorkflowDefinitionID = "1",
            };
            try
            {
                dao.Add(wf);
            }
            catch (Exception ex)
            {

            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            WorkflowQueryForm form = new WorkflowQueryForm
            {
                ID = "11",
            };
            dao.Delete(form);
        }
    }
}
