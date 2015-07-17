using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DreamWorkflow.Engine;
using DreamWorkflow.Engine.Model;
using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Form;
using SOAFramework.Library.Cache;

namespace DreamWorkflow.Engine.UnitTest
{
    /// <summary>
    /// TableCacheHelperTesting 的摘要说明
    /// </summary>
    [TestClass]
    public class TableCacheHelperTesting
    {
        private ICache cache = CacheFactory.Create(CacheType.DefaultMemoryCache);

        public TableCacheHelperTesting()
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

        [TestInitialize]
        public void Init()
        {
            WorkflowDao dao = new WorkflowDao();
            dao.Add(new Workflow
            {
                ID = "1",
                WorkflowDefinitionID = "1",
                Name = "UnitTest",
                CreateTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                
            });
        }

        [TestCleanup]
        public void CleanUp()
        {
            WorkflowDao dao = new WorkflowDao();
            dao.Delete(new WorkflowQueryForm { ID = "1" });
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

        [TestMethod]
        public void GetDataFromCacheTest()
        {
            var list = TableCacheHelper.GetDataFromCache<Workflow>(typeof(WorkflowDao));
            if (list.Count == 0)
            {
                return;
            }
            WorkflowDao dao = new WorkflowDao();
            string key = typeof(Workflow).FullName;
            var item = cache.GetItem(key);
            CacheEntity<Workflow> cacheentity = item.Value as CacheEntity<Workflow>;
            Assert.IsNotNull(item);
            Assert.AreEqual(list, cacheentity.List);
            DateTime dt = DateTime.Now;
            dao.Update(new WorkflowUpdateForm
            {
                Entity = new Workflow { LastUpdateTime = dt },
                WorkflowQueryForm = new WorkflowQueryForm { ID = list[0].ID }
            }); 
            list = TableCacheHelper.GetDataFromCache<Workflow>(typeof(WorkflowDao));
            item = cache.GetItem(key);
            cacheentity = item.Value as CacheEntity<Workflow>;
            Assert.AreSame(dt, cacheentity.LastUpdateTime);
        }
    }
}
