using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DDnsSharp.Monitor.Utils
{
    public class TimerDispatch
    {
        private static object locker = new object();
        private static TimerDispatch dispath;

        public static TimerDispatch Current
        {
            get
            {
                lock (locker)
                {
                    if (dispath == null)
                    {
                        lock (locker)
                        {
                            dispath = new TimerDispatch();
                            dispath.lastTimeStamp = DateTime.UtcNow;
                        }
                    }
                }
                return dispath;
            }
        }

        public TimerDispatch()
        {
            models = new List<TimerModel>();
            timer = new Timer(onTick, this, 0, 1000);
        }

        private Timer timer;
        private List<TimerModel> models;
        private DateTime lastTimeStamp;

        public TimerModel AddInterval(Action<TimerModel> action, int interval, int times = -1)
        {
            var model = new TimerModel()
            {
                Interval = interval,
                Times = times,
                LastTimeStamp = DateTime.UtcNow,
                OnTick = action
            };
            models.Add(model);
            return model;
        }

        public TimerModel AddTimeout(Action<TimerModel> action, int timeout)
        {
            var model = new TimerModel()
            {
                Interval = timeout,
                Times = 1,
                LastTimeStamp = DateTime.UtcNow,
                OnTick = action
            };
            models.Add(model);
            return model;
        }

        public void RemoveTimer(TimerModel m)
        {
            models.Remove(m);
        }

        private void onTick(object state)
        {
            var dispatch = state as TimerDispatch;
            var i = models.Count;
            while (i-- > 0)
            {
                var now = DateTime.UtcNow;
                TimeSpan ts = now - dispath.lastTimeStamp;

                var t = models[i];
                if (t.LastTimeStamp.AddMilliseconds(t.Interval) <= now)
                {
                    t.LastTimeStamp = now;
                    t.OnTick(t);
                    if(t.Times>0)
                        t.Times--;
                }
                if (t.Times == 0)
                {
                    models.RemoveAt(i);
                    i++;
                }
                dispath.lastTimeStamp = now;
            }
        }
    }

    public class TimerModel
    {
        public Action<TimerModel> OnTick;
        public int Interval;
        public int Times;
        public DateTime LastTimeStamp;
    }
}
