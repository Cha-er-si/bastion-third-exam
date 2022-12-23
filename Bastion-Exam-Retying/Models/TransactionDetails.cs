using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bastion_Exam_Retying.Models
{
    public class TransactionDetails
    {
        public string cardNum { get; set; }
        public string cardName { get; set; }
        public string cardCvv { get; set; }

        public string cardExpiry { get; set; }

        public string productAmount { get; set; }

        //public TransactionDetails cardDetails {get;}
    }
}