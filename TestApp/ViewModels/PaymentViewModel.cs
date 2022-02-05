using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Stripe;

namespace TestApp.ViewModels
{
    class PaymentViewModel//https://www.youtube.com/watch?v=_b8kNxoGW3k used this video to learn how to use stripe
    {
        API_Obj Test;
        Dictionary<string, double> exchangeRates;
        bool isPaid = false;
        int membership = 0;
        public PaymentViewModel()
        {

        }
        /// <summary>
        /// This function creates a customer account and charges the new customer
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="yearExpires"></param>
        /// <param name="monthExpires"></param>
        /// <param name="cvcNumber"></param>
        /// <param name="cardName"></param>
        /// <param name="email"></param>
        /// <param name="amountPaid"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public bool makePayment(string cardNumber,long yearExpires,long monthExpires,string cvcNumber,string cardName,string email,long amountPaid,string currency)
        {
            try
            {
                StripeConfiguration.SetApiKey("sk_test_51KLV7MADf8rgziOA4uajs5wRdQDS2vmsTlOlgOTfAqpEZ0Pgt0iGxevzp2IOQRlo89Jzc3Aia5aEhb7QzYiEWePy00VnyvgIra");
                Stripe.TokenCardOptions stripcard = new Stripe.TokenCardOptions();
                stripcard.Number = cardNumber;
                stripcard.ExpYear = yearExpires;
                stripcard.ExpMonth = monthExpires;
                stripcard.Cvc = cvcNumber;
                //Step 1 : Assign Card to Token Object and create Token
                Stripe.TokenCreateOptions token = new Stripe.TokenCreateOptions();
                token.Card = stripcard;
                Stripe.TokenService serviceToken = new Stripe.TokenService();
                Stripe.Token newToken = serviceToken.Create(token);
                // Step 2 : Assign Token to the Source
                var options = new SourceCreateOptions
                {
                    Type = SourceType.Card,
                    Currency = currency,
                    Token = newToken.Id
                };
                var service = new SourceService();
                Source source = service.Create(options);
                //Step 3 : Now generate the customer who is doing the payment
                Stripe.CustomerCreateOptions myCustomer = new Stripe.CustomerCreateOptions()
                {
                    Name = cardName,
                    Email = email,
                    Description = "Customer for " + email,
                };
                var customerService = new Stripe.CustomerService();
                Stripe.Customer stripeCustomer = customerService.Create(myCustomer);
                //Step 4 : Now Create Charge Options for the customer. 
                var chargeoptions = new Stripe.ChargeCreateOptions
                {
                    Amount = amountPaid,
                    Currency = currency,
                    ReceiptEmail = email,
                    Customer = stripeCustomer.Id,
                    Source = source.Id

                };
                //Step 5 : Perform the payment by  Charging the customer with the payment. 
                var service1 = new Stripe.ChargeService();
                Stripe.Charge charge = service1.Create(chargeoptions); // This will do the Payment
                return true;
            }
            catch (StripeException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        /// <summary>
        /// This method calls a exchange rate api so the membership prices can reflect what the user should be paying in their currency
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, double> getCurrentExchangeRate()//https://app.exchangerate-api.com/
        {
            String URLString = "https://v6.exchangerate-api.com/v6/a050309c53399e5e5f400366/latest/EUR";
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(URLString);
                Test = JsonConvert.DeserializeObject<API_Obj>(json);
            }
            changeValueToMap();
            return exchangeRates;
        }
        /// <summary>
        /// A map of the exchange rate prices tied to a key
        /// </summary>
        private void changeValueToMap()
        {
            exchangeRates = new Dictionary<string, double>();
            exchangeRates["AED"] = Test.conversion_rates.AED;
            exchangeRates["ARS"] = Test.conversion_rates.ARS;
            exchangeRates["AUD"] = Test.conversion_rates.AUD;
            exchangeRates["BGN"] = Test.conversion_rates.BGN;
            exchangeRates["BRL"] = Test.conversion_rates.BRL;
            exchangeRates["BSD"] = Test.conversion_rates.BSD;
            exchangeRates["CAD"] = Test.conversion_rates.CAD;
            exchangeRates["CHF"] = Test.conversion_rates.CHF;
            exchangeRates["CLP"] = Test.conversion_rates.CLP;
            exchangeRates["CNY"] = Test.conversion_rates.CNY;
            exchangeRates["COP"] = Test.conversion_rates.COP;
            exchangeRates["CZK"] = Test.conversion_rates.CZK;
            exchangeRates["DKK"] = Test.conversion_rates.DKK;
            exchangeRates["DOP"] = Test.conversion_rates.DOP;
            exchangeRates["EGP"] = Test.conversion_rates.EGP;
            exchangeRates["EUR"] = Test.conversion_rates.EUR;
            exchangeRates["FJD"] = Test.conversion_rates.FJD;
            exchangeRates["GBP"] = Test.conversion_rates.GBP;
            exchangeRates["GTQ"] = Test.conversion_rates.GTQ;
            exchangeRates["HKD"] = Test.conversion_rates.HKD;
            exchangeRates["HRK"] = Test.conversion_rates.HRK;
            exchangeRates["HUF"] = Test.conversion_rates.HUF;
            exchangeRates["IDR"] = Test.conversion_rates.IDR;
            exchangeRates["ILS"] = Test.conversion_rates.ILS;
            exchangeRates["INR"] = Test.conversion_rates.INR;
            exchangeRates["ISK"] = Test.conversion_rates.ISK;
            exchangeRates["JPY"] = Test.conversion_rates.JPY;
            exchangeRates["KRW"] = Test.conversion_rates.KRW;
            exchangeRates["KZT"] = Test.conversion_rates.KZT;
            exchangeRates["MXN"] = Test.conversion_rates.MXN;
            exchangeRates["MYR"] = Test.conversion_rates.MYR;
            exchangeRates["NOK"] = Test.conversion_rates.NOK;
            exchangeRates["NZD"] = Test.conversion_rates.NZD;
            exchangeRates["PAB"] = Test.conversion_rates.PAB;
            exchangeRates["PEN"] = Test.conversion_rates.PEN;
            exchangeRates["PHP"] = Test.conversion_rates.PHP;
            exchangeRates["PKR"] = Test.conversion_rates.PKR;
            exchangeRates["PLN"] = Test.conversion_rates.PLN;
            exchangeRates["PYG"] = Test.conversion_rates.PYG;
            exchangeRates["RON"] = Test.conversion_rates.RON;
            exchangeRates["RUB"] = Test.conversion_rates.RUB;
            exchangeRates["SAR"] = Test.conversion_rates.SAR;
            exchangeRates["SEK"] = Test.conversion_rates.SEK;
            exchangeRates["SGB"] = Test.conversion_rates.SGD;
            exchangeRates["THB"] = Test.conversion_rates.THB;
            exchangeRates["TRY"] = Test.conversion_rates.TRY;
            exchangeRates["TWD"] = Test.conversion_rates.TWD;
            exchangeRates["UAH"] = Test.conversion_rates.UAH;
            exchangeRates["USD"] = Test.conversion_rates.USD;
            exchangeRates["UYU"] = Test.conversion_rates.UYU;
            exchangeRates["ZAR"] = Test.conversion_rates.ZAR;
        }
        /// <summary>
        /// Used to check if payment was successful
        /// </summary>
        public bool PaymentSuccessful
        {
            get { return isPaid; }
            set { isPaid = value; }
        }
        /// <summary>
        /// Used to get the selected membership so I can enter how long the user membership is for
        /// </summary>
        public int WhichMembershipSelectedSuccessful
        {
            get { return membership; }
            set { membership = value; }
        }
    }
}
