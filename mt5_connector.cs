using mtapi.mt5;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Examples
    {
        static void Main(string[] args)
        {
            new Examples().Run();

        }

        static string Format(string type)
        {
            return type.Substring(type.LastIndexOf(".") + 1);
        }

            

        void Run()
        {
            Logger.OnMsg += Logger_OnMsg;
            //Certificate();
            //Serialize();
            //Connect();
            //DownloadQuoteHistory();
            //OpenedOrders();
            //OrderUpdate();
            //MarketOrder();
            //MarketOrder2();
            //CloseBy();
            //Connect();
            //Proxy();
            //OnDisconnect();
            //PendingOrder();
            RealtimeQuotes();
            //ServersDat();
            //MarketOrderMany();
            //MarketOrderAsync();
            //MarketCloseAsync();
            //MarketOrderMany();
            //MarketOrder();
            //Pending();
            //ServersDat();
            //ServersDatConnect();
            //OrderHistory();
            //TestOpenedOrders();
            //WorkAround();
            //Hedging();
            //ModifyOrder();
            //Quotes();
            //SendOrder();
            //ConnectDisconnect();
            //Sessions();
            //mt();
            //OrderProfit();
            //QuoteHist();
            //Margin();
            //DownloadOrderHistory();

            //RequestOrderHistory();
            //TradeMode();
            //D();
            //ModifyPending();
            //Form1_Load(null, null);
            //Console.WriteLine("1");
            //button1_Click(null, null);
            //Console.WriteLine("2");
            //button2_Click(null, null);
            //Console.WriteLine("3");
            //Console.ReadKey();
            //DemoAccountCreate();
            //ExecutionSpeed();
            //test();
            //TradeCopy();
            //CheckEquity();
            //Subscribe();
            //Connect2();
            //Connect3();
            //OrderHistory2();
        }

        private void DownloadQuoteHistory()
        {
            var api = new MT5API(5005878206, "eggzbi8z", "access.metatrader5.com", 443);
            api.Connect();
            var t = api.ServerTime;
            var res = api.DownloadQuoteHistoryMonth("XAUUSD", 2022, 7, 1, 1440);
            Console.WriteLine(res.Count);
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }

        private void OrderHistory2()
        {
            string strStart = "2022-02-28 01:00:01";
            string strEnd = "2022-05-31 23:59:59";

            DateTime dtStart = DateTime.MinValue;
            DateTime dtEnd = DateTime.MaxValue;

            DateTime.TryParse(strStart, out dtStart);
            DateTime.TryParse(strEnd, out dtEnd);

            var qc = new MT5API(9111443, "Readonly12", "13.113.185.156", 443);
            var task = Task.Run(() => qc.Connect());
            qc.Connect();
            Console.WriteLine("Connected");
            Console.WriteLine(qc.Account.Balance);
            var hist = qc.DownloadOrderHistory(dtStart, dtEnd);

            foreach (var item in hist.InternalOrders)
                Console.WriteLine(ConvertTo.DateTime(item.OpenTime));

            Console.WriteLine("End");
            Console.ReadKey();
        }

        void Connect2()
        {
            var Api = new MT5API(16053, "vQhx4TzL", "157.175.246.82", 443);
            Api.Connect();
            foreach (var position in Api.GetOpenedOrders())
                Console.WriteLine($"Ticket N {position.Ticket}, Lots = {position.Lots} Symbol = {position.Symbol} {position.OrderType} OpenPrice = {position.OpenPrice}");
            Api.OrderSendAsync(1, "AUDNZD.m", 0.1, double.NaN, OrderType.Sell, fillPolicy: FillPolicy.Any);
            Task.Delay(500).Wait();
            foreach (var position in Api.GetOpenedOrders())
                Console.WriteLine($"Ticket N {position.Ticket}, Lots = {position.Lots} Symbol = {position.Symbol} {position.OrderType} OpenPrice = {position.OpenPrice}");
            Task.Delay(2000).Wait();
            Console.WriteLine("------------------------");
            Api.OrderSendAsync(1, "AUDNZD.m", 0.3, double.NaN, OrderType.Buy, fillPolicy: FillPolicy.Any);
            Task.Delay(1000).Wait();
            Api.OrderSendAsync(1, "AUDNZD.m", 0.7, double.NaN, OrderType.Sell, fillPolicy: FillPolicy.Any);
            Task.Delay(500).Wait();
            foreach (var position in Api.GetOpenedOrders())
                Console.WriteLine($"Ticket N {position.Ticket}, Lots = {position.Lots} Symbol = {position.Symbol} {position.OrderType} OpenPrice = {position.OpenPrice}");
            Task.Delay(1500).Wait();
            Console.WriteLine("------------------------");
            Api.OrderSendAsync(1, "AUDNZD.m", 0.7, double.NaN, OrderType.Sell, fillPolicy: FillPolicy.Any);
            Task.Delay(3000).Wait();
            foreach (var position in Api.GetOpenedOrders())
                Console.WriteLine($"Ticket N {position.Ticket}, Lots = {position.Lots} Symbol = {position.Symbol} {position.OrderType} OpenPrice = {position.OpenPrice}");
            var position1 = Api.GetOpenedOrders().FirstOrDefault();
            //Here the volume of the last position has to be "0.2"
            Console.WriteLine($"Ticket N {position1.Ticket}, Lots = {position1.Lots}");
            Console.WriteLine("================================");
            while (true)
            {
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

                Task.Delay(1000).Wait();
                foreach (var position in Api.GetOpenedOrders())
                    Console.WriteLine($"Ticket N {position.Ticket}, Lots = {position.Lots} Symbol = {position.Symbol} {position.OrderType} OpenPrice = {position.OpenPrice}");
                Console.WriteLine("Pres...");
                Console.ReadKey();
            }
            Console.WriteLine("Done");
            Console.ReadKey();
        }

        void Connect3()
        {
            var Api = new MT5API(16053, "vQhx4TzL", "157.175.246.82", 443);
            Api.Connect();
            Api.OrderSendAsync(1, "USDJPY.m", 0.1, double.NaN, OrderType.Buy, fillPolicy: FillPolicy.Any);
            Task.Delay(1000).Wait();
            Api.OrderSendAsync(1, "USDJPY.m", 0.1, double.NaN, OrderType.Buy, fillPolicy: FillPolicy.Any);
            Task.Delay(1000).Wait();
            Api.OrderSendAsync(1, "USDJPY.m", 0.1, double.NaN, OrderType.Buy, fillPolicy: FillPolicy.Any);
            Task.Delay(1000).Wait();
            Api.OrderSendAsync(1, "USDJPY.m", 0.1, double.NaN, OrderType.Buy, fillPolicy: FillPolicy.Any);
            Task.Delay(1000).Wait();
            Api.OrderSendAsync(1, "USDJPY.m", 0.1, double.NaN, OrderType.Buy, fillPolicy: FillPolicy.Any);
            Task.Delay(1000).Wait();
            OrderType type = OrderType.Sell;
            while (true)
            {
                Api.Disconnect();
                Task.Delay(1000).Wait();
                Api = new MT5API(16053, "vQhx4TzL", "157.175.246.82", 443);
                Api.Connect();
                Task.Delay(500).Wait();
                Api.OrderSendAsync(1, "USDJPY.m", 1, double.NaN, type, fillPolicy: FillPolicy.Any);
                Task.Delay(2000).Wait();
                var positions = Api.GetOpenedOrders();
                foreach (var position in positions)
                {
                    Console.WriteLine($"Ticket N {position.Ticket}, Lots = {position.Lots}, OrderType = {position.OrderType}");
                }
                type = type == OrderType.Buy ? OrderType.Sell : OrderType.Buy;
            }
        }

        private void Subscribe()
        {
            var Api = new MT5API(7703, "P1O4jNi1", "164.132.207.155", 443);
            Api.Connect();
            Api.OnConnectProgress += (MT5API api, ConnectEventArgs args) => Console.WriteLine(args.Progress.ToString());
            Api.OnQuote += (MT5API api, Quote quote) => Console.WriteLine(quote);
            foreach (var s in Api.Symbols.Infos)
            {
                Api.Subscribe(s.Key);
                //Thread.Sleep(1000);
            }
            //Api.Subscribe("EURUSD.s");
            //Thread.Sleep(1000);
            //Api.Subscribe("NZDUSD");
            //Thread.Sleep(1000);
            //Api.Subscribe("Alphabet.ETP");
            //Thread.Sleep(1000);
            //Api.Subscribe("Apple");
            //Thread.Sleep(1000);
            //Api.Subscribe("Microsoft.ETP");
            //Thread.Sleep(1000);
            //Api.Subscribe("Apple.ETP");
            //Thread.Sleep(1000);
            //Api.Subscribe("Microsoft");
            //Thread.Sleep(1000);
            //Api.Subscribe("USDCHF.s");
            //Thread.Sleep(1000);
            //Api.Subscribe("Alphabet-Class-A");
            //Thread.Sleep(1000);

            while (true)
            {
                Thread.Sleep(200);
            }
        }


        private void CheckEquity()
        {
            try
            {
                var api = new MT5API(50988995, "Ly6Ry4q6g", "dc2.mt5demo.mydcaccess.com", 443);

                api.OnConnectProgress += Api_OnConnectProgress;
                //api.LoginIdPath = "http://localhost:2200/loginid";
                api.Connect();

                while (true)
                {
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine(api.AccountEquity);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        MT5API Dest;
        Dictionary<long, long> Tickets = new Dictionary<long, long>();
        private void TradeCopy()
        {
            var src = new MT5API(111111, "kzdx3zxt", "46.235.34.40", 443);
            src.Connect();
            Dest = new MT5API(34456308, "co0hcwrb", "78.140.180.198", 443); //mq demo gbp
            Dest.Connect();
            src.OnOrderUpdate += Qc1_OnOrderUpdate1; ;
            Console.WriteLine("Open account 111111 and 34456308 in terminal. After that try to open and close market orders on account 111111 from terminal and see changes on account 34456308 from terminal");
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }

        private void Qc1_OnOrderUpdate1(MT5API sender, OrderUpdate update)
        {
            try
            {
                var order = update.Order;
                if (update.Type == UpdateType.MarketOpen)
                {
                    while (Dest.GetQuote(order.Symbol) == null)
                        Thread.Sleep(1);
                    double price = Dest.GetQuote(order.Symbol).Ask;
                    if (order.DealType == DealType.DealSell)
                        price = Dest.GetQuote(order.Symbol).Bid;
                    var destOrder = Dest.OrderSend(order.Symbol, order.Lots, price, order.OrderType, 0, 0, 1000);
                    Tickets.Add(order.Ticket, destOrder.Ticket);
                    Console.WriteLine("Open copied");
                }
                if (update.Type == UpdateType.MarketClose)
                {
                    while (Dest.GetQuote(order.Symbol) == null)
                        Thread.Sleep(1);
                    double price = Dest.GetQuote(order.Symbol).Ask;
                    if (order.DealType == DealType.DealBuy)
                        price = Dest.GetQuote(order.Symbol).Bid;
                    Dest.OrderClose(Tickets[order.Ticket], order.Symbol, price, order.CloseVolume, order.OrderType, 1000);
                    Console.WriteLine("Close copied");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        void test()
        {
            Console.WriteLine("Connecting...");
            var mt5Api = new MT5API(17703, "Mill2021", "101.97.56.165", 443);
            //var mt5Api = new MT5API(50483124, "LYBeTAz1", "47.89.176.80", 443);
            mt5Api.Connect();
            Console.WriteLine("Connected");

            try
            {
                Order order = null;

                var tiket = 1223690;
                var action = "Modify SL";
                var instrument = "GBPJPY";
                ulong deviation = 0;
                var price = 0;
                var lots = 0.1;
                var sl = 151.05;
                var tp = 149.15;
                var comment = "Test App";
                Expiration expiration = null;
                //expiration = new Expiration() { DateTime = new DateTime(2021, 04, 09, 12, 15, 18), Type = ExpirationType.GTC };

                switch (action)
                {
                    case "Deal Buy":
                        order = mt5Api.OrderSend(instrument, lots, 0, OrderType.Buy, sl, tp, deviation, comment, 0, FillPolicy.ImmediateOrCancel, TradeType.Transfer, 0, expiration);
                        break;

                    case "Deal Sell":
                        order = mt5Api.OrderSend(instrument, lots, 0, OrderType.Sell, sl, tp, deviation, comment, 0, FillPolicy.ImmediateOrCancel, TradeType.Transfer, 0, expiration);
                        break;

                    case "Order Buy Limit":
                        order = mt5Api.OrderSend(instrument, lots, price, OrderType.BuyLimit, sl, tp, deviation, comment, 0, FillPolicy.FlashFill, TradeType.SetOrder, 0, expiration);
                        break;

                    case "Order Buy Stop":
                        order = mt5Api.OrderSend(instrument, lots, price, OrderType.BuyStop, sl, tp, deviation, comment, 0, FillPolicy.FlashFill, TradeType.SetOrder, 0, expiration);
                        break;

                    case "Order Sell Limit":
                        order = mt5Api.OrderSend(instrument, lots, price, OrderType.SellLimit, sl, tp, deviation, comment, 0, FillPolicy.FlashFill, TradeType.SetOrder, 0, expiration);
                        break;

                    case "Order Sell Stop":
                        order = mt5Api.OrderSend(instrument, lots, price, OrderType.SellStop, sl, tp, deviation, comment, 0, FillPolicy.FlashFill, TradeType.SetOrder, 0, expiration);
                        break;

                    case "Remove Order":
                        //ordertype from last order
                        // aplways set price 0
                        order = mt5Api.OrderClose(tiket, instrument, 0, lots, OrderType.SellStop);
                        break;

                    case "Close Trade":
                        //ordertype != last order if sell then buy, if buy then sell
                        order = mt5Api.OrderClose(tiket, instrument, 0, lots, OrderType.Sell, 0, FillPolicy.ImmediateOrCancel);
                        break;

                    case "Close By":
                        break;

                    case "Modify SL":
                    case "Modify TP":
                        //ordertype from last order
                        mt5Api.OrderModify(tiket, instrument, lots, price, OrderType.Sell, sl, tp);
                        Console.WriteLine("modified");
                        break;
                }
                if (order != null)
                    Console.WriteLine($"Tiket: {order.Ticket}, Open Price: {order.OpenPrice}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Disconnecting...");
            mt5Api.Disconnect();
            Console.WriteLine("Disconnected");
        }





        void Pending()
        {
            var api = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);
            api.OnQuote += ApiOnOnQuote;
            api.PlacedType = PlacedType.ByExpert;
            api.OnOrderUpdate += Qc_OnOrderUpdate;
            api.Connect();

            var openOrder = api.OrderSend("EURUSD.DEMO", 0.01, 1.23, OrderType.BuyStop, fillPolicy: FillPolicy.FlashFill);
            Console.WriteLine($"Buy order sent: ticket# {openOrder.Ticket}, open price {openOrder.OpenPrice}, lots {openOrder.Lots}, contract size {openOrder.ContractSize}");
            Console.WriteLine("Press any key...");
            Console.ReadKey();

            var closeOrder = api.OrderClose(openOrder.Ticket, openOrder.Symbol, 0, openOrder.Lots, openOrder.OrderType);
            Console.WriteLine(closeOrder.CloseTime + " Closed, press any key...");
            Console.ReadKey();
        }

        private void ExecutionSpeed()
        {
            //MT5API api = new MT5API(432524, "wbBWft23", "mt5.b1.gomarkets.com", 443);
            var api = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);
            Console.WriteLine("Connecting...");
            api.Connect();
            Console.WriteLine("Connected to server");
            api.OnOrderProgress += Oc_OnOrderProgress;
            while (api.GetQuote("EURUSD.DEMO") == null)
                Thread.Sleep(10);
            double ask = api.GetQuote("EURUSD.DEMO").Ask;
            Console.WriteLine(DateTime.Now.ToString("hh.mm.ss.ffffff"));
            int id = api.GetRequestId();
            api.OrderSendAsync(id, "EURUSD.DEMO", 0.01, ask, OrderType.Buy, 0, 0, 100, null, 0, FillPolicy.FillOrKill);
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private void Oc_OnOrderProgress(MT5API sender, OrderProgress args)
        {
            Console.WriteLine(args.TradeResult.Status + " " + args.OrderUpdate.Action + " " + DateTime.Now.ToString("hh.mm.ss.ffffff"));
            var status = args.TradeResult.Status;
            if (status != Msg.REQUEST_ACCEPTED && status != Msg.REQUEST_ON_WAY && status != Msg.REQUEST_EXECUTED
                        && status != Msg.DONE && status != Msg.ORDER_PLACED)
                Console.WriteLine("Failed: " + status);
            if (status == Msg.REQUEST_EXECUTED)
                Console.WriteLine(args.TradeResult.TicketNumber);
        }


        private void DemoAccountCreate()
        {
            var req = new AccountRequest
            {
                UserName = "User Name",
                AccType = "demo",
                Country = "Canada",
                City = "Toronto",
                State = "Toronto",
                ZipCode = "12345",
                Address = "Address Address",
                Phone = "+79125667433",
                Email = "timurilsasa@yahoo.com",
                CompanyName = "metaquotes",
                Deposit = 1000000
            };

            var brokers = new Dictionary<string, string>();
            var host = "78.140.180.198"; var broker = "mq";
            brokers.Add(host, broker);
            host = "136.244.109.100"; broker = "centroid";
            brokers.Add(host, broker);
            host = "35.72.155.74"; broker = "blue";
            brokers.Add(host, broker);
            host = "dc2.mt5demo.mydcaccess.com"; broker = "alpari";
            brokers.Add(host, broker);
            int port = 443;

            for (int i = 0; i < 1; i++)
            {
                foreach (var item in brokers)
                {
                    try
                    {
                        var addr = item.Key;
                        var brok = item.Value;
                        var acc = MT5API.RequestDemoAccount(req, addr, port);
                        File.AppendAllText("accounts.txt", String.Join(" ", acc.Login, acc.Password, addr, port, brok) + Environment.NewLine);
                        Console.WriteLine(acc.Login);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(item.Value + " " + ex.Message);
                    }
                    Thread.Sleep(10000);
                }
            }
            Console.WriteLine("Done");
            Console.ReadKey();

        }

        MT5API Api;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //CheckForIllegalCrossThreadCalls = false;
                Api = new MT5API(190531, "jkjk123", "173.249.52.92", 443);
                Api.Connect();
                Api.OnQuote += Api_OnQuote3;
                Api.Subscribe("GCZ20-AV");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        Quote Quote;

        private void Api_OnQuote3(MT5API api, Quote quote)
        {
            Quote = quote;
            //label1.Text = quote.Ask.ToString();
            //label2.Text = quote.Bid.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Quote q;
            try
            {
                string symbol = "GCZ20-AV";
                Api.Subscribe(symbol);
                while (Api.GetQuote(symbol) == null)
                    Thread.Sleep(1);
                Order o;
                while (true)
                    try
                    {
                        q = Api.GetQuote(symbol);
                        try
                        {
                            o = Api.OrderSend(symbol, 0.1, q.Ask, OrderType.Buy, 0, 0, 1000, null);
                        }
                        catch (ServerException ex)
                        {
                            //Connection is fine, server returned that something wrong in trade request
                            throw;
                        }
                        catch (Exception)
                        {
                            //All other exceptions, most possible connect exceptions
                            throw;
                        }
                        
                        break;
                    }
                    catch (ServerException ex)
                    {
                        if (ex.Code == Msg.REQUOTE)
                            continue;
                        if (ex.Code == Msg.NO_PRICES)
                            continue;
                        else
                            throw ex;
                    }
                Console.WriteLine("Order " + o.Ticket + " opened at " + o.OpenPrice);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Quote q;
            try
            {
                string symbol = "GCZ20-AV";
                Api.Subscribe(symbol);
                while (Api.GetQuote(symbol) == null)
                    Thread.Sleep(1);
                Order o;
                while (true)
                    try
                    {
                        q = Api.GetQuote(symbol);
                        o = Api.OrderSend(symbol, 0.1, q.Bid, OrderType.Sell, 0, 0, 1000, null);
                        break;
                    }
                    catch (ServerException ex)
                    {
                        if (ex.Code == Msg.REQUOTE)
                            continue;
                        if (ex.Code == Msg.NO_PRICES)
                            continue;
                        else
                            throw ex;
                    }
                Console.WriteLine("Order " + o.Ticket + " opened at " + o.OpenPrice);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        void ModifyPending()
        {
            var api = new MT5API(50284689, "sHXbDfNW", "207.246.127.101", 443);
            api.Connect();
            var order = api.OrderSend("EURUSD", 0.2, 1.3, OrderType.BuyStop, 0, 0, 0, null, 0,
                FillPolicy.FlashFill, TradeType.Transfer, 0, new Expiration
                {
                    DateTime = DateTime.Now.AddHours(4),
                    Type = ExpirationType.Specified
                });
            Thread.Sleep(100);
            api.OrderModify(order.Ticket, "EURUSD", 0.2, 1.3, OrderType.BuyStop, 0, 0, 0, 0, null);

        }

        void D()
        {
            var api = new MT5API(32115516, "baunn2wo", "195.201.195.179", 443);
            api.Connect();
            Console.WriteLine("Connected. ");
            string symbol = "EURUSD";
            api.Subscribe(symbol);
            while (api.GetQuote(symbol) == null)
                Thread.Sleep(1);
            Order o;
            while (true)
                try
                {
                    o = api.OrderSend(symbol, 0.01, api.GetQuote(symbol).Ask, OrderType.Buy, 0, 0, 100, null);
                    break;
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Contains("requote"))
                        continue;
                    else
                        throw ex;
                }
            Console.WriteLine("Order " + o.Ticket + " opened at " + o.OpenPrice);
            Console.WriteLine("Opened orders:");
            foreach (var item in api.GetOpenedOrders())
                Console.Write(item.Ticket + " ");
            Console.WriteLine();
            Console.WriteLine("Press any key to close order..");
            Console.ReadKey();
            while (true)
                try
                {
                    o = api.OrderClose(o.Ticket, o.Symbol, api.GetQuote(symbol).Bid, o.Lots, o.OrderType, 100);
                    break;
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Contains("requote"))
                        continue;
                    else
                        throw ex;
                }
            Console.WriteLine("Closed at " + o.ClosePrice);
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        private void TradeMode()
        {
            //var api = new MT5API(5100971, "QC8qVxD9JmE", "52.178.106.228", 443);
            var api = new MT5API(62401092, "Chien123456", "35.157.110.232", 443);
            //var api = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);
            api.Connect();
            var g = api.Symbols.GetGroup("EURUSD");
            Console.WriteLine(g.TradeMode);
            Console.ReadKey();
        }

        private void Margin()
        {
            //var api = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);
            var api = new MT5API(3000016727, "g99Z0J58Uw", "demoUK-mt5.darwinex.com", 443);
            Console.WriteLine("Connecting...");
            api.Connect();
            Console.WriteLine(api.Symbols.GetGroup("EURUSD").MaxLots);
            Console.WriteLine(api.Symbols.GetGroup("EURUSD").MaxVolume);
            Console.WriteLine(api.AccountMargin);
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private void QuoteHist()
        {
            //var api = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);
            //var api = new MT5API(111111, "kzdx3zxt", "46.235.34.40", 443);
            //var api = new MT5API(5344392, "urDYZ3Ng", "35.178.164.104", 443);
            //var api = new MT5API(5005878206, "eggzbi8z", "access.metatrader5.com", 443);
            var api = new MT5API(2040487, "test1234", "access.metatrader5.com", 443);
            Console.WriteLine("Connecting...");
            api.Connect();
            Console.WriteLine("Connected");
            api.OnQuoteHistory += Api_OnQuoteHistory;
            //api.RequestQuoteHistoryToday("EURUSD.DEMO");
            api.RequestQuoteHistoryMonth("EURUSD", 2022, 6, 1);//, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        List<Bar> ConvertToTimeframe(List<Bar> bars, int minutes)
        {
            int i = 0;
            while (bars[i].Time.Minute % minutes > 0)
                i++;
            List<Bar> res = new List<Bar>();
            Bar bar = new Bar();
            bar.OpenPrice = bars[0].OpenPrice;
            bar.Time = bars[0].Time;
            bar.LowPrice = bars[0].LowPrice;
            DateTime time = bars[0].Time.AddMinutes(minutes);
            for (; i < bars.Count; i++)
            {
                if (bars[i].Time >= time)
                {
                    bar.ClosePrice = bars[i - 1].ClosePrice;
                    res.Add(bar);
                    bar = new Bar();
                    bar.OpenPrice = bars[i].OpenPrice;
                    bar.Time = bars[i].Time;
                    bar.LowPrice = bars[i].LowPrice;
                    time = time.AddMinutes(minutes);
                }
                if (bars[i].HighPrice > bar.HighPrice)
                    bar.HighPrice = bars[i].HighPrice;
                if (bars[i].LowPrice < bar.LowPrice)
                    bar.LowPrice = bars[i].LowPrice;
            }
            return res;
        }


        List<Bar> Bars = new List<Bar>();
        private void Api_OnQuoteHistory(MT5API sender, QuoteHistoryEventArgs hist)
        {
            Bars.AddRange(hist.Bars);
            Console.WriteLine(hist.Symbol + " " + hist.Bars.Count + " " + hist.Bars[0].Time + " " + hist.Bars[hist.Bars.Count - 1].Time);
            var m5Bars = ConvertToTimeframe(Bars, 5);
        }

        private void OrderProfit()
        {
            //var api = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);
            //var api = new MT5API(40910506, "JWT81h4i50gA", "51.38.84.252", 443);
            //var api = new MT5API(32115516, "baunn2wo", "195.201.195.179", 443);
            //var api = new MT5API(50284689, "sHXbDfNW", "207.246.127.101", 443);
            //var api = new MT5API(5031, "fgg#42!dggr", "37.188.119.195", 443);
            //var api = new MT5API(5027, "zt8zkszg", "37.188.119.195", 443);
            //var api = new MT5API(1090001349, "Aovqhta700", "95.168.183.92", 443);
            //var api = new MT5API(10900000024, "m6qysswp", "95.168.183.92", 443);
            //var api = new MT5API(1090003266, "Fczrcrp302", "95.168.183.92", 443);
            //var api = new MT5API(111111, "kzdx3zxt", "46.235.34.40", 443);
            //var api = new MT5API(1090003249, "Ppwzvzv538", "95.168.183.92", 443);
            //var api = new MT5API(5006747, "n6a3yp23", "185.96.247.159", 443);
            var api = new MT5API(3458, "forex123", "104.243.43.183", 443);
            Console.WriteLine("Connecting...");
            api.Connect();
            api.Subscribe("GBPUSD");
            api.OnQuote += Api_OnQuote1;
            api.OnOrderUpdate += Api_OnOrderUpdate;
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private void Api_OnOrderUpdate(MT5API sender, OrderUpdate update)
        {
            try
            {
                if (update.Type == UpdateType.MarketOpen)
                    Console.WriteLine("------" + update.Order.OpenPrice);
            }
            catch (Exception ex)
            {


            }
        }

        private void IsInvestor()
        {
            var api = new MT5API(5031, "fgg#42!dggr", "37.188.119.195", 443);
            Console.WriteLine("Connecting...");
            api.Connect();
            int InvestorFlag = 8;
            if ((api.Account.TradeFlags & InvestorFlag) != 0)
                Console.WriteLine("investor mode");
        }

        private void Api_OnQuote1(MT5API api, Quote quote)
        {
            //foreach (var item in api.GetOpenedOrders())
            //	Console.Write(item.Profit + " ");
            Console.Write(Math.Round(api.AccountProfit, 2) + " " + api.GetOpenedOrders().Length + " ");
        }

        private void mt()
        {
            string response;

            var request = (HttpWebRequest)WebRequest.Create("https://www.mtsystems.com/translate.php");
            var data = Encoding.ASCII.GetBytes(File.ReadAllText(@"C:\YandexDisk\Tim\java\mt5api\code.cs").Replace(Environment.NewLine, ""));

            request.Method = "POST";
            request.ContentType = "application/text";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var resp = (HttpWebResponse)request.GetResponse();
            response = new StreamReader(resp.GetResponseStream()).ReadToEnd();
            Console.WriteLine(response);
            Console.ReadKey();
        }

        private void Sessions()
        {
            //var api = new MT5API(511105, "E2Ib58ykBM", "209.58.183.121", 443);
            var api = new MT5API(2233358, "W3lc0m3123", "mt5trial.exness.com", 443);
            api.Connect();
            api.OnQuote += ApiOnOnQuote;
            Console.WriteLine("Connected");
            //wait for the quote to update server time
            while (api.GetQuote("EURUSDm") == null)
                Thread.Sleep(1);
            Console.WriteLine("Got server time");
            //wait for 
            while (api.LastQuoteTime.Hour * 60 + api.LastQuoteTime.Minute <
                api.Symbols.Sessions["AMZNm"].Quotes[(int)api.LastQuoteTime.DayOfWeek][0].StartTime)
                Thread.Sleep(1000);
            Console.WriteLine("Quote session began");
            api.Subscribe("AMZNm");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static void ApiOnOnQuote(MT5API sender, Quote quote)
        {
            //if (quote.Symbol == "EURUSD")
            Console.WriteLine(quote);
        }

        private void ConnectDisconnect()
        {
            var api = new MT5API(511105, "E2Ib58ykBM", "209.58.183.121", 443);

            Console.WriteLine("1");
            api.Connect();
            Console.WriteLine("2");
            api.Disconnect();
            Console.WriteLine("3");
            api.Connect();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private void SendOrder()
        {
            //var api = new MT5API(511105, "E2Ib58ykBM", "209.58.183.121", 443);
            //var api = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);

        }

        void Quotes()
        {
            //var api = new MT5API(18574, "uaDZ3mjC6", "13.114.200.108", 443);
            //var api = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);
            var api = new MT5API(5006747, "n6a3yp23", "185.96.247.159", 443);
            api.OnQuote += ApiOnOnQuote;
            api.Connect();
            api.Subscribe("EURUSD");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        void Hedging()
        {
            var api = new MT5API(18574, "uaDZ3mjC6", "13.114.200.108", 443);
            Console.WriteLine("Connecting...");
            api.Connect();
            api.OnOrderProgress += Qc_OnOrderProgress;
            Console.WriteLine("Connected");

            string symbol = "EURUSD";
            api.Subscribe(symbol);
            Console.WriteLine("Waiting for the quote...");
            while (api.GetQuote(symbol) == null)
                Thread.Sleep(1);
            for (int i = 0; i < 1; i++)
            {
                var quote = api.GetQuote(symbol);
                //api.OnOrderProgress += Qc_OnOrderProgress;
                Console.WriteLine("Opened orders:");
                foreach (var item in api.GetOpenedOrders())
                    Console.WriteLine(item.Ticket + " " + item.Lots);
                var o = api.OrderSend(symbol, 0.5, quote.Ask, OrderType.Buy, 0, 0, 100, null, 0);
                Console.WriteLine("Order " + o.Ticket + " opened at " + o.OpenPrice);
                Console.WriteLine("Opened orders:");
                foreach (var item in api.GetOpenedOrders())
                    Console.WriteLine(item.Ticket + " " + item.Lots);
                o = api.OrderClose(o.Ticket, o.Symbol, 0, 0.4, o.OrderType, 100);
                Console.WriteLine("Closed at " + o.ClosePrice);
                Console.WriteLine("Opened orders:");
                foreach (var item in api.GetOpenedOrders())
                    Console.WriteLine(item.Ticket + " " + item.Lots);
                o = api.OrderClose(o.Ticket, o.Symbol, 0, 0.1, o.OrderType, 100);
                Console.WriteLine("Closed at " + o.ClosePrice);
                Console.WriteLine("Opened orders:");
                foreach (var item in api.GetOpenedOrders())
                    Console.WriteLine(item.Ticket + " " + item.Lots);
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        void WorkAround()
        {
            //var api = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);
            var api = new MT5API(18574, "uaDZ3mjC6", "13.114.200.108", 443);
            api.Connect();
            api.OnQuote += Api_OnQuote;
            api.Subscribe("EURUSD");
            api.Subscribe("USDCHF");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private void Api_OnQuote(MT5API sender, Quote quote)
        {
            try
            {
                //Console.WriteLine(sender.Workaround.AccountEquity() + " " + sender.ServerTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void TestOpenedOrders()
        {
            var api = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);
            api.Connect();
            var tickets1 = api.GetOpenedOrders();//.Select(x => x.Ticket).ToList();
            Console.WriteLine(tickets1.Count());
            //Thread.Sleep(1000);
            var open = api.OrderSend("EURUSD.DEMO", 0.02, 0.0, OrderType.Sell, 0, 0, 0, null, 0, FillPolicy.FillOrKill);
            //Thread.Sleep(1000);
            var tickets2 = api.GetOpenedOrders();//.Select(x => x.Ticket).ToList();
            Console.WriteLine(tickets2.Count());
            //Thread.Sleep(1000);
            var tickets3 = api.GetOpenedOrders();//.Select(x => x.Ticket).ToList();
            Console.WriteLine(tickets3.Count());
            Console.WriteLine("Press any key...");
            Console.ReadKey();
            api.Disconnect();
        }

        void OrderHistory()
        {
            //var api = new MT5API(18574, "uaDZ3mjC6", "13.114.200.108", 443);
            //var api = new MT5API(18574, "uaDZ3mjC6", "13.114.200.108", 443);
            //var api = new MT5API(993617, "dtc1kjgu", "63.32.197.118", 443);
            MT5API api = new MT5API(4000000825L, "InvPswDWX22", "185.97.161.224", 443);
            api.Connect();
            Console.WriteLine("Connected");
            Console.WriteLine(api.Account.Balance);
            api.OnOrderHistory += Api_OnOrderHistory;
            //api.RequestOrderHistory(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1));
            //api.RequestOrderHistory(new DateTime(2020, 03, 24), new DateTime(2020, 04, 02));
            api.RequestOrderHistory(DateTime.Now.AddDays(-1024), DateTime.Now);
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        void DownloadOrderHistory()
        {
            //170.82.68.216:443 ping:
            //10.29.130.55:443 ping:
            //191.239.249.87:443 ping:
            //170.82.70.81:441 ping:
            //10.14.30.151:441 ping:
            //170.82.70.81:442 ping:
            //170.82.70.81:443 ping:
            //20.195.163.72:4433 ping:
            //20.195.163.72:4434 ping:
            //170.82.68.222:443 ping:
            //10.29.130.50:443 ping:
            //170.82.68.213:443 ping:
            //10.29.130.52:443 ping:
            //191.234.191.90:443 ping:
            //170.82.68.214:443 ping:
            //10.29.130.53:443 ping:
            //170.82.70.81:444 ping:
            //10.14.30.154:444 ping:
            //var res = Broker.Search("XMTrading");
            //var api = new MT5API(70175587, "Test1234", "35.197.217.65", 443);
            //var api = new MT5API(8480844, "123456!", "170.82.70.81", 444);
            var api = new MT5API(26817585, "Abc12345", "146.177.21.50", 443);
            api.Connect();
            Console.WriteLine("Connected");
            Console.WriteLine(api.Account.Balance);
            var ar = api.DownloadOrderHistory(DateTime.Now.AddMonths(-24), DateTime.Now).Orders;
            foreach (var item in ar)
                Console.WriteLine(item.Ticket + " " + item.OpenTime + " " + item.CloseTime + " " + item.OrderType);
            Console.WriteLine("Press any key...");
            Console.ReadKey();

            //var hist = api.DownloadOrderHistory(DateTime.Now.AddDays(-1024), DateTime.Now);
            //Console.WriteLine(hist.Action);
            //if(hist.Action == 14)
            //request more data
            //foreach (var item in hist.Orders)//new DateTime(2020, 03, 24), new DateTime(2020, 04, 02);
            //foreach (var item in api.DownloadOrderHistory(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)).Orders)//new DateTime(2020, 03, 24), new DateTime(2020, 04, 02);
            //Console.WriteLine(item.Ticket);
            //Console.WriteLine("Press any key...");
            //Console.ReadKey();
        }

        void RequestOrderHistory()
        {
            //var api = new MT5API(26817585, "Abc12345", "35.230.145.63", 443);
            //var api = new MT5API(26891038, "v85Tu10mXfN4FpR", "35.230.145.63", 443);
            MT5API api = new MT5API(26891038, "v85Tu10mXfN4FpR", "54.254.127.18", 443);
            //MT5API api = new MT5API(51229818, "Q0DU3zu7Q", "178.255.202.119", 443);
            //var api = new MT5API(3115991, "djfuya0s", "98.158.104.97", 443);
            api.Connect();
            Console.WriteLine("Connected");
            Console.WriteLine(api.ServerTime);
            Console.WriteLine(api.Account.Balance);
            api.OnOrderHistory += Api_OnOrderHistory1;
            for (int i = 1; i < 7; i++)
                api.RequestOrderHistory(2022, i);
            //api.RequestOrderHistory(DateTime.Now.AddMonths(-60), DateTime.Now.AddMonths(1));
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private void Api_OnOrderHistory1(MT5API sender, OrderHistoryEventArgs args)
        {
            Console.WriteLine(args.Orders.Count + " " + args.Orders.First().OpenTime + " " + args.Orders.Last().OpenTime);
            if (args.Action == 14)
            {
                Console.WriteLine("need to request more data"); 
                sender.RequestOrderHistory(args.InternalDeals.Last().OpenTimeAsDateTime.Year, args.InternalDeals.Last().OpenTimeAsDateTime.Month,
                    args.InternalDeals);
            }
        }

        private void Api_OnOrderHistory(MT5API sender, OrderHistoryEventArgs args)
        {
            foreach (var item in args.Orders)
                Console.WriteLine(item.Ticket
                    + " " + item.DealInternalIn?.PlacedType + " " + item.DealInternalIn?.Comment
                    + " " + item.DealInternalOut?.PlacedType + " " + item.DealInternalOut?.Comment);
            Console.WriteLine(args.Action);
            if (args.Action == 14)
            {
                Console.WriteLine(args.Orders.Last().CloseTime);
                sender.RequestOrderHistory(args.Orders.Last().CloseTime, DateTime.Now);
            }
        }

        void ServersDat()
        {
            var servers = MT5API.LoadServersDat(@"C:\Users\sam\AppData\Roaming\MetaQuotes\Terminal\D0E8209F77C8CF37AD8BF550E51FF075\config\servers.dat");
            foreach (var server in servers)
            {
                if (server.ServerInfo != null)
                    Console.WriteLine(server.ServerInfo.ServerName);
                if (server.ServerInfoEx != null)
                    Console.WriteLine(server.ServerInfoEx.ServerName);
                foreach (var access in server.Accesses)
                    foreach (var addr in access.Addresses)
                        Console.WriteLine(addr.Address + " ping: "  /* + PingHost(addr.Address)*/);
                Console.WriteLine();
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        void ServersDatConnect()
        {
            var servers = MT5API.LoadServersDat(@"C:\Users\sam\AppData\Roaming\MetaQuotes\Terminal\D0E8209F77C8CF37AD8BF550E51FF075\config\servers.dat");
            foreach (var server in servers)
            {
                //"Coinexx-Live"
                string srv = "";
                if (server.ServerInfo != null)
                {
                    Console.WriteLine(server.ServerInfo.ServerName);
                    srv = server.ServerInfo.ServerName;
                }
                if (server.ServerInfoEx != null)
                {
                    Console.WriteLine(server.ServerInfoEx.ServerName);
                    srv = server.ServerInfoEx.ServerName;
                }
                foreach (var access in server.Accesses)
                    foreach (var addr in access.Addresses)
                    {
                        Console.WriteLine(addr.Address);
                        if (srv == "Coinexx-Live")
                        {
                            var host_port = ParseHostAndPort(addr.Address);
                            var api = new MT5API(933533, "#qQ@9XF@jja@7i3", host_port.Key, host_port.Value);
                            try
                            {
                                api.Connect();
                                Console.WriteLine("Connected");
                                Console.ReadKey();
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                Console.WriteLine();
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        public static KeyValuePair<string, int> ParseHostAndPort(string ip)
        {
            int port;
            int i = 0;
            string host = "";
            while (i < ip.Length && ip[i] != (byte)':')
                host += (char)ip[i++];
            if (i == ip.Length)
                port = 443;
            else
            {
                i++;
                string strPort = "";
                while (i < ip.Length)
                    strPort += (char)ip[i++];
                port = int.Parse(strPort);
            }
            return new KeyValuePair<string, int>(host.Trim(), port);
        }
        public static int PingHost(string nameOrAddress)
        {
            if (nameOrAddress.Contains(":"))
                nameOrAddress = nameOrAddress.Substring(0, nameOrAddress.LastIndexOf(":"));
            Ping pinger = null;
            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress, 1000);
                if (reply.Status == IPStatus.Success)
                    return (int)reply.RoundtripTime;
            }
            catch (PingException ex)
            {
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
            return -1;
        }

        void Connect()
        {
            //var api = new MT5API(18574, "uaDZ3mjC6", "13.114.200.108", 443);
            //var api = new MT5API(99083840, "ychqluy5", "112.126.96.179", 443); //xdl demo
            //var api = new MT5API(40910506, "JWT81h4i50gA", "91.134.185.136", 1950); //admiral demo
            //var api = new MT5API(40910506, "JWT81h4i50gA", "91.134.185.136", 1950); //admiral demo
            //var api = new MT5API(22098451, "test1234", "mt5demo101.loginandtrade.com", 443);
            //var api = new MT5API(1000028, "aykJc3ta", "51.143.162.218", 443);
            //var api = new MT5API(510159, "12345678!", "189.125.122.9", 443);
            //var api = new MT5API(12212677, "uuModal123", "mt5.modalmais.com.br", 443);
            //var api = new MT5API(14877, "cV5bD4tL4rP1", "78.41.199.234", 443);
            var api = new MT5API(5005878206, "eggzbi8z", "access.metatrader5.com", 443);
            api.OnConnectProgress += delegate (MT5API sender, ConnectEventArgs args)
            {
                Console.WriteLine(args.Progress);
                if (args.Progress == ConnectProgress.Disconnect)
                    sender.Connect();
            };
            api.Connect();

            string symbols = string.Join(" ", api.Symbols.Infos.Keys.ToArray());
            Console.WriteLine(symbols);
            while (true)
            {
                Console.Write(api.GetOpenedOrders().Length + " " + api.AccountProfit + " | ");
                Thread.Sleep(1000);
            }
            Console.WriteLine("Done");
            Console.ReadKey();
        }



        void Proxy()
        {
            //			85.209.177.6
            //port Socks5: 45786
            //port http: 45785
            //username: Seljerome
            //password: C6e6SyV
            //var qc = new MT5API(6011, "c24242424", "136.244.109.100", 443, "95.217.201.27", 1080, "mtapi", "test");
            var qc = new MT5API(6011, "c24242424", "136.244.109.100", 443, "85.209.177.6", 45786, "Seljerome", "C6e6SyV", ProxyTypes.Socks5);
            qc.Connect();
            Console.WriteLine("Done");
            Console.ReadKey();
        }

        void OnDisconnect()
        {
            var api = new MT5API(99083840, "ychqluy5", "112.126.96.179", 443);
            try
            {
                api.Connect();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(api.LoginIdPath + Environment.NewLine + ex.StackTrace);
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();

        }

        private void Api_OnConnectProgress(MT5API sender, ConnectEventArgs args)
        {
            Console.WriteLine(args.Progress);
        }

        void RealtimeQuotes()
        {
            var qc = new MT5API(62333850, "tecimil4", "78.140.180.198", 443);
            Console.WriteLine("Connecting...");
            qc.OnQuote += Qc_OnQuote;
            qc.Connect();
            foreach (var item in qc.GetOpenedOrders())
                Console.WriteLine(item.Ticket + " " + item.Profit);
            qc.Subscribe("EURUSD");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
            qc.Disconnect();
        }

        private void Qc_OnQuote(MT5API api, Quote quote)
        {
            Console.WriteLine(quote);
        }

        void MarketOrderMany()
        {
            //var api = new MT5API(22625866, "MRmaingold", "mt5demo101.loginandtrade.com", 443);
            //var api = new MT5API(103483, "quigrvH3", "mt5-demo02.pepperstone.com", 443);
            //var api = new MT5API(58045834, "dlvzwfn0", "78.140.180.198", 443);
            //var api = new MT5API(245139, "1qggaqwt", "148.251.41.107", 443);
            var api = new MT5API(2004, "1qwerty", "51.140.160.21", 443);
            api.Connect();
            Console.WriteLine(api.Account.Balance);
            string symbol = "EURUSD";
            var quote = api.GetQuote(symbol, 10000);
            Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff"));
            for (int i = 0; i < 50; i++)
            {
                new Thread((ThreadStart)delegate
                {
                    try
                    {
                        var o = api.OrderSend(symbol, 0.01, api.GetQuote(symbol).Ask, OrderType.Buy, 0, 0, 1000, null, 0);
                        Console.WriteLine(o.RequestId + " Order " + o.Ticket + " opened at " + o.OpenPrice);
                        o = api.OrderClose(o.Ticket, o.Symbol, api.GetQuote(symbol).Bid, o.Lots, o.OrderType, 1000);
                        Console.WriteLine(o.RequestId + " " + o.Ticket + " Closed at " + o.ClosePrice);
                        Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff"));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }).Start();
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        void MarketOrderAsync()
        {
            //var api = new MT5API(103483, "quigrvH3", "mt5-demo02.pepperstone.com", 443);
            //var api new MT5API(22625866, "MRmaingold", "mt5demo101.loginandtrade.com", 443);
            //var api = new MT5API(1820949, "ugrs4cvg", "5.9.30.186", 444);
            var api = new MT5API(24924566, "y8fepagn", "access.metatrader5.com", 443);
            api.Connect();
            Console.WriteLine("Connected");
            string symbol = "EURUSD";
            api.Subscribe(symbol);
            while (api.GetQuote(symbol) == null)
                Thread.Sleep(1);
            var quote = api.GetQuote(symbol);
            api.OnOrderProgress += Qc_OnOrderProgress;
            int reqID = api.GetRequestId();
            Console.WriteLine(DateTime.Now.ToString("hh.mm.ss.ffffff"));
            api.OrderSendAsync(reqID, symbol, 0.01, api.GetQuote(symbol).Ask, OrderType.Buy, 0, 0, 100, "comment", 0);
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        void MarketCloseAsync()
        {
            //var api = new MT5API(103483, "quigrvH3", "mt5-demo02.pepperstone.com", 443);
            //var api new MT5API(22625866, "MRmaingold", "mt5demo101.loginandtrade.com", 443);
            //var api = new MT5API(1820949, "ugrs4cvg", "5.9.30.186", 444);
            //var api = new MT5API(24924566, "y8fepagn", "access.metatrader5.com", 443);
            var api = new MT5API(27607, "xli3fbtu", "3.1.10.161", 443);
                
            api.Connect();
            Console.WriteLine("Connected");
            string symbol = "EURUSD";
            api.Subscribe(symbol);
            while (api.GetQuote(symbol) == null)
                Thread.Sleep(1);
            var quote = api.GetQuote(symbol);
            api.OnOrderProgress += Qc_OnOrderProgress;
            int reqID = api.GetRequestId();
            api.OrderCloseAsync(reqID, 2873358, symbol, api.GetQuote(symbol).Bid, 0.04, OrderType.Buy, 100);
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private void Qc_OnOrderProgress(MT5API api, OrderProgress progress)
        {
            Console.WriteLine(DateTime.Now.ToString("hh.mm.ss.ffffff") + " " + progress.TradeRequest.RequestId + " " + progress.TradeResult.Status + " " + progress.TradeRequest.Comment);
        }

        void PendingOrder()
        {
            //var api = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);
            //var api = new MT5API(23993883, "6bU3Nbqn78l0", "37.187.164.80", 443);
            var api = new MT5API(668722, "4exjryvc", "185.17.145.100", 443);
            api.Connect();
            //api.OnOrderUpdate += Qc_OnOrderUpdate;
            string symbol = "EURUSD.";
            //api.OnOrderProgress += Qc_OnOrderProgress;
            api.OnOrderUpdate += Qc_OnOrderUpdate;
            Expiration exp = new Expiration();
            exp.Type = ExpirationType.Specified;
            exp.DateTime = DateTime.Now.AddDays(1);
            var o = api.OrderSend(symbol, 0.01, 1.1, OrderType.BuyLimit, 0, 0, 0, null, 0, FillPolicy.FlashFill, TradeType.Transfer, 0, exp);
            Console.WriteLine("Order " + o.Ticket + " opened at " + o.OpenPrice);
            foreach (var item in api.GetOpenedOrders())
                Console.Write(item.Ticket + " ");
            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
            o = api.OrderClose(o.Ticket, o.Symbol, o.OpenPrice, o.Lots, o.OrderType);
            Console.WriteLine(o.State.ToString());
            foreach (var item in api.GetOpenedOrders())
                Console.Write(item.Ticket + " ");
            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        void ModifyOrder()
        {
            //var api = new MT5API(103483, "quigrvH3", "mt5-demo02.pepperstone.com", 443);
            //var api = new MT5API(22625866, "MRmaingold", "mt5demo101.loginandtrade.com", 443);
            //var api = new MT5API(500014759, "5cbfwsmx", "193.42.110.107", 443);
            //var api = new mtapi.mt5.MT5API(6030240, "vtrmcs1q", "185.212.168.30", 443);
            //MT5API api = new MT5API(24205117, "pwx3xnrv", "metaquotes.superkoud.com", 1999);
            //var api = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);
            //var api = new MT5API(18457, "HXEKon72fs", "13.114.200.108", 443);
            //var api = new MT5API(18574, "uaDZ3mjC6", "13.114.200.108", 443);
            //var api = new MT5API(24924566, "y8fepagn", "access.metatrader5.com", 443);
            var api = new MT5API(50360624, "rL5N19PR", "47.89.176.80", 443);
            Console.WriteLine("Connecting...");
            api.Connect();
            api.OnOrderUpdate += Qc_OnOrderUpdate;
            Console.WriteLine("Connected");
            string symbol = "EURUSD";
            api.Subscribe(symbol);
            while (api.GetQuote(symbol) == null)
                Thread.Sleep(1);
            var o = api.OrderSend(symbol, 0.02, api.GetQuote(symbol).Ask, OrderType.Buy, 0, 0, 100, null, 0, FillPolicy.ImmediateOrCancel);
            Console.WriteLine("Order " + o.Ticket + " opened at " + o.OpenPrice);
            api.OrderModify(o.Ticket, o.Symbol, o.Lots, o.OpenPrice, OrderType.Buy, 1.1, 1.2);
            Console.WriteLine("Modified");
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
            o = api.OrderClose(o.Ticket, o.Symbol, api.GetQuote(symbol).Bid, o.Lots, o.OrderType, 100, FillPolicy.ImmediateOrCancel);
            Console.WriteLine("Closed at " + o.ClosePrice);
            Console.WriteLine("Press any key...");
            Console.ReadKey();

        }

        void Certificate()
        {
            string certPath = @"C:\Yandex.Disk\Tim\docs\tmp\2041174_HungDingFinancial_22.pfx";
            string certPass = "Dev@1234";
            var api = new MT5API(2041174, "Dev@1234", "20.48.6.157", 443, File.ReadAllBytes(certPath), certPass);
            api.Connect();
            string symbol = "USDCHF";
            Console.WriteLine("Connected");
            var o = api.OrderSend(symbol, 0.01, 0, OrderType.Buy);
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        void Serialize()
        {
            var api = new MT5API(67058082, "MyPassword", "188.72.232.121", 4432);
            Console.WriteLine("Connecting...");
            api.Connect();
            string str = JsonConvert.SerializeObject(api, Formatting.Indented);
            var res = JsonConvert.DeserializeObject<MT5API>(str);
            res.Connect();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        void MarketOrder()
        {
            var api = new MT5API(67058082, "MyPassword", "188.72.232.121", 4432);
            api.OnOrderUpdate += Qc_OnOrderUpdate;
            Console.WriteLine("Connecting...");
            api.Connect();
            foreach (var item in api.GetOpenedOrders())
            {
                Console.WriteLine(item.Ticket);
            }
            string symbol = "USDCHF";
            while (api.GetQuote(symbol) == null)
                Thread.Sleep(1);
            Console.WriteLine("Connected");
            DateTime start = DateTime.Now;
            var o = api.OrderSend(symbol, 0.01, api.GetQuote(symbol).Ask, OrderType.Buy, deviation: 1000);
            Console.WriteLine("Open time(ms) " + DateTime.Now.Subtract(start).TotalMilliseconds);
            Console.WriteLine("Order " + o.Ticket + " opened at " + o.OpenPrice);
            foreach (var item in api.GetOpenedOrders())
            {
                Console.WriteLine(item.Ticket);
            } 
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
            start = DateTime.Now;
            o = api.OrderClose(o.Ticket, o.Symbol, api.GetQuote(symbol).Bid, 0.01, o.OrderType, deviation: 1000);
            var tmp2 = api.GetOpenedOrders();
            Console.WriteLine("Close time(ms) " + DateTime.Now.Subtract(start).TotalMilliseconds);
            Console.WriteLine("Closed at " + o.ClosePrice + " " + o.CloseTime);
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private void Api_OnOrderProgress(MT5API sender, OrderProgress progress)
        {
            Console.WriteLine(progress.TradeResult.Status + " " + progress.TradeRequest.RequestId);
        }

        void CloseBy()
        {
            //var api = new MT5API(103483, "quigrvH3", "mt5-demo02.pepperstone.com", 443);
            //var api = new MT5API(22625866, "MRmaingold", "mt5demo101.loginandtrade.com", 443);
            //var api = new MT5API(1101, "demo123", "m5875.contaboserver.net", 443);
            //var api = new MT5API(9992287, "ihhj8qkd", "185.96.244.15", 443); //coinex demo
            //var api = new MT5API(35393967, "syg7izhp", "51.255.159.171", 443);
            //var api = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);
            //var api = new MT5API(38475530, "pdufws7c", "78.140.180.198", 443); //mq
            var api = new MT5API(90431179, "Epcar74029", "191.238.216.181", 4431);
            api.Connect();
            Console.WriteLine("Connected. ");
            string symbol = "ABCB4";
            api.Subscribe(symbol);
            while (api.GetQuote(symbol) == null)
                Thread.Sleep(1);
            var quote = api.GetQuote(symbol);
            //api.OnOrderProgress += Qc_OnOrderProgress;
            var o = api.OrderSend(symbol, 100, quote.Ask, OrderType.Buy, 0, 0, 1000);
            Console.WriteLine("Order " + o.Ticket + " opened at " + o.OpenPrice);
            var o2 = api.OrderSend(symbol, 100, quote.Bid, OrderType.Sell, 0, 0, 1000);
            Console.WriteLine("Order " + o2.Ticket + " opened at " + o2.OpenPrice);
            foreach (var item in api.GetOpenedOrders())
                Console.Write(item.Ticket + " ");
            Console.WriteLine();
            Console.WriteLine("Press any key to close order..");
            Console.ReadKey();
            quote = api.GetQuote(symbol);
            o = api.OrderClose(o.Ticket, o.Symbol, 0, o.Lots, o.OrderType, 0, FillPolicy.FillOrKill, 0, null, o2.Ticket);
            Console.WriteLine("Closed at " + o.ClosePrice + ". Press any key...");
            foreach (var item in api.GetOpenedOrders())
                Console.Write(item.Ticket + " ");
            Console.WriteLine();
            Console.ReadKey();
        }

        void MarketOrder2()
        {
            //var api = new MT5API(103483, "quigrvH3", "mt5-demo02.pepperstone.com", 443);
            //var api = new MT5API(22625866, "MRmaingold", "mt5demo101.loginandtrade.com", 443);
            //var api = new MT5API(1101, "demo123", "m5875.contaboserver.net", 443);
            //var api = new MT5API(9992287, "ihhj8qkd", "185.96.244.15", 443); //coinex demo
            //var api = new MT5API(35393967, "syg7izhp", "51.255.159.171", 443);
            //var api = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);
            //MT5API api = new MT5API(9956, "TYTY1212", "2.58.47.226", 443);
            //var api = new MT5API(41694, "rAqMT3Ka", "185.17.145.100", 443);
            //var api = new MT5API(41368, "aQoFu2qb", "185.17.145.100", 443); //eu brokerage live
            var api = new MT5API(2202262, "P11MWBql4iN9b4", "52.47.42.117", 4445); //sc live

            api.Connect();
            api.OnOrderUpdate += delegate (MT5API sender, OrderUpdate update)
            {
                if (update.Type == UpdateType.OnStopLoss)
                    Console.WriteLine("------ SL" + update.Order.ClosePrice);
                if (update.Type == UpdateType.MarketOpen)
                    Console.WriteLine("------ Open " + update.Order.OpenPrice);
                else if (update.Type == UpdateType.MarketClose)
                    Console.WriteLine("------ Close" + update.Order.ClosePrice);

            };
            Console.WriteLine("Connected. ");
            string symbol = "EURUSD";//api.Symbols.Infos.Keys.ToArray()[1];
            api.Subscribe(symbol);
            while (api.GetQuote(symbol) == null)
                Thread.Sleep(1);
            Thread.Sleep(1000);
            var quote = api.GetQuote(symbol);
            //api.OnOrderProgress += Qc_OnOrderProgress;
            var o = api.OrderSend(symbol, 0.01, api.GetQuote(symbol).Ask, OrderType.Buy, 0, 0, 0, null, 0, FillPolicy.ImmediateOrCancel);
            Console.WriteLine("Order " + o.Ticket + " opened at " + o.OpenPrice);
            foreach (var item in api.GetOpenedOrders())
                Console.Write(item.Ticket + " " + item.Lots + " ");
            o = api.OrderClose(o.Ticket, o.Symbol, api.GetQuote(symbol).Bid, o.Lots, o.OrderType, 0, FillPolicy.ImmediateOrCancel);
            Console.WriteLine("Closed at " + o.ClosePrice + ". Press any key...");
            foreach (var item in api.GetOpenedOrders())
                Console.Write(item.Ticket + " " + item.Lots + " ");
            Console.WriteLine();
            Console.ReadKey();
        }

        void OpenedOrders()
        {
            //MT5API qc = new MT5API(103483, "quigrvH3", "mt5-demo02.pepperstone.com", 443);
            //MT5API qc = new MT5API(24205117, "pwx3xnrv", "metaquotes.superkoud.com", 1999);
            //MT5API qc = new MT5API(40788302, "E0K8vi5o1oV9", "51.38.84.252", 443);
            //var qc = new MT5API(18574, "uaDZ3mjC6", "13.114.200.108", 443);
            //var qc = new MT5API(18574, "uaDZ3mjC6", "13.114.200.108", 443);
            //var qc = new MT5API(111111, "kzdx3zxt", "46.235.34.40", 443);
            //var qc = new MT5API(1090003266, "Fczrcrp302", "95.168.183.92", 443);
            //var qc = new MT5API(5027, "zt8zkszg", "193.30.23.27", 443);
            //var qc = new MT5API(5031, "fgg#42!dggr", "37.188.119.195", 443);
            //var qc = new MT5API(5031, "fgg#42!dggr", "193.30.23.27", 443);
            var qc = new MT5API(8631960, "dd4444vv", "mt5demo.acetopfx.com", 443);
            Console.WriteLine("Connecting...");
            qc.Connect();
            Console.WriteLine("Connected");
            foreach (var item in qc.GetOpenedOrders())
                Console.WriteLine(item.Ticket + " " + item.Lots);
            while (!Console.KeyAvailable)
            {
                foreach (var item in qc.GetOpenedOrders())
                    Console.WriteLine(item.Ticket + " " + item.Lots);
                Console.WriteLine(qc.Account.Balance);
                Thread.Sleep(1000);
                Console.WriteLine("--");
            }
            //Console.WriteLine("Press any key...");
            //Console.ReadKey();
        }

        void OrderUpdate()
        {
            //var api = new MT5API(933533, "#qQ@9XF@jja@7i3", "50.28.102.111", 443);
            var api = new MT5API(6011, "c24242424", "136.244.109.100", 443);
            api.OnOrderUpdate += Qc_OnOrderUpdate;
            api.Connect();
            Console.WriteLine("Connected");
            Console.WriteLine(api.Account.Balance + api.AccountCurrency);
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }


        private void Qc_OnOrderUpdate(MT5API api, OrderUpdate update)
        {

            Console.WriteLine(update.Type + " " + update.Order?.Ticket + " " + update.Order?.DealType + " " + " " + update.Order?.OrderType
                    + " " + " " + update.Order?.ClosePrice + " " + update.Deal?.PlacedType + " " + update.OppositeDeal?.PlacedType);
        }

        static readonly object Lock = new object();

        static void Logger_OnMsg(object sender, string msg, Logger.MsgType type)
        {
            lock (Lock)
            {
                string txt = type.ToString().PadLeft(5) + ": " + msg;
                if ((int)type >= (int)Logger.MsgType.Warn)
                    Console.WriteLine(txt);
            }
        }
    }
}



//Type type = typeof(MT5API);
//string str = "";
//foreach (var meth in type.GetMethods())
//{
//    //str += $"public {meth.}"
//    str += $"{Format(meth.ReturnType.ToString())} {meth.Name}(";
//    foreach (var param in meth.GetParameters())
//    {
//        str += $"{Format(param.ParameterType.ToString())} {param.Name},";
//    }
//    str = str.Substring(0, str.Length - 1);
//    str += $"){Environment.NewLine}";
//}
