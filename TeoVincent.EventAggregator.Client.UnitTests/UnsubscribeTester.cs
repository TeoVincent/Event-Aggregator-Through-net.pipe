﻿using System;
using System.Collections.Generic;
using System.Threading;
using Rhino.Mocks;
using TeoVincent.EA.Client.UnitTests.EventMocks;
using TeoVincent.EA.Client.UnitTests.ListenerMocks;
using TeoVincent.EA.Common;
using Xunit;

namespace TeoVincent.EA.Client.UnitTests
{
    public class UnsubscribeTester
    {
        private readonly InternalEventAggregator eventAggregator;

        public UnsubscribeTester()
        {
            var syncContexts = new SynchronizationContext();
            eventAggregator = new InternalEventAggregator(syncContexts);
        }

        [Fact]
        public void Unsubscribe_Validate()
        {
            // 1) arrange
            var listener = new Simple_MockListener();

            // 2) act
            eventAggregator.Subscribe(listener);
            eventAggregator.Unsubscribe(listener);
            Dictionary<Type, List<IListener>> listeners = FieldReflector.GetListeners(eventAggregator);

            // 3) assert
            Assert.DoesNotContain(listener, listeners[typeof(Simple_MockEvent)]);
        }

        [Fact]
        public void Unsubscribe_Not_Exists_Validate()
        {
            // 1) arrange
            var listener = new Simple_MockListener();
            var twiceListener = new CallHandleCounter_ForTwoEvents_MockListener();

            // 2) act
            eventAggregator.Subscribe(listener);
            eventAggregator.Unsubscribe<Simple_MockEvent>(twiceListener);
            Dictionary<Type, List<IListener>> listeners = FieldReflector.GetListeners(eventAggregator);

            // 3) assert
            Assert.Contains(listener, listeners[typeof(Simple_MockEvent)]);
        }

        [Fact]
        public void Unsubscribe_Generic_Validate()
        {
            // 1) arrange
            var listener = MockRepository.GenerateMock<IListener<Simple_MockEvent>>();

            // 2) act
            eventAggregator.Subscribe(listener);
            eventAggregator.Unsubscribe(listener);
            Dictionary<Type, List<IListener>> listeners = FieldReflector.GetListeners(eventAggregator);

            // 3) assert
            Assert.DoesNotContain(listener, listeners[typeof(Simple_MockEvent)]);
        }

        [Fact]
        public void Unsubscribe_More_The_One_Validate()
        {
            // 1) arrange
            var listenerOne = MockRepository.GenerateMock<IListener<Simple_MockEvent>>();
            var listenerTwo = new Simple_MockListener();

            // 2) act
            eventAggregator.Subscribe(listenerOne);
            eventAggregator.Subscribe(listenerTwo);
            eventAggregator.Unsubscribe(listenerOne);
            eventAggregator.Unsubscribe(listenerTwo);
            Dictionary<Type, List<IListener>> listeners = FieldReflector.GetListeners(eventAggregator);

            // 3) assert
            Assert.DoesNotContain(listenerOne, listeners[typeof(Simple_MockEvent)]);
            Assert.DoesNotContain(listenerTwo, listeners[typeof(Simple_MockEvent)]);
        }

        [Fact]
        public void Unsubscribe_Listener_For_Two_Type_Events_Validate()
        {
            // 1) arrange
            var listener = new CallHandleCounter_ForTwoEvents_MockListener();

            // 2) act
            eventAggregator.Subscribe(listener);
            eventAggregator.Unsubscribe(listener);
            Dictionary<Type, List<IListener>> listeners = FieldReflector.GetListeners(eventAggregator);

            // 3) assert
            Assert.DoesNotContain(listener, listeners[typeof(Simple_MockEvent)]);
            Assert.DoesNotContain(listener, listeners[typeof(Another_MockEvent)]);
        }

        [Fact]
        public void Unsubscribe_Listener_Only_One_Type_Of_Event_Validate()
        {
            // 1) arrange
            var listener = new CallHandleCounter_ForTwoEvents_MockListener();

            // 2) act
            eventAggregator.Subscribe(listener);
            eventAggregator.Unsubscribe<Simple_MockEvent>(listener);
            Dictionary<Type, List<IListener>> listeners = FieldReflector.GetListeners(eventAggregator);

            // 3) assert
            Assert.DoesNotContain(listener, listeners[typeof(Simple_MockEvent)]);
            Assert.Contains(listener, listeners[typeof(Another_MockEvent)]);
        }

        [Fact]
        public void Unsubscribe_Not_Contains_Listener()
        {
            // 1) arrange
            var listener = new Simple_MockListener();

            // 2) act
            eventAggregator.Unsubscribe(listener);

            // 3) assert
            Dictionary<Type, List<IListener>> listeners = FieldReflector.GetListeners(eventAggregator);
            Assert.DoesNotContain(typeof(Simple_MockEvent), listeners.Keys);
        }
    }
}