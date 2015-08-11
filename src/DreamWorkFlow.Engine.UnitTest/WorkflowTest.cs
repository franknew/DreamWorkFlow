using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Model;
using DreamWorkflow.Engine.Form;
using IBatisNet.DataMapper.Scope;

namespace DreamWorkflow.Engine.UnitTest
{
    [TestClass]
    public class EngineTest
    {
        private WorkflowDao dao = new WorkflowDao();

        [TestMethod]
        public void TestWorkflowAdd()
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

            var statement = dao.Mapper.GetMappedStatement("QueryWorkflow");
            if (!dao.Mapper.IsSessionStarted)
            {
                dao.Mapper.OpenConnection();
            }
            RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, form, dao.Mapper.LocalSession);
            string result = scope.PreparedStatement.PreparedSql;
            var list = dao.Query(form);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("testing add", list[0].Name);
            Assert.AreEqual("1", list[0].WorkflowDefinitionID);
            Assert.AreEqual("test add", list[0].Creator);
            Assert.AreEqual(0, list[0].Status);
            dao.Delete(form);
        }

        [TestMethod]
        public void TestWorkflowQuery()
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
        public void TestWorkflowUpdate()
        {
            WorkflowQueryForm form = new WorkflowQueryForm
            {
                ID = "11",
            };
            var list = dao.Query(form);
            WorkflowUpdateForm updateform = new WorkflowUpdateForm
            {
                Entity = new Workflow
                {
                    Status = 1
                },
                WorkflowQueryForm = new WorkflowQueryForm
                {
                    ID = list[0].ID,
                },
            };

            var statement = dao.Mapper.GetMappedStatement("UpdateWorkflow");
            if (!dao.Mapper.IsSessionStarted)
            {
                dao.Mapper.OpenConnection();
            }
            RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, updateform, dao.Mapper.LocalSession);
            string result = scope.PreparedStatement.PreparedSql;
            dao.Update(updateform);
            list = dao.Query(form);
            Assert.AreEqual(1, list[0].Status);
        }

        [TestMethod]
        public void TestWorkflowDelete()
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
            #region workflow
            Cleanup();
            Workflow wf = new Workflow
            {
                ID = "11",
                Creator = "test",
                Name = "testing",
                Status = 0,
                CreateTime = DateTime.Now,
                WorkflowDefinitionID = "1",
            };
            Workflow wf2 = new Workflow
            {
                ID = "22",
                Creator = "test2",
                Name = "testing2",
                Status = 0,
                CreateTime = DateTime.Now,
                WorkflowDefinitionID = "1",
            };
            Workflow wf3 = new Workflow
            {
                ID = "33",
                Creator = "test3",
                Name = "testing3",
                Status = 0,
                CreateTime = DateTime.Now,
                WorkflowDefinitionID = "1",
            };
            dao.Add(wf);
            dao.Add(wf2);
            dao.Add(wf3);
            #endregion
        }

        [TestCleanup]
        public void Cleanup()
        {
            WorkflowQueryForm form = new WorkflowQueryForm
            {
                ID = "11",
            };
            dao.Delete(form);
            dao.Delete(new WorkflowQueryForm { ID = "22" });
            dao.Delete(new WorkflowQueryForm { ID = "33" });
        }
    }
}
