using System;
using System.IO;
using System.Linq;

namespace Problem2_1
{
    enum Direction
    {
        FORWARD,
        UP,
        DOWN
    }

    class Command
    {
        public Command(Direction direction, int amount)
        {
            Direction = direction;
            this.amount = amount;
        }

        public Direction Direction { get; }
        public int amount { get; }
    }

    class Submarine
    {
        public Submarine(int depth = 0, int position = 0)
        {
            this.depth = depth;
            this.position = position;
        }

        public Submarine Move(Command command)
        {
            switch (command.Direction)
            {
                case Direction.FORWARD:
                    return new Submarine(depth, position + command.amount);
                case Direction.UP:
                    return new Submarine(depth - command.amount, position);
                case Direction.DOWN:
                    return new Submarine(depth + command.amount, position);
                default:
                    throw new Exception("wut");
            }
        }

        public int depth { get; }
        public int position { get; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var app = new Program();
            app.Run();
        }

        private void Run()
        {
            var data = File.ReadAllLines("data.txt");
            var commands = data.Select(item => ParseCommand(item));
            var finalState = commands.Aggregate(new Submarine(), (currentState, command) => currentState.Move(command), (finalState) => finalState.depth * finalState.position);
            Console.WriteLine(finalState);
        }

        private Command ParseCommand(string data)
        {
            var commandParts = data.Split(' ');
            return new Command(Enum.Parse<Direction>(commandParts[0].ToUpper()), int.Parse(commandParts[1]));
        }
    }
}
