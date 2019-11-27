using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;
using UnityEngine;

namespace Knight.Core
{
    public class Message
    {

        public delegate void ListenEvent(params object[] data);
        //private static Dictionary<string, ListenEvent> message = new Dictionary<string, ListenEvent>();
        //public void AttachEvent(string name, ListenEvent listenEvent)
        //{
        //    ListenEvent _listen;
        //    if (!message.TryGetValue(name, out _listen))
        //    {
        //        message.Add(name, _listen);
        //    }
        //}
        //public void DetachEvent(string name)
        //{
        //    if (message.ContainsKey(name)) message.Remove(name);
        //}
        //public void SendMessage(string name, params object[] data)
        //{
        //    ListenEvent listen;
        //    if (message.TryGetValue(name, out listen))
        //    {
        //        try
        //        {
        //            listen(data);
        //        }
        //        finally
        //        {
        //        }

        //    }
        //}
        private static Dictionary<string, _Subject> subjects = new Dictionary<string, _Subject>();

        private Message()
        {
        }

        /// <summary>
        /// 添加监听对象
        /// </summary>
        /// <param name="name"></param>
        public static Subject AttachSubject(string name)
        {
            if (subjects.ContainsKey(name)) return null;
            _Subject _subject = new _Subject();
            _subject.name = name;
            subjects.Add(name, _subject);
            return _subject;
        }
        /// <summary>
        /// 移除监听对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        public static void DetachSubject(string name)
        {
            if (subjects.ContainsKey(name))
                subjects.Remove(name);
        }
        /// <summary>
        /// 添加监听事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="listenEvent"></param>
        public static void AttachObseverEvent(string name, ListenEvent listenEvent)
        {
            _Subject _subject;
            if (!subjects.TryGetValue(name, out _subject))
            {
                AttachSubject(name);
            }
            subjects.TryGetValue(name, out _subject);
            _subject.listen += listenEvent;
        }
        /// <summary>
        /// 移除监听事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="listenEvent"></param>
        public static void DetachObseverEvent(string name, ListenEvent listenEvent)
        {
            _Subject _subject;
            if (subjects.TryGetValue(name, out _subject))
            {
                _subject.listen -= listenEvent;
            }
        }
        public static void Notify(string name, params object[] data)
        {
            _Subject _subject;
            if (subjects.TryGetValue(name, out _subject))
            {
                _subject.Notify(data);
            }
        }
        internal class _Subject : Subject
        {
            internal ListenEvent listen;
            public string name;
            public override void Detach()
            {
                DetachSubject(name);
            }

            public override string Name()
            {
                return name;
            }

            public override void Notify(params object[] data)
            {
                if (listen == null) return;
                listen(data);
            }

        }
    }
    public abstract class Subject
    {
        public abstract void Notify(params object[] data);
        public abstract void Detach();
        public abstract string Name();
    }
}
