using DDnsPod.Core.Models;
using DDnsPod.Core.Services;
using DDnsPod.Monitor.Core;
using DDnsPod.Monitor.Design;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DDnsPod.Monitor.ViewModels
{
    public class RecordManageWindowViewModel : ViewModelBase
    {
        public RecordManageWindowViewModel()
        {
            if (this.IsInDesignMode)
            {
                record = new DesigntimeUpdateModel();
            }
            else
            {
                DomainsCache = MonitorIoc.Current.Get<DomainsCache>();
                TempStorage = MonitorIoc.Current.Get<TempStorage>();
                record = TempStorage.GetAndRemove<UpdateModel>(DDNSMonitorWindowViewModel.RECORD_FETCH_KEY);
                if (record == null)
                    record = new UpdateModel();
                if (DomainsCache.DomainInfos.Count() <= 0)
                    RefreshDataCommand.Execute(null);
                else
                    BindData();
            }
        }

        private DomainsCache DomainsCache { get; set; }
        private TempStorage TempStorage { get; set; }
        private UpdateModel record;

        #region INPC
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
                return record.SubDomain;
            }

            set
            {
                if (record.SubDomain == value)
                {
                    return;
                }

                RaisePropertyChanging(SubDomainPropertyName);
                record.SubDomain = value;
                RaisePropertyChanged(SubDomainPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="DomainName" /> property's name.
        /// </summary>
        public const string DomainNamePropertyName = "DomainName";

        /// <summary>
        /// Sets and gets the DomainName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string DomainName
        {
            get
            {
                return record.DomainName;
            }

            set
            {
                if (record.DomainName == value)
                {
                    return;
                }

                RaisePropertyChanging(DomainNamePropertyName);
                record.DomainName = value;
                RaisePropertyChanged(DomainNamePropertyName);
            }
        }

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
                return record.LineName;
            }

            set
            {
                if (record.LineName == value)
                {
                    return;
                }

                RaisePropertyChanging(LineNamePropertyName);
                record.LineName = value;
                RaisePropertyChanged(LineNamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="DomainList" /> property's name.
        /// </summary>
        public const string DomainListPropertyName = "DomainList";

        private ObservableCollection<string> _domainList = new ObservableCollection<string>();

        /// <summary>
        /// Sets and gets the DomainList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<string> DomainList
        {
            get
            {
                return _domainList;
            }

            set
            {
                if (_domainList == value)
                {
                    return;
                }

                RaisePropertyChanging(DomainListPropertyName);
                _domainList = value;
                RaisePropertyChanged(DomainListPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SubDomainList" /> property's name.
        /// </summary>
        public const string SubDomainListPropertyName = "SubDomainList";

        private ObservableCollection<string> _subDomainList = new ObservableCollection<string>();

        /// <summary>
        /// Sets and gets the SubDomainList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<string> SubDomainList
        {
            get
            {
                return _subDomainList;
            }

            set
            {
                if (_subDomainList == value)
                {
                    return;
                }

                RaisePropertyChanging(SubDomainListPropertyName);
                _subDomainList = value;
                RaisePropertyChanged(SubDomainListPropertyName);
            }
        }
        #endregion

        #region RefreshDataCommand
        private RelayCommand _refreshDataCommand;

        /// <summary>
        /// Gets the RefreshDataCommand.
        /// </summary>
        public RelayCommand RefreshDataCommand
        {
            get
            {
                return _refreshDataCommand
                    ?? (_refreshDataCommand = new RelayCommand(RefreshList));
            }
        }
        #endregion

        #region DomainSelectedCommand
        private RelayCommand<string> _domainSelectedCommand;

        /// <summary>
        /// Gets the DomainSelectedCommand.
        /// </summary>
        public RelayCommand<string> DomainSelectedCommand
        {
            get
            {
                return _domainSelectedCommand
                    ?? (_domainSelectedCommand = new RelayCommand<string>(OnDomainSelected));
            }
        }
        #endregion

        #region SubDomainSelectedCommand
        private RelayCommand<string> _subDomainSelectedCommand;

        /// <summary>
        /// Gets the SubDomainSelectedCommand.
        /// </summary>
        public RelayCommand<string> SubDomainSelectedCommand
        {
            get
            {
                return _subDomainSelectedCommand
                    ?? (_subDomainSelectedCommand = new RelayCommand<string>(OnSubDomainSelected));
            }
        }
        #endregion

        private async void RefreshList()
        {
            var domainList = await DomainService.GetList();
            if (domainList.Status.Code == 1)
            {
                DomainsCache.DomainInfos = (from d in domainList.Domains
                                            where d.Status == "enable"
                                            select d).ToList();
                BindData();
            }
            else
            {
                MessageBox.Show(domainList.Status.Message);
            }
        }

        private async void OnDomainSelected(string domainName)
        {
            DomainName = domainName;
            var recordInfo = DomainsCache.DomainInfos.FirstOrDefault(d => d.Name == domainName);
            if (recordInfo == null)
                return;
            if (DomainsCache.DomainRecordSet.ContainsKey(recordInfo))
            {
                SubDomainList = new ObservableCollection<string>(
                    from r in DomainsCache.DomainRecordSet[recordInfo]
                    select r.Name);
            }
            else
            {
                var recordList = await RecordService.GetList(recordInfo.ID);
                if (recordList.Status.Code == 1)
                {
                    DomainsCache.DomainRecordSet[recordInfo] = recordList.Records.
                        Where(i=>i.Type=="A").Distinct().ToList();
                    SubDomainList = new ObservableCollection<string>(
                        from r in DomainsCache.DomainRecordSet[recordInfo]
                        select r.Name);
                }
            }
        }

        private void OnSubDomainSelected(string subDomain)
        {
            SubDomain = subDomain;
        }

        private void BindData()
        {
            DomainList = new ObservableCollection<string>(
                from d in DomainsCache.DomainInfos
                select d.Name);
        }
    }
}
