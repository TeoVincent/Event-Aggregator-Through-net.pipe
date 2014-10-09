﻿#region Licence
// The MIT License (MIT)
// 
// Copyright (c) 2014 TeoVincent Artur Wincenciak
// TeoVincent.EventAggregator2013
// TeoVincent.OwnNewApllication
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeoVincent.EventAggregator.Client;
using TeoVincent.EventAggregator.Common;
using TeoVincent.EventAggregator.Common.Events.Example;

namespace TeoVincent.OwnNewApllication
{
    internal class MyOwnNewListener : IListener<MyExampleEvent>, IListener<MyNewOwnEvent>
    {
        public void Handle(MyExampleEvent a_receivedEvent)
        {
            Console.WriteLine("Do sth...");
        }

        public void Handle(MyNewOwnEvent a_receivedEvent)
        {
            Console.WriteLine("My new own event in my new own listener... {0}", 
                a_receivedEvent.Data);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            EventAggregatorClient.Instance.SubscribePlugin("MY NEW OWN  APP");
            var myOwnNewListener = new MyOwnNewListener();
            EventAggregatorClient.Instance.Subscribe(myOwnNewListener);

            EventAggregatorClient.Instance.GlobalPublish(new MyExampleEvent());

            Console.ReadKey();
        }
    }
}
