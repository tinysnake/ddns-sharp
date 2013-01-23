using DDnsPod.Core.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Monitor.Models
{
    public class MonitorRuntime:ObservableObject
    {
        #region UserInfo
        /// <summary>
        /// The <see cref="UserInfo" /> property's name.
        /// </summary>
        public const string UserInfoPropertyName = "UserInfo";

        private UserInfo _userInfo = null;

        /// <summary>
        /// Sets and gets the UserInfo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public UserInfo UserInfo
        {
            get
            {
                return _userInfo;
            }

            set
            {
                if (_userInfo == value)
                {
                    return;
                }

                RaisePropertyChanging(UserInfoPropertyName);
                _userInfo = value;
                RaisePropertyChanged(UserInfoPropertyName);
            }
        }
        #endregion
        #region UpdateList
        /// <summary>
        /// The <see cref="UpdateList" /> property's name.
        /// </summary>
        public const string UpdateListPropertyName = "UpdateList";

        private ObservableCollection<UpdateModelWrapper> _updateList = new ObservableCollection<UpdateModelWrapper>();

        /// <summary>
        /// Sets and gets the UpdateList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<UpdateModelWrapper> UpdateList
        {
            get
            {
                return _updateList;
            }

            set
            {
                if (_updateList == value)
                {
                    return;
                }

                RaisePropertyChanging(UpdateListPropertyName);
                _updateList = value;
                RaisePropertyChanged(UpdateListPropertyName);
            }
        }
        #endregion

        public void SetUpdateList(IEnumerable<UpdateModel> list)
        {
            UpdateList = new ObservableCollection<UpdateModelWrapper>(from i in list 
                                                                      select new UpdateModelWrapper(i));
        }
    }
}
