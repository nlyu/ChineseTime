using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChineseTime.Data
{
    internal interface IChineseTimeDataManager
    {
        public ChineseTimeByHour GetChineseTimeDataFromHour(int hour);

        public void AddChineseTimeByHour(int hour, ChineseTimeByHour data);
    }
}
