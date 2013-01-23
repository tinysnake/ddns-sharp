using DDnsPod.Core.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsPod.Monitor.Models
{
    public class UpdateModelWrapper:ObservableObject
    {
        public UpdateModelWrapper(UpdateModel um=null)
        {
            if (um == null)
                model = new UpdateModel();
            else
                model = um;
        }

        private UpdateModel model;

        #region INPC
        #region DomainName
        /// <summary>
        /// The <see cref="DomainName" /> property's name.
        /// </summary>
        public const string DomainNamePropertyName = "Name";

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string DomainName
        {
            get
            {
                return model.DomainName;
            }

            set
            {
                if (model.DomainName == value)
                {
                    return;
                }

                RaisePropertyChanging(DomainNamePropertyName);
                model.DomainName = value;
                RaisePropertyChanged(DomainNamePropertyName);
            }
        }
        #endregion
        #region DomainID
        /// <summary>
        /// The <see cref="DomainID" /> property's name.
        /// </summary>
        public const string DomainIDPropertyName = "DomainID";

        /// <summary>
        /// Sets and gets the DomainID property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int DomainID
        {
            get
            {
                return model.DomainID;
            }

            set
            {
                if (model.DomainID == value)
                {
                    return;
                }

                RaisePropertyChanging(DomainIDPropertyName);
                model.DomainID = value;
                RaisePropertyChanged(DomainIDPropertyName);
            }
        }
        #endregion
        #region SubDomain
        /// <summary>
        /// The <see cref="SubDomain" /> property's name.
        /// </summary>
        public const string SubDomainPropertyName = "SubDomain";

        /// <summary>
        /// Sets and gets the SubDomain property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SubDomain
        {
            get
            {
                return model.SubDomain;
            }

            set
            {
                if (model.SubDomain == value)
                {
                    return;
                }

                RaisePropertyChanging(SubDomainPropertyName);
                model.SubDomain = value;
                RaisePropertyChanged(SubDomainPropertyName);
            }
        }
        #endregion
        #region RecordID
        /// <summary>
        /// The <see cref="RecordID" /> property's name.
        /// </summary>
        public const string RecordIDPropertyName = "RecordID";

        /// <summary>
        /// Sets and gets the RecordID property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int RecordID
        {
            get
            {
                return model.RecordID;
            }

            set
            {
                if (model.RecordID == value)
                {
                    return;
                }

                RaisePropertyChanging(RecordIDPropertyName);
                model.RecordID = value;
                RaisePropertyChanged(RecordIDPropertyName);
            }
        }
        #endregion
        #region LineName
        /// <summary>
        /// The <see cref="LineName" /> property's name.
        /// </summary>
        public const string LineNamePropertyName = "LineName";

        /// <summary>
        /// Sets and gets the LineName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LineName
        {
            get
            {
                return model.LineName;
            }

            set
            {
                if (model.LineName == value)
                {
                    return;
                }

                RaisePropertyChanging(LineNamePropertyName);
                model.LineName = value;
                RaisePropertyChanged(LineNamePropertyName);
            }
        }
        #endregion
        #region LastUpdateIP
        /// <summary>
        /// The <see cref="LastUpdateIP" /> property's name.
        /// </summary>
        public const string LastUpdateIPPropertyName = "LastUpdateIP";

        /// <summary>
        /// Sets and gets the LastUpdateIP property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LastUpdateIP
        {
            get
            {
                return model.LastUpdateIP;
            }

            set
            {
                if (model.LastUpdateIP == value)
                {
                    return;
                }


                RaisePropertyChanging(LastUpdateIPPropertyName);
                model.LastUpdateIP = value;
                RaisePropertyChanged(LastUpdateIPPropertyName);
            }
        }
        #endregion
        #region LastUpdatedTime
        /// <summary>
        /// The <see cref="LastUpdatedTime" /> property's name.
        /// </summary>
        public const string LastUpdatedTimePropertyName = "LastUpdatedTime";

        /// <summary>
        /// Sets and gets the LastUpdatedTime property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime LastUpdatedTime
        {
            get
            {
                return model.LastUpdatedTime;
            }

            set
            {
                if (model.LastUpdatedTime == value)
                {
                    return;
                }

                RaisePropertyChanging(LastUpdatedTimePropertyName);
                model.LastUpdatedTime = value;
                RaisePropertyChanged(LastUpdatedTimePropertyName);
            }
        }
        #endregion
        #region Enabled
        /// <summary>
        /// The <see cref="Enabled" /> property's name.
        /// </summary>
        public const string EnabledPropertyName = "Enabled";

        /// <summary>
        /// Sets and gets the Enabled property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool Enabled
        {
            get
            {
                return model.Enabled;
            }

            set
            {
                if (model.Enabled == value)
                {
                    return;
                }

                RaisePropertyChanging(EnabledPropertyName);
                model.Enabled = value;
                RaisePropertyChanged(EnabledPropertyName);
            }
        }
        #endregion
        #endregion

        public UpdateModel UnWrap()
        {
            return model;
        }
    }
}
