using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Settings
{
    public class SettingsModel : INotifyPropertyChanged
    {
        private string _pathFolderLogs;
        private string _pathAbout;
        private string _nameUnitePdf; 
        private KeyValuePair<string, string> _selectedPathSavePdf;
        private KeyValuePair<string, string> _selectedPathFolderOpenFile;
        private KeyValuePair<string, bool> _selectedIsVisibleExcel;
        private KeyValuePair<string, bool> _selectedIsVisibleWord;

        public KeyValuePair<string, string> SelectedPathSavePdf
        {
            get { return _selectedPathSavePdf; }
            set 
            {
                _selectedPathSavePdf = value;
                OnPropertyChanged("SelectedPathSavePdf");
            }
        }
        public KeyValuePair<string, string> SelectedPathFolderOpenFile
        {
            get { return _selectedPathFolderOpenFile; }
            set 
            {
                _selectedPathFolderOpenFile = value;
                OnPropertyChanged("SelectedPathFolderOpenFile");
            }
        }
        public KeyValuePair<string, bool> SelectedIsVisibleExcel
        {
            get { return _selectedIsVisibleExcel; }
            set 
            {
                _selectedIsVisibleExcel = value;
                OnPropertyChanged("SelectedIsVisibleExcel");
            }
        }
        public KeyValuePair<string, bool> SelectedIsVisibleWord
        {
            get { return _selectedIsVisibleWord; }
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
            get { return _pathAbout; }
            set
            {
                _pathAbout = value;
                OnPropertyChanged("PathAbout");
            }
        }
        public string NameUnitePdf
        {
            get { return _nameUnitePdf; }
            set
            {
                _nameUnitePdf = value;
                OnPropertyChanged("NameUnitePdf");
            }
        }
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "PathFolderLogs":
                        if (string.IsNullOrWhiteSpace(PathFolderLogs))
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
                }
                return error;
            }
        }

        //public string Error => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
