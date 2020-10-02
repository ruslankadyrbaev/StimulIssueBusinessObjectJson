using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Stimulsoft.Report;
using Stimulsoft.Report.Engine;

namespace StimulIssueBusinessObjectJson
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            using var report = new StiReport();
            report.Load(await File.ReadAllBytesAsync(@"ReceiptPD4.mrt"));
            report.CacheAllData = false;
            
            var businessObject =  new PD4ReceiptModel
            {
                Payment = new Payment
                {
                    Name = "XXXXXXXXXXXXXXXXXXXXXXXXX123",
                    rub = "123",
                    penny = "123",
                    rubCom = "123",
                    pennyCom = "123",
                    rubTotal = "123",
                    pennyTotal = "123",
                    day = "1",
                    month = "1",
                    year = "2020"
                },
                Payer = new Payer
                {
                    firstName = "XXXXXXXXXXXXXXXXXXXXXXXXXPayerName1",
                    lastName = "PayerName2",
                    middleName = "qwe",
                    address = "qwe"
                },
                Name = "XXXXXXXXXXXXXXXXXXXXXXXXXqwe",
                inn = "123123",
                bankAccount = "123123",
                bankName = "qwe",
                bic = "123123",
                bankCorrAccount = "123123"
            };
            var businessObjects = new[] {businessObject,};
            var businessObjectsJson = JsonConvert.SerializeObject(businessObjects, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            
            var businessObjectsJObject = Stimulsoft.Base.Json.Linq.JToken.Parse(businessObjectsJson);
            report.RegBusinessObject(category: "Receipt", name: "Reciver", alias: "Reciver", value: businessObjectsJObject);
            
            await report.Dictionary.SynchronizeAsync();
            //report.Dictionary.SynchronizeBusinessObjects();
            await report.RenderAsync(new StiRenderState(false));
            await report.ExportDocumentAsync(StiExportFormat.Pdf, $@"c:\temp\{Guid.NewGuid().ToString()}.pdf");
        }
    }
}