using System;
using System.Collections.Generic;

namespace ChineseTime.Data
{
    internal class ChineseTimeDataManager : IChineseTimeDataManager
    {
        private readonly IDictionary<int, ChineseTimeByHour> chineseTimeByHourData = new Dictionary<int, ChineseTimeByHour>();
        private readonly ChineseTimeByHour nullChineseTimeByHour = new ChineseTimeByHour();

        public ChineseTimeDataManager()
        {
        }

        public ChineseTimeByHour GetChineseTimeDataFromHour(int hour)
        {
            if (hour < 0 || hour > 24)
            {
                return nullChineseTimeByHour;
            }

            if (chineseTimeByHourData.TryGetValue(hour, out var chineseTimeByHour))
            {
                return chineseTimeByHour;
            }

            return nullChineseTimeByHour;
        }

        public void AddChineseTimeByHour(int hour, ChineseTimeByHour data)
        {
            this.chineseTimeByHourData[hour] = data;
        }
    }
}
