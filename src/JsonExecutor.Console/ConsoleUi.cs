using System;
using JsonExecutor.Console.Model;

namespace JsonExecutor.Console
{
    internal class ConsoleUi
    {
        /// <summary>
        /// Synchronization object as console calls should be locked to appear colors properly under threading.
        /// </summary>
        private static readonly object SyncObject = new object();

        private readonly ConsoleColor _color;
        public ConsoleUi(MessageType type)
        {
            switch (type)
            {
                case MessageType.Success:
                    this._color = ConsoleColor.Green;
                    break;
                case MessageType.Fail:
                    this._color = ConsoleColor.Red;
                    break;
                case MessageType.Info:
                    this._color = ConsoleColor.Cyan;
                    break;
            }
        }

        public void Show(string message)
        {
            var backColor = System.Console.ForegroundColor;
            lock (SyncObject)
            {
                try
                {
                    System.Console.ForegroundColor = this._color;
                    System.Console.WriteLine(message);
                }
                finally
                {
                    System.Console.ForegroundColor = backColor;
                }
            }
        }

    }
}
