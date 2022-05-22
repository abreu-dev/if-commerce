﻿using System;

namespace IfCommerce.Core.Messaging
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }
        public DateTime Timestamp { get; private set; }

        protected Message()
        {
            MessageType = GetType().Name;
            Timestamp = DateTime.UtcNow;
        }
    }
}
