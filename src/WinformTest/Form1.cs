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
            wf.ID = "1c74c3e0ce2d4c5f983fab3dc6063223";
            var list = dao.Query(wf);

            WorkflowModel model = WorkflowModel.Load("1c74c3e0ce2d4c5f983fab3dc6063223");
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
            string json = "{\"form\":{\"Buyers\":[{\"ID\":\"395f7ce8de8340eda2dfd22098c81290\",\"Name\":\"爱的色放\",\"CardType\":\"1\",\"IdentityCode\":\"4444444444\",\"Phone\":\"123123123123\",\"Gender\":\"1\",\"Marrage\":\"1\",\"Address\":\"啊都是法师打发而且额外人\",\"OrignalName\":\"\",\"OrignalIdentityCode\":\"\",\"BankCode\":\"\",\"BankType\":\"1\",\"WorkUnit\":\"\",\"Quotient\":\"222\"},{\"ID\":\"\",\"Name\":\"阿萨法 \",\"CardType\":\"1\",\"IdentityCode\":\"986799283948723984\",\"Phone\":\"123123\",\"Gender\":\"2\",\"Marrage\":\"1\",\"Address\":\"三个地方集团研究研究\",\"OrignalName\":\"\",\"OrignalIdentityCode\":\"\",\"BankCode\":\"\",\"BankType\":\"\",\"WorkUnit\":\"\",\"Quotient\":\"333\"},{\"ID\":\"712feaff6c034244ab3f066268b9fe5a\",\"Name\":\"阿斯顿飞\",\"CardType\":\"1\",\"IdentityCode\":\"12312312312323\",\"Phone\":\"123123123\",\"Gender\":\"1\",\"Marrage\":\"1\",\"Address\":\"嘎达嗦嘎多个地方十多个地方各个\",\"OrignalName\":\"\",\"OrignalIdentityCode\":\"\",\"BankCode\":\"\",\"BankType\":\"1\",\"WorkUnit\":\"\",\"Quotient\":\"222\"}],\"Sellers\":[{\"ID\":\"55b71c225dc841a7b99ead4cecc601c5\",\"Name\":\"aeeboo\",\"CardType\":\"1\",\"IdentityCode\":\"234234235235\",\"Phone\":\"324234234234\",\"Gender\":\"1\",\"Marrage\":\"2\",\"Address\":\"的方式购房合同和投入和\",\"OrignalName\":\"\",\"OrignalIdentityCode\":\"\",\"BankCode\":\"\",\"BankType\":\"2\",\"WorkUnit\":\"\",\"Quotient\":\"111\"},{\"ID\":\"\",\"Name\":\"阿萨德飞44\",\"CardType\":\"1\",\"IdentityCode\":\"237856234\",\"Phone\":\"34234234\",\"Gender\":\"1\",\"Marrage\":\"1\",\"Address\":\"然后统一集团研究与\",\"OrignalName\":\"\",\"OrignalIdentityCode\":\"\",\"BankCode\":\"\",\"BankType\":\"\",\"WorkUnit\":\"\",\"Quotient\":\"123\"}],\"Assets\":[{\"ID\":\"\",\"Code\":\"44444444\",\"Usage\":\"1\",\"Position\":\"2\",\"Address\":\"景田西路八个道路\",\"Area\":\"123\",\"RegPrice\":\"44232\"},{\"ID\":\"\",\"Code\":\"1412412132\",\"Usage\":\"1\",\"Position\":\"1\",\"Address\":\"水电费个人个人高\",\"Area\":\"234324\",\"RegPrice\":\"123123\"}],\"Project\":{\"Source\":\"1\",\"AgentName\":\"213213\",\"CertificateData\":\"2015-08-05\",\"AgentContact\":\"\",\"Rebater\":\"\",\"RebateAccount\":\"\",\"OtherRebateInfo\":\"\",\"OrignalMortgageBank\":\"1\",\"OrignalMortgageBranch\":\"阿斯顿发顺丰\",\"OrignalFundCenter\":\"1\",\"OrignalFundBranch\":\"\",\"SupplyCardCopy\":\"\",\"OrignalCreditPI\":\"123123\",\"OrignalCreditCommerceMoney\":\"123\",\"OrignalCreditFundMoney\":\"123\",\"AssetRansomCustomerManager\":\"124142\",\"AssetRansomContactPhone\":\"24124\",\"NewCreditBank\":\"1\",\"NewCreditBranch\":\"2r323\",\"ShortTermAssetRansomBank\":\"1\",\"ShortTermAssetRansomBranch\":\"\",\"GuaranteeMoney\":\"123\",\"GuaranteeMonth\":\"1231\",\"BuyerCreditCommerceMoney\":\"213\",\"BuyerCreditFundMoney\":\"2\",\"LoanMoney\":\"123123\",\"DealMoney\":\"123123\",\"EarnestMoney\":\"123123\",\"SupervisionMoney\":\"123123\",\"SupervisionBank\":\"12123\",\"AssetRansomMoney\":\"122323\",\"CustomerPredepositMoney\":\"323232\",\"CreditReceiverName\":\"23123\",\"CreditReceiverBank\":\"2323\",\"CreditReceiverAccount\":\"2323\",\"TrusteeshipAccount\":\"\",\"AssetRansomPredictMoney\":\"2323\",\"AssetRansomer\":\"232323\",\"AssetRansomType\":\"1\",\"PredictDays\":\"2323\",\"ChargeType\":\"1\",\"CheckNumbersAndLimit\":\"123123\",\"Stagnationer\":\"\"}},\"token\":\"0cbbd08b6b694428a30afe52098e5f7a\"}";
            //var result = HttpHelper.Post("http://localhost:8088/Execute/RiskMgr.Api.LogonApi/Logon", Encoding.UTF8.GetBytes("{\"form\":{\"UserName\":\"admin\",\"Password\":\"admin\"}}"));
            var result = HttpHelper.Post("http://localhost/Service/Execute/RiskMgr.Api.ProjectApi/Add", Encoding.UTF8.GetBytes(json));
            //var result = HttpHelper.Post("http://localhost/Service/Execute/RiskMgr.Api.LogonApi/Logon", Encoding.UTF8.GetBytes("{\"form\":{\"UserName\":\"admin\",\"Password\":\"admin\"}}"));
            MessageBox.Show(result);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var workflow = WorkflowDefinitionModel.Load("1");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var workflow = WorkflowDefinitionModel.Load("1");
            workflow.StartNew("frank", "1", new GetUser());
            RiskMgr.Model.Menu m = new RiskMgr.Model.Menu();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var workflow = WorkflowModel.Load("66c728aa03bb4f8aa6f26b43c705e70f");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RiskMgr.Model.Menu m = new RiskMgr.Model.Menu();
            string workflowid = txbworkflowid.Text;
            string activityid = txbactivityid.Text;
            string taskid = txbtaskid.Text;
            var workflow = WorkflowModel.Load(workflowid);
            workflow.ProcessActivity(new Approval
            {
                ActivityID = activityid,
                WorkflowID = workflow.Value.ID,
                //Type = (int)ApprovalStatus.Agree,
                Status = (int)ApprovalStatus.Agree,
                Remark = "同意",
            }, "1", new GetUser()
            );

        }

        private void button10_Click(object sender, EventArgs e)
        {
            RiskMgr.Model.Menu m = new RiskMgr.Model.Menu();
            string workflowid = txbworkflowid.Text;
            string activityid = txbactivityid.Text;
            string taskid = txbtaskid.Text;
            var workflow = WorkflowModel.Load(workflowid);
            workflow.ProcessActivity(new Approval
            {
                ActivityID = activityid,
                WorkflowID = workflow.Value.ID,
                //Type = (int)ApprovalStatus.Agree,
                Status = (int)ApprovalStatus.Disagree,
                Remark = "不同意",
            }, "1", new GetUser()
            );
        }
    }
    public class GetUser : IWorkflowAuthority
    {
        public List<string> GetUserIDList(System.Collections.Generic.List<ActivityAuth> auth)
        {
            return new List<string> { "1", "2" };
        }
    }
}
