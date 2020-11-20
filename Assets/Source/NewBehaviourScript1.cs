using UnityEngine;
using System.Collections;

namespace Assets
{
    public class Events
    {
        public void EventProcess(byte eventCode, object content, int senderId)
        {
            switch (eventCode)
            {
                case 0: JoinEvent(); break;
            }
        }
        /// <summary>
        /// 他のプレイヤーが参加したとき発生するイベント。
        /// </summary>
        public virtual void JoinEvent()
        {
            
        }
        public virtual void Event1()
        {

        }
    }
}