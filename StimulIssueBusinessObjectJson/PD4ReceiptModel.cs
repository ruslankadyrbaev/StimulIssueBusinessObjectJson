namespace StimulIssueBusinessObjectJson
{
    public class PD4ReceiptModel
    {
        public Payment Payment { get; set; }

        public Payer Payer { get; set; }

        public string Name { get; set; }
        
        public string inn { get; set; }
        
        public string bankAccount { get; set; }
        
        public string bankName { get; set; }
        
        public string bic { get; set; }
        
        public string bankCorrAccount { get; set; }
    }
}