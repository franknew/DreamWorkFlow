using DreamWorkflow.Engine;
using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Form;
using DreamWorkflow.Engine.Model;
using Host;
using IBatisNet.DataMapper;
using SOAFramework.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WorkflowDao dao = new WorkflowDao();
            var wf = new WorkflowQueryForm();
            wf.ID = "2";
            var list = dao.Query(wf);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var list = TableCacheHelper.GetDataFromCache<Workflow>(typeof(WorkflowDao));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //string baseAddress = "http://localhost:8088/Service";
            //ServiceHost host = new ServiceHost(typeof(Service1), new Uri(baseAddress));
            //WebHttpBinding webBinding = new WebHttpBinding();
            //webBinding.ContentTypeMapper = new MyRawMapper();
            //ChannelFactory<IService1> newFactory = new ChannelFactory<IService1>(webBinding, new EndpointAddress(baseAddress + "/json"));
            //host.AddServiceEndpoint(typeof(IService1), webBinding, "json").Behaviors.Add(new NewtonsoftJsonBehavior());
            //host.Open();

            //var pet = HttpHelper.Post(baseAddress + "/json/EchoPet", Encoding.UTF8.GetBytes("{\"Name\":\"Fido\",\"Color\":\"Black and white\",\"Markings\":\"None\",\"Id\":1}"));
            //newFactory.Endpoint.Behaviors.Add(new NewtonsoftJsonBehavior());
            //IService1 newProxy = newFactory.CreateChannel();
            //Console.WriteLine(newProxy.EchoPet(new Pet { Color = "gold", Id = 2, Markings = "Collie", Name = "Lassie" }));
            //host.Close();

            //ServiceHost host = new ServiceHost(typeof(NewHostService));
            //host.Open();
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    ServiceHost host = new ServiceHost(typeof(NewHostService));
                    host.Open();
                }
                catch
                {
                    throw;
                }
            });

            Console.ReadLine();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var result = HttpHelper.Post("http://localhost:8088/Execute/Test/Test1", Encoding.UTF8.GetBytes("{\"UserName\":\"Frank\"}"));
            MessageBox.Show(result);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var workflow = WorkflowDefinitionModel.Load("1");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var workflow = WorkflowDefinitionModel.Load("1");
            workflow.StartNew("frank");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var workflow = WorkflowModel.Load("66c728aa03bb4f8aa6f26b43c705e70f");
        }
    }
}
