using System;
using System.Collections.Generic;

namespace Katas
{
    public enum TcpStates
    {
        CLOSED,
        LISTEN,
        SYN_SENT,
        SYN_RCVD,
        ESTABLISHED,
        CLOSE_WAIT,
        LAST_ACK,
        FIN_WAIT_1,
        FIN_WAIT_2,
        CLOSING,
        TIME_WAIT
    }

    public enum TcpEvents
    {
        APP_PASSIVE_OPEN,
        APP_ACTIVE_OPEN,
        APP_SEND,
        APP_CLOSE,
        APP_TIMEOUT,
        RCV_SYN,
        RCV_ACK,
        RCV_SYN_ACK,
        RCV_FIN,
        RCV_FIN_ACK
    }

    public static class TcpFsm
    {
        private static Dictionary<(TcpStates, TcpEvents), TcpStates> _fsm = new() 
        {
            [(TcpStates.CLOSED, TcpEvents.APP_PASSIVE_OPEN)] = TcpStates.LISTEN,
            [(TcpStates.CLOSED, TcpEvents.APP_ACTIVE_OPEN)] = TcpStates.SYN_SENT,
            [(TcpStates.LISTEN, TcpEvents.RCV_SYN)] = TcpStates.SYN_RCVD,
            [(TcpStates.LISTEN, TcpEvents.APP_SEND)] = TcpStates.SYN_SENT,
            [(TcpStates.LISTEN, TcpEvents.APP_CLOSE)] = TcpStates.CLOSED,
            [(TcpStates.SYN_RCVD, TcpEvents.APP_CLOSE)] = TcpStates.FIN_WAIT_1,
            [(TcpStates.SYN_RCVD, TcpEvents.RCV_ACK)] = TcpStates.ESTABLISHED,
            [(TcpStates.SYN_SENT, TcpEvents.RCV_SYN)] = TcpStates.SYN_RCVD,
            [(TcpStates.SYN_SENT, TcpEvents.RCV_SYN_ACK)] = TcpStates.ESTABLISHED,
            [(TcpStates.SYN_SENT, TcpEvents.APP_CLOSE)] = TcpStates.CLOSED,
            [(TcpStates.ESTABLISHED, TcpEvents.APP_CLOSE)] = TcpStates.FIN_WAIT_1,
            [(TcpStates.ESTABLISHED, TcpEvents.RCV_FIN)] = TcpStates.CLOSE_WAIT,
            [(TcpStates.FIN_WAIT_1, TcpEvents.RCV_FIN)] = TcpStates.CLOSING,
            [(TcpStates.FIN_WAIT_1, TcpEvents.RCV_FIN_ACK)] = TcpStates.TIME_WAIT,
            [(TcpStates.FIN_WAIT_1, TcpEvents.RCV_ACK)] = TcpStates.FIN_WAIT_2,
            [(TcpStates.CLOSING, TcpEvents.RCV_ACK)] = TcpStates.TIME_WAIT,
            [(TcpStates.FIN_WAIT_2, TcpEvents.RCV_FIN)] = TcpStates.TIME_WAIT,
            [(TcpStates.TIME_WAIT, TcpEvents.APP_TIMEOUT)] = TcpStates.CLOSED,
            [(TcpStates.CLOSE_WAIT, TcpEvents.APP_CLOSE)] = TcpStates.LAST_ACK,
            [(TcpStates.LAST_ACK, TcpEvents.RCV_ACK)] = TcpStates.CLOSED
        };
        public static string TraverseStates(string[] events)
        {
            var state = TcpStates.CLOSED;
            foreach (var @event in events)
            {
                if (!Enum.TryParse(@event, out TcpEvents currentEvent))
                {
                    return "ERROR";
                }

                if (!_fsm.TryGetValue((state, currentEvent), out state))
                {
                    return "ERROR";
                }
            }

            return state.ToString();
        }
    }
}