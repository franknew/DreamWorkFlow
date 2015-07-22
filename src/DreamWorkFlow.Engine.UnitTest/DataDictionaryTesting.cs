using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DreamWorkflow.Engine.Model;
using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Form;
using IBatisNet.DataMapper.Scope;

namespace DreamWorkflow.Engine.UnitTest
{
    /// <summary>
    /// DataDictionaryTesting 的摘要说明
    /// </summary>
    [TestClass]
    public class DataDictionaryTesting
    {
        private DataDictionaryGroupAndItemDao dgidao = new DataDictionaryGroupAndItemDao();

        public DataDictionaryTesting()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        [TestInitialize]
        public void Init()
        {
            Cleanup();
            DataDictionaryGroupDao groupdao = new DataDictionaryGroupDao();
            DataDictionaryDao datadao = new DataDictionaryDao();
            groupdao.Add(new DataDictionaryGroup
            {
                ID = "1",
                Name = "unittest1",
                LastUpdateTime = DateTime.Now,
            });
            groupdao.Add(new DataDictionaryGroup
            {
                ID = "2",
                Name = "unittest2",
                LastUpdateTime = DateTime.Now,
            });

            datadao.Add(new DataDictionary
            {
                ID = "1",
                DataDictionaryGroupID = "1",
                Name = "unittest1",
                Value = 1,
            });

            datadao.Add(new DataDictionary
            {
                ID = "2",
                DataDictionaryGroupID = "1",
                Name = "unittest2",
                Value = 2,
            });
            datadao.Add(new DataDictionary
            {
                ID = "3",
                DataDictionaryGroupID = "2",
                Name = "unittest3",
                Value = 1,
            });
        }

        [TestCleanup]
        public void Cleanup()
        {
            DataDictionaryGroupDao groupdao = new DataDictionaryGroupDao();
            DataDictionaryDao datadao = new DataDictionaryDao();
            groupdao.Delete(new DataDictionaryGroupQueryForm { ID = "1" });
            datadao.Delete(new DataDictionaryQueryForm { DataDictionaryGroupID = "1" });
            groupdao.Delete(new DataDictionaryGroupQueryForm { ID = "2" });
            datadao.Delete(new DataDictionaryQueryForm { DataDictionaryGroupID = "2" });
        }

        [TestMethod]
        public void QueryByGroupNameTesting()
        {

            var statement = dgidao.Mapper.GetMappedStatement("QueryDataDictionaryGroupAndItemByGroupName");
            if (!dgidao.Mapper.IsSessionStarted)
            {
                dgidao.Mapper.OpenConnection();
            }
            RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, new List<string> { "unittest1" }, dgidao.Mapper.LocalSession);
            string result = scope.PreparedStatement.PreparedSql;
            var list = dgidao.QueryByGroupName(new List<string> { "unittest1", "unittest2" });
            Assert.IsTrue(list.Count == 3);
            Assert.AreEqual("2", list.Find(t => t.DataDictionaryName == "unittest2").DataDictionaryID);

            list = dgidao.QueryByGroupName(new List<string> { "unittest1" });
            Assert.IsTrue(list.Count == 2);
        }
    }
}
