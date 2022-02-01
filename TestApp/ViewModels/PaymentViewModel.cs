using System;
using Newtonsoft.Json;
using Stripe;

namespace TestApp.ViewModels
{
    class PaymentViewModel//https://www.youtube.com/watch?v=_b8kNxoGW3k used this video to learn how to use stripe
    {
        public PaymentViewModel()
        {

        }
        //string mycustomer;
        //string getchargedID;
        //string refundID;

        public void makePayment(string cardNumber,long yearExpires,long monthExpires,string cvcNumber,string physioName,string physioEmail,long amountPaid)
        {
            StripeConfiguration.SetApiKey("sk_test_51KLV7MADf8rgziOA4uajs5wRdQDS2vmsTlOlgOTfAqpEZ0Pgt0iGxevzp2IOQRlo89Jzc3Aia5aEhb7QzYiEWePy00VnyvgIra");

            
            Stripe.TokenCardOptions stripcard = new Stripe.TokenCardOptions();
            stripcard.Number = "4000000000003055";//change to var
            stripcard.ExpYear = 2023;//change to var
            stripcard.ExpMonth = 08;//change to var
            stripcard.Cvc = "199";//change to var


            //Step 1 : Assign Card to Token Object and create Token
            Stripe.TokenCreateOptions token = new Stripe.TokenCreateOptions();
            token.Card = stripcard;
            Stripe.TokenService serviceToken = new Stripe.TokenService();
            Stripe.Token newToken = serviceToken.Create(token);

            // Step 2 : Assign Token to the Source
            var options = new SourceCreateOptions
            {
                Type = SourceType.Card,
                Currency = "EUR",//change to var
                Token = newToken.Id
            };

            var service = new SourceService();
            Source source = service.Create(options);

            //Step 3 : Now generate the customer who is doing the payment
            Stripe.CustomerCreateOptions myCustomer = new Stripe.CustomerCreateOptions()
            {
                Name = "Daniel",//change to var
                Email = "dannydinellilaois@gmail.com",//change to var
                Description = "Customer for dannydinellilaois@gmail.com",//change to var
            };

            var customerService = new Stripe.CustomerService();
            Stripe.Customer stripeCustomer = customerService.Create(myCustomer);

            //mycustomer = stripeCustomer.Id; // Not needed

            //Step 4 : Now Create Charge Options for the customer. 
            var chargeoptions = new Stripe.ChargeCreateOptions
            {
                Amount = 50,//change to var
                Currency = "EUR",//change to var
                ReceiptEmail = "dannydinellilaois@gmail.com",//change to var
                Customer = stripeCustomer.Id,
                Source = source.Id

            };

            //Step 5 : Perform the payment by  Charging the customer with the payment. 
            var service1 = new Stripe.ChargeService();
            Stripe.Charge charge = service1.Create(chargeoptions); // This will do the Payment

            //getchargedID = charge.Id; // Not needed
        }


        /*public void GetCustomerInformationID(object sender, EventArgs e)
        {
            var service = new CustomerService();
            var customer = service.Get(mycustomer);
            var serializedCustomer = JsonConvert.SerializeObject(customer);
            //  var UserDetails = JsonConvert.DeserializeObject<CustomerRetriveModel>(serializedCustomer);

        }


        public void GetAllCustomerInformation(object sender, EventArgs e)
        {
            var service = new CustomerService();
            var options = new CustomerListOptions
            {
                Limit = 3,
            };
            var customers = service.List(options);
            var serializedCustomer = JsonConvert.SerializeObject(customers);
        }


        public void GetRefundForSpecificTransaction(object sender, EventArgs e)
        {
            var refundService = new RefundService();
            var refundOptions = new RefundCreateOptions
            {
                Charge = getchargedID,
            };
            Refund refund = refundService.Create(refundOptions);
            refundID = refund.Id;
        }


        public void GetRefundInformation(object sender, EventArgs e)
        {
            var service = new RefundService();
            var refund = service.Get(refundID);
            var serializedCustomer = JsonConvert.SerializeObject(refund);

        }*/
    }
}
