using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Settings
{
    public class SettingsModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _pathFolderLogs;
        private string _pathAbout;
        private string _nameUnitePdf;
        private string _pathFolderSaveConverting;
        private KeyValuePair<string, string> _selectedPathSavePdf;
        private KeyValuePair<string, string> _selectedPathFolderOpenFile;
        private KeyValuePair<string, bool> _selectedIsVisibleExcel;
        private KeyValuePair<string, bool> _selectedIsVisibleWord;

        public string PathFolderSaveConverting
        {
            get => _pathFolderSaveConverting;
            set
            {
                _pathFolderSaveConverting = value;
                OnPropertyChanged("PathFolderSaveConverting");
            }
        }

        public KeyValuePair<string, string> SelectedPathSavePdf
        {
            get => _selectedPathSavePdf;
            set
            {
                _selectedPathSavePdf = value;
                OnPropertyChanged("SelectedPathSavePdf");
            }
        }
        public KeyValuePair<string, string> SelectedPathFolderOpenFile
        {
            get => _selectedPathFolderOpenFile;
            set
            {
                _selectedPathFolderOpenFile = value;
                OnPropertyChanged("SelectedPathFolderOpenFile");
            }
        }
        public KeyValuePair<string, bool> SelectedIsVisibleExcel
        {
            get => _selectedIsVisibleExcel;
            set
            {
                _selectedIsVisibleExcel = value;
                OnPropertyChanged("SelectedIsVisibleExcel");
            }
        }
        public KeyValuePair<string, bool> SelectedIsVisibleWord
        {
            get => _selectedIsVisibleWord;
            set
            {
                _selectedIsVisibleWord = value;
                OnPropertyChanged("SelectedIsVisibleWord");
            }
        }
        public string PathFolderLogs
        {
            get => _pathFolderLogs;
            set
            {
                _pathFolderLogs = value;
                OnPropertyChanged("PathFolderLogs");
            }
        }
        public string PathAbout
        {
            get => _pathAbout;
            set
            {
                _pathAbout = value;
                OnPropertyChanged("PathAbout");
            }
        }
        public string NameUnitePdf
        {
            get => _nameUnitePdf;
            set
            {
                _nameUnitePdf = value;
                OnPropertyChanged("NameUnitePdf");
            }
        }
        #region IDataErrorInfo Members
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "PathFolderLogs":
                        if (string.IsNullOrEmpty(PathFolderLogs))
                        {
                            error = "Необходимо указать путь";
                        }
                        break;
                    case "PathAbout":
                        if (string.IsNullOrWhiteSpace(PathAbout))
                        {
                            error = "Необходимо указать путь";
                        }
                        break;
                    case "NameUnitePdf":
                        if (string.IsNullOrWhiteSpace(NameUnitePdf))
                        {
                            error = "Необходимо указать имя";
                        }
                        break;
                    case "PathFolderSaveConverting":
                        if (string.IsNullOrWhiteSpace(PathFolderSaveConverting))
                        {
                            error = "Необходимо указать путь";
                        }
                        break;
                }
                return error;
            }
        }

        public string Error => string.Empty;

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
