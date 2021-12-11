using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC.App.Problems.Day11
{
    enum MessageType
    {
        INCREASE_ENERGY,
        RESET_ENERGY,
        OCTOPUS_FLASHED
    }
    abstract class Message
    {
        public Message(MessageType type)
        {
            Type = type;
        }

        public MessageType Type { get; }
    }
    abstract class MessageWithData<T> : Message
    {
        public MessageWithData(MessageType type, T data)
            : base(type)
        {
            Data = data;
        }

        public T Data { get; }
    }

    class IncreaseEnergyMessage : Message
    {
        public IncreaseEnergyMessage()
            : base(MessageType.INCREASE_ENERGY)
        {}
    }

    class ResetEnergyMessage : Message
    {
        public ResetEnergyMessage()
            : base(MessageType.RESET_ENERGY)
        {}

    }

    class OctopusFlashedMessage : MessageWithData<(int, int)>
    {
        public OctopusFlashedMessage((int, int) location)
            : base(MessageType.OCTOPUS_FLASHED, location)
        {
        }
    }
    class Publisher
    {
        public Publisher()
        {
            _messages = new Queue<Message>();
            _subscribers = new Dictionary<MessageType, List<Octopus>>();
        }
        public void SendMessage(Message message)
        {
            if (_messages.Count == 0)
            {
                _messages.Enqueue(message);

                while (_messages.Count != 0)
                {
                    var currentMessage = _messages.Peek();
                    if (_subscribers.ContainsKey(currentMessage.Type))
                    {
                        foreach (var octopus in _subscribers[currentMessage.Type])
                        {
                            octopus.HandleMessage(currentMessage);
                        }
                    }

                    _messages.Dequeue();
                }
            }
            else
            {
                _messages.Enqueue(message);
            }       
        }

        public void AddSubscriber(MessageType type, ref Octopus octopus)
        {
            if (!_subscribers.ContainsKey(type))
            {
                _subscribers[type] = new List<Octopus>();
            }

            _subscribers[type].Add(octopus);
        }

        private Queue<Message> _messages;
        private Dictionary<MessageType, List<Octopus>> _subscribers;
    }
    class Octopus
    {
        public Octopus((int, int) location, int energyLevel, Publisher publisher)
        {
            this._location = location;
            _energyLevel = energyLevel;
            this._publisher = publisher;
            _flashCount = 0;
        }

        public void HandleMessage(Message message)
        {
            switch (message.Type)
            {
                case MessageType.INCREASE_ENERGY:
                {
                    _energyLevel += 1;
                    
                    if (_energyLevel > 9)
                    {
                        _flashCount += 1;
                        _publisher.SendMessage(new OctopusFlashedMessage(_location));
                    }

                    return;
                }
                case MessageType.RESET_ENERGY:
                {
                    if (_energyLevel > 9)
                    {
                        _energyLevel = 0;
                    }

                    return;
                }
                case MessageType.OCTOPUS_FLASHED:
                {
                    if (_energyLevel > 9)
                    {
                        return;
                    }

                    var messageWithData = (OctopusFlashedMessage)message;

                    if (
                        messageWithData.Data == (_location.Item1, _location.Item2 + 1) ||
                        messageWithData.Data == (_location.Item1, _location.Item2 - 1) ||
                        messageWithData.Data == (_location.Item1 + 1, _location.Item2) ||
                        messageWithData.Data == (_location.Item1 - 1, _location.Item2) ||
                        messageWithData.Data == (_location.Item1 - 1, _location.Item2 - 1) ||
                        messageWithData.Data == (_location.Item1 - 1, _location.Item2 + 1) ||
                        messageWithData.Data == (_location.Item1 + 1, _location.Item2 - 1) ||
                        messageWithData.Data == (_location.Item1 + 1, _location.Item2 + 1)
                    )
                    {
                        _energyLevel += 1;

                        if (_energyLevel > 9)
                        {
                            _flashCount += 1;
                            _publisher.SendMessage(new OctopusFlashedMessage(_location));
                        }
                    }
                    
                    return;
                }
                default:
                {
                    return;
                }
            }
        }

        public override string ToString()
        {
            return _energyLevel.ToString();
        }

        public int FlashCount {
            get
            {
                return _flashCount;
            }
        }

        private readonly (int, int) _location;
        private int _energyLevel;
        private readonly Publisher _publisher;
        private int _flashCount;
    }

    [ProblemName("11_1")]
    public class Problem11_1Solver : BaseSolver
    {
        public override void Solve()
        {
            var input = GetData();
            var stopwatch = Stopwatch.StartNew();

            List<Octopus> octopuses = new List<Octopus>();
            var publisher = new Publisher();
            var row = 0;
            foreach (var line in input)
            {
                var column = 0;
                foreach (var energyLevel in line.Select(c => int.Parse(c.ToString())))
                {
                    var octopusObject = new Octopus((row, column), energyLevel, publisher);
                    octopuses.Add(octopusObject);
                    publisher.AddSubscriber(MessageType.INCREASE_ENERGY, ref octopusObject);
                    publisher.AddSubscriber(MessageType.OCTOPUS_FLASHED, ref octopusObject);
                    publisher.AddSubscriber(MessageType.RESET_ENERGY, ref octopusObject);
                    column++;
                }
                row++;
            }

            for (var step = 0; step < 100; step++)
            {
                publisher.SendMessage(new IncreaseEnergyMessage());
                publisher.SendMessage(new ResetEnergyMessage());
                //Console.WriteLine($"After step {step + 1}:\n{PrintOctopusEnergy(octopuses)}");
            }

            var flashCount = octopuses.Select(o => o.FlashCount).Sum();

            stopwatch.Stop();

            Console.WriteLine(flashCount);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }

        private string PrintOctopusEnergy(IEnumerable<Octopus> octopuses)
        {
            var position = 0;
            string result = "";
            foreach (var octopus in octopuses)
            {
                result += octopus.ToString();
                if ((position % 10) == 9)
                {
                    result += '\n';
                }

                position++;
            }

            return result;
        }
    }
}