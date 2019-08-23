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
        private static Message _message;
        public static Message Instence
        {
            get
            {
                if (_message == null)
                {
                    _message = new Message();
                }
                return _message;
            }
        }
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
        private Dictionary<string, Subject> subjects = new Dictionary<string, Subject>();
        /// <summary>
        /// 添加监听对象
        /// </summary>
        /// <param name="name"></param>
        public void AttachSubject(string name)
        {
            if (subjects.ContainsKey(name)) return;
            Subject _subject = new Subject();
            subjects.Add(name, _subject);
        }
        /// <summary>
        /// 移除监听对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        public void DetachSubject(string name)
        {
            if (subjects.ContainsKey(name))
                subjects.Remove(name);
        }
        /// <summary>
        /// 添加监听事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="listenEvent"></param>
        public void AttachObseverEvent(string name, ListenEvent listenEvent)
        {
            Subject _subject;
            if (subjects.TryGetValue(name, out _subject))
            {
                _subject.listen += listenEvent;
            }
        }
        /// <summary>
        /// 移除监听事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="listenEvent"></param>
        public void DetachObseverEvent(string name, ListenEvent listenEvent)
        {
            Subject _subject;
            if (subjects.TryGetValue(name, out _subject))
            {
                _subject.listen -= listenEvent;
            }
        }
        public void Notify(string name,params object[] data)
        {
            Subject _subject;
            if (subjects.TryGetValue(name,out _subject))
            {
                _subject.Notify(data);
            }
        }
        internal class Subject
        {
            internal ListenEvent listen;
            public void Notify(params object[] data)
            {
                listen(data);
            }
        }
    }
}
