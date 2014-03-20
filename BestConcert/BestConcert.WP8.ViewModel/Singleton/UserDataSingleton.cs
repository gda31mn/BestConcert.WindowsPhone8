using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestConcert.WP8.Model;

namespace BestConcert.WP8.ViewModel.Singleton
{
    sealed class UserDataSingleton
    {
        public UserModel User { get; set; }

        private static readonly UserDataSingleton _instance = new UserDataSingleton();

        public static UserDataSingleton Instance
        {
            get { return _instance; }
        }
    }
}
