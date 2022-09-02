using System;

namespace Lesson
{
    public class FSMachine
    {
        public enum States
        {
            Start,
            Standby,
            On
        };
        public States State { get; set; }
        public enum Events
        {
            PlugIn,
            TurnOn,
            TurnOff,
            RemovePower
        };
        private Action[,] fsm;

        public FSMachine()
        {
            this.fsm = new Action[3, 4] {
                { this.PowerOn, null,                null,               null         },
                { null,         this.StandbyWhenOff, null,               this.PowerOff},
                { null,         null,                this.StandbyWhenOn, this.PowerOff} };
        }

        public void ProcessEvent(Events events)
        {
            this.fsm[(int)this.State, (int)events].Invoke();
        }
        private void PowerOn()
        {
            this.State = States.Standby;
        }
        private void PowerOff()
        {
            this.State = States.Start;
        }
        private void StandbyWhenOn()
        {
            this.State = States.Standby;
        }
        private void StandbyWhenOff()
        {
            this.State = States.On;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var fsm = new FSMachine();
            Console.WriteLine(fsm.State);
            fsm.ProcessEvent(FSMachine.Events.PlugIn);
            Console.WriteLine(fsm.State);
            fsm.ProcessEvent(FSMachine.Events.TurnOn);
            Console.WriteLine(fsm.State);
            fsm.ProcessEvent(FSMachine.Events.TurnOff);
            Console.WriteLine(fsm.State);
            fsm.ProcessEvent(FSMachine.Events.TurnOn);
            Console.WriteLine(fsm.State);
            fsm.ProcessEvent(FSMachine.Events.RemovePower);
            Console.WriteLine(fsm.State);
        }
    }
}
