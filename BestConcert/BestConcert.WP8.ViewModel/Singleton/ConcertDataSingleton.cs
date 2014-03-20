using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestConcert.WP8.Model;

namespace BestConcert.WP8.ViewModel.Singleton
{
    sealed class ConcertDataSingleton
    {
        public ConcertModel Concert { get; set; }

        private static readonly ConcertDataSingleton _instance = new ConcertDataSingleton();

        public static ConcertDataSingleton Instance
        {
            get { return _instance; }
        }
    }
}
