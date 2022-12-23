using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using Bastion_Exam_Retying.Models;
using WebAcqSample;

namespace Bastion_Exam_Retying.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Validate(TransactionDetails transactionDetails)
        {
            CPaymentAdapter paymentAdapter = new CPaymentAdapter();
            string cardNum = paymentAdapter.PARAM_CARD + "=" + transactionDetails.cardNum;
            string cardName = paymentAdapter.PARAM_CHNAME + "=" + transactionDetails.cardName;
            string cardCvv = paymentAdapter.PARAM_CVV + "=" + transactionDetails.cardCvv;

            //string getCardExpiry = transactionDetails.cardExpiry;
            //string[] cardExpiryArray = getCardExpiry.Split('-');
            //string cardExpiry = paymentAdapter.PARAM_EXPIRY_MMYYYY + "=" + cardExpiryArray[1] + cardExpiryArray[0
            string cardExpiry = paymentAdapter.PARAM_EXPIRY_MMYYYY + "=" + transactionDetails.cardExpiry;
            string productAmount = paymentAdapter.PARAM_AMOUNT + "=" + transactionDetails.productAmount;
            string[] cardDetails = { cardNum, cardExpiry, cardName, cardCvv, productAmount };
            //List<TransactionDetails> cardDetails = new List<TransactionDetails>();
            //cardDetails.Add(transactionDetails);

            CPaymentResult paymentResult = paymentAdapter.ProcessCardPayment(cardDetails.ToArray());
            string[] transaction = { paymentResult._transactionid, paymentResult._trandatetime, transactionDetails.cardName, transactionDetails.productAmount };

            TempData["transactionDetails"] = transaction;
            if (paymentResult._returncode == "00")
            {

                return RedirectToAction("TransactionSuccess");
            }
            else if (paymentResult._returncode == "89")
            {

                return RedirectToAction("TransactionFailed");
            }
            else if (paymentResult._returncode == "99")
            {
                return RedirectToAction("TransactionTimeout");
            }

            return View();
        }

        public ActionResult TransactionSuccess(string[] transaction)
        {
            string[] transactionDetails = (string[])TempData["transactionDetails"];
            ViewData["TransactionId"] = transactionDetails[0];
            //DateTime transactionDate = DateTime.Parse(transactionDetails[1]);
            //ViewData["TransactionDate"] = transactionDate;
            string dateTime = transactionDetails[1];
            string date;
            string time;
            date = dateTime.Substring(0, 2) + "/" + dateTime.Substring(2, 2) + "/" + dateTime.Substring(4, 4);
            time = dateTime.Substring(8, 2) + ":" + dateTime.Substring(10, 2) + ":" + dateTime.Substring(12, 2);
            ViewData["TransactionDate"] = date;
            ViewData["TransactionTime"] = time;
            ViewData["TransactionName"] = transactionDetails[2];
            ViewData["TransactionPrice"] = transactionDetails[3];
            //ViewData["TransactionTime"] = time;
            return View();
        }

        public ActionResult TransactionFailed()
        {
            return View();
        }

        public ActionResult TransactionTimeout()
        {
            return View();
        }
    }


}